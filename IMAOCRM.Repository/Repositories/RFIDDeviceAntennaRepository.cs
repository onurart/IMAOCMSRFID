using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Repositories;
namespace IMAOCRM.Repository.Repositories;
public class RFIDDeviceAntennaRepository : GenericRepository<RFIDDeviceAntenna>, IRFIDDeviceAntennaRepository
{
    public RFIDDeviceAntennaRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}