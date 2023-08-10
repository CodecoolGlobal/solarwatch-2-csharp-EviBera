using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarWatch2Async.Models;
using SolarWatch2Async.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace SolarWatch2Async.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityService _cityService;


        public CityController(ILogger<CityController> logger, ICityService cityService)
        {
            _logger = logger;
            _cityService = cityService;
        }

        [HttpGet("GetAsync")]
        public async Task<City> GetAsync(string cityName, [Required] DateOnly day)
        {
            _logger.LogInformation("Long running process started");
            await Task.Delay(30000);
            
            var city = _cityService.GetCity(cityName, day);
            return city;

        }

    }
}
