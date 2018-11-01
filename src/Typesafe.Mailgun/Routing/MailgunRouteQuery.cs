using Typesafe.Mailgun.Extensions.Json;

namespace Typesafe.Mailgun.Routing
{
	internal class MailgunRouteQuery : MailgunQuery<Route>
	{
		public MailgunRouteQuery(IMailgunAccountInfo accountInfo) : base(accountInfo, "../routes") { }

		public override Route MapJsonItem(dynamic item)
		{
			return ((object) item).ToRoute();
		}
	}
}
