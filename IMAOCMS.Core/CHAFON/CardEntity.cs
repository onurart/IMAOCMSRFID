using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMAOCMS.Core.CHAFON
{
    public class CardEntity
    {
        public const int USER = 0x0400;
        public const int WM_SENDTAG = USER + 101;
        public const int WM_SENDTAGSTAT = USER + 102;
        public const int WM_SENDSTATU = USER + 103;
        public const int WM_SENDBUFF = USER + 104;
        public const int WM_MIXTAG = USER + 105;
        public const int WM_SHOWNUM = USER + 106;
        public const int WM_FASTID = USER + 107;
        public const int WM_JB_MIX = USER + 108;
        public const int WM_JB_TAG = USER + 109;
        public const int WM_GB_MIX = USER + 110;
        public const int WM_GB_TAG = USER + 111;
        public static byte fComAdr = 0xff; //şu anda çalışıyorComAdr
        public int ferrorcode;
        public byte fBaud;
        public double fdminfre;
        public double fdmaxfre;
        public int fCmdRet = 30; //Yürütülen tüm komutların dönüş değeri
        public bool fisinventoryscan_6B;
        public byte[] fOperEPC = new byte[100];
        public byte[] fPassWord = new byte[4];
        public byte[] fOperID_6B = new byte[10];
        private int CardNum1 = 0;
        private string fInventory_EPC_List; //Sorgu listesini saklayın (okunan veriler değişmediyse yenilenmeyecektir)
        public static int frmcomportindex;
        //private bool SeriaATflag = false;
        private byte Target = 0;
        private byte InAnt = 0;
        private byte Scantime = 0;
        private byte FastFlag = 0;
        private byte Qvalue = 0;
        private byte Session = 0;
        private int total_tagnum = 0;//etiket sayısı
        private int CardNum = 0;
        private int total_time = 0;//toplam zaman
        private int targettimes = 0;
        private byte TIDFlag = 0;
        private byte tidLen = 0;
        private byte tidAddr = 0;
        public static byte antinfo = 0;
        private int AA_times = 0;
        private int CommunicationTime = 0;
        public DeviceClass SelectedDevice;
        private static List<DeviceClass> DevList;
        //private static SearchCallBack searchCallBack = new SearchCallBack(searchCB);
        private string ReadTypes = "";
        private static object LockFlag = new object();
    }
}
