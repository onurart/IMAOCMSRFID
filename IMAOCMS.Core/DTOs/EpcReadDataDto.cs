namespace IMAOCMS.Core.DTOs;
public class EpcReadDataDto
{
    public int Id { get; set; }
    public int Count { get; set; }
    public string Epc { get; set; }
    public int Rssi { get; set; }
    public byte Ant { get; set; }
    public DateTime EpcDate { get; set; }
    public DateTime CreatedDate { get; set; }
}