using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace PushNotificationDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly FirebaseMessaging _firebaseMessaging;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, FirebaseMessaging firebaseMessaging)
        {
            _logger = logger;
            _firebaseMessaging = firebaseMessaging;
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

        [HttpPost, Route("[action]")]
        public async Task<IActionResult> SendNotificationAsync()
        {
            // This registration token comes from the client FCM SDKs.
            var registrationToken = "fnfqJqzsTLyRtMqAY3U89i:APA91bEKZEJVlpEC8dYbv5i35oE7WFHtz_-RC4SW3I6tZYFmZqiPUc3PuBZSb7s9xHDaz_m7K686Kh9u0vo1R8Hb1mRg1V4bk7w57tPwcEibaZAE4Z2wpPU1wiHNbqAak-c6WxB88Xor";

            // See documentation on defining a message payload.

            int userId = 0;
            for (int i = 1; i <= 5; i++)
            {
                userId++;
                var message = new Message()
                {
                    Data = new Dictionary<string, string>()
                   {
                     { "userId", $"{userId}" },
                     { "time", $"{DateTime.Now}" },
                    },
                    Notification = new Notification()
                    {
                        Title = "TestNotification",
                        Body = $"Hello Malik! {userId}"
                    },
                    Token = registrationToken,
                };
                // Send a message to the device corresponding to the provided
                // registration token.
                string response = await _firebaseMessaging.SendAsync(message);
                Console.WriteLine("Successfully sent message: " + response);
                // Response is a message ID string.

            }

            return Ok();



        }
    }
}
