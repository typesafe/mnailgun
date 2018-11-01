using System;

namespace Typesafe.Mailgun.Routing
{
	public class Route : MailgunResource
	{
		public DateTime CreatedAt { get; set; }

		public int Priority { get; set; }

		public string Description { get; set; }

		public RouteAction[] Actions { get; set; }

		public RouteFilter Filter { get; set; }
	}
}
