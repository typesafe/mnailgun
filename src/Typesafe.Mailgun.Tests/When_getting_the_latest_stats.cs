using Xunit;

namespace Typesafe.Mailgun.Tests
{
	[Trait("Category", TestCategory.Integration)]
	public class When_getting_the_latest_stats
	{
		[Fact]
		public void a_list_of_events_should_be_returned()
		{
			int count;
			var e = MailgunClientBuilder.GetClient().GetStats(0, 100, MailgunEventTypes.Sent, out count);

			Assert.True(count > 0);
		}
	}
}
