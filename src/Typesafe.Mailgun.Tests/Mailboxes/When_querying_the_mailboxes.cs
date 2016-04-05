using NUnit.Framework;

namespace Typesafe.Mailgun.Tests.Mailboxes
{
	[TestFixture]
	public class When_querying_the_mailboxes
	{
		[Test]
		public void the_new_mailbox_should_be_returned()
		{
			var client = new MailgunClient("samples.mailgun.org", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0", 3);

			var result = client.CreateMailbox("gino.heyman", "foobar");

			Assert.IsNotNull(result);
		}
	}
}