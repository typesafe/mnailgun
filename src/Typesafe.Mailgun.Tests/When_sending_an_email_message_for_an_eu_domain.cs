using Xunit;

namespace Typesafe.Mailgun.Tests
{
	[Trait("Category", TestCategory.Integration)]
	public class When_sending_an_email_message_for_an_eu_domain
	{
		[Fact]
		public void the_eu_api_url_should_be_used()
		{
			var client = MailgunClientBuilder.GetClient("foobar.com", true);

			const string euApiUrl = "https://api.eu.mailgun.net";

			Assert.StartsWith(euApiUrl, client.DomainBaseUrl.ToString());
		}
	}
}
