using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Repositories;
namespace IMAOCRM.Repository.Repositories;
public class RFIDDeviceRepository : GenericRepository<RFIDDevice>, IRFIDDeviceRepository
{
    public RFIDDeviceRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}