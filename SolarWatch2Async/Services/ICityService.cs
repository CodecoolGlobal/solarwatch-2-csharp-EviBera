using SolarWatch2Async.Models;

namespace SolarWatch2Async.Services
{
    public interface ICityService
    {
        Task<string> GetCoordinatesAsync(string cityName);
        Task<string> GetSolarDataAsync(DateOnly when, City city);
        Task<City> GetCityAsync(string cityData, DateOnly when);
    }
}
