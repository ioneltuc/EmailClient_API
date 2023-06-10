namespace PR_lab5.Domain
{
    public class MailInfo
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Files { get; set; }

        public MailInfo()
        {
            Files = new List<string>();
        }
    }
}