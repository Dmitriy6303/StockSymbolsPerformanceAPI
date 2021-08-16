using AutoMapper;
using StockSymbolsPerformanceAPI.DAL.Entities;
using StockSymbolsPerformanceAPI.Infrastructure.Dto;

namespace StockSymbolsPerformanceAPI.AutoMapper
{
    /// <summary>
    /// Mapping dto
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <inheritdoc />
        public MappingProfile()
        {
            CreateMap<TimeSeriesDto, TimeSeries>()
                .ForMember(
                    dest => dest.Timestamp,
                    src => src.MapFrom(x => x.Time)).ReverseMap();
        }
    }
}
