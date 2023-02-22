using System.Runtime.InteropServices;
using System.Text;
namespace IMAOCMS.Core.Entites
{
    internal delegate void SearchCallBack(IntPtr dev, IntPtr data);

    public class DevControl
    {
        private const string DLL_NAME = "dmdll.dll";
        public enum tagErrorCode
        {
            DM_ERR_OK,				/* hata yok */
            DM_ERR_PARA,			/* parametre hatası */
            DM_ERR_NOAUTH,			/* */
            DM_ERR_AUTHFAIL,		/* yetkilendirme başarısız */
            DM_ERR_SOCKET,			/* soket hatası */
            DM_ERR_MEM,				/* */
            DM_ERR_TIMEOUT,
            DM_ERR_ARG,
            DM_ERR_MATCH,			/* komut ve yanıttaki parametreler eşleşmiyor */
            DM_ERR_OPR,
            DM_ERR_MAX
        };
        internal enum DataType
        {
            PARA_TYPE_STRING,
            PARA_TYPE_UCHAR,
            PARA_TYPE_USHORT,
            PARA_TYPE_ULONG,
            PARA_TYPE_UCHAR_HEX,
            PARA_TYPE_INVALID
        };
        /// <summary>
        /// Sistemi başlat
        /// </summary>
        /// <param name="searchCB">Cihaz bulunduktan sonra geri arama işlevi</param>
        /// <returns>Başlatmanın başarılı olup olmadığı</returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_Init(SearchCallBack searchCB, IntPtr data);
        /// <summary>
        /// geri dönüşüm sistemi
        /// </summary>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_DeInit();
        /// <summary>
        /// Ağdaki cihazları arayın ve bulunan cihazlar arama geri arama işlevini geçecektir.SearchCallBackgeri dönmek
        /// </summary>
        /// <param name="deviceIP">Tüm kullanımlar aranıyorsa aranacak IP255.255.255.255</param>
        /// <param name="timeout">fazla mesai</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_SearchDevice(uint deviceIP, int timeout);
        /// <summary>
        /// Temel olarak arama geri aramasında kullanılan cihazın temel bilgilerini döndürür
        /// </summary>
        /// <param name="devhandle">dahili kol</param>
        /// <param name="ipaddr">IPadres</param>
        /// <param name="macaddr">fiziksel adres</param>
        /// <param name="devname">cihaz adı</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_GetDeviceInfo(IntPtr devhandle, ref uint ipaddr, StringBuilder macaddr, StringBuilder devname);
        /// <summary>
        /// giriş cihazı
        /// </summary>
        /// <param name="devHandle">Cihazın dahili tanıtıcısı</param>
        /// <param name="name">Kullanıcı adı</param>
        /// <param name="password">şifre</param>
        /// <param name="timeout">fazla mesai</param>
        /// <returns>Giriş sonucu</returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_AuthLogin(IntPtr devHandle, StringBuilder name, StringBuilder password, int timeout);
        /// <summary>
        /// Cihaz şifresini değiştir
        /// </summary>
        /// <param name="devHandle">Cihazın dahili tanıtıcısı</param>
        /// <param name="oldPassword">Kullanıcının mevcut şifresi</param>
        /// <param name="newPassword">kullanıcı yeni şifre</param>
        /// <param name="timeout">fazla mesai</param>
        /// <returns>Giriş sonucu</returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_ModifyPassword(IntPtr devHandle, StringBuilder oldPassword, StringBuilder newPassword, int timeout);
        /// <summary>
        /// Al ve yapılandır parametrelerinin bir listesini oluşturun
        /// </summary>
        /// <param name="devHandle">dahilihandle</param>
        /// <returns>Oluşturma başarısız olursa boş döner</returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr paralistCreate(IntPtr devHandle);
        /// <summary>
        /// bir bağımsız değişken listesini yok eder
        /// </summary>
        /// <param name="list">create işlevi tarafından döndürülen işaretçi</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode paralistDestroy(IntPtr list);
        /// <summary>
        /// bağımsız değişken listesine bir bağımsız değişken ekleyin
        /// </summary>
        /// <param name="list">liste işaretçisi</param>
        /// <param name="chanNo">Kanal numarası</param>
        /// <param name="paraType">Parametre Türü</param>
        /// <param name="valueLen">Yapılandırılmış veri uzunluğu</param>
        /// <param name="value">yapılandırılmış veri</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode paralist_addnode(IntPtr list, int chanNo, PARA_TYPES paraType, int valueLen, byte[] value);
        /// <summary>
        /// bağımsız değişken listesine bir bağımsız değişken ekleyin
        /// </summary>
        /// <param name="list">liste işaretçisi</param>
        /// <param name="chanNo">Kanal numarası</param>
        /// <param name="paraType">Parametre Türü</param>
        /// <returns></returns>
        internal static tagErrorCode paralist_addnode(IntPtr list, int chanNo, PARA_TYPES paraType)
        {
            return paralist_addnode(list, chanNo, paraType, 0, null);
        }
        /// <summary>
        /// Parametre listesindeki bir parametreyi yok eder
        /// </summary>
        /// <param name="list">liste işaretçisi</param>
        /// <param name="chanNo">Kanal numarası</param>
        /// <param name="paraType">Parametre Türü</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode paralist_delnode(IntPtr list, int chanNo, PARA_TYPES paraType);
        /// <summary>
        /// Elde edilen parametre listesinden sonucu almak için sorgulama
        /// </summary>
        /// <param name="list">liste</param>
        /// <param name="chanNo">Kanal numarası</param>
        /// <param name="paraType">Parametre Türü</param>
        /// <param name="valueType">veri türü</param>
        /// <param name="valueLen">veri uzunluğu</param>
        /// <param name="value">veri değeri</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode paralist_getnode(IntPtr list, int chanNo, PARA_TYPES paraType, ref int valueLen, byte[] value);
        /// <summary>
        /// Elde edilen parametre listesinden elde edilen sonucu sorgulayın ve tüm sonuçlar dizelerdir;
        /// </summary>
        /// <param name="paraType">Parametre Türü</param>
        /// <param name="valueType">veri arabelleği</param>
        /// <param name="valueLen">veri uzunluğu</param>
        /// <param name="value">dizi veri arabelleği</param>
        /// /// <param name="valueLen">dizi veri uzunluğu</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_Value2String(PARA_TYPES eParaType, byte[] bufferValue, int nValueLen, StringBuilder bufferString, ref int nStringLen);
        /// <summary>
        /// Sonucu elde etmek için elde edilen parametre listesini sorgulayın ve tüm sonuçlar dizelerdir；
        /// </summary>
        /// <param name="paraType">Parametre Türü</param>
        /// <param name="valueType">veri arabelleği</param>
        /// <param name="valueLen">veri uzunluğu</param>
        /// <param name="value">dizi veri arabelleği</param>
        /// /// <param name="valueLen">dizi veri uzunluğu</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_String2Value(PARA_TYPES eParaType, StringBuilder bufferString, int nStringLen, byte[] bufferValue, ref int nValueLen);
        /// <summary>
        /// Parametrelerin geçerli olduğunu doğrulayın
        /// </summary>
        /// <param name="devHandle">cihaz kolu</param>
        /// <param name="chanNo">Kanal numarası</param>
        /// <param name="paraType">Parametre Türü</param>
        /// <param name="valuelen">parametre uzunluğu</param>
        /// <param name="value">parametre değeri</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_CheckPara(IntPtr devHandle, int chanNo, PARA_TYPES paraType, int valuelen, byte[] value);
        /// <summary>
        /// Listedeki parametreleri cihazdan alın
        /// </summary>
        /// <param name="devHandle">cihaz kolu</param>
        /// <param name="list">list</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_GetPara(IntPtr devHandle, IntPtr list, int timeout);
        /// <summary>
        /// Listedeki parametreleri cihaza yapılandırın
        /// </summary>
        /// <param name="devHandle">cihaz kolu</param>
        /// <param name="list">list</param>
        /// <param name="timeout">fazla mesai</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_SetPara(IntPtr devHandle, IntPtr list, int timeout);
        /// <summary>
        /// Cihazı yeniden başlatın ve geçerli ayar parametrelerini kaydedin；
        /// </summary>
        /// <param name="devHandle">cihaz kolu</param>
        /// <param name="timeout">fazla mesai</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_ResetDevice(IntPtr devHandle, int timeout);
        /// <summary>
        /// Geçerli ayar parametrelerini kaydetmeden cihazı yeniden başlatın；
        /// </summary>
        /// <param name="devHandle">teçhizathandle</param>
        /// <param name="timeout">fazla mesai</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_ResetDeviceWithoutSave(IntPtr devHandle, int timeout);
        /// <summary>
        /// Cihazın varsayılan parametrelerini geri yükleyin, ancak cihazı kaydetmeyin veya yeniden başlatmayın;
        /// </summary>
        /// <param name="devHandle">teçhizathandle</param>
        /// <param name="timeout">fazla mesai</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_LoadDefault(IntPtr devHandle, int timeout);
        /// <summary>
        /// cihazdan çıkış yap
        /// </summary>
        /// <param name="devHandle">teçhizatandle</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_LogOutDevice(IntPtr devHandle, int timeout);
        /// <summary>
        /// aramayı cihaza bırak
        /// </summary>
        /// <param name="devHandle">teçhizathandle</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_FreeDevice(IntPtr devHandle);
        /// <summary>
        /// Cihazın belirtilen COM'u destekleyip desteklemediğini kontrol edin；
        /// </summary>
        /// <param name="devHandle">teçhizathandle</param>
        /// <param name="comNum">COMseri numarası</param>
        /// <returns>BOOLEAN</returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool DM_IsComEnable(IntPtr devHandle, int comNum);
        internal enum PARA_TYPES
        {
            BAUDRATE,
            DATABITS,
            STOPBITS,
            PARITY,
            FLOWCONTROL,
            FIFO,
            ENABLEPACKING,
            IDLEGAPTIME,
            MATCH2BYTESEQUENCE,
            SENDFRAMEONLY,
            SENDTRAILINGBYTES,
            INPUTWITHACTIVECONNECT,
            OUTPUTWITHACTIVECONNECT,
            INPUTWITHPASSIVECONNECT,
            OUTPUTWITHPASSIVECONNECT,
            INPUTATTIMEOFDISCONNECT,
            OUTPUTATTIMEOFDISCONNECT,
            IPCONFIGURATION,				/* */
            AUTONEGOTIATE,
            MACADDRESS,						/* ethernet arabirimi mac adresi*/
            SPEED,
            DUPLEX,
            NETPROTOCOL,
            ACCEPTINCOMING,
            ARPCACHETIMEOUT,
            TCPKEEPACTIVE,
            CPUPERFORMANCEMODE,
            HTTPSERVERPORT,
            MTUSIZE,
            RETRYCOUNTER,
            IPADDRESS,						/* ethernet arabirimi ip adresi(statik) */
            FIRMWARE,
            UPTIME,
            SERIALNO,
            RETRYTIMEOUT,
            HOSTLIST1_IP,
            HOSTLIST1_PORT,
            HOSTLIST2_IP,
            HOSTLIST2_PORT,
            HOSTLIST3_IP,
            HOSTLIST3_PORT,
            HOSTLIST4_IP,
            HOSTLIST4_PORT,
            HOSTLIST5_IP,
            HOSTLIST5_PORT,
            HOSTLIST6_IP,
            HOSTLIST6_PORT,
            HOSTLIST7_IP,
            HOSTLIST7_PORT,
            HOSTLIST8_IP,
            HOSTLIST8_PORT,
            HOSTLIST9_IP,
            HOSTLIST9_PORT,
            HOSTLIST10_IP,
            HOSTLIST10_PORT,
            HOSTLIST11_IP,
            HOSTLIST11_PORT,
            HOSTLIST12_IP,
            HOSTLIST12_PORT,
            FIRSTMATCHBYTE,
            LASTMATCHBYTE,
            DATAGRAMTYPE,
            DEVICEADDRESSTABLE1_BEGINIP,
            UDPLOCALPORT,
            UDPREMOTEPORT,
            UDPNETSEGMENT,
            DEVICEADDRESSTABLE2_BEGINIP,
            DEVICEADDRESSTABLE2_PORT,
            DEVICEADDRESSTABLE2_ENDIP,
            DEVICEADDRESSTABLE3_BEGINIP,
            DEVICEADDRESSTABLE3_PORT,
            DEVICEADDRESSTABLE3_ENDIP,
            DEVICEADDRESSTABLE4_BEGINIP,
            DEVICEADDRESSTABLE4_PORT,
            DEVICEADDRESSTABLE4_ENDIP,
            UDPUNICASTLOCALPORT,
            ACCEPTIONINCOMING,
            ACTIVECONNECT,
            STARTCHARACTER,
            ONDSRDROP,
            HARDDISCONNECT,
            CHECKEOT,
            INACTIVITYTIMEOUT_M,
            INACTIVITYTIMEOUT_S,
            LOCALPORT,
            REMOTEHOST,
            REMOTEPORT,
            DNSQUERYPERIOD,
            DEVICEADDRESSTABLE1_ENDIP,
            DEVICEADDRESSTABLE1_PORT,
            CONNECTRESPONSE,
            TERMINALNAME,
            USEHOSTLIST,
            EMAILADDRESS,
            EMAILUSERNAME,
            EMAILPASSWORD,
            EMAILINPUTTRIGGERMESSAGE,
            EMAILADDRESS1,
            EMAILADDRESS2,
            EMAILADDRESS3,
            POP3DOMAINNAME,
            SMTPDOMAINNAME,
            POP3PORT,
            SMTPPORT,
            COLDSTART,
            DCDCHANGED,
            DSRCHANGED,
            WARMSTART,
            AUTHENTICATIONFAILURE,
            IPADDRESSCHANGED,
            ENABLESERIALTRIGGERINPUT,
            SERIALCHANNEL,
            SERIALDATASIZE,
            SERIALMATCHDATA1,
            SERIALMATCHDATA2,
            EMAILTRIGGERSUBJECT,
            PRIORITY,
            INPUTPRIORITY,
            INPUTMINNOTIFICATIONINTERVAL,
            MINNOTIFICATIONINTERVAL,
            RENOTIFICATIONINTERVAL,
            NEWUSERPSW,
            BOOTP,
            DHCP,
            AUTOIP,
            DHCPHOSTNAME,
            SUBNET,
            DEFAULTGATEWAY,
            DEVICENAME,				/* cihaz adı, sunucu adı*/
            TIMEZONE,
            LOCALTIME_YEAR,
            LOCALTIME_MONTH,
            LOCALTIME_DAY,
            LOCALTIME_HOUR,
            LOCALTIME_MINUTE,
            LOCALTIME_SECOND,
            TIMESERVER,
            WEBCONSOLE,
            TELNETCONSOLE,
            PASSWORDCHANGED,
            SERIALPORTOPTIONS,
            PREFERREDDNSSERVER,
            ALTERNATEDNSSERVER,
            SERIALMATCHDATA3,
            INPUT1,
            INPUT2,
            IO1,
            IO2,
            IO1TYPE,
            IO2TYPE,
            IO1STATE,
            IO2STATE,
            SERIALPORTPROTOCOL,
            FIRMWAREID,
            PPPOEUSERNAME,
            PPPOEPASSWORD,
            PPPOEWORKMODE,
            PPPOEMAXREDIALTIMES,
            PPPOEREDIALINTERVAL,
            PPPOEIDLETIME,
            PPPOESTATUS,
            PPPOEIP,
            PPPOEGATEWAY,
            PPPOEDNS1,
            PPPOEDNS2,
            ENABLEBACKUPLINK,

            END_OF_PARA_TYPES
        };
        internal static tagErrorCode getParaStringValue(IntPtr paraList, int chanNo, PARA_TYPES paraType, ref int valueLen, StringBuilder value)
        {
            byte[] bufferValue = new byte[100];
            int getLen = bufferValue.Length;
            tagErrorCode errCode;
            errCode = paralist_getnode(paraList, chanNo, paraType, ref getLen, bufferValue);
            if (errCode == DevControl.tagErrorCode.DM_ERR_OK)
            {
                errCode = DM_Value2String(paraType, bufferValue, getLen, value, ref valueLen);
            }

            return errCode;
        }
    }
}
