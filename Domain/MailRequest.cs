namespace PR_lab5.Domain
{
    public class MailRequest
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile>? Files { get; set; }

        public MailRequest()
        {
            ToEmails = new List<string>();
            Files = new List<IFormFile>();
        }
    }
}