using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Repositories;
namespace IMAOCRM.Repository.Repositories
{
    public class EpcReadDataRepository : GenericRepository<EpcReadData>, IEpcReadDataRepository
    {
        public EpcReadDataRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}