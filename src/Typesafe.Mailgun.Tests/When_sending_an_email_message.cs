using System.Net.Mail;
using FluentAssertions;
using Xunit;

namespace Typesafe.Mailgun.Tests
{
	[Trait("Category", TestCategory.Integration)]
	public class When_sending_an_email_message
	{
		public When_sending_an_email_message()
		{
			result = MailgunClientBuilder.GetClient()
				.SendMail(new MailMessage("gino@samples.mailgun.org", "gino.heyman@gmail.com")
				{
					Body = "this is a test message from mailgun.",
					Subject = "Hello from mailgun"
				});
		}

		private readonly SendMailCommandResult result;

		[Fact]
		public void the_response_should_include_a_message()
		{
			result.Message.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public void the_response_should_include_an_id()
		{
			result.Id.Should().NotBeNull();
		}
	}
}
