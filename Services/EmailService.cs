using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PR_lab5.Domain;

namespace PR_lab5.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.Email);
            foreach (string toEmail in mailRequest.ToEmails)
            {
                email.To.Add(MailboxAddress.Parse(toEmail));
            }
            email.Subject = mailRequest.Subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = mailRequest.Body;
            foreach (IFormFile attachment in mailRequest.Files)
            {
                bodyBuilder.Attachments.Add(attachment.FileName, attachment.OpenReadStream());
            }
            email.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_emailSettings.SendingHost, _emailSettings.SendingPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
        }

        public async Task<List<MailInfo>> GetAllEmailsAsync()
        {
            var allEmails = new List<MailInfo>();

            using (var imap = new ImapClient())
            {
                imap.Connect(_emailSettings.RetrievingHost, _emailSettings.RetrievingPort, true);
                imap.Authenticate(_emailSettings.Email, _emailSettings.Password);

                IMailFolder inbox = imap.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadOnly);

                foreach (var email in inbox)
                {
                    var mailInfo = new MailInfo();
                    mailInfo.From = email.From.ToString();
                    mailInfo.Subject = email.Subject;
                    mailInfo.Body = email.TextBody;
                    foreach (MimeEntity attachment in email.Attachments)
                    {
                        mailInfo.Files.Add(attachment.ToString());
                    }
                    allEmails.Add(mailInfo);
                }

                imap.Disconnect(true);
            }

            allEmails.Reverse();
            return allEmails;
        }
    }
}