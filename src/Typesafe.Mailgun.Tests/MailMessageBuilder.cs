using System.Net.Mail;

namespace Typesafe.Mailgun.Tests
{
	public static class MailMessageBuilder
	{
		public static MailMessage CreateMailWithoutAttachments()
		{
			return new MailMessage("gino@samples.mailgun.org", "gino.heyman@gmail.com")
			{
				Body = "this is a test message from mailgun.",
				Subject = "Hello from mailgun"
			};
		}
	}
}
