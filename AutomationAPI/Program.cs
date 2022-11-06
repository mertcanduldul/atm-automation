using Microsoft.EntityFrameworkCore;
using Automation.Core.Repository;
using Automation.Core.Service;
using Automation.Core.UnitOfWork;
using Automation.Repository.Repository;
using Automation.Repository.UnitOfWork;
using Automation.Service.Service;
using System.Reflection;
using Hangfire;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new() { Title = "AutomationAPI", Version = "v1" });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IMoneyRepository, MoneyRepository>();
builder.Services.AddScoped<IMoneyService, MoneyService>();

builder.Services.AddDbContext<Automation.Repository.Context.AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"),
        options => options.MigrationsAssembly(Assembly.GetAssembly(typeof(Automation.Repository.Context.AppDbContext)).GetName().Name));
});
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFireConnectionString")
    ));
builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.HeadContent = "<a href ='https://localhost:7240/hangfire'> Goto Hangfire Dashboard </a>");
    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        AppPath = "/swagger", //Back to site alaný içindir
        DashboardTitle = "ATM Automation Hangfire Dashboard" //Header içindir
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();