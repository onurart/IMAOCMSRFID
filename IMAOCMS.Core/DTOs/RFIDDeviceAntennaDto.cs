namespace IMAOCMS.Core.DTOs;
public class RFIDDeviceAntennaDto
{
    public int RFIDDeviceId { get; set; }
    public int Antenna { get; set; }
    public int AntennaPower { get; set; }
    public bool IsActive { get; set; } = true;
}