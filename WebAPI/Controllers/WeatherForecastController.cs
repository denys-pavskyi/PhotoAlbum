using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly InternetPhotoAlbumDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, InternetPhotoAlbumDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            /*UnitOfWork uow = new UnitOfWork(_context);
            await uow.AlbumPhotoRepository.AddAsync(new AlbumPhoto { Id = 22, PhotoId = 1, AlbumId = 1, AdditionDate = new DateTime(2022, 12, 3) });
            await uow.SaveAsync();
            var tmp1 = (await uow.AlbumPhotoRepository.GetAllAsync()).ToList();
            int tmp = tmp1.FirstOrDefault().Id;
            */
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                //TemperatureC = tmp,
                TemperatureC = 1,
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}