using System;
using System.ComponentModel.DataAnnotations.Schema;
using StockSymbolsPerformanceAPI.DAL.Entities.Common;

namespace StockSymbolsPerformanceAPI.DAL.Entities
{
    /// <summary>
    /// TimeSeries entity
    /// </summary>
    public class TimeSeries : BaseEntity
    {
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

        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Foreign key for Stock Symbol entity
        /// </summary>
        public long StockSymbolId { get; set; }

        /// <summary>
        /// Stock Symbol
        /// </summary>
        [ForeignKey(nameof(StockSymbolId))]
        public StockSymbol StockSymbol { get; set; }
    }
}
