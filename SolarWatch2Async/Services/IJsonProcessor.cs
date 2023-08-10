using SolarWatch2Async.Models;

namespace SolarWatch2Async.Services
{
    public interface IJsonProcessor
    {
        City ProcessJsonCityData(string cityData);
        SunsetSunriseData ProcessJsonSolarData(string solarData, DateOnly when);
    }
}
