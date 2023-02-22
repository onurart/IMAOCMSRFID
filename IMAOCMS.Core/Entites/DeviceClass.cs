using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMAOCMS.Core.Entites
{
    /// <summary>
    /// Cihaz nesnesi, cihaz bilgilerini kaydedin ve cihaz işletim arayüzü sağlayın；
    /// </summary>
    public class DeviceClass
    {
        private int communicationTimeout = 1000;

        /// <summary>
        /// yayın adresi;
        /// </summary>
        public static uint Broadcast
        {
            get
            {
                return 0xffffffff;
            }
        }

        private IntPtr _devHandle;
        /// <summary>
        /// cihaz kolu;
        /// </summary>
        public IntPtr DevHandle
        {
            get
            {
                return _devHandle;
            }
            set
            {
                _devHandle = value;
            }
        }

        private uint _devIP;
        /// <summary>
        /// Cihaz IP adresi;
        /// </summary>
        public uint DeviceIP
        {
            get
            {
                return _devIP;
            }
            set
            {
                _devIP = value;
            }
        }

        private string _devMac;
        /// <summary>
        /// 设备设备Mac地址；
        /// </summary>
        public string DeviceMac
        {
            get
            {
                return _devMac;
            }
            set
            {
                _devMac = value;
            }
        }

        private string _devName;
        /// <summary>
        /// Ekipman adı;
        /// </summary>
        public string DeviceName
        {
            get
            {
                return _devName;
            }
            set
            {
                _devName = value;
            }
        }

        private bool _isLogin = false;
        /// <summary>
        /// Cihaz oturum açma durumu;
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
        }

        /// <summary>
        /// cihaz nesnesi yapıcısı;
        /// </summary>
        /// <param name="devHandle">cihaz kolu</param>
        /// <param name="deviceIP">Cihaz IP adresi</param>
        /// <param name="deviceMac">Cihaz Mac adresi</param>
        /// <param name="deviceName">Ekipman adı</param>
        public DeviceClass(IntPtr devHandle, uint deviceIP, string deviceMac, string deviceName)
        {
            this.DevHandle = devHandle;
            this.DeviceIP = deviceIP;
            this.DeviceMac = deviceMac;
            this.DeviceName = deviceName;
        }

        /// <summary>
        /// giriş cihazı；
        /// </summary>
        /// <param name="userName">Kullanıcı adı</param>
        /// <param name="password">şifre</param>
        /// <returns>tagErrorCode</returns>
        public DevControl.tagErrorCode Login(string userName, string password)
        {
            StringBuilder nameBuf, passwordBuf;
            DevControl.tagErrorCode eCode;

            nameBuf = new StringBuilder(userName);
            passwordBuf = new StringBuilder(password);
            eCode = DevControl.DM_AuthLogin(this._devHandle, nameBuf, passwordBuf, this.communicationTimeout);
            if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
            {
                this._isLogin = true;
            }
            else
            {
                this._isLogin = false;
            }

            return eCode;
        }

        /// <summary>
        /// cihazdan çıkış yap；
        /// </summary>
        /// <returns>tagErrorCode</returns>
        public DevControl.tagErrorCode Logout()
        {
            DevControl.tagErrorCode eCode = DevControl.tagErrorCode.DM_ERR_OK;
            if (this._isLogin == true)
            {
                eCode = DevControl.DM_LogOutDevice(this._devHandle, this.communicationTimeout);
                this._isLogin = false;
            }

            return eCode;
        }

        /// <summary>
        /// Cihaz şifresini değiştir；
        /// </summary>
        /// <param name="oldPassword">Kullanıcının mevcut şifresi</param>
        /// <param name="newPassword">kullanıcı yeni şifre</param>
        /// <returns>tagErrorCode</returns>
        public DevControl.tagErrorCode ModifyPassword(string oldPassword, string newPassword)
        {
            StringBuilder newPasswordBuf, passwordBuf;
            DevControl.tagErrorCode eCode;

            passwordBuf = new StringBuilder(oldPassword);
            newPasswordBuf = new StringBuilder(newPassword);
            eCode = DevControl.DM_ModifyPassword(this._devHandle, passwordBuf, newPasswordBuf, this.communicationTimeout);

            return eCode;
        }

        /// <summary>
        /// Cihazı yeniden başlat；
        /// </summary>
        /// <param name="rebootType">Geçerli parametrelerin kaydedilip kaydedilmeyeceği, varsayılan değerlerin geri yüklenip yüklenmeyeceği vb. gibi cihaz yeniden başlatma modunun türü.</param>
        /// <returns>tagErrorCode</returns>
        public DevControl.tagErrorCode RebootManage(RebootType rebootType)
        {
            DevControl.tagErrorCode eCode;

            switch (rebootType)
            {
                case RebootType.DefaultWithoutReboot:
                    eCode = DevControl.DM_LoadDefault(this._devHandle, this.communicationTimeout);
                    break;

                case RebootType.DefaultAndReboot:
                    eCode = DevControl.DM_LoadDefault(this._devHandle, this.communicationTimeout);
                    if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
                    {
                        eCode = DevControl.DM_ResetDevice(this._devHandle, this.communicationTimeout);
                    }
                    break;

                case RebootType.RebootWithoutSave:
                    eCode = DevControl.DM_ResetDeviceWithoutSave(this._devHandle, this.communicationTimeout);
                    break;

                case RebootType.SaveAndReboot:
                    eCode = DevControl.DM_ResetDevice(this._devHandle, this.communicationTimeout);
                    break;

                default:
                    eCode = DevControl.tagErrorCode.DM_ERR_ARG;
                    Debug.Fail("Not Support this RebootType!");
                    break;
            };

            return eCode;
        }

        /// <summary>
        /// Cihaz tarafından desteklenen seri bağlantı noktası sayısını öğrenin；
        /// </summary>
        /// <returns>Cihaz tarafından desteklenen seri bağlantı noktası sayısı</returns>
        public bool IsSupportChannel(int channelNum)
        {
            return DevControl.DM_IsComEnable(this._devHandle, channelNum);
        }
    }

    /// <summary>
    /// 重启方式类型；
    /// </summary>
    public enum RebootType
    {
        /// <summary>
        /// Yalnızca varsayılan parametreleri geri yükle，Cihazı yeniden başlatmayın；
        /// </summary>
        DefaultWithoutReboot,
        /// <summary>
        /// Cihazı yeniden başlat，ve varsayılan parametreleri geri yükleyin；
        /// </summary>
        DefaultAndReboot,
        /// <summary>
        /// Kaydedilmemiş parametreleri kaydetmeden cihazı yeniden başlatın;
        /// </summary>
        RebootWithoutSave,
        /// <summary>
        /// Cihazı yeniden başlatın ve parametreleri kaydedin;
        /// </summary>
        SaveAndReboot
    };
}
