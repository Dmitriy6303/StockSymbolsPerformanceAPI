using System;

namespace StockSymbolsPerformanceAPI.Infrastructure.Dto
{
    /// <summary>
    /// Dto for stock data 
    /// </summary>
    public class TimeSeriesDto
    {
        /// <summary>
        /// Datetime
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Open price 
        /// </summary>
        public double Open { get; set; }

        /// <summary>
        /// High price 
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// Low price 
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// Close price 
        /// </summary>
        public double Close { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        public long Volume { get; set; }
    }
}
