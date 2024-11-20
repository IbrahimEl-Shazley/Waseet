namespace Wasit.Services.Interfaces.Dashboard
{
    public interface IDNotificationsService
    {
        Task<(bool isSuccess, string message)> Send(string message, string userType);

    }
}
