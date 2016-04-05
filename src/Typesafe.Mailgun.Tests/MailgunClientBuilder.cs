namespace Typesafe.Mailgun.Tests
{
	public static class MailgunClientBuilder
	{
		public static MailgunClient GetClient(string domain = "samples.mailgun.org")
		{
			return new MailgunClient(domain, "key-3ax6xnjp29jd6fds4gc373sgvjxteol0", 3);
		}
	}
}