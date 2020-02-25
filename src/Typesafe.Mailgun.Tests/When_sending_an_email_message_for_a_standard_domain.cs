using Xunit;

namespace Typesafe.Mailgun.Tests
{
	[Trait("Category", TestCategory.Integration)]
	public class When_sending_an_email_message_for_a_standard_domain
	{
		[Fact]
		public void the_standard_api_url_should_be_used()
		{
			var client = MailgunClientBuilder.GetClient("foobar.com", false);

			const string apiUrl = "https://api.mailgun.net";

			Assert.StartsWith(apiUrl, client.DomainBaseUrl.ToString());
		}
	}
}
