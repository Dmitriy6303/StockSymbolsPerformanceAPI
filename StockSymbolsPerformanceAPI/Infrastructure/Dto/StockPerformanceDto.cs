using System.Collections.Generic;

namespace StockSymbolsPerformanceAPI.Infrastructure.Dto
{
    /// <summary>
    /// Dto for view performance of stock symbols
    /// </summary>
    public class StockPerformanceDto
    {
        /// <summary>
        /// Performance for symbol that the user enters on UI
        /// </summary>
        public List<PerformanceDto> PerformanceOfIncomingSymbol { get; set; }

        /// <summary>
        /// Performance for SPY symbol, which is hard set in the application settings
        /// </summary>
        public List<PerformanceDto> PerformanceOfDefaultSymbol { get; set; }
    }
}
