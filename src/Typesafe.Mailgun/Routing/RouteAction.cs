using System;
using System.Net.Mail;

namespace Typesafe.Mailgun.Routing
{
	public class RouteAction
	{
		protected internal RouteAction(string expression)
		{
			Expression = expression;
		}

		public static RouteAction MailForward(MailAddress address)
		{
			return new RouteAction($"forward(\"{address.Address}\")");
		}

		public static RouteAction InvokeWebHook(Uri url)
		{
			return new RouteAction($"forward(\"{url}\")");
		}

		public static RouteAction Stop()
		{
			return new RouteAction("stop()");
		}

		public string Expression { get; }

		public override string ToString()
		{
			return Expression;
		}
	}
}
