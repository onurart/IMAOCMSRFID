using AutoMapper;
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
        private readonly IRFIDDeviceService _rfidDeviceService;
        private readonly IRFIDDeviceAntennaService _rfidDeviceAntennaService;
        private readonly IMapper _mapper;
        RFIDCallBack elegateRFIDCallBack;
        public Chafone718Service(ILogger<Chafone718Service> logger, IEpcReadDataService epcReadDataService, IEPCReadTempService epcReadTempService, IRFIDDeviceService rfidDeviceService, IRFIDDeviceAntennaService rfidDeviceAntennaService)
        {
            _logger = logger;
            _epcReadDataService = epcReadDataService;
            _epcReadTempService = epcReadTempService;

            elegateRFIDCallBack = new RFIDCallBack(GetUid);
            _rfidDeviceService = rfidDeviceService;
            _rfidDeviceAntennaService = rfidDeviceAntennaService;
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
        private static bool fIsInventoryScan = false;
        private static bool toStopThread = false;
        private byte[] fPassWord = new byte[4];
        private static List<RFIDDeviceAntenna> fAntennaList;
        public async Task<ApiDataResponse<RFIDDevice>> AddDeviceConnectionSettingsDb(RFIDDeviceDto rFIDDeviceDto)
        {
            //var value =  _mapper.Map<RFIDDevice>(rFIDDeviceDto);
            var result = await _rfidDeviceService.AddAsync(rFIDDeviceDto.Adapt<RFIDDevice>());
            if (result.Success)
                return await Task.FromResult(new ApiDataResponse<RFIDDevice>() { Data = result.Data, Message = result.Message, Status = true });
            else
                return await Task.FromResult(new ApiDataResponse<RFIDDevice>() { Data = result.Data, Message = result.Message, Status = false });
        }
        public async Task<ApiDataResponse<RFIDDeviceAntenna>> AddDeviceAntennaDb(RFIDDeviceAntennaDto rFIDDeviceAntennaDto)
        {
            //var value =  _mapper.Map<RFIDDevice>(rFIDDeviceDto);
            var result = await _rfidDeviceAntennaService.AddAsync(rFIDDeviceAntennaDto.Adapt<RFIDDeviceAntenna>());
            if (result.Success)
                return await Task.FromResult(new ApiDataResponse<RFIDDeviceAntenna>() { Data = result.Data, Message = result.Message, Status = true });
            else
                return await Task.FromResult(new ApiDataResponse<RFIDDeviceAntenna>() { Data = result.Data, Message = result.Message, Status = false });
        }
        public async Task<ApiResponse> ConnectionDeviceAsync(RFIDDeviceDto fIDDeviceDto)
        {

            //int portNum = 3;
            int FrmPortIndex = 0;
            string strException = string.Empty;
            fBaud = 6;
            //if (fBaud > 2)
            //    fBaud = Convert.ToByte(fBaud + 2);
            fComAdr = 255;//广播地址打开设备
            var result = fCmdRet = RWDev.OpenComPort(fIDDeviceDto.Portnum, ref fComAdr, Convert.ToByte(fIDDeviceDto.Baud), ref FrmPortIndex);
            if (fCmdRet != 0)
            {
                string strLog = "Connect failed: " + GetReturnCodeDesc(fCmdRet);
                _logger.LogError(strLog);
                return await Task.FromResult(new ApiResponse() { Message = GetReturnCodeDesc(fCmdRet), Status = false });
                //return;
            }
            else
            {
                frmcomportindex = FrmPortIndex;
                // string strLog = "Connected " + "@" + fBaud.ToString();
                // _logger.LogInformation(strLog);
                if (FrmPortIndex > 0)
                    RWDev.InitRFIDCallBack(elegateRFIDCallBack, true, FrmPortIndex);
                return await Task.FromResult(new ApiResponse() { Message = GetReturnCodeDesc(fCmdRet), Status = true });
            }
        }
        public async Task<ApiResponse> DisconnectDeviceAsync()
        {
            try
            {
                var result = fCmdRet = RWDev.CloseSpecComPort(frmcomportindex);
                if (fCmdRet != 0)
                {
                    return await Task.FromResult(new ApiResponse() { Message = GetReturnCodeDesc(fCmdRet), Status = false });
                }
                else
                {
                    return await Task.FromResult(new ApiResponse() { Message = GetReturnCodeDesc(fCmdRet), Status = true });
                }
            }
            catch (Exception e)
            {
                return await Task.FromResult(new ApiResponse() { Message = e.Message, Status = true });
            }


        }

        public async Task<ApiResponse> StartReadAsync()
        {


            try
            {
                fIsInventoryScan = true;
                NewInventory();
                //inventory();
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
            if (curList.Count != 0)
            {


                var value = curList.GroupBy(x => x.UID).Select(group =>
                       new
                       {
                           Id = 0,
                           Count = group.Count(),
                           Rssi = group.FirstOrDefault().RSSI,
                           Epc = group.Key,
                           Ant = group.FirstOrDefault().ANT == 4 ? 3 : group.FirstOrDefault().ANT == 8 ? 4 : group.FirstOrDefault().ANT == 16 ? 5 : group.FirstOrDefault().ANT == 32 ? 6 : group.FirstOrDefault().ANT == 64 ? 7 : group.FirstOrDefault().ANT == 128 ? 8 : 1,
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

                if (EpcData.Count != 0)
                {
                    foreach (var item in EpcData)
                    {
                        EpcReadData epcReadData = new EpcReadData
                        {
                            Id = item.Id,
                            Count = item.Count,
                            Rssi = item.Rssi,
                            Epc = item.Epc,
                            Ant = Convert.ToByte(item.Ant),
                            EpcDate = DateTime.Now,
                            CreatedDate = item.CreatedDate
                        };
                        _epcReadDataService.Add(epcReadData);
                    }
                    //var result = await _epcReadDataService.AddRangeAsync(EpcData);
                    //var resultes =  fCmdRet = RWDev.CloseSpecComPort(frmcomportindex);
                    EpcData.Clear();
                    curList.Clear();
                    fIsInventoryScan = false;
                }
            }
            //var result = EpcData2.Adapt(sa);

            //sa.Adapt(EpcData2);



        }
        public Task<ApiResponse> StopReadAsync()
        {
            //toStopThread = true;
            // var sadas = curList2;
            //var sdas = curList;


            try
            {

                // var result =  await Task.Run(() => fCmdRet = RWDev.CloseSpecComPort(frmcomportindex));
                //await DisconnectDeviceAsync();

                AddRangeToDatabaseAsync();
                EpcData.Clear();
                curList.Clear();



                //fIsInventoryScan = true;
                //DisConnect();


                fCmdRet = RWDev.CloseSpecComPort(frmcomportindex);
                return Task.FromResult(new ApiResponse() { Message = "Ok", Status = true });
            }
            catch (Exception ex)
            {
                //_logger.LogError($"VF474Service-DisconnectDeviceAsync:{ex.ToString()}|{ex.StackTrace.ToString()}");
                return Task.FromResult(new ApiResponse() { Message = "Fault" + " Hata: " + ex.ToString(), Status = false });
            }
        }

        public async Task<ApiResponse> StartEpcReader()
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
                return await Task.FromResult(new ApiResponse() { Message = strLog, Status = false });
                //return;
            }
            else
            {
                frmcomportindex = FrmPortIndex;
                string strLog = "Connected " + portNum.ToString() + "@" + fBaud.ToString();
                _logger.LogInformation(strLog);
                if (FrmPortIndex > 0)
                {
                    RWDev.InitRFIDCallBack(elegateRFIDCallBack, true, FrmPortIndex);
                    NewInventory();


                }

                return await Task.FromResult(new ApiResponse() { Message = strLog, Status = true });
            }
        }
        void ConnectionAsync()
        {
            int portNum = 3;
            int FrmPortIndex = 0;
            string strException = string.Empty;
            fBaud = 6;
            //if (fBaud > 2)
            //    fBaud = Convert.ToByte(fBaud + 2);
            fComAdr = 255;//广播地址打开设备

            var result =  fCmdRet = RWDev.OpenComPort(portNum, ref fComAdr, fBaud, ref FrmPortIndex);
            if (result != 0)

            //fCmdRet = RWDev.OpenComPort(portNum, ref fComAdr, fBaud, ref FrmPortIndex);
            //if (fCmdRet != 0)
            {
                string strLog = "Connect failed: " + GetReturnCodeDesc(fCmdRet);
                _logger.LogError(strLog);
            }
            else
            {
                frmcomportindex = FrmPortIndex;
                string strLog = "Connected " + portNum.ToString() + "@" + fBaud.ToString();
                _logger.LogInformation(strLog);
                if (FrmPortIndex > 0)
                    RWDev.InitRFIDCallBack(elegateRFIDCallBack, true, FrmPortIndex);
            }
        }
        async Task DisConnectAsync()
        {
            _logger.LogError("Disconnect : " + fCmdRet.ToString());
            if (fCmdRet != 48)
                await Task.Run(() => fCmdRet = RWDev.CloseSpecComPort(frmcomportindex));
            //fCmdRet = RWDev.CloseSpecComPort(frmcomportindex);

        }
        public async Task ConStartAndStopAndClose()
        {
            //Connection();
            var result = await _rfidDeviceAntennaService.GetListAsync();
            if (result.Success)
            {
                var list = result.Data.ToList();
                fAntennaList = list.ToList();


                ConnectionAsync();

                fIsInventoryScan = true;
                //}

                flashmix_G2();
                //if (fCmdRet == 30 || fCmdRet == 48 || fCmdRet == 55)
                //{
                //    ConnectionAsync();

                //    fIsInventoryScan = true;
                //    //}

                //    flashmix_G2();

                //    //await DisConnectAsync();
                   
                //}
                //else
                //{
                   
                //}


                // return Task.FromResult(true);
            }
        }

        public async Task<ApiResponse> StartRead2Async()
        {
            //Array.Clear(antlist, 0, 16);

            //antlist[0] = 1;
            //InAnt = 0x80;

            //antlist[1] = 1;
            //InAnt = 0x81;

            //antlist[2] = 1;
            //InAnt = 0x82;

            //antlist[3] = 1;
            //InAnt = 0x83;

            //antlist[4] = 1;
            //InAnt = 0x84;

            //antlist[5] = 1;
            //InAnt = 0x85;

            //antlist[6] = 1;
            //InAnt = 0x86;

            //antlist[7] = 1;
            //InAnt = 0x87;

            //antlist[8] = 1;
            //InAnt = 0x88;

            //antlist[9] = 1;
            //InAnt = 0x89;

            //antlist[10] = 1;
            //InAnt = 0x8A;

            //antlist[11] = 1;
            //InAnt = 0x8B;

            //antlist[12] = 1;
            //InAnt = 0x8C;

            //antlist[13] = 1;
            //InAnt = 0x8D;

            //antlist[14] = 1;
            //InAnt = 0x8E;



            //antlist[15] = 1;
            //InAnt = 0x8F;



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
                //NewInventory();
                //DisConnect ();
                 await ConStartAndStopAndClose();


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
        private async void NewInventory()
        {
            //Array.Clear(antlist, 0, 16);

            //antlist[0] = 1;
            //InAnt = 0x80;

            //antlist[1] = 1;
            //InAnt = 0x81;

            //antlist[2] = 1;
            //InAnt = 0x82;

            //antlist[3] = 1;
            //InAnt = 0x83;

            //antlist[4] = 1;
            //InAnt = 0x84;

            //antlist[5] = 1;
            //InAnt = 0x85;

            //antlist[6] = 1;
            //InAnt = 0x86;

            //antlist[7] = 1;
            //InAnt = 0x87;

            //antlist[8] = 1;
            //InAnt = 0x88;

            //antlist[9] = 1;
            //InAnt = 0x89;

            //antlist[10] = 1;
            //InAnt = 0x8A;

            //antlist[11] = 1;
            //InAnt = 0x8B;

            //antlist[12] = 1;
            //InAnt = 0x8C;

            //antlist[13] = 1;
            //InAnt = 0x8D;

            //antlist[14] = 1;
            //InAnt = 0x8E;



            //antlist[15] = 1;
            //InAnt = 0x8F;

            //for (int m = 0; m < fAntennaList.Count(); m++)
            //{
            //    InAnt = (byte)(m | 0x80);
            //    FastFlag = 1;
            //    if (fAntennaList[m].IsActive == true)
            //    {
            //        //_logger.LogInformation("Anten Döngü" + InAnt.ToString());
            //        flashmix_G2();

            //    }
            //}

            //for (int m = 0; m < AntennaNum; m++)
            //{
            //    InAnt = (byte)(m | 0x80);
            //    FastFlag = 1;
            //    if (antlist[m] == 1)
            //    {
            //        flashmix_G2();
            //    }
            //}
            //if (fIsInventoryScan == true)
            //{

            //}
            flashmix_G2();
        }


        private async void flashmix_G2()
        {
            

            ReadMem = (byte)1;
            ReadAdr = HexStringToByteArray("0002");
            ReadLen = Convert.ToByte("06", 16);
            Psd = HexStringToByteArray("00000000");

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
            // int cbtime = System.Environment.TickCount;
            CardNum = 0;

            //var result = await Task.Run(() => fCmdRet = RWDev.InventoryMix_G2(ref fComAdr, Qvalue, Session, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, ReadMem, ReadAdr, ReadLen, Psd, Target, InAnt, Scantime, FastFlag, EPC, ref Ant, ref Totallen, ref TagNum, frmcomportindex));
            //if (fCmdRet==0)
            //{
            //    await Task.Delay(50);
            //}

            for (int m = 0; m < fAntennaList.Count(); m++)
            {
                FastFlag = 1;
                try
                {
                    if (fAntennaList[m].IsActive == true)
                    {
                        InAnt = 0x80;
                        Ant = Convert.ToByte(fAntennaList[m].Antenna);
                        switch (Ant)
                        {
                            case 1:
                                InAnt = 0x80;
                                break;
                            case 2:
                                InAnt = 0x81;
                                break;
                            case 3:
                                InAnt = 0x82;
                                break;
                            case 4:
                                InAnt = 0x83;
                                break;
                            case 5:
                                InAnt = 0x84;
                                break;
                            case 6:
                                InAnt = 0x85;
                                break;
                            case 7:
                                InAnt = 0x86;
                                break;
                            case 8:
                                InAnt = 0x87;
                                break;
                            case 9:
                                InAnt = 0x88;
                                break;
                            case 10:
                                InAnt = 0x89;
                                break;
                            case 11:
                                InAnt = 0x8A;
                                break;
                            case 12:
                                InAnt = 0x8B;
                                break;
                            case 13:
                                InAnt = 0x8C;
                                break;
                            case 14:
                                InAnt = 0x8D;
                                break;
                            case 15:
                                InAnt = 0x8E;
                                break;
                        }
                        //_logger.LogInformation("Anten Döngü" + InAnt.ToString());
                        if (fCmdRet != 48 || fCmdRet != 55)
                             fCmdRet = RWDev.InventoryMix_G2(ref fComAdr, Qvalue, Session, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, ReadMem, ReadAdr, ReadLen, Psd, Target, InAnt, Scantime, FastFlag, EPC, ref Ant, ref Totallen, ref TagNum, frmcomportindex);


                    }


                }
                catch (Exception)
                {

                }
            }
             //fCmdRet = RWDev.CloseSpecComPort(frmcomportindex);


          
            //try
            //{
            //    if (fCmdRet != 48 || fCmdRet != 55)
            //        await Task.Run(() => fCmdRet = RWDev.InventoryMix_G2(ref fComAdr, Qvalue, Session, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, ReadMem, ReadAdr, ReadLen, Psd, Target, InAnt, Scantime, FastFlag, EPC, ref Ant, ref Totallen, ref TagNum, frmcomportindex));
            //    await Task.Delay(150);

            //}
            //catch (Exception)
            //{

        }



        #region 
        private string GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00://0
                    return "Başarılı";
                case 0x01://1
                    return "Envanter Bitmeden iade edin";
                case 0x02://2
                    return "Envanter tarama zamanı taşması";
                case 0x03://3
                    return "Daha Fazla Veri";
                case 0x04://4
                    return "Okuyucu modülü MCU Dolu";
                case 0x05://5
                    return "Erişim Şifresi Hatası";
                case 0x09://9
                    return "Şifreyi Yok Et Hatası";
                case 0x0a://10
                    return "Şifre Yok Hatası Sıfır Olamaz";
                case 0x0b://11
                    return "Etiket komutu desteklemiyor";
                case 0x0c://12
                    return "Komutu kullanın, Erişim Şifresi Sıfır Olamaz";
                case 0x0d://13
                    return "Etiket korumalı, tekrar ayarlanamıyor";
                case 0x0e://14
                    return "Etiket korumasız, sıfırlamaya gerek yok";
                case 0x10://16
                    return "Bazı kilitli baytlar var, yazma başarısız";
                case 0x11://17
                    return "Kilitleyemezsin";
                case 0x12://18
                    return "Kilitli, Tekrar Kilitlenemez";
                case 0x13://19
                    return "Parametre Kaydetme Başarısız, Güç Vermeden Önce Kullanılabilir";
                case 0x14://20
                    return "Ayarlanamıyor";
                case 0x15://21
                    return "Envanter bitmeden iade edin";
                case 0x16://22
                    return "Envanter-Tarama-Zamanı taşması";
                case 0x17://23
                    return "Daha Fazla Veri";
                case 0x18://24
                    return "Okuyucu modülü MCU dolu";
                case 0x19://25
                    return "Komutu Desteklemiyor Veya AccessPassword Sıfır Olamaz";
                case 0x1A://26
                    return "Hata etiketi özel işlevini gerçekleştirin";
                case 0xF8://248
                    return "Anten bağlantısı algılama hataları";
                case 0xF9://249
                    return "Komut yürütme hatası";
                case 0xFA://250
                    return "Etiket Al,Zayıf İletişim,Çalışamaz";
                case 0xFB://251
                    return "Çalıştırılabilir Etiket Yok";
                case 0xFC://252
                    return "Etiket Dönüş HataKodu";
                case 0xFD://253
                    return "Komut uzunluğu yanlış";
                case 0xFE://254
                    return "Geçersiz komut";
                case 0xFF://255
                    return "Parametre Hatası";
                case 0x30://48
                    return "İletişim hatası";
                case 0x31://49
                    return "CRC sağlama toplamı hatası";
                case 0x32://50
                    return "Dönüş veri uzunluğu hatası";
                case 0x33://51
                    return "İletişim meşgul";
                case 0x34://52
                    return "Meşgul, komut yürütülüyor";
                case 0x35://53
                    return "ComPort Açıldı";
                case 0x36://54
                    return "ComPort Kapalı";
                case 0x37://55
                    return "Geçersiz tutamaç";
                case 0x38://56
                    return "Geçersiz Bağlantı Noktası";
                case 0xEE://238
                    return "Dönüş Komutu Hatası";
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
