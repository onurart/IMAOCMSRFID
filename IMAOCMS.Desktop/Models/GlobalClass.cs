using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMAOCMS.Desktop.Models
{
    public class GlobalClass
    {
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if (hexString.Length % 2 != 0)
                hexString += " ";
            byte[] toHexByte = new byte[hexString.Length / 2];
            for (int index = 0; index < toHexByte.Length; ++index)
                toHexByte[index] = Convert.ToByte(hexString.Substring(index * 2, 2), 16);
            return toHexByte;
        }

        public static string byteToHexStr(byte[] bytes)
        {
            string hexStr = "";
            if (bytes != null)
            {
                for (int index = 0; index < bytes.Length; ++index)
                    hexStr += bytes[index].ToString("X2");
            }
            return hexStr;
        }

        public static string CRC16(string sources)
        {
            string str = "";
            try
            {
                byte[] toHexByte = GlobalClass.strToToHexByte(sources);
                if (toHexByte != null)
                {
                    int length = toHexByte.Length;
                    if (length > 0)
                    {
                        ushort num1 = ushort.MaxValue;
                        for (int index1 = 0; index1 < length; ++index1)
                        {
                            num1 ^= (ushort)toHexByte[index1];
                            for (int index2 = 0; index2 < 8; ++index2)
                                num1 = ((int)num1 & 1) != 0 ? (ushort)((int)num1 >> 1 ^ 40961) : (ushort)((uint)num1 >> 1);
                        }
                        byte num2 = (byte)(((int)num1 & 65280) >> 8);
                        str = ((byte)((uint)num1 & (uint)byte.MaxValue)).ToString("X2") + num2.ToString("X2");
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return str;
        }
    }
}
