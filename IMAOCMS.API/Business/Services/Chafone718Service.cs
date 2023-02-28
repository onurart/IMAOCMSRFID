﻿using Azure.Core;
using IMAOCMS.API.Business.Services.Interfaces;
using IMAOCMS.Core.CHAFON;
using IMAOCMS.Core.Common.Responses;
using IMAOCMS.Core.Request;
using IMAOCMS.Repository.Constants;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.InteropServices;

namespace IMAOCMS.API.Business.Services;
public class Chafone718Service : IChafone718Service
{
    public static byte fComAdr = 0xff;
    private int fCmdRet = 30;
    private byte fBaud;
    public static int frmcomportindex;
    RFIDCallBack elegateRFIDCallBack;
    RfidTagCallBack myCallBack;
    private static object LockFlag = new object();
    List<RFIDTag> curList = new List<RFIDTag>();
    byte[] ReadAdr = new byte[2];
    byte[] Psd = new byte[4];
    byte ReadLen = 0;
    byte ReadMem = 0;
    int scanType = 0;
    private int total_tagnum = 0;
    private int CardNum = 0;
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


    public void GetEPC(RFIDTag mtag)
    {
        lock (LockFlag)
        {
            curList.Add(mtag);
            total_tagnum++;
        }
    }
    private string GetReturnCodeDesc(int cmdRet)
    {
        switch (cmdRet)
        {
            case 0x00:
            case 0x26:
                return "success";
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
                return "'Not Support Command Or AccessPassword Cannot be Zero";
            case 0x1A:
                return "Tag custom function error";
            case 0xF8:
                return "Check antenna error";
            case 0xF9:
                return "Command execute error";
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
                return Convert.ToString(cmdRet, 16);
        }
    }
    public async Task<ApiDataResponse<BaseRequest>> ConnectionDeviceAsync()
    {
        elegateRFIDCallBack = new RFIDCallBack(GetUid);
        myCallBack = new RfidTagCallBack(GetEPC);
        BaseRequest baseRequest = new BaseRequest();
        try
        {
            baseRequest.ComPort = 3;
            baseRequest.Baudrate = 115200;
            fBaud = 6;
            fComAdr = 255;
            int FrmPortIndex = 0;
            await Task.Run(() => fCmdRet = RWDev.OpenComPort(baseRequest.ComPort, ref fComAdr, fBaud, ref FrmPortIndex));
            if (fCmdRet == 0)
            {
                frmcomportindex = FrmPortIndex;
                if (FrmPortIndex > 0)
                    RWDev.InitRFIDCallBack(elegateRFIDCallBack, true, FrmPortIndex);
                //DeviceConnectionInfoAdd(request);
                return await Task.FromResult(new ApiDataResponse<BaseRequest>() { Data = baseRequest, Message ="Ok", Status = true });
            }
            else
              //  _logger.LogError(ResultMessage.ConnectDeviceFailed + "-" + baseRequest.IPAddress + "-" + baseRequest.Port);
            return await Task.FromResult(new ApiDataResponse<BaseRequest>() { Data = baseRequest, Message = "Fault", Status = false });

        }
        catch (Exception ex)
        {
           // _logger.LogError($"VF474Service-ConnectionDeviceAsync:{ex.ToString()}|{ex.StackTrace.ToString()}");
            return await Task.FromResult(new ApiDataResponse<BaseRequest>() { Data = baseRequest, Message = "Faultes" + " Hata: " + ex.ToString(), Status = false });
        }
    }

    public async Task<ApiResponse> DisconnectDeviceAsync()
    {
        try
        {
            await Task.Run(() => fCmdRet = RWDev.CloseSpecComPort(frmcomportindex));
            return await Task.FromResult(new ApiResponse() { Message = "Ok", Status = true });
        }
        catch (Exception ex)
        {
            //_logger.LogError($"VF474Service-DisconnectDeviceAsync:{ex.ToString()}|{ex.StackTrace.ToString()}");
            return await Task.FromResult(new ApiResponse() { Message = "Fault" + " Hata: " + ex.ToString(), Status = false });
        }
    }
}