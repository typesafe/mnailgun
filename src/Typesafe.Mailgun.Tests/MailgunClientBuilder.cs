namespace Typesafe.Mailgun.Tests
{
    public static class MailgunClientBuilder
    {
        public static MailgunClient GetClient(string domain = "www.keenprofile.com")
        {
            return new MailgunClient(domain, "key-06dfae80d176be561877d48dacc8b4d3", 3);
        }
    }
}