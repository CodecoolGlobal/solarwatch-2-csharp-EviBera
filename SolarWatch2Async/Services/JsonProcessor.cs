﻿using SolarWatch2Async.Models;
using System.Text.Json;

namespace SolarWatch2Async.Services
{
    public class JsonProcessor : IJsonProcessor
    {
        public City ProcessJsonCityData(string cityData)
        {
            JsonDocument json = JsonDocument.Parse(cityData);

            var jsonElement = json.RootElement.EnumerateArray().ElementAt(0);

            JsonElement name = jsonElement.GetProperty("name");
            JsonElement lat = jsonElement.GetProperty("lat");
            JsonElement lon = jsonElement.GetProperty("lon");
            JsonElement country = jsonElement.GetProperty("country");
            JsonElement state;
            jsonElement.TryGetProperty("state", out state);

            City city = new City
            {
                CityName = name.ToString(),
                Lat = lat.GetDouble(),
                Lon = lon.GetDouble(),
                Country = country.ToString(),
                State = state.ToString()
            };

            return city;
        }

        public SunsetSunriseData ProcessJsonSolarData(string solarData, DateOnly when)
        {
            JsonDocument json = JsonDocument.Parse(solarData);

            string sunrise = json.RootElement.GetProperty("results").GetProperty("sunrise").ToString();
            string sunset = json.RootElement.GetProperty("results").GetProperty("sunset").ToString();

            SunsetSunriseData sunsetSunriseData = new SunsetSunriseData
            {
                Date = when,
                Sunrise = TimeOnly.Parse(sunrise),
                Sunset = TimeOnly.Parse(sunset)
            };

            return sunsetSunriseData;
        }

    }
}
