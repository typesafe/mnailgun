using System;
using FluentAssertions;
using Typesafe.Mailgun.Routing;
using Xunit;

namespace Typesafe.Mailgun.Tests.Routing
{
	[Trait("Category", TestCategory.Integration)]
	public class When_creating_a_route_for_a_web_hook
	{
		[Fact]
		public void the_new_route_should_be_returned()
		{
			var route = MailgunClientBuilder.GetClient().CreateRoute(1, "catch all that does nothing test for mnailgun",
				RouteFilter.CatchAll(),
				RouteAction.InvokeWebHook(new Uri("http://foobar.com/messages")),
				RouteAction.Stop());

			route.Should().NotBeNull();
			route.Id.Should().NotBeEmpty();
		}
	}
}
