using System;
using FluentAssertions;
using Typesafe.Mailgun.Routing;
using Xunit;

namespace Typesafe.Mailgun.Tests.Routing
{
    [Trait("Category", Categories.Integrations)]
    public class When_creating_a_route_for_another_domain
    {
        [Fact]
        public void the_new_route_should_be_returned()
        {
            Action act = () => MailgunClientBuilder.GetClient().CreateRoute(
                1,
                "catch all that does nothing test for mnailgun",
                RouteFilter.MatchRecipient("*@foobar.com"),
                RouteAction.Stop());

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("The 'match_reciptient' function is not recognized.");
        }
    }
}