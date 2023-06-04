using CurrencyExchange.Application.Interfaces;
using CurrencyExchange.Infrastructure.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDbConnection>(b =>
            {
                return new SqlConnection(configuration.GetConnectionString("dbConnection"));
            });

        }
    }
}
