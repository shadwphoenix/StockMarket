using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Data
{
    internal class StockMarketDbContextFactory : IDesignTimeDbContextFactory<StockMarketDbContext>
    {
        public StockMarketDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StockMarketDbContext>();
            optionsBuilder.UseSqlServer("server=.\\sqlexpress;database=StockMarket;MultipleActiveResultSets=true;trusted_connection=true;encrypt=yes;trustservercertificate=yes;");
            return new StockMarketDbContext(optionsBuilder.Options);
        }
    }
}
