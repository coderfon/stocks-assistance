using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using StocksAssistance.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace StocksAssistance.EF.Context
{
    public class StocksAssistanceContextFactory : IDesignTimeDbContextFactory<StocksAssistanceDbContext>
    {
        public StocksAssistanceDbContext CreateDbContext(string[] args)
        {
            IConfiguration settings = ConfigHelper.GetConfig();

            var optionsBuilder = new DbContextOptionsBuilder<StocksAssistanceDbContext>();
            optionsBuilder.UseLazyLoadingProxies()
                          .UseSqlServer(settings.GetConnectionString("StocksAssistanceApiDatabase"));

            return new StocksAssistanceDbContext(optionsBuilder.Options);
        }
    }
}
