using System;

namespace StockSymbolsPerformanceAPI.Infrastructure.Dto
{
    /// <summary>
    /// Dto for Performance
    /// </summary>
    public class PerformanceDto
    {
        /// <summary>
        /// Datetime
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Performance 
        /// </summary>
        public double Performance { get; set; }

        /// <summary>
        /// Price 
        /// </summary>
        public double Price { get; set; }
    }
}
