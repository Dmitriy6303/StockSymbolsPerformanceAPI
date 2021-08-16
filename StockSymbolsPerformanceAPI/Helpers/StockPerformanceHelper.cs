using System.Collections.Generic;
using System.Linq;
using StockSymbolsPerformanceAPI.Infrastructure.Dto;
using StockSymbolsPerformanceAPI.Infrastructure.Enums;

namespace StockSymbolsPerformanceAPI.Helpers
{
    /// <summary>
    /// Helper for calculates performance
    /// </summary>
    public static class StockPerformanceHelper
    {
        /// <summary>
        /// Calculates performance comparison between stock symbols
        /// </summary>
        /// <param name="incomingSymbolTimeSeries">Incoming stock symbol</param>
        /// <param name="spySymbolTimeSeries">SPY stock symbol</param>
        /// <param name="filterType">Type of datetime filters</param>
        /// <returns></returns>
        public static StockPerformanceDto CalculatePerformanceComparison(
            List<TimeSeriesDto> incomingSymbolTimeSeries,
            List<TimeSeriesDto> spySymbolTimeSeries,
            FilterType filterType)
        {
            return new StockPerformanceDto
            {
                PerformanceOfIncomingSymbol = Calculate(incomingSymbolTimeSeries, filterType),
                PerformanceOfDefaultSymbol = Calculate(spySymbolTimeSeries, filterType)
            };
        }

        private static List<PerformanceDto> Calculate(List<TimeSeriesDto> listOfTimeSeries, FilterType filterType)
        {
            var result = new List<PerformanceDto>();

            if (filterType == FilterType.ByDay)
            {
                listOfTimeSeries = CalculateMaxClosePriceByDay(listOfTimeSeries);
            }

            if (!listOfTimeSeries.Any())
            {
                return result;
            }

            var sortedListOfTimeSeries = listOfTimeSeries.OrderBy(dto => dto.Time).ToList();
            var firstDayClosePrice = sortedListOfTimeSeries.First().Close;

            foreach (var timeSeries in sortedListOfTimeSeries)
            {
                var performance = (timeSeries.Close - firstDayClosePrice) / firstDayClosePrice * 100;
                var performanceDto = new PerformanceDto
                {
                    Performance = performance,
                    Price = timeSeries.Close,
                    Time = timeSeries.Time
                };
                result.Add(performanceDto);
            }

            return result;
        }

        private static List<TimeSeriesDto> CalculateMaxClosePriceByDay(List<TimeSeriesDto> listOfTimeSeries)
        {
            var listOfTimeSeriesByDay = new List<TimeSeriesDto>();
            var groupedListOfTimeSeries = listOfTimeSeries.GroupBy(dto => dto.Time.Date);
            foreach (var timeSeries in groupedListOfTimeSeries)
            {
                var maxClosePriceByDay = timeSeries.Max(dto => dto.Close);
                listOfTimeSeriesByDay.Add(new TimeSeriesDto
                {
                    Close = maxClosePriceByDay,
                    Time = timeSeries.Key
                });
            }

            return listOfTimeSeriesByDay;
        }
    }
}
