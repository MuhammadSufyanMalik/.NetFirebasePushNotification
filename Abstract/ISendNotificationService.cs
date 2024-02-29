namespace PushNotificationDemo.Abstract
{
    public interface ISendNotificationService
    {
        /// <summary>
        /// This method is used to send notification to the client for a specific user registration token once
        /// </summary>
        /// <returns></returns>
        Task<string> SendNotificationSingleAsync();

        /// <summary>
        /// This method is used to send notification to the client for a specific user registration token multiple times same message
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<List<string>> SendNotificationSingleLoopAsync(int length);

        /// <summary>
        /// This method is used to send notification to multiple clients with multiple user registration tokens
        /// </summary>
        /// <returns></returns>
        Task<List<string>> SendNotificationMultipleAsync();

        /// <summary>
        /// This method is used to send notification to the clients using topic
        /// </summary>
        /// <returns></returns>
        Task SendNotificationUsingTopic();

    }
}