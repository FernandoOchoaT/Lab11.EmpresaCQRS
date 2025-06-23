using Hangfire;
using Lab11.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly NotificationService _notificationService;

        public NotificationController(IBackgroundJobClient backgroundJobClient, NotificationService notificationService)
        {
            _backgroundJobClient = backgroundJobClient;
            _notificationService = notificationService;
        }

        [HttpPost("delayed/{user}")]
        public IActionResult EnviarNotificacionConRetraso(string user)
        {
            BackgroundJob.Schedule<NotificationService>(
                service => service.SendNotification(user),
                TimeSpan.FromMinutes(10));

            return Ok($"Notificación programada para {user} en 10 minutos");
        }
    }
}