using Microsoft.EntityFrameworkCore;
using PrimeiroProjeto.Model.Context;

namespace PrimeiroProjeto.Configurations
{
    public static class DatabaseConfig
    {
        public static IServiceCollection 
            AddDatabaseConfiguration
            (this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration
                ["MSSQLServerSQLConnection:MSSQLServerSQLConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException
                    ("Connection string MSSQLServerSQLConnection:MSSQLServerSQLConnectionString Not Found");
            }
            services.AddDbContext<MSSQLContext>(
                options => options
                .UseSqlServer(connectionString));
            return services;
        }
    }
}
