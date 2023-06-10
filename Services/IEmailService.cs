using PR_lab5.Domain;

namespace PR_lab5.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

        Task<List<MailInfo>> GetAllEmailsAsync();
    }
}