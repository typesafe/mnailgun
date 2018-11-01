
namespace Typesafe.Mailgun.Routing
{
	internal class CreateRouteCommandResult : CommandResult
	{
		public CreateRouteCommandResult(string message, Route route) : base(message)
		{
			Route = route;
		}

		public Route Route { get; }
	}
}
