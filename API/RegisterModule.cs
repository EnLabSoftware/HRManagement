using Autofac;
using Microsoft.EntityFrameworkCore;
using Data.EF.Interfaces;
using Data.EF.Repositories;
using Data.EF;
using Service.Users;

namespace API
{
    public class RegisterModule : Module
    {
        ConfigurationManager _conf;
        protected override void Load(ContainerBuilder builder)
        {
            string dbconstr = _conf.GetConnectionString("DDDConnectionString");

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IAsyncRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.Register(c => {
                var options = new DbContextOptionsBuilder<EFContext>();
                options.UseLoggerFactory(c.Resolve<ILoggerFactory>()).EnableSensitiveDataLogging();
                options.UseSqlServer(dbconstr, b => b.MigrationsAssembly("P3.Data"));
                return options.Options;
            }).InstancePerLifetimeScope();
            builder.RegisterType<EFContext>()
                  .AsSelf()
                  .InstancePerLifetimeScope();
            builder.RegisterType<UserService>().AsSelf();
        }

        public RegisterModule(ConfigurationManager conf)
        {
            this._conf = conf;
        }
    }

    // Other Lifetime
    //    .InstancePerDependency();
    //    .InstancePerLifetimeScope(); = DI scoped
    //    .SingleInstance();
    //builder.RegisterAssemblyModules(typeof(ServerSettings).Assembly);

}
