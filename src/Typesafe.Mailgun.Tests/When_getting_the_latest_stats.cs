using NUnit.Framework;

namespace Typesafe.Mailgun.Tests
{
	[TestFixture]
	public class When_getting_the_latest_stats
	{
		[Test]
		public void a_list_of_events_should_be_returned()
		{
			var client = new MailgunClient("samples.mailgun.org", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0");

			int count;
			var e = client.GetStats(out count);

			Assert.IsTrue(count > 0);
		}
	}
}