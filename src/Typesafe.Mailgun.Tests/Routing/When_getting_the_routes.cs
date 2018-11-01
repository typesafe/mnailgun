using FluentAssertions;
using Xunit;

namespace Typesafe.Mailgun.Tests.Routing
{
	[Trait("Category", TestCategory.Integration)]
	public class When_getting_the_routes
	{
		[Fact]
		public void a_list_of_routes_should_be_returned()
		{
			int count;
			var routes = MailgunClientBuilder.GetClient().GetRoutes(0, 10, out count);

			count.Should().BeGreaterThan(0);

			foreach (var route in routes) route.Id.Should().NotBeNullOrEmpty();
		}
	}
}
