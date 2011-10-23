using System.Net.Mail;
using NUnit.Framework;
using Typesafe.Mailgun.Routing;

namespace Typesafe.Mailgun.Tests.Routing
{
	[TestFixture]
	public class When_creating_a_route
	{
		[Test]
		public void the_new_route_should_be_returned()
		{
			var route = MailgunClientBuilder.GetClient().CreateRoute(1, "catch all that does nothing test for mnailgun", RouteFilter.CatchAll(), RouteAction.Stop());

			Assert.IsNotNull(route);

			Assert.IsNotNullOrEmpty(route.Id);
		}
	}
}