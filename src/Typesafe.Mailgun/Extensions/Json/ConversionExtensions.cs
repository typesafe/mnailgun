using System;
using System.Collections.Generic;
using Typesafe.Mailgun.Routing;

namespace Typesafe.Mailgun.Extensions.Json
{
	public static class ConversionExtensions
	{
		public static Route ToRoute(this object route)
		{
			dynamic item = route;

			var actions = new List<RouteAction>();
			foreach (var a in item.actions) actions.Add(new RouteAction(a.Value));

			return new Route
			{
				Id = item.id.Value,
				Description = item.description.Value,
				CreatedAt = DateTime.Parse(item.created_at.Value),
				Priority = (int) item.priority.Value,
				Filter = new RouteFilter(item.expression.Value),
				Actions = actions.ToArray()
			};
		}
	}
}
