using Microsoft.AspNetCore.Mvc;
using PushNotificationDemo.Abstract;

namespace PushNotificationDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FirebaseNotificationController : ControllerBase
    {
        private readonly ILogger<FirebaseNotificationController> _logger;

        private readonly ISendNotificationService _sendNotificationService;

        public FirebaseNotificationController(ILogger<FirebaseNotificationController> logger, ISendNotificationService sendNotificationService)
        {
            _logger = logger;
            _sendNotificationService = sendNotificationService;
        }

        /// <summary>
        /// This method is used to send notification to the client
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("[action]")]
        public async Task<IActionResult> SendNotificationSingleAsync()
        {
            try
            {
                var response = await _sendNotificationService.SendNotificationSingleAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error sending notification: " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// This method is used to send notification to the client multiple times
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        [HttpPost, Route("[action]")]
        public async Task<IActionResult> SendNotificationSingleLoopAsync(int length)
        {
            try
            {
                var response = await _sendNotificationService.SendNotificationSingleLoopAsync(length);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error sending notification: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
