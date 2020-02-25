namespace Typesafe.Mailgun.Tests
{
	public static class MailgunClientBuilder
	{
		// TODO: put your domain and API key here before running tests
		private const string YourDomain = "your.domain";

		private const bool IsEuDomain = false;

		private const string YourApiKey = "your-api-key";

		public static MailgunClient GetClient(string domain = YourDomain, bool euDomain = IsEuDomain)
		{
			return new MailgunClient(domain, YourApiKey, 3, euDomain);
		}
	}
}
