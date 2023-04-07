using IMAOCMS.API.Business.Interfaces;
using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.DTOs;
using IMAOCMS.Core.SanwoRelay;
using IMAOCMS.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.IO.Ports;

namespace IMAOCMS.API.Business;
public class RelayCardService : IRelayCardService
{
    private readonly ILogger<RelayCardService> _logger;
    private readonly IChafone718Service _chafone718Service;
    // private readonly IRelayCardService _service;
    private static SerialPort _serialPort;
    public int devAddr = 1;
    public int inCount = 3;
    public double readinterval = 1.0;
    public RelayCardService(ILogger<RelayCardService> logger, IChafone718Service chafone718Service)
    {
        _logger = logger;
        _chafone718Service = chafone718Service;


        //_service = service;
    }
    public async Task<ApiResponse> ConnectionRelayAsync(RelayCardDeviceDto deviceDto)
    {
        _serialPort = new SerialPort()
        {
            PortName = deviceDto.Portnum,
            BaudRate = deviceDto.Baud,
            Parity = Parity.None,
            DataBits = 8,
            //StopBits = StopBits.None
        };
        try
        {
            if (!(_serialPort.IsOpen))
                _serialPort.Open();
            return await Task.FromResult(new ApiResponse() { Message = _serialPort.IsOpen.ToString(), Status = true });
            /// _serialPort.Write("SI\r\n");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error opening/writing to serial port :: " + ex.Message, "Error!");
            return await Task.FromResult(new ApiResponse() { Message = _serialPort.IsOpen.ToString(), Status = false });
        }
    }
    public async Task<ApiResponse> DisconnectRelayAsync()
    {
        try
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
            return await Task.FromResult(new ApiResponse() { Message = _serialPort.IsOpen.ToString(), Status = true });
            /// _serialPort.Write("SI\r\n");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error opening/writing to serial port :: " + ex.Message, "Error!");
            return await Task.FromResult(new ApiResponse() { Message = _serialPort.IsOpen.ToString(), Status = false });
        }
    }
    public async Task<ApiResponse> SetOut(string Outs)
    {
        if (_serialPort != null && _serialPort.IsOpen)
        {

            string sources = GetReturnOutCode(Outs);
            string hexString = sources;
            byte[] toHexByte = GlobalClass.strToToHexByte(hexString);
            _serialPort.Write(toHexByte, 0, toHexByte.Length);
            return await Task.FromResult(new ApiResponse() { Message = _serialPort.IsOpen.ToString(), Status = true });
        }
        _logger.LogError("Error opening/writing to serial port :: " + _serialPort.IsOpen.ToString(), "Error!");
        return await Task.FromResult(new ApiResponse() { Message = _serialPort.IsOpen.ToString(), Status = false });
    }
    private Dictionary<int, int> ReadStatus()
    {
        Dictionary<int, int> keys = new Dictionary<int, int>();
        try
        {
            Queue<string> stringQueue = new Queue<string>();
            string sources1 = devAddr.ToString("X2") + "020000" + inCount.ToString("X4");//010200000008
            string str1 = sources1 + GlobalClass.CRC16(sources1);//01020000000879CC
            stringQueue.Enqueue(str1);
            string sources2 = devAddr.ToString("X2") + "010000" + inCount.ToString("X4");//010100000008
            string str2 = sources2 + GlobalClass.CRC16(sources2);//0101000000083DCC
            stringQueue.Enqueue(str2);
            //while (true)
            //{
            try
            {
                if (_serialPort.IsOpen)
                {
                    string hexString = stringQueue.Dequeue();
                    stringQueue.Enqueue(hexString);
                    byte[] toHexByte = GlobalClass.strToToHexByte("01020000000879CC");
                    //byte[] toHexByte = GlobalClass.strToToHexByte("0101000000083DCC");
                    _serialPort.Write(toHexByte, 0, toHexByte.Length);
                    Thread.Sleep(100);
                    if (_serialPort.BytesToRead > 0)
                    {
                        byte[] numArray = new byte[_serialPort.BytesToRead];
                        _serialPort.Read(numArray, 0, numArray.Length);
                        keys = AnalyzeData(toHexByte, numArray);
                    }
                }
            }
            catch
            {
            }
            //Thread.Sleep(TimeSpan.FromSeconds(this.readinterval));
            //}
        }
        catch (Exception e)
        {

        }
        return keys;
    }
    private async Task<Dictionary<int, int>> ReadStatusWork()
    {
        Dictionary<int, int> keys = new Dictionary<int, int>();
        // RelayCardDeviceDto deviceDto
        try
        {
            _serialPort = new SerialPort()
            {
                PortName = "COM9",
                BaudRate = 9600,
                Parity = Parity.None,
                DataBits = 8,
                //StopBits = StopBits.None
            };
            if (!_serialPort.IsOpen)
                _serialPort.Open();
            if (_serialPort.IsOpen)
            {
                byte[] toHexByte = GlobalClass.strToToHexByte("01020000000879CC");
                //byte[] toHexByte = GlobalClass.strToToHexByte("0101000000083DCC");
                _serialPort.Write(toHexByte, 0, toHexByte.Length);
                Thread.Sleep(100);
                if (_serialPort.BytesToRead > 0)
                {
                    byte[] numArray = new byte[_serialPort.BytesToRead];
                    _serialPort.Read(numArray, 0, numArray.Length);
                    keys = AnalyzeData(toHexByte, numArray);
                    _serialPort.Close();

                    
                    //if(keys.get.FirstOrDefault(x=>x.Value==1))

                    if (keys.ContainsKey(1).Equals(0))
                    {
                        if (keys.ContainsValue(0))
                        {
                            using HttpClient httpClient = new();
                            string baseurl = "https://localhost:7091/api/";
                            var response = await httpClient.GetFromJsonAsync<ApiResponse>(baseurl + "StopEpcReader");
                        }
                        else
                        {
                            using HttpClient httpClient = new();
                            string baseurl = "https://localhost:7091/api/";
                            var response = await httpClient.GetFromJsonAsync<ApiResponse>(baseurl + "StartEpcReader");
                        }

                    }
                    else if (keys.ContainsKey(1) == keys.ContainsValue(1))
                    {

                    }


                }
            }
        }
        catch
        {
        }



        return keys;
    }
    private Dictionary<int, int> AnalyzeData(byte[] sendBuff, byte[] recBuff)
    {
        Dictionary<int, int> dic = new Dictionary<int, int>();
        try
        {
            if ((int)sendBuff[0] == (int)recBuff[0] && (int)sendBuff[1] == (int)recBuff[1])
            {
                int num1 = (int)recBuff[2];
                int key = 1;
                for (int index1 = 0; index1 < num1; ++index1)
                {
                    for (int index2 = 0; index2 < inCount; ++index2)
                    {
                        int num2 = (int)recBuff[3 + index1] >> index2 & 1;
                        dic.Add(key, num2);
                        ++key;
                    }
                }
            }
            if (dic.Count <= 0)
                return dic;
            switch (((int)sendBuff[1]).ToString("X2"))
            {
                case "02":
                    this.AnalyzeInData(dic);
                    break;
                case "01":
                    this.AnalyzeOutData(dic);
                    break;
            }
        }
        catch (Exception ex)
        {
        }
        return dic;
    }
    private void AnalyzeInData(Dictionary<int, int> dic)
    {
        try
        {

            if (dic.ContainsKey(1))

                // btnX1.BackColor = Color.Green;
                if (dic.ContainsKey(2))
                    // btnX2.BackColor = Color.Green;
                    if (dic.ContainsKey(3))
                        //  btnX3.BackColor = Color.Green;
                        if (dic.ContainsKey(4))
                            //  btnX4.BackColor = Color.Green;
                            if (dic.ContainsKey(5))
                                //  btnX5.BackColor = Color.Green;
                                if (dic.ContainsKey(6))
                                    //  btnX6.BackColor = Color.Green;
                                    if (dic.ContainsKey(7))
                                        //  btnX7.BackColor = Color.Green;
                                        if (dic.ContainsKey(8))
                                            //  btnX8.BackColor = Color.Green;
                                            if (dic.ContainsKey(9))
                                                //this.SetImage(this.in9, dic[9], true);
                                                if (dic.ContainsKey(10))
                                                    //this.SetImage(this.in10, dic[10], true);
                                                    if (dic.ContainsKey(11))
                                                        //this.SetImage(this.in11, dic[11], true);
                                                        if (dic.ContainsKey(12))
                                                            //this.SetImage(this.in12, dic[12], true);
                                                            if (dic.ContainsKey(13))
                                                                //this.SetImage(this.in13, dic[13], true);
                                                                if (dic.ContainsKey(14))
                                                                    //this.SetImage(this.in14, dic[14], true);
                                                                    if (dic.ContainsKey(15))
                                                                        //this.SetImage(this.in15, dic[15], true);
                                                                        if (dic.ContainsKey(16))
                                                                            //this.SetImage(this.in16, dic[16], true);
                                                                            if (dic.ContainsKey(17))
                                                                                //this.SetImage(this.in17, dic[17], true);
                                                                                if (dic.ContainsKey(18))
                                                                                    //this.SetImage(this.in18, dic[18], true);
                                                                                    if (dic.ContainsKey(19))
                                                                                        //this.SetImage(this.in19, dic[19], true);
                                                                                        if (dic.ContainsKey(20))
                                                                                            //this.SetImage(this.in20, dic[20], true);
                                                                                            if (dic.ContainsKey(21))
                                                                                                //this.SetImage(this.in21, dic[21], true);
                                                                                                if (dic.ContainsKey(22))
                                                                                                    //this.SetImage(this.in22, dic[22], true);
                                                                                                    if (dic.ContainsKey(23))
                                                                                                        //this.SetImage(this.in23, dic[23], true);
                                                                                                        if (dic.ContainsKey(24))
                                                                                                            //this.SetImage(this.in24, dic[24], true);
                                                                                                            if (dic.ContainsKey(25))
                                                                                                                //this.SetImage(this.in25, dic[25], true);
                                                                                                                if (dic.ContainsKey(26))
                                                                                                                    //this.SetImage(this.in26, dic[26], true);
                                                                                                                    if (dic.ContainsKey(27))
                                                                                                                        //this.SetImage(this.in27, dic[27], true);
                                                                                                                        if (dic.ContainsKey(28))
                                                                                                                            //this.SetImage(this.in28, dic[28], true);
                                                                                                                            if (dic.ContainsKey(29))
                                                                                                                                //this.SetImage(this.in29, dic[29], true);
                                                                                                                                if (dic.ContainsKey(30))
                                                                                                                                    //this.SetImage(this.in30, dic[30], true);
                                                                                                                                    if (dic.ContainsKey(31))
                                                                                                                                        //this.SetImage(this.in31, dic[31], true);
                                                                                                                                        if (!dic.ContainsKey(32))
                                                                                                                                            return;
            //this.SetImage(this.in32, dic[32], true);

        }
        catch (Exception ex)
        {
        }
    }
    private void AnalyzeOutData(Dictionary<int, int> dic)
    {
        try
        {


            if (dic.ContainsKey(1))
                //  this.SetImage(this.out1, dic[1], false);
                if (dic.ContainsKey(2))
                    //  this.SetImage(this.out2, dic[2], false);
                    if (dic.ContainsKey(3))
                        //   this.SetImage(this.out3, dic[3], false);
                        if (dic.ContainsKey(4))
                            //   this.SetImage(this.out4, dic[4], false);
                            if (dic.ContainsKey(5))
                                //   this.SetImage(this.out5, dic[5], false);
                                if (dic.ContainsKey(6))
                                    //   this.SetImage(this.out6, dic[6], false);
                                    if (dic.ContainsKey(7))
                                        //   this.SetImage(this.out7, dic[7], false);
                                        if (dic.ContainsKey(8))
                                            //   this.SetImage(this.out8, dic[8], false);
                                            if (dic.ContainsKey(9))
                                                //  this.SetImage(this.out9, dic[9], false);
                                                if (dic.ContainsKey(10))
                                                    //  this.SetImage(this.out10, dic[10], false);
                                                    if (dic.ContainsKey(11))
                                                        //   this.SetImage(this.out11, dic[11], false);
                                                        if (dic.ContainsKey(12))
                                                            //  this.SetImage(this.out12, dic[12], false);
                                                            if (dic.ContainsKey(13))
                                                                //    this.SetImage(this.out13, dic[13], false);
                                                                if (dic.ContainsKey(14))
                                                                    //  this.SetImage(this.out14, dic[14], false);
                                                                    if (dic.ContainsKey(15))
                                                                        //   this.SetImage(this.out15, dic[15], false);
                                                                        if (dic.ContainsKey(16))
                                                                            //   this.SetImage(this.out16, dic[16], false);
                                                                            if (dic.ContainsKey(17))
                                                                                //   this.SetImage(this.out17, dic[17], false);
                                                                                if (dic.ContainsKey(18))
                                                                                    //   this.SetImage(this.out18, dic[18], false);
                                                                                    if (dic.ContainsKey(19))
                                                                                        //    this.SetImage(this.out19, dic[19], false);
                                                                                        if (dic.ContainsKey(20))
                                                                                            //   this.SetImage(this.out20, dic[20], false);
                                                                                            if (dic.ContainsKey(21))
                                                                                                //     this.SetImage(this.out21, dic[21], false);
                                                                                                if (dic.ContainsKey(22))
                                                                                                    //   this.SetImage(this.out22, dic[22], false);
                                                                                                    if (dic.ContainsKey(23))
                                                                                                        //  this.SetImage(this.out23, dic[23], false);
                                                                                                        if (dic.ContainsKey(24))
                                                                                                            //  this.SetImage(this.out24, dic[24], false);
                                                                                                            if (dic.ContainsKey(25))
                                                                                                                //   this.SetImage(this.out25, dic[25], false);
                                                                                                                if (dic.ContainsKey(26))
                                                                                                                    //  this.SetImage(this.out26, dic[26], false);
                                                                                                                    if (dic.ContainsKey(27))
                                                                                                                        //  this.SetImage(this.out27, dic[27], false);
                                                                                                                        if (dic.ContainsKey(28))
                                                                                                                            //   this.SetImage(this.out28, dic[28], false);
                                                                                                                            if (dic.ContainsKey(29))
                                                                                                                                //   this.SetImage(this.out29, dic[29], false);
                                                                                                                                if (dic.ContainsKey(30))
                                                                                                                                    //  this.SetImage(this.out30, dic[30], false);
                                                                                                                                    if (dic.ContainsKey(31))
                                                                                                                                        //   this.SetImage(this.out31, dic[31], false);
                                                                                                                                        if (dic.ContainsKey(32))
                                                                                                                                            //  this.SetImage(this.out32, dic[32], false);
                                                                                                                                            if (dic.ContainsKey(33))
                                                                                                                                                //  this.SetImage(this.out33, dic[33], false);
                                                                                                                                                if (dic.ContainsKey(34))
                                                                                                                                                    //  this.SetImage(this.out34, dic[34], false);
                                                                                                                                                    if (dic.ContainsKey(35))
                                                                                                                                                        //  this.SetImage(this.out35, dic[35], false);
                                                                                                                                                        if (dic.ContainsKey(36))
                                                                                                                                                            //    this.SetImage(this.out36, dic[36], false);
                                                                                                                                                            if (dic.ContainsKey(37))
                                                                                                                                                                //   this.SetImage(this.out37, dic[37], false);
                                                                                                                                                                if (dic.ContainsKey(38))
                                                                                                                                                                    //   this.SetImage(this.out38, dic[38], false);
                                                                                                                                                                    if (dic.ContainsKey(39))
                                                                                                                                                                        //   this.SetImage(this.out39, dic[39], false);
                                                                                                                                                                        if (dic.ContainsKey(40))
                                                                                                                                                                            //   this.SetImage(this.out40, dic[40], false);
                                                                                                                                                                            if (dic.ContainsKey(41))
                                                                                                                                                                                //  this.SetImage(this.out41, dic[41], false);
                                                                                                                                                                                if (dic.ContainsKey(42))
                                                                                                                                                                                    //   this.SetImage(this.out42, dic[42], false);
                                                                                                                                                                                    if (dic.ContainsKey(43))
                                                                                                                                                                                        //  this.SetImage(this.out43, dic[43], false);
                                                                                                                                                                                        if (dic.ContainsKey(44))
                                                                                                                                                                                            //   this.SetImage(this.out44, dic[44], false);
                                                                                                                                                                                            if (dic.ContainsKey(45))
                                                                                                                                                                                                //   this.SetImage(this.out45, dic[45], false);
                                                                                                                                                                                                if (dic.ContainsKey(46))
                                                                                                                                                                                                    //  this.SetImage(this.out46, dic[46], false);
                                                                                                                                                                                                    if (dic.ContainsKey(47))
                                                                                                                                                                                                        //  this.SetImage(this.out47, dic[47], false);
                                                                                                                                                                                                        if (!dic.ContainsKey(48))
                                                                                                                                                                                                            return;
            // this.SetImage(this.out48, dic[48], false);

        }
        catch (Exception ex)
        {
        }
    }
    private string GetReturnOutCode(string get)
    {
        switch (get)
        {
            case "Y1Open"://Y1 Open
                return "01050000FF008C3A";
            case "Y1Close"://Y1 Close
                return "0105000000FF8D8A";
            case "Y2Open"://Y2 Open
                return "01050001FF00DDFA";
            case "Y2Close"://Y2 Close
                return "0105000100FFDC4A";
            case "Y3Open"://Y3 Open
                return "01050002FF002DFA";
            case "Y3Close"://Y3 Close
                return "0105000200FF2C4A";
            case "Y4Open"://Y4 Open
                return "01050003FF007C3A";
            case "Y4Close"://Y4 Close
                return "0105000300FF7D8A";
            case "Y5Open"://Y5 Open
                return "01050004FF00CDFB";
            case "Y5Close"://Y5 Close
                return "0105000400FFCC4B";
            case "Y6Open"://Y6 Open
                return "01050005FF009C3B";
            case "Y6Close"://Y6 Close
                return "0105000500FF9D8B";
            case "Y7Open"://Y7 Open
                return "01050006FF006C3B";
            case "Y7Close"://Y7 Close
                return "0105000600FF6D8B";
            case "Y8Open"://Y8 Open
                return "01050007FF003DFB";
            case "Y8Close"://Y8 Close
                return "0105000700FF3C4B";
            default:
                return "";
        }
    }

    public async Task<ApiDicListResponse> ReadStatusAsync()
    {
        var result = ReadStatus();
        return await Task.FromResult(new ApiDicListResponse() { Data = result, Message = _serialPort.IsOpen.ToString(), Status = true });
    }

    public async Task<ApiDicListResponse> ReadStatusWorkAsync()
    {
        var result = await ReadStatusWork();
        return await Task.FromResult(new ApiDicListResponse() { Data = result, Message = _serialPort.IsOpen.ToString(), Status = true });
    }
}