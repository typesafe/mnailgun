using FluentAssertions;
using Typesafe.Mailgun.Routing;
using Xunit;

namespace Typesafe.Mailgun.Tests.Routing
{
	[Trait("Category", TestCategory.Integration)]
	public class When_deleting_a_route
	{
		[Fact]
		public void the_new_route_should_be_deleted()
		{
			var client = MailgunClientBuilder.GetClient();

			var route = client.CreateRoute(
				1,
				"catch all that does nothing test for mnailgun",
				RouteFilter.CatchAll(),
				RouteAction.Stop());
			
			var result = client.DeleteRoute(route.Id);

			result.Should().NotBeNull();
		}
	}
}
