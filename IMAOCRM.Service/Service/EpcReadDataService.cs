using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Repositories;
using IMAOCMS.Core.Services;
using IMAOCMS.Core.UnitOfWorks;
namespace IMAOCRM.Service.Service;
public class EpcReadDataService : Services<EpcReadData>, IEpcReadDataService
{
    public EpcReadDataService(IGenericRepository<EpcReadData> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }
}