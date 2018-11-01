using FluentAssertions;
using Typesafe.Mailgun.Routing;
using Xunit;

namespace Typesafe.Mailgun.Tests.Routing
{
	[Trait("Category", "Integrations")]
	public class When_creating_a_route
	{
		[Fact]
		public void the_new_route_should_be_returned()
		{
			var route = MailgunClientBuilder.GetClient().CreateRoute(
				1,
				"catch all that does nothing test for mailgun",
				RouteFilter.CatchAll(),
				RouteAction.Stop());

			route.Should().NotBeNull();
			route.Id.Should().NotBeEmpty();
		}
	}
}
