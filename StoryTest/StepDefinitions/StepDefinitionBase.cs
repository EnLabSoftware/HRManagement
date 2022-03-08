using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Business.Departments;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace P6.StoryTest.StepDefinitions
{
    public class StepDefinitionBase
    {
        public readonly ScenarioContext context;
        private readonly MyWebApplicationFactory<Program> webApplicationFactory;
        private string projectDir;
        private string configPath;
        public HttpClient client;
        IConfigurationRoot config;
        public StepDefinitionBase(
          ScenarioContext context,
          WebApplicationFactory<Program> webApplicationFactory)
        {
            // Inject auto test connection string to application under test
            projectDir = Directory.GetCurrentDirectory();
            configPath = Path.Combine(projectDir, "appsettings.test.json");
            config = new ConfigurationBuilder().AddJsonFile(configPath).Build();

            this.context = context;
            this.webApplicationFactory = new MyWebApplicationFactory<Program>(config);
            //this.webApplicationFactory = webApplicationFactory.WithWebHostBuilder(builder =>
            //{
            //    builder.ConfigureAppConfiguration((context, conf) =>
            //    {
            //        conf.AddJsonFile(configPath);
            //    });
            //});

            // create an http client of the application under test
            client = this.webApplicationFactory.CreateClient();

            // prepare an empty database for auto test
            using var provider = new ServiceCollection()
                .AddDbContext<Data.EF.EFContext>(options =>
                     options.UseSqlServer(config.GetConnectionString("DDDConnectionString")))
                .AddScoped<Data.EF.EFContext>()
                .BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Data.EF.EFContext>();
                db.Database.ExecuteSqlRaw("Drop Table IF Exists Payslips");
                db.Database.ExecuteSqlRaw("Drop Table IF Exists Users");
                db.Database.ExecuteSqlRaw("Drop Table IF Exists Departments");
                db.Database.ExecuteSqlRaw("Drop Table IF Exists __EFMigrationsHistory");
                db.Database.Migrate();
                DbSet<Department> _dbSet = db.Set<Department>();
                _dbSet.Add(new Department("IT", "Information Technology"));
                db.SaveChanges();
            }
        }
        public class MyWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
        {
            IConfigurationRoot config;
            public MyWebApplicationFactory(IConfigurationRoot config) : base()
            {
                this.config = config;
            }
            protected override IHost CreateHost(IHostBuilder builder)
            {
                builder.ConfigureHostConfiguration(config =>
                {
                    config.AddInMemoryCollection(new Dictionary<string, string> { 
                        { "ConnectionStrings:DDDConnectionString", this.config.GetConnectionString("DDDConnectionString") }
                    });
                });
                return base.CreateHost(builder);
            }
        }
    }
}
