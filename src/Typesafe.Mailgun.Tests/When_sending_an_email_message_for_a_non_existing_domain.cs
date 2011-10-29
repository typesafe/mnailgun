using System;
using System.Net.Mail;
using NUnit.Framework;

namespace Typesafe.Mailgun.Tests
{
	[TestFixture]
	public class When_sending_an_email_message_for_a_non_existing_domain
	{
		[Test]
		public void an_invalid_operation_exception_should_be_thrown()
		{
			Assert.Throws<InvalidOperationException>(() => MailgunClientBuilder.GetClient("foobar.com").SendMail(MailMessageBuilder.CreateMailWithoutAttachments()));
		}
	}
}