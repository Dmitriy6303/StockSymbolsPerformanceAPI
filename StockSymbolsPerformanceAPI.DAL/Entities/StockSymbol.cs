using System.Collections.Generic;
using StockSymbolsPerformanceAPI.DAL.Entities.Common;

namespace StockSymbolsPerformanceAPI.DAL.Entities
{
    /// <summary>
    /// StockSymbol entity
    /// </summary>
    public class StockSymbol : BaseEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">StockSymbol name</param>
        public StockSymbol(string name)
        {
            Name = name;
            TimeSeries = new List<TimeSeries>();
        }

        /// <summary>
        /// StockSymbol Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// TimeSeries of stockSymbol 
        /// </summary>
        public ICollection<TimeSeries> TimeSeries { get; set; }
    }
}
