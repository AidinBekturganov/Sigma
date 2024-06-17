using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Sigma.DAL.DbContext;

namespace Sigma.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void MainServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PgDbContext>(option => 
            option.UseNpgsql(builder.Configuration.GetSection("Pg").Value));
    }
}