using Autofac;
using IMAOCMS.Core.Repositories;
using IMAOCMS.Core.Services;
using IMAOCMS.Core.UnitOfWorks;
using IMAOCRM.Repository;
using IMAOCRM.Repository.Repositories;
using IMAOCRM.Repository.UnitOfWorks;
using IMAOCRM.Service.Mapping;
using IMAOCRM.Service.Service;
using System.Reflection;

namespace IMAOCMS.API.Modules
{
    public class RepoServiceModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Services<>)).As(typeof(IServices<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssemmbly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssemmbly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssemmbly, serviceAssemmbly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, repoAssemmbly, serviceAssemmbly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

        }
    }
}
