using NUnit.Framework;
using Typesafe.Mailgun.Routing;

namespace Typesafe.Mailgun.Tests.Mailboxes
{
	[TestFixture]
	public class When_creating_a_mailbox
	{
		[Test]
		public void the_new_mailbox_should_be_returned()
		{
			var client = new MailgunClient("samples.mailgun.org", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0");

			var result = client.CreateMailbox("gino.heyman", "foobar");

			Assert.IsNotNull(result);
		}
	}
}