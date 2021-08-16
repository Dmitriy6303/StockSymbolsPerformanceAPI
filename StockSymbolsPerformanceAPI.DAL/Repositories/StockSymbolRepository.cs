using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockSymbolsPerformanceAPI.DAL.Abstractions;
using StockSymbolsPerformanceAPI.DAL.Entities;

namespace StockSymbolsPerformanceAPI.DAL.Repositories
{
    /// <summary>
    /// StockSymbolRepository
    /// </summary>
    public class StockSymbolRepository : IStockSymbolRepository
    {
        private readonly StockSymbolsDbContext _dbContext;

        /// <summary>
        /// Constructor for di
        /// </summary>
        /// <param name="dbContext">Database context</param>
        public StockSymbolRepository(StockSymbolsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<StockSymbol> GetById(long id)
        {
            return await _dbContext.StockSymbols.FirstOrDefaultAsync(symbol => symbol.Id == id);
        }

        /// <inheritdoc />
        public async Task<StockSymbol> GetByName(string name)
        {
            name = name.ToLower();
            return await _dbContext.StockSymbols
                .Include(symbol => symbol.TimeSeries)
                .FirstOrDefaultAsync(symbol => symbol.Name.ToLower() == name);
        }

        /// <inheritdoc />
        public async Task<bool> Create(StockSymbol symbol)
        {
            var existing = await GetByName(symbol.Name);
            if (existing != null)
            {
                return false;
            }

            await _dbContext.StockSymbols.AddAsync(symbol);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> Update(StockSymbol symbol)
        {
            var existing = await GetById(symbol.Id);
            if (existing == null)
            {
                return false;
            }

            existing.TimeSeries = symbol.TimeSeries;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
