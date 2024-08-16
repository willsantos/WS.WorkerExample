namespace WS.WorkerExample.Application.Interfaces
{
    public interface IEmail
    {
        Task SendEmailAsync(string to, string subject, string message);
    }
}