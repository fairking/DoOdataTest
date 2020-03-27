using AutoMapper;
using DoOdataTest.Entities;
using DoOdataTest.ViewModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xtensive.Orm;

namespace DoOdataTest.Controllers
{
    [Route("/odata/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Session _session;
        private readonly IMapper _mapper;

        public WeatherForecastController(Session session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_mapper.ProjectTo<WeatherForecastVm>(_session.Query.All<WeatherForecast>()));
        }
    }
}
