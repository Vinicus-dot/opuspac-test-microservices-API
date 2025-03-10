using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MicroServiceContextFactory : IDesignTimeDbContextFactory<MicroServiceContext>
    {
        public MicroServiceContext CreateDbContext(string[] args)
        {
            // Carrega a configuração do appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MicroServiceContext>();
            var connectionString = config.GetConnectionString("DEFAULT_CONNECTION");

            optionsBuilder.UseNpgsql(connectionString); // Altere para UseSqlServer(connectionString) se for SQL Server

            return new MicroServiceContext(optionsBuilder.Options);
        }
    }
}
