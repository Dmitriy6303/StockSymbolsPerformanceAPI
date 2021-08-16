using Microsoft.Extensions.DependencyInjection;
using StockSymbolsPerformanceAPI.DAL.Abstractions;
using StockSymbolsPerformanceAPI.DAL.Repositories;

namespace StockSymbolsPerformanceAPI.DAL
{
    /// <summary>
    /// Extensions for adding repositories to the project
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adding repositories to the project
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddDataAccessLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IStockSymbolRepository, StockSymbolRepository>();
        }
    }
}
