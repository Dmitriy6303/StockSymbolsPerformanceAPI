using System.Threading.Tasks;
using StockSymbolsPerformanceAPI.DAL.Entities;

namespace StockSymbolsPerformanceAPI.DAL.Abstractions
{
    /// <summary>
    /// StockSymbolRepository repository interface
    /// </summary>
    public interface IStockSymbolRepository
    {
        /// <summary>
        /// Get StockSymbol by identifier
        /// </summary>
        /// <param name="id">StockSymbol identifier</param>
        /// <returns><see cref="StockSymbol"/>Object</returns>
        Task<StockSymbol> GetById(long id);

        /// <summary>
        /// Get StockSymbol by his name
        /// </summary>
        /// <param name="name">symbol name</param>
        /// <returns><see cref="StockSymbol"/>Object</returns>
        Task<StockSymbol> GetByName(string name);

        /// <summary>
        /// Create StockSymbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>True of false</returns>
        Task<bool> Create(StockSymbol symbol);

        /// <summary>
        /// Update StockSymbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>True of false</returns>
        Task<bool> Update(StockSymbol symbol);
    }
}
