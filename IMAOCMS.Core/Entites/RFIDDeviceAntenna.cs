using System.ComponentModel.DataAnnotations.Schema;
namespace IMAOCMS.Core.Entites;
public class RFIDDeviceAntenna:BaseEntity
{
    [ForeignKey(nameof(RFIDDeviceId))]
    public int RFIDDeviceId { get; set; }
    public virtual RFIDDevice? Fiche { get; set; }
    public int AntennaPower { get; set; }
    public int Antenna { get; set; }
}