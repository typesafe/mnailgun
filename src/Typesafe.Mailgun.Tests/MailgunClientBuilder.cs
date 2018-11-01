namespace Typesafe.Mailgun.Tests
{
    public static class MailgunClientBuilder
    {
        public static MailgunClient GetClient(string domain = "yourdomain")
        {
            return new MailgunClient(domain, "key-withyourapikey", 3);
        }
    }
}