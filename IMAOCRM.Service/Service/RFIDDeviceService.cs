using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Repositories;
using IMAOCMS.Core.Services;
using IMAOCMS.Core.UnitOfWorks;
namespace IMAOCRM.Service.Service;
public class RFIDDeviceService : Services<RFIDDevice>, IRFIDDeviceService
{
    public RFIDDeviceService(IGenericRepository<RFIDDevice> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }
}