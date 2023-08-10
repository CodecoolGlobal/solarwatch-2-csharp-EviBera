using Microsoft.AspNetCore.Mvc;
using SolarWatch2Async.Controllers;
using SolarWatch2Async.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Xml.Linq;


namespace SolarWatch2Async.Services
{
    public class CityService : ICityService
    {
        private readonly ILogger<CityService> _logger;
        private readonly IJsonProcessor _jsonProcessor;

        public CityService(ILogger<CityService> logger, IJsonProcessor jsonProcessor)
        {
            _logger = logger;
            _jsonProcessor = jsonProcessor;
        }

        WebClient client = new WebClient();

        public string GetCoordinates(string cityName)
        {
            var apiKey = "5d6fd30487df4b2187e713818f4ea218";
            var urlCity = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=1&appid={apiKey}";
            _logger.LogInformation("Calling OpenWeather API with url: {url}", urlCity);
            var cityData = client.DownloadString(urlCity);

            if (cityData == "[]")
            {
                throw new Exception("City not found");
            }

            return cityData;
        }

        public string GetSolarData(DateOnly when, City aCity)
        {
            string day = $"{when.Year}-{when.Month}-{when.Day}";
            var urlSun = $"https://api.sunrise-sunset.org/json?lat={aCity.Lat}&lng={aCity.Lon}&date={day}";
            _logger.LogInformation("Calling OpenWeather API with url: {url}", urlSun);
            var solarData = client.DownloadString(urlSun);

            if (solarData == "[]")
            {
                throw new Exception("Solar data not found");
            }

            return solarData;
        }

        public City GetCity(string cityName, DateOnly day)
        {
            var cityData = GetCoordinates(cityName);
            var city = _jsonProcessor.ProcessJsonCityData(cityData);
            var solarData = GetSolarData(day, city);
            var sunsetSunriseData = _jsonProcessor.ProcessJsonSolarData(solarData, day);

            if (city.Country == "HU")
            {
                SunsetSunriseData localSolarData = new SunsetSunriseData()
                {
                    Date = day,
                    Sunrise = sunsetSunriseData.Sunrise.AddHours(2),
                    Sunset = sunsetSunriseData.Sunset.AddHours(2),
                };

                city.SolarData = localSolarData;
            }
            else
            {
                city.SolarData = sunsetSunriseData;
            }

            return city;
        }
    }
}
