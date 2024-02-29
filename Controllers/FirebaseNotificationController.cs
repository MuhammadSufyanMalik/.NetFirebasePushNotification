using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace PushNotificationDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FirebaseNotificationController : ControllerBase
    {

        private readonly FirebaseMessaging _firebaseMessaging;

        private readonly ILogger<FirebaseNotificationController> _logger;

        public FirebaseNotificationController(ILogger<FirebaseNotificationController> logger, FirebaseMessaging firebaseMessaging)
        {
            _logger = logger;
            _firebaseMessaging = firebaseMessaging;
        }

        /// <summary>
        /// This method is used to send notification to the client
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("[action]")]
        public async Task<IActionResult> SendNotificationAsync()
        {
            List<string> responseIdStrings = new();
            // This Device Registration Token registration token comes from the client.
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
                _logger.LogInformation("Successfully sent message: " + response);
                if (response != null)
                {
                    responseIdStrings.Add(response);
                }
            }

            return Ok(responseIdStrings);



        }
    }
}
