using Microsoft.EntityFrameworkCore;
using Data.EF.Interfaces;
using Data.EF.Repositories;
using Data.EF;
using Service.Users;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var temp = builder.Configuration.GetConnectionString("DDDConnectionString");
builder.Services.AddDbContext<EFContext>(options =>
         options
         //                     .UseLazyLoadingProxies()
         .UseSqlServer(builder.Configuration.GetConnectionString("DDDConnectionString"), b => b.MigrationsAssembly("P3.Data")));

builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services
    .AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services
    .AddScoped<UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }