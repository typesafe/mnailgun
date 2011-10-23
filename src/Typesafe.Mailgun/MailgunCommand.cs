using System.Net;
using Typesafe.Mailgun.Extensions.HttpWebResponse;

namespace Typesafe.Mailgun
{
	internal abstract class MailgunCommand : MailgunCommand<CommandResult>
	{
		protected MailgunCommand(IMailgunAccountInfo accountInfo, string path, string httpVerb = "POST") : base(accountInfo, path, httpVerb) { }

		public override CommandResult TranslateResponse(HttpWebResponse response)
		{
			return new CommandResult(response.BodyAsJson().message.Value);
		}
	}
}