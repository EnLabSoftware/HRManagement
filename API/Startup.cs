using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Business.Users;
using Business.Departments;
using Data.EF.Interfaces;
using Data.EF.Repositories;
using Data.EF;
using Service.Users;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Application services
            //    .AddDatabase(Configuration)
            // var temp = Configuration.GetConnectionString("DDDConnectionString");
            services.AddDbContext<EFContext>(options =>
                     options
//                     .UseLazyLoadingProxies()
                     .UseSqlServer(Configuration.GetConnectionString("DDDConnectionString"), b => b.MigrationsAssembly("P3.Data")));
            //    .AddUnitOfWork()
            services
                .AddScoped<IUnitOfWork, UnitOfWork>();
            //    .AddRepositories()
            services
                .AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IDepartmentRepository, DepartmentRepository>();
            //    .AddBusinessServices();
            services
                .AddScoped<UserService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}