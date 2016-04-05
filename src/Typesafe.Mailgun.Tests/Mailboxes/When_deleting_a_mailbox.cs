using NUnit.Framework;

namespace Typesafe.Mailgun.Tests.Mailboxes
{
	[TestFixture]
	public class When_deleting_a_mailbox
	{
		[Test]
		public void the_new_mailbox_should_be_deleted()
		{
			var client = new MailgunClient("samples.mailgun.org", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0", 3);

			var result = client.DeleteMailbox("gino.heyman");

			Assert.IsNotNull(result);
		}
	}
}