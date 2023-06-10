namespace PR_lab5.Domain
{
    public class EmailSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SendingHost { get; set; }
        public string RetrievingHost { get; set; }
        public string DisplayName { get; set; }
        public int SendingPort { get; set; }
        public int RetrievingPort { get; set; }
    }
}