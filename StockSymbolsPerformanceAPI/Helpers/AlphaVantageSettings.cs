namespace StockSymbolsPerformanceAPI.Helpers
{
    /// <summary>
    /// Settings for AV service
    /// </summary>
    public class AlphaVantageSettings
    {
        /// <summary>
        /// Key for AV API, 5 API requests per minute and 500 requests per day
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Url of AV API
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// SPDR® S&P 500
        /// </summary>
        public string DefaultEtfSymbolName { get; set; }
    }
}
