using NUnit.Framework;

namespace Typesafe.Mailgun.Tests.Routing
{
	[TestFixture]
	public class When_getting_the_routes
	{
		[Test]
		public void a_list_of_routes_should_be_returned()
		{
			int count;
			var routes = MailgunClientBuilder.GetClient().GetRoutes(0, 10, out count);

			Assert.IsTrue(count > 0);

			foreach (var route in routes)
			{
				Assert.IsNotNullOrEmpty(route.Id);
			}
		}
	}
}