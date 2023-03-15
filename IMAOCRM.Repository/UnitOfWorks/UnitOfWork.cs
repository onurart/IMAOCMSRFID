using IMAOCMS.Core.UnitOfWorks;

namespace IMAOCRM.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Commit()
        {
            _appDbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            try
            {
await _appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

               // throw;
            }
            
        }
    }
}
