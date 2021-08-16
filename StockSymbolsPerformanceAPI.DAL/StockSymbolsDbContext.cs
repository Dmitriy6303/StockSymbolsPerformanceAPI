using Microsoft.EntityFrameworkCore;
using StockSymbolsPerformanceAPI.DAL.Entities;

namespace StockSymbolsPerformanceAPI.DAL
{
    /// <summary>
    /// Database context
    /// </summary>
    public class StockSymbolsDbContext : DbContext
    {
        /// <inheritdoc />
        public StockSymbolsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StockSymbol> StockSymbols { get; set; }

        public DbSet<TimeSeries> TimeSeries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockSymbol>().Property(entity => entity.Name).IsRequired().HasMaxLength(30);
        }
    }
}
