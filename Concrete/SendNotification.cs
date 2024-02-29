using FirebaseAdmin.Messaging;
using PushNotificationDemo.Abstract;

namespace PushNotificationDemo.Concrete
{
    public class SendNotification : ISendNotificationService
    {
        private readonly FirebaseMessaging _firebaseMessaging;
        private readonly ILogger<SendNotification> _logger;

        public SendNotification(FirebaseMessaging firebaseMessaging, ILogger<SendNotification> logger)
        {
            _firebaseMessaging = firebaseMessaging;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to send notification to the client for a specific user registration token once
        /// </summary>
        /// <returns></returns>
        public async Task<string> SendNotificationSingleAsync()
        {
            try
            {
                // This Device Registration Token registration token comes from the client.
                var registrationToken = "fnfqJqzsTLyRtMqAY3U89i:APA91bEKZEJVlpEC8dYbv5i35oE7WFHtz_-RC4SW3I6tZYFmZqiPUc3PuBZSb7s9xHDaz_m7K686Kh9u0vo1R8Hb1mRg1V4bk7w57tPwcEibaZAE4Z2wpPU1wiHNbqAak-c6WxB88Xor";
                int userId = 1;
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
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error sending notification: " + ex.Message);

                throw;
            }
        }

        public async Task<List<string>> SendNotificationSingleLoopAsync(int length)
        {
            try
            {
                List<string> responseIdStrings = new();
                // This Device Registration Token registration token comes from the client.
                var registrationToken = "fnfqJqzsTLyRtMqAY3U89i:APA91bEKZEJVlpEC8dYbv5i35oE7WFHtz_-RC4SW3I6tZYFmZqiPUc3PuBZSb7s9xHDaz_m7K686Kh9u0vo1R8Hb1mRg1V4bk7w57tPwcEibaZAE4Z2wpPU1wiHNbqAak-c6WxB88Xor";

                // See documentation on defining a message payload.

                int userId = 0;
                for (int i = 1; i <= length; i++)
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
                return responseIdStrings;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error sending notification: " + ex.Message);
                throw;
            }
        }

    }
}