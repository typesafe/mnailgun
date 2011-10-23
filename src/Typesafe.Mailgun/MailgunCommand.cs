using System.Net;
using Typesafe.Mailgun.Extensions.HttpWebResponse;

namespace Typesafe.Mailgun
{
	internal abstract class MailgunCommand : MailgunCommand<CommandResult>
	{
		protected MailgunCommand(IMailgunAccountInfo accountInfo, string path) : base(accountInfo, path) { }

		public override CommandResult TranslateResponse(HttpWebResponse response)
		{
			return new CommandResult(response.BodyAsJson().message.Value);
		}
	}
}