using Microsoft.AspNetCore.Mvc;
using PR_lab5.Domain;
using PR_lab5.Services;

namespace PR_lab5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public MailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromForm] MailRequestDto mailRequestDto)
        {
            var mailRequest = new MailRequest();
            mailRequest.ToEmails.AddRange(mailRequestDto.ToEmails);
            mailRequest.Subject = mailRequestDto.Subject;
            mailRequest.Body = mailRequestDto.Body;
            mailRequest.Files.AddRange(mailRequestDto.Files);

            try
            {
                await _emailService.SendEmailAsync(mailRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }

        [HttpGet]
        public async Task<List<MailInfo>> GetAllEmails()
        {
            try
            {
                return await _emailService.GetAllEmailsAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }
        }
    }
}