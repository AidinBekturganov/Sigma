using System.Text.Json.Serialization;
using FluentValidation;
using Sigma.BLL.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sigma.BLL.Interfaces;
using Sigma.BLL.Services;
using Sigma.DAL.DbContext;
using Sigma.DAL.Interfaces;
using Sigma.DAL.Repositories;
using Sigma.Domain.Dto.ClientServices;
using Sigma.Domain.Entity;

namespace Sigma.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void MainServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PgDbContext>(option => 
            option.UseNpgsql(builder.Configuration.GetSection("Pg").Value));
    }
    
    public static void UseRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRepository<Client>, Repository<Client>>();
    }
    
    public static void UseServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IClientService, ClientService>();
    }
    
    public static void UseAutoMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(MappingProfile));
    }
}