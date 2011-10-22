using System;
using System.Net.Mail;
using NUnit.Framework;

namespace Typesafe.Mailgun.Tests
{
	[TestFixture]
	public class When_sending_an_email_message_for_a_non_existing_domain
	{
		[Test]
		public void an_entry_point_not_found_exception_should_be_thrown()
		{
			var client = new MailgunClient("idonotexist", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0");

			Assert.Throws<EntryPointNotFoundException>(() => client.SendMail(MailMessageBuilder.CreateMailWithoutAttachments()));
		}
	}
}