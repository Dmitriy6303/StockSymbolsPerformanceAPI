using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StockSymbolsPerformanceAPI.DAL.Abstractions;
using StockSymbolsPerformanceAPI.DAL.Entities;
using StockSymbolsPerformanceAPI.Helpers;
using StockSymbolsPerformanceAPI.Infrastructure.Dto;
using StockSymbolsPerformanceAPI.Infrastructure.Enums;

namespace StockSymbolsPerformanceAPI.ApplicationServices
{
    /// <summary>
    /// Service for working with stock data
    /// </summary>
    public class FinanceService
    {
        private const string ByHourInterval = "60min";

        private readonly ILogger<FinanceService> _logger;
        private readonly IMapper _mapper;
        private readonly AlphaVantageSettings _alphaVantageSettings;
        private readonly IStockSymbolRepository _stockSymbolRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="alphaVantageSettings"></param>
        /// <param name="stockSymbolRepository"></param>
        /// <param name="mapper"></param>
        public FinanceService(
            ILogger<FinanceService> logger,
            IOptions<AlphaVantageSettings> alphaVantageSettings,
            IStockSymbolRepository stockSymbolRepository,
            IMapper mapper)
        {
            _logger = logger;
            _stockSymbolRepository = stockSymbolRepository;
            _mapper = mapper;
            _alphaVantageSettings = alphaVantageSettings.Value;
        }

        /// <summary>
        /// Create or update symbol in database, downloading list of prices data to database
        /// </summary>
        /// <param name="symbol">Stock symbol, if not set then uses DefaultEtfSymbolName</param>
        /// <returns></returns>
        public async Task UpdateSymbolData(string symbol = null)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                symbol = _alphaVantageSettings.DefaultEtfSymbolName;
            }

            var queryUrl = _alphaVantageSettings.ApiUrl +
                           $"query?function=TIME_SERIES_INTRADAY_EXTENDED&symbol={symbol}&interval={ByHourInterval}&slice=year1month1&apikey={_alphaVantageSettings.ApiKey}";
            var queryUri = new Uri(queryUrl);

            using var client = new WebClient();
            var downloadString = client.DownloadString(queryUri);

            using var reader = new StringReader(downloadString);
            using var csv = new CsvReader(reader);
            csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;
            csv.Configuration.IsHeaderCaseSensitive = false;

            var listOfTimeSeries = csv.GetRecords<TimeSeriesDto>().ToList();

            var isExistSymbol = true;
            var stockSymbol = await _stockSymbolRepository.GetByName(symbol);
            if (stockSymbol == null)
            {
                stockSymbol = new StockSymbol(symbol);
                isExistSymbol = false;
            }

            foreach (var timeSeries in listOfTimeSeries)
            {
                var mappedTimeSeries = _mapper.Map<TimeSeries>(timeSeries);
                mappedTimeSeries.StockSymbol = stockSymbol;

                var isDataExist = stockSymbol.TimeSeries.Any(series => series.Timestamp == mappedTimeSeries.Timestamp);
                if (!isDataExist)
                {
                    stockSymbol.TimeSeries.Add(mappedTimeSeries);
                }
            }

            if (isExistSymbol)
            {
                await _stockSymbolRepository.Update(stockSymbol);
            }
            else
            {
                await _stockSymbolRepository.Create(stockSymbol);
            }
        }

        /// <summary>
        /// Calculates performance comparison between stock symbols
        /// </summary>
        /// <param name="symbol">Incoming stock symbol</param>
        /// <param name="filterType">Type of datetime filters</param>
        /// <returns><see cref="StockPerformanceDto"/>Object</returns>
        public async Task<StockPerformanceDto> CalculateStockPerformance(string symbol, FilterType filterType)
        {
            var spySymbol = await _stockSymbolRepository.GetByName(_alphaVantageSettings.DefaultEtfSymbolName);
            if (spySymbol == null)
            {
                await UpdateSymbolData();
                spySymbol = await _stockSymbolRepository.GetByName(_alphaVantageSettings.DefaultEtfSymbolName);
            }

            await UpdateSymbolData(symbol);

            var incomingSymbol = await _stockSymbolRepository.GetByName(symbol);

            List<TimeSeries> listOfSpyTimeSeries;
            List<TimeSeries> listOfIncomingTimeSeries;
            switch (filterType)
            {
                case FilterType.ByDay:
                {
                    listOfSpyTimeSeries = spySymbol.TimeSeries.Where(series =>
                        series.Timestamp <= DateTime.UtcNow &&
                        series.Timestamp >= DateTime.UtcNow.Subtract(TimeSpan.FromDays(7))
                    ).ToList();

                    listOfIncomingTimeSeries = incomingSymbol.TimeSeries.Where(series =>
                        series.Timestamp <= DateTime.UtcNow &&
                        series.Timestamp >= DateTime.UtcNow.Subtract(TimeSpan.FromDays(7))
                    ).ToList();

                    break;
                }
                case FilterType.ByHour:
                {
                    var lastTradingDay = spySymbol.TimeSeries.Max(series => series.Timestamp);

                    listOfSpyTimeSeries = spySymbol.TimeSeries.Where(series =>
                        series.Timestamp.Year == lastTradingDay.Year &&
                        series.Timestamp.Month == lastTradingDay.Month &&
                        series.Timestamp.Day == lastTradingDay.Day
                    ).ToList();

                    listOfIncomingTimeSeries = incomingSymbol.TimeSeries.Where(series =>
                        series.Timestamp.Year == lastTradingDay.Year &&
                        series.Timestamp.Month == lastTradingDay.Month &&
                        series.Timestamp.Day == lastTradingDay.Day
                    ).ToList();

                    break;
                }
                default:
                    _logger.LogError(new ArgumentOutOfRangeException(nameof(filterType), filterType, null), "Filter type error");
                    return null;
            }

            var performanceComparison = StockPerformanceHelper.CalculatePerformanceComparison(
                _mapper.Map<List<TimeSeriesDto>>(listOfIncomingTimeSeries),
                _mapper.Map<List<TimeSeriesDto>>(listOfSpyTimeSeries),
                filterType
            );

            return performanceComparison;
        }
    }
}
