using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockSymbolsPerformanceAPI.ApplicationServices;
using StockSymbolsPerformanceAPI.Infrastructure.Dto;
using StockSymbolsPerformanceAPI.Infrastructure.Enums;

namespace StockSymbolsPerformanceAPI.Controllers
{
    /// <summary>
    /// Controller for working with stock symbols data
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StockSymbolsController : ControllerBase
    {
        private readonly FinanceService _financeService;

        /// <summary>
        /// Constructor
        /// </summary>
        public StockSymbolsController(FinanceService financeService)
        {
            _financeService = financeService;
        }

        /// <summary>
        /// Download and save to database SPDR® S&P 500 (SPY) symbol historical data for last month with 60min interval
        /// </summary>
        [HttpPost("spy/update")]
        public async Task<IActionResult> UpdateDefaultSymbolData()
        {
            await _financeService.UpdateSymbolData();
            return Ok();
        }

        /// <summary>
        /// Calculates performance comparison for last available week based on external API and local database data
        /// </summary>
        /// <param name="symbol">Incoming symbol</param>
        /// <returns><see cref="StockPerformanceDto"/>JSON</returns>
        [HttpGet("{symbol}/perfCompByDay")]
        public async Task<ActionResult<StockPerformanceDto>> GetPerformanceComparisonByDay(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                return BadRequest("Symbol parameter is required");
            }

            var result = await _financeService.CalculateStockPerformance(symbol, FilterType.ByDay);
            return Ok(result);
        }

        /// <summary>
        /// Calculates performance comparison for last available day based on external API and local database data
        /// </summary>
        /// <param name="symbol">Incoming symbol</param>
        /// <returns><see cref="StockPerformanceDto"/>JSON</returns>
        [HttpGet("{symbol}/perfCompByHour")]
        public async Task<ActionResult<StockPerformanceDto>> GetPerformanceComparisonByHour(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                return BadRequest("Symbol parameter is required");
            }

            var result = await _financeService.CalculateStockPerformance(symbol, FilterType.ByHour);
            return Ok(result);
        }
    }
}
