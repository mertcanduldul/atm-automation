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
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using AutomationAPI.AuthBusiness;
using Microsoft.AspNetCore.Server.Kestrel;
using Automation.Repository.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using Hangfire.Dashboard;
using System.Net;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new() { Title = "AutomationAPI", Version = "v1" });
    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Basic"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IMoneyRepository, MoneyRepository>();
builder.Services.AddScoped<IMoneyService, MoneyService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<Automation.Repository.Context.AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"),
        options => options.MigrationsAssembly(Assembly.GetAssembly(typeof(Automation.Repository.Context.AppDbContext)).GetName().Name));
});
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFireConnectionString")
    ));
builder.Services.AddHangfireServer();



var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    AppDbContext context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{


    app.UseSwagger();
    app.UseSwaggerUI(c => c.HeadContent = "<a href ='https://localhost:7240/hangfire'> Goto Hangfire Dashboard </a>");
    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        AppPath = "/swagger", //Back to site alaný içindir
        DashboardTitle = "ATM Automation Hangfire Dashboard", //Header içindir
        Authorization = new[] { new HangfireAuthFilter() }
    });
}


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();