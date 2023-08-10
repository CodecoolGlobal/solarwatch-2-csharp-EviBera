namespace SolarWatch2Async.Models
{
    public class City
    {
        public string CityName { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Country { get; set; }
        public string? State { get; set; }

        public SunsetSunriseData? SolarData { get; set; }

    }
}
