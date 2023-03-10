namespace IMAOCMS.Core.Entites;
public class EpcReadData:BaseEntity
{
    public int Count { get; set; }
    public string Epc { get; set; }
    public int Rssi { get; set; }
    public byte Ant { get; set; }
    public DateTime EpcDate { get; set; }
}