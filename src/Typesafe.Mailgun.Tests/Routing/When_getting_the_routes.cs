using NUnit.Framework;

namespace Typesafe.Mailgun.Tests.Routing
{
	[TestFixture]
	public class When_getting_the_routes
	{
		[Test]
		public void a_list_of_routes_should_be_returned()
		{
			var client = new MailgunClient("samples.mailgun.org", "key-3ax6xnjp29jd6fds4gc373sgvjxteol0");

			int count;
			var routes = client.GetRoutes(0, 10, out count);

			Assert.IsTrue(count > 0);

			foreach (var route in routes)
			{
				Assert.IsNotNullOrEmpty(route.Id);
			}
		}
	}
}