namespace IMAOCMS.Core.Entites
{
    public class Com48 : BaseEntity
    {
        public int Num { get; set; }
        public int Num1 { get; set; }
        public int Num2 { get; set; }
        public int Num3 { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public int StopBits { get; set; }
        public int Parity { get; set; }
        public int OutCount { get; set; }
        public int DevAddr { get; set; }
        public int BytesToRead { get; set; }
        public int InCount { get; set; }
        public string Sources { get; set; }
        public string HexString { get; set; }
        public string Str1 { get; set; }
        public string Str2 { get; set; }
        public string PortName { get; set; }
        public double Laohuaintval { get; set; }
        public double Readinterval { get; set; }
        public double Xunhuaintval { get; set; }
        public bool IsBackground { get; set; }
        public bool IsEnabled { get; set; }
        public byte NumArray { get; set; }
        public byte ToHexByte { get; set; }
    }
}
