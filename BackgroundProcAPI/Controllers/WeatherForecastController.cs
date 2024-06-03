using Domain.Handlers;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundProcAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        #region Props & vars

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IBackgroundProcService _backgroundProcService;

        #endregion Props & vars

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBackgroundProcService backgroundProcService)
        {
            _backgroundProcService = backgroundProcService;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetWeatherForecastBackground")]
        public WeatherForecastBackground GetBackgroundProc()
        {
            string idProc = Guid.NewGuid().ToString();

            Task<string> procTask = BackgroundExecution(idProc);

            var processing = new BackgroundProcHandler { Id = idProc, ProcTask = procTask };
            _backgroundProcService.AddProc(processing);

            var forecastResult = new WeatherForecastBackground
            {
                IdBackgroundProc = idProc,
                WeatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
            .ToArray()
            };

            return forecastResult;
        }

        [HttpGet("GetStatus/{idProc}")]
        public IActionResult GetStatus(string idProc)
        {
            if (_backgroundProcService.GetProc(idProc, out var processing))
            {
                if (processing.ProcTask.IsCompleted)
                {
                    return Ok("Processing completed successfully.");
                }
                else
                {
                    return Ok("Processing in progress...");
                }
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<string> BackgroundExecution(string idProc)
        {
            await Task.Delay(70000); // Simulate an execution of over 1 minute for background processing

            _logger.LogInformation("Background execution completed.");

            return idProc;
        }
    }
}
