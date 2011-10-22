using System.IO;
using System.Net.Mail;
using System.Text;
using NUnit.Framework;

namespace Typesafe.Mailgun.Tests
{
	[TestFixture]
	public class When_sending_an_email_message_with_an_attachment
	{
		[Test]
		public void no_exception_should_be_thrown()
		{
			var client = new MailgunClient("samples.mailgun.org", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0");

			var mailMessage = new MailMessage("gino@samples.mailgun.org", "gino.heyman@gmail.com")
			{
				Body = "this is a test message from mailgun with an attachment.", 
				Subject = "Hello from mailgun"
			};

			mailMessage.Attachments.Add(new Attachment(new MemoryStream(Encoding.ASCII.GetBytes("This is the content of a test file.")), "test-file.txt"));

			client.SendMail(mailMessage);
		}
	}
}