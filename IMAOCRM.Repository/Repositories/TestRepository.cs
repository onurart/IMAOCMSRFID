using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Repositories;

namespace IMAOCRM.Repository.Repositories
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        public TestRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
