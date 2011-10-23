namespace Typesafe.Mailgun.Tests
{
	public static class MailgunClientBuilder
	{
		public static MailgunClient GetClient()
		{
			return new MailgunClient("samples.mailgun.org", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0");
		}
	}
}