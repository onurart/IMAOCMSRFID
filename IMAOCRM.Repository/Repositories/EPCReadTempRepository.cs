using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Repositories;
namespace IMAOCRM.Repository.Repositories;
public class EPCReadTempRepository : GenericRepository<EPCReadTemp>, IEPCReadTempRepository
{
    public EPCReadTempRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}