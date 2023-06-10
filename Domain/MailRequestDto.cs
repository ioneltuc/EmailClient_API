namespace PR_lab5.Domain
{
    public class MailRequestDto
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile>? Files { get; set; }

        public MailRequestDto()
        {
            ToEmails = new List<string>();
            Files = new List<IFormFile>();
        }
    }
}