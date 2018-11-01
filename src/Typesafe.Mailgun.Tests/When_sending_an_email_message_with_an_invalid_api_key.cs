using System.Security.Authentication;
using Xunit;

namespace Typesafe.Mailgun.Tests
{
	[Trait("Category", TestCategory.Integration)]
	public class When_sending_an_email_message_with_an_invalid_api_key
	{
		[Fact]
		public void an_authorization_exception_should_be_thrown()
		{
			var client = new MailgunClient("samples.mailgun.org", "key-aaaaaaaaaaaaaa", 3);

			Assert.Throws<AuthenticationException>(() => client.SendMail(MailMessageBuilder.CreateMailWithoutAttachments()));
		}
	}
}
