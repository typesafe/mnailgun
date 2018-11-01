namespace Typesafe.Mailgun.Tests
{
	public static class MailgunClientBuilder
	{
		// TODO: put your domain and API key here before running tests
		public static MailgunClient GetClient(string domain = "yourdomain")
		{
			return new MailgunClient(domain, "key-withyourapikey", 3);
		}
	}
}
