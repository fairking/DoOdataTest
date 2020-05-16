using AutoMapper;
using DoOdataTest.Entities;
using System;

namespace DoOdataTest.ViewModels
{
    public class BaseWeatherForecastVm
    {
        public string Town { get; set; }
    }

    public class WeatherForecastVm : BaseWeatherForecastVm
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string Summary { get; set; }
    }

    public class WeatherForecastVmAutoMapperProfile : Profile
    {
        public WeatherForecastVmAutoMapperProfile()
        {
            CreateMap<WeatherForecast, WeatherForecastVm>()
                .ForMember(d => d.TemperatureF, map => map.MapFrom(s => 32 + (int)(s.TemperatureC / 0.5556)))
                .ForMember(d => d.Town, map => map.MapFrom(s => s.Town.Name));
        }
    }
}
