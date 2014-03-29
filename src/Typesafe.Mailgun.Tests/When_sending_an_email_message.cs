using System.Net.Mail;
using NUnit.Framework;

namespace Typesafe.Mailgun.Tests
{
	[TestFixture]
	public class When_sending_an_email_message
	{
		private SendMailCommandResult result;

		public When_sending_an_email_message()
		{
			result = MailgunClientBuilder.GetClient().SendMail(new MailMessage("gino@samples.mailgun.org", "gino.heyman@gmail.com") { Body = "this is a test message from mailgun.", Subject = "Hello from mailgun" });
		}

		[Test]
		public void the_response_should_include_a_message()
		{
			Assert.IsNotNull(result.Message);
		}

		[Test]
		public void the_response_should_include_an_id()
		{
			Assert.IsNotNull(result.Id);
		}
	}
}
