using NUnit.Framework;
using Typesafe.Mailgun.Routing;

namespace Typesafe.Mailgun.Tests.Routing
{
	[TestFixture]
	public class When_deleting_a_route
	{
		[Test]
		public void the_new_route_should_be_deleted()
		{
			var client = MailgunClientBuilder.GetClient();

			var route = client.CreateRoute(1, "catch all that does nothing test for mnailgun", RouteFilter.CatchAll(), RouteAction.Stop());
			var result = client.DeleteRoute(route.Id);

			Assert.IsNotNull(result);

		}
	}
}