using System;
using NUnit.Framework;
using Typesafe.Mailgun.Routing;

namespace Typesafe.Mailgun.Tests.Routing
{
	[TestFixture]
	public class When_creating_a_route_for_another_domain
	{
		[Test]
		public void the_new_route_should_be_returned()
		{
			Assert.Throws<InvalidOperationException>(
				() => MailgunClientBuilder.GetClient().CreateRoute(1, "catch all that does nothing test for mnailgun", RouteFilter.MatchRecipient("*@foobar.com"), RouteAction.Stop()),
				"The 'match_reciptient' function is not recognized.");
		}
	}
}