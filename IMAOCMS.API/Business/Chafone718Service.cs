using IMAOCMS.API.Business.Interfaces;
using IMAOCMS.Core.CHAFON;
using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.DTOs;
using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Request;
using IMAOCMS.Core.Services;
using Mapster;
using System;
using System.Runtime.InteropServices;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IMAOCMS.API.Business
{
    public class Chafone718Service : IChafone718Service
    {
        private readonly ILogger<Chafone718Service> _logger;
        private readonly IEpcReadDataService _epcReadDataService;
        private readonly IEPCReadTempService _epcReadTempService;
        RFIDCallBack elegateRFIDCallBack;
        public Chafone718Service(ILogger<Chafone718Service> logger, IEpcReadDataService epcReadDataService, IEPCReadTempService epcReadTempService)
        {
            _logger = logger;
            _epcReadDataService = epcReadDataService;
            _epcReadTempService = epcReadTempService;

            elegateRFIDCallBack = new RFIDCallBack(GetUid);
        }
        private static object LockFlag = new object();

        public static List<RFIDTag> curList = new List<RFIDTag>();
        public void GetUid(IntPtr p, Int32 nEvt)
        {

            RFIDTag ce = (RFIDTag)Marshal.PtrToStructure(p, typeof(RFIDTag));
            lock (LockFlag)
            {
                curList.Add(ce);
                total_tagnum++;
                CardNum++;
            }
        }
        BaseRequest baseRequest = new BaseRequest();
        private static byte fComAdr = 0xff;
        private static byte fBaud;
        private int fCmdRet = 30; //Yürütülen tüm komutların dönüş değeri
        private static int frmcomportindex;
        private static byte Qvalue = 0;
        private static byte Session = 0;
        private static byte TIDFlag = 0;
        private static byte Target = 0;
        private static byte InAnt = 0;
        private static byte Scantime = 0;
        private static byte FastFlag = 0;
        private static int total_turns = 0;//轮数
        private static int AA_times = 0;
        private static int CommunicationTime = 0;
        private static int total_tagnum = 0;//标签数量
        private static int total_time = 0;//总时间
        private Thread? mythread = null;
        public static List<EpcReadData> EpcData = new();
        public static List<EpcReadData> EpcData2 = new();
        public static EPCReadTemp EpcTemp;
        private volatile bool fIsInventoryScan = false;
        private static bool toStopThread = false;
        private byte[] fPassWord = new byte[4];
        public async Task<ApiDataResponse<BaseRequest>> ConnectionDeviceAsync()
        {

            int portNum = 3;
            int FrmPortIndex = 0;
            string strException = string.Empty;
            fBaud = 6;
            //if (fBaud > 2)
            //    fBaud = Convert.ToByte(fBaud + 2);
            fComAdr = 255;//广播地址打开设备
            await Task.Run(() => fCmdRet = RWDev.OpenComPort(portNum, ref fComAdr, fBaud, ref FrmPortIndex));
            if (fCmdRet != 0)
            {
                string strLog = "Connect failed: " + GetReturnCodeDesc(fCmdRet);
                _logger.LogError(strLog);
                return await Task.FromResult(new ApiDataResponse<BaseRequest>() { Data = baseRequest, Message = strLog, Status = false });
                //return;
            }
            else
            {
                frmcomportindex = FrmPortIndex;
                string strLog = "Connected " + portNum.ToString() + "@" + fBaud.ToString();
                _logger.LogInformation(strLog);
                if (FrmPortIndex > 0)
                    RWDev.InitRFIDCallBack(elegateRFIDCallBack, true, FrmPortIndex);
                return await Task.FromResult(new ApiDataResponse<BaseRequest>() { Data = baseRequest, Message = strLog, Status = true });
            }
        }
        public async Task<ApiResponse> DisconnectDeviceAsync()
        {
            //_frmcomportindex = 1;
            try
            {
                await Task.Run(() => fCmdRet = RWDev.CloseSpecComPort(frmcomportindex));
                return await Task.FromResult(new ApiResponse() { Message = "Ok", Status = true });
            }
            catch (Exception e)
            {
                return await Task.FromResult(new ApiResponse() { Message = e.Message, Status = true });
                throw;
            }


        }

        public async Task<ApiResponse> StartReadAsync()
        {
            await ConnectionDeviceAsync();
            //toStopThread = false;
            //if (fIsInventoryScan == false)
            //{
            //    mythread = new Thread(new ThreadStart(inventory));
            //    mythread.IsBackground = true;
            //    mythread.Start();
            //}
            try
            {
                inventory();
                //await Task.Run(() => fCmdRet = RWDev.CloseSpecComPort(frmcomportindex));
                return await Task.FromResult(new ApiResponse() { Message = "Ok", Status = true });
            }
            catch (Exception ex)
            {
                //_logger.LogError($"VF474Service-DisconnectDeviceAsync:{ex.ToString()}|{ex.StackTrace.ToString()}");
                return await Task.FromResult(new ApiResponse() { Message = "Fault" + " Hata: " + ex.ToString(), Status = false });
            }
        }
        private void AddRangeToDatabaseAsync()
        {
            var value = EpcData2.GroupBy(x => x.Epc).Select(group =>
                   new
                   {
                       Id = 0,
                       Count = group.Count(),
                       Rssi = group.FirstOrDefault().Rssi,
                       Epc = group.Key,
                       Ant = 1,
                       EpcDate = DateTime.Now,
                       CreatedDate = DateTime.Now,
                   })
             .OrderBy(group => group.Epc.First()).OrderByDescending(x => x.Epc).ToList();

            foreach (var item in value)
            {
                EpcData.Add(new EpcReadData
                {
                    Id = item.Id,
                    Count = item.Count,
                    Rssi = item.Rssi,
                    Epc = item.Epc,
                    Ant = Convert.ToByte(item.Ant),
                    EpcDate = DateTime.Now,
                    CreatedDate = item.CreatedDate
                });
            }

            //var result = EpcData2.Adapt(sa);

            //sa.Adapt(EpcData2);

            var result = _epcReadDataService.AddRangeAsync(EpcData);
        }
        public async Task<ApiResponse> StopReadAsync()
        {
            toStopThread = true;
            // var sadas = curList2;
            var sdas = curList;

            curList.Clear();
            try
            {

                AddRangeToDatabaseAsync();
                EpcData.Clear();
                fIsInventoryScan = true;
                await DisconnectDeviceAsync();
                //await Task.Run(() => fCmdRet = RWDev.CloseSpecComPort(frmcomportindex));
                return await Task.FromResult(new ApiResponse() { Message = "Ok", Status = true });
            }
            catch (Exception ex)
            {
                //_logger.LogError($"VF474Service-DisconnectDeviceAsync:{ex.ToString()}|{ex.StackTrace.ToString()}");
                return await Task.FromResult(new ApiResponse() { Message = "Fault" + " Hata: " + ex.ToString(), Status = false });
            }
        }
        private async void inventory()
        {
            //toStopThread = false;
            //while (!toStopThread)
            //{

            byte Ant = 0;
            int CardNum = 0;
            int Totallen = 0;
            int EPClen, m;
            byte[] EPC = new byte[50000];
            int CardIndex;
            string temps, temp;
            temp = "";
            string sEPC;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            byte MaskFlag = 0;
            byte AdrTID = 0;
            byte LenTID = 0;
            AdrTID = 0;
            LenTID = 6;
            MaskFlag = 0;
            int cbtime = System.Environment.TickCount;
            //DataGridViewRow rows = new DataGridViewRow();
            CardNum = 0;
            //ref fComAdr=0, Qvalue=4, Session=0, MaskMem=0, MaskAdr=0, MaskLen=0, MaskData=0, MaskFlag=0, AdrTID=0, LenTID=6, TIDFlag=0, Target=0, InAnt=128, Scantime=10, FastFlag=1, EPC, ref Ant=1, ref Totallen=266, ref CardNum=19, frmcomportindex=3
            fCmdRet = RWDev.Inventory_G2(ref fComAdr, Qvalue, Session, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, AdrTID, LenTID, TIDFlag, Target, InAnt, Scantime, FastFlag, EPC, ref Ant, ref Totallen, ref CardNum, frmcomportindex);
            total_turns = total_turns + 1;
            int x_time = System.Environment.TickCount - cbtime;//命令时间
            string strLog = "Inventory: " + GetReturnCodeDesc(fCmdRet);
            _logger.LogTrace(strLog);

            if ((fCmdRet == 1) | (fCmdRet == 2) | (fCmdRet == 3) | (fCmdRet == 4))//代表已查找结束，
            {
                byte[] daw = new byte[Totallen];
                Array.Copy(EPC, daw, Totallen);
                temps = ByteArrayToHexString(daw);
                m = 0;
                if (CardNum == 0)
                {
                    if (Session > 1)
                        AA_times = AA_times + 1;
                    return;
                }
                AA_times = 0;
                //antstr = Convert.ToString(Ant, 2).PadLeft(4, '0');
                for (CardIndex = 0; CardIndex < CardNum; CardIndex++)
                {
                    EPClen = daw[m] + 1;
                    temp = temps.Substring(m * 2 + 2, EPClen * 2);
                    sEPC = temp.Substring(0, temp.Length - 2);
                    string RSSI = Convert.ToInt32(temp.Substring(temp.Length - 2, 2), 16).ToString();
                    m = m + EPClen + 1;
                    if (sEPC.Length != (EPClen - 1) * 2)
                    {
                        return;
                    }
                    bool isonlistview = false;

                    EpcData2.Add(new EpcReadData() { Id = 0, Epc = sEPC, Ant = Ant, EpcDate = DateTime.Now, Rssi = Convert.ToInt32(RSSI) });
                    //EpcTemp = new();
                    //EpcTemp.Id = 0;
                    //EpcTemp.Epc = sEPC;
                    //EpcTemp.Ant = Ant;
                    //EpcTemp.EpcDate = DateTime.Now;
                    //EpcTemp.Rssi = Convert.ToInt32(RSSI);
                    //_epcReadTempService.Add(EpcTemp);



                }
            }
            //if (x_time > CommunicationTime)
            //    x_time = x_time - CommunicationTime;//减去通讯时间等于标签的实际时间
            //int sulv = (CardNum * 1000) / x_time;//速度等于张数/时间
            //total_tagnum = total_tagnum + CardNum;
            //}
        }




        public async Task<ApiResponse> StartRead2Async()
        {
            Array.Clear(antlist, 0, 16);
           
                antlist[0] = 1;
                InAnt = 0x80;
         
                antlist[1] = 1;
                InAnt = 0x81;
       
                antlist[2] = 1;
                InAnt = 0x82;
      
                antlist[3] = 1;
                InAnt = 0x83;
      
                antlist[4] = 1;
                InAnt = 0x84;
       
                antlist[5] = 1;
                InAnt = 0x85;
           
                antlist[6] = 1;
                InAnt = 0x86;
        
                antlist[7] = 1;
                InAnt = 0x87;
        
                antlist[8] = 1;
                InAnt = 0x88;
      
                antlist[9] = 1;
                InAnt = 0x89;
           
                antlist[10] = 1;
                InAnt = 0x8A;
          
                antlist[11] = 1;
                InAnt = 0x8B;
          
                antlist[12] = 1;
                InAnt = 0x8C;
       
                antlist[13] = 1;
                InAnt = 0x8D;
            
                antlist[14] = 1;
                InAnt = 0x8E;
            

            
                antlist[15] = 1;
                InAnt = 0x8F;
           


            //await ConnectionDeviceAsync();
            //toStopThread = false;
            //if (fIsInventoryScan == false)
            //{
            //    mythread = new Thread(new ThreadStart(inventory));
            //    mythread.IsBackground = true;
            //    mythread.Start();
            //}
            try
            {
                NewInventory();
                //await Task.Run(() => fCmdRet = RWDev.CloseSpecComPort(frmcomportindex));
                return await Task.FromResult(new ApiResponse() { Message = "Ok", Status = true });
            }
            catch (Exception ex)
            {
                //_logger.LogError($"VF474Service-DisconnectDeviceAsync:{ex.ToString()}|{ex.StackTrace.ToString()}");
                return await Task.FromResult(new ApiResponse() { Message = "Fault" + " Hata: " + ex.ToString(), Status = false });
            }
        }
        private int CardNum = 0;
        byte[] ReadAdr = new byte[2];
        byte[] Psd = new byte[4];
        byte ReadLen = 0;
        byte ReadMem = 0;
        byte[] antlist = new byte[16];
        int AntennaNum = 16;
        private void NewInventory()
        {
            for (int m = 0; m < AntennaNum; m++)
            {
                InAnt = (byte)(m | 0x80);
                FastFlag = 1;
                if (antlist[m] == 1)
                {

                    flashmix_G2();

                }
            }
        }


        private void flashmix_G2()
        {

            ReadMem = (byte)1;
            ReadAdr = HexStringToByteArray("0002");
            ReadLen = Convert.ToByte("06", 16);
            Psd = HexStringToByteArray("00000001");






            byte Ant = 0;
            int TagNum = 0;
            int Totallen = 0;
            byte[] EPC = new byte[50000];
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            byte MaskFlag = 0;
            MaskFlag = 0;
            int cbtime = System.Environment.TickCount;
            CardNum = 0;
            fCmdRet = RWDev.InventoryMix_G2(ref fComAdr, Qvalue, Session, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, ReadMem, ReadAdr, ReadLen, Psd, Target, InAnt, Scantime, FastFlag, EPC, ref Ant, ref Totallen, ref TagNum, frmcomportindex);
            int cmdTime = System.Environment.TickCount - cbtime;//命令时间

            if (CardNum == 0)
            {
                if (Session > 1)
                    AA_times = AA_times + 1;//没有得到标签只更新状态栏
            }
            else
                AA_times = 0;
            if ((fCmdRet == 1) || (fCmdRet == 2) || (fCmdRet == 0xFB) || (fCmdRet == 0x26))
            {

                var dsa = ByteToHex(EPC);

                var sada = HexToByte(dsa);







                //if (cmdTime > CommunicationTime)
                //    cmdTime = cmdTime - CommunicationTime;//减去通讯时间等于标签的实际时间
                //if (cmdTime > 0)
                //{
                //    //int tagrate = (CardNum * 1000) / cmdTime;//速度等于张数/时间
                //    //IntPtr ptrWnd = IntPtr.Zero;
                //    //ptrWnd = FindWindow(null, "UHFReader288 Demo V5.0"); //0x00060f1a
                //    //if (ptrWnd != IntPtr.Zero)         // 检查当前统计窗口是否打开
                //    //{
                //    //    string para = tagrate.ToString() + "," + total_tagnum.ToString() + "," + cmdTime.ToString();
                //    //    SendMessage(ptrWnd, WM_SENDTAGSTAT, IntPtr.Zero, para);
                //    //}
                //}

            }
            //IntPtr ptrWnd1 = IntPtr.Zero;
            //ptrWnd1 = FindWindow(null, "UHFReader288 Demo V5.0");
            //if (ptrWnd1 != IntPtr.Zero)         // 检查当前统计窗口是否打开
            //{
            //    string para = fCmdRet.ToString();
            //    SendMessage(ptrWnd1, WM_SENDSTATU, IntPtr.Zero, para);
            //}
            //ptrWnd1 = IntPtr.Zero;
        }

        public string[] EpcStr = new string[7];
        private void EpcReader(byte[] Epcrecebuffer)
        {
            string str;
            int i;
            str = "";
            //okuma sayısı
            for (i = 0; i < 5; i++)
            {
                str += (char)Epcrecebuffer[i];
            }
            EpcStr[3] = str;
            //Anten numarası
            str = "";
            for (i = 0; i < 2; i++)
            {
                str += (char)Epcrecebuffer[0x07 + i];
            }
            EpcStr[4] = str;
            //etiket türü
            str = "";
            for (i = 0; i < 2; i++)
            {
                str += (char)Epcrecebuffer[0x0b + i];
            }
            EpcStr[5] = str;
            //EPC
            str = "";
            for (i = 0; i < 74; i++)
            {
                str += (char)Epcrecebuffer[0x0f + i];
            }
            EpcStr[6] = str.Trim();
        }









        #region 
        private string GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "successfully";
                case 0x01:
                    return "Return before Inventory finished";
                case 0x02:
                    return "the Inventory-scan-time overflow";
                case 0x03:
                    return "More Data";
                case 0x04:
                    return "Reader module MCU is Full";
                case 0x05:
                    return "Access Password Error";
                case 0x09:
                    return "Destroy Password Error";
                case 0x0a:
                    return "Destroy Password Error Cannot be Zero";
                case 0x0b:
                    return "Tag Not Support the command";
                case 0x0c:
                    return "Use the commmand,Access Password Cannot be Zero";
                case 0x0d:
                    return "Tag is protected,cannot set it again";
                case 0x0e:
                    return "Tag is unprotected,no need to reset it";
                case 0x10:
                    return "There is some locked bytes,write fail";
                case 0x11:
                    return "can not lock it";
                case 0x12:
                    return "is locked,cannot lock it again";
                case 0x13:
                    return "Parameter Save Fail,Can Use Before Power";
                case 0x14:
                    return "Cannot adjust";
                case 0x15:
                    return "Return before Inventory finished";
                case 0x16:
                    return "Inventory-Scan-Time overflow";
                case 0x17:
                    return "More Data";
                case 0x18:
                    return "Reader module MCU is full";
                case 0x19:
                    return "Not Support Command Or AccessPassword Cannot be Zero";
                case 0x1A:
                    return "Perform error tag custom function";
                case 0xF8:
                    return "Antenna connection detect errors";
                case 0xF9:
                    return "Command execution error";
                case 0xFA:
                    return "Get Tag,Poor Communication,Inoperable";
                case 0xFB:
                    return "No Tag Operable";
                case 0xFC:
                    return "Tag Return ErrorCode";
                case 0xFD:
                    return "Command length wrong";
                case 0xFE:
                    return "Illegal command";
                case 0xFF:
                    return "Parameter Error";
                case 0x30:
                    return "Communication error";
                case 0x31:
                    return "CRC checksummat error";
                case 0x32:
                    return "Return data length error";
                case 0x33:
                    return "Communication busy";
                case 0x34:
                    return "Busy,command is being executed";
                case 0x35:
                    return "ComPort Opened";
                case 0x36:
                    return "ComPort Closed";
                case 0x37:
                    return "Invalid Handle";
                case 0x38:
                    return "Invalid Port";
                case 0xEE:
                    return "Return Command Error";
                default:
                    return "";
            }
        }
        private string GetErrorCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "Other error";
                case 0x03:
                    return "Memory out or pc not support";
                case 0x04:
                    return "Memory Locked and unwritable";
                case 0x0b:
                    return "No Power,memory write operation cannot be executed";
                case 0x0f:
                    return "Not Special Error,tag not support special errorcode";
                default:
                    return "";
            }
        }
        #endregion
        #region 
        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

        }

        public static byte[] HexToByte(string hex)
        {
            byte[] byteArray = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
                byteArray[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return byteArray;
        }
        public static string ByteToTexts(byte[] source)
        {
            return Encoding.ASCII.GetString(source);
        }
        public static string ByteToHex(byte[] data)
        {
            StringBuilder builder = new StringBuilder(data.Length * 2);
            foreach (byte bytee in data)
                builder.AppendFormat("{0:X2}", bytee);
            return builder.ToString();
        }
        #endregion
    }
}
