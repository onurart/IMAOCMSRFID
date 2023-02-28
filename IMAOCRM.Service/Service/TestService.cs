using IMAOCMS.Core.Entites;
using IMAOCMS.Core.Repositories;
using IMAOCMS.Core.Services;
using IMAOCMS.Core.UnitOfWorks;
using IMAOCRM.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMAOCRM.Service.Service
{
    public class TestService : Services<Test>, ITestService
    {
        public TestService(IGenericRepository<Test> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
