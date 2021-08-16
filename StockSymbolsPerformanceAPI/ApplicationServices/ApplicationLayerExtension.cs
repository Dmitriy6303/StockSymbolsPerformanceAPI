using Microsoft.Extensions.DependencyInjection;

namespace StockSymbolsPerformanceAPI.ApplicationServices
{
    /// <summary>
    /// Class for extending the capabilities of the standard collection of services
    /// </summary>
    public static class ApplicationLayerExtension
    {
        /// <summary>
        /// Adding Application Services to a Service Collection
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddApplicationLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<FinanceService>();
        }
    }
}
