using SolarWatch2Async.Models;

namespace SolarWatch2Async.Services
{
    public interface ICityService
    {
        string GetCoordinates(string cityName);
        string GetSolarData(DateOnly when, City city);
        City GetCity(string cityData, DateOnly when);
    }
}
