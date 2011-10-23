using NUnit.Framework;

namespace Typesafe.Mailgun.Tests
{
	[TestFixture]
	public class When_getting_the_latest_stats
	{
		[Test]
		public void a_list_of_events_should_be_returned()
		{
			int count;
			var e = MailgunClientBuilder.GetClient().GetStats(0, 100, MailgunEventTypes.Sent, out count);

			Assert.IsTrue(count > 0);
		}
	}
}