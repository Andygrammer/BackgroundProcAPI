namespace Domain.Models
{
    public class WeatherForecastBackground
    {
        public string? IdBackgroundProc { get; set; }

        public IEnumerable<WeatherForecast>? WeatherForecasts { get; set; }
    }
}
