namespace Lab11.Application.Services // Ajusta el namespace según tu estructura
{
    public class NotificationService
    {
        public void SendNotification(string user)
        {
            Console.WriteLine($"Notificación enviada a {user} en {DateTime.Now}");
        }
    }
}