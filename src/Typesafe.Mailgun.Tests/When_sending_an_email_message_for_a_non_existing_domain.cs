using System;
using Xunit;

namespace Typesafe.Mailgun.Tests
{
	[Trait("Category", TestCategory.Integration)]
	public class When_sending_an_email_message_for_a_non_existing_domain
	{
		[Fact]
		public void an_invalid_operation_exception_should_be_thrown()
		{
			Assert.Throws<InvalidOperationException>(() =>
				MailgunClientBuilder.GetClient("foobar.com")
					.SendMail(MailMessageBuilder.CreateMailWithoutAttachments()));
		}
	}
}
