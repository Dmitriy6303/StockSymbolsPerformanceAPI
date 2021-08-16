using System.ComponentModel.DataAnnotations;

namespace StockSymbolsPerformanceAPI.DAL.Entities.Common
{
    /// <summary>
    /// Common entity
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Private key
        /// </summary>
        [Key] public long Id { get; set; }
    }
}
