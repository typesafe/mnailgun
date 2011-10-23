using System.Net.Mail;
using NUnit.Framework;

namespace Typesafe.Mailgun.Tests
{
	[TestFixture]
	public class When_sending_an_email_message
	{
		[Test]
		public void no_exception_should_be_thrown()
		{
			var client = new MailgunClient("samples.mailgun.org", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0");

			var result = client.SendMail(new MailMessage("gino@samples.mailgun.org", "gino.heyman@gmail.com") { Body = "this is a test message from mailgun.", Subject = "Hello from mailgun" });

			Assert.IsNotNull(result);
		}
	}
}
