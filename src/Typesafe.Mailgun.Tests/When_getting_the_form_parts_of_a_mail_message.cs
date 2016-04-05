using System.Linq;
using System.Net.Mail;
using NUnit.Framework;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun.Tests
{
	[TestFixture]
	public class When_getting_the_form_parts_of_a_mail_message
	{
		[Test]
		public void the_from_part_should_include_display_name()
		{
			var cmd = new SendMailCommand(
				new MailgunClient("domain", "api", 3), 
				new MailMessage(
					new MailAddress("gino@samples.mailgun.org", "Gino Heyman"),
					new MailAddress("gino.heyman@gmail.com", "Gino Heyman")) 
					{ Body = "this is a test message from mailgun.", Subject = "Hello from mailgun" });

			Assert.AreEqual("\"Gino Heyman\" <gino@samples.mailgun.org>", ((SimpleFormPart)cmd.CreateFormParts().First(p => p is SimpleFormPart)).Value);
		}
	}
}