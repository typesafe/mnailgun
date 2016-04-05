using System.Security.Authentication;
using NUnit.Framework;

namespace Typesafe.Mailgun.Tests
{
	[TestFixture]
	public class When_sending_an_email_message_with_an_invalid_api_key
	{
		[Test]
		public void an_authorization_exception_should_be_thrown()
		{
			var client = new MailgunClient("samples.mailgun.org", "key-aaaaaaaaaaaaaa", 3);

			Assert.Throws<AuthenticationException>(() => client.SendMail(MailMessageBuilder.CreateMailWithoutAttachments()));
		}
	}
}