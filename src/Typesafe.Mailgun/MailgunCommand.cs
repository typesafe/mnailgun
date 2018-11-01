using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun
{
	internal abstract class MailgunCommand : MailgunCommand<CommandResult>
	{
		protected MailgunCommand(IMailgunAccountInfo accountInfo, string path, string httpVerb = "POST") : base(accountInfo, path, httpVerb)
		{
		}

		public override CommandResult TranslateResponse(MailgunHttpResponse response)
		{
			return new CommandResult(response.Message);
		}
	}
}
