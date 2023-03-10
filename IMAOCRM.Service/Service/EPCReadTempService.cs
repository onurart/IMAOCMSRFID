using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Repositories;
using IMAOCMS.Core.Services;
using IMAOCMS.Core.UnitOfWorks;
namespace IMAOCRM.Service.Service;
public class EPCReadTempService : Services<EPCReadTemp>, IEPCReadTempService
{
    public EPCReadTempService(IGenericRepository<EPCReadTemp> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }
}