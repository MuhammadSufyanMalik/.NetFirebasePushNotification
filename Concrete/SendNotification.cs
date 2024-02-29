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
                var registrationToken = "YOUR_REGISTRATION_TOKEN";
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
                var registrationToken = "YOUR_REGISTRATION_TOKEN";

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

        /// <summary>
        /// This method is used to send notification to multiple clients with multiple user registration tokens
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> SendNotificationMultipleAsync()
        {
            try
            {
                // Create a list containing up to 500 registration tokens.
                // These registration tokens come from the client FCM SDKs.
                var registrationTokens = new List<string>()
                    {
                                "YOUR_REGISTRATION_TOKEN_1",
                    // ...
                                "YOUR_REGISTRATION_TOKEN_n",
                    };
                var message = new MulticastMessage()
                {
                    Tokens = registrationTokens,
                    Data = new Dictionary<string, string>()
                    {
                    { "userId", $"userId" },
                     { "time", $"{DateTime.Now}" },
                     },
                    Notification = new Notification()
                    {
                        Title = "TestNotification",
                        Body = "Hello!"
                    },
                };
                var response = await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message);
                // See the BatchResponse reference documentation
                // for the contents of response.
                Console.WriteLine($"{response.SuccessCount} messages were sent successfully");
                if (response.FailureCount > 0)
                {
                    var failedTokens = new List<string>();
                    for (var i = 0; i < response.Responses.Count; i++)
                    {
                        if (!response.Responses[i].IsSuccess)
                        {
                            // The order of responses corresponds to the order of the registration tokens.
                            failedTokens.Add(registrationTokens[i]);
                        }
                    }

                    Console.WriteLine($"List of tokens that caused failures: {failedTokens}");
                    return failedTokens;
                }
                return new List<string>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error sending notification: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// This method is used to send notification to the clients using topic
        /// </summary>
        /// <returns></returns>
        public async Task SendNotificationUsingTopic()
        {
            try
            {
                // The topic name can be optionally prefixed with "/topics/".
                var topic = "highScores"; // "highScores" is the topic name change with your topic name
                var message = new Message()
                {
                    Data = new Dictionary<string, string>()
                    {
                    { "userId", $"userId" },
                     { "time", $"{DateTime.Now}" },
                     },
                    Notification = new Notification()
                    {
                        Title = "TestNotification",
                        Body = "Hello!"
                    },
                    Topic = topic,
                };
                // Send a message to the devices subscribed to the provided topic.
                string response = await _firebaseMessaging.SendAsync(message);
                // Response is a message ID string.
                Console.WriteLine("Successfully sent message: " + response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error sending notification: " + ex.Message);
                throw;
            }
        }
    }
}