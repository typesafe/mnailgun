using System.Collections.Generic;
using System.Linq;
using Typesafe.Mailgun.Extensions.Json;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun.Routing
{
	internal class CreateRouteCommand : MailgunCommand<CreateRouteCommandResult>
	{
		private readonly int priority;
		private readonly string description;
		private readonly RouteFilter expression;
		private readonly RouteAction[] actions;

		public CreateRouteCommand(IMailgunAccountInfo accountInfo, int priority, string description, RouteFilter expression, params RouteAction[] actions)
			: base(accountInfo, "../routes")
		{
			this.priority = priority;
			this.description = description;
			this.expression = expression;
			this.actions = actions;
		}

		protected internal override IEnumerable<FormPart> CreateFormParts()
		{
			var formParts = new List<FormPart>
			{
				new SimpleFormPart("priority", priority.ToString()),
				new SimpleFormPart("description", description),
				new SimpleFormPart("expression", expression.ToString())
			};

			formParts.AddRange(actions.Select(action => new SimpleFormPart("action", action.ToString())));
			return formParts;
		}

		public override CreateRouteCommandResult TranslateResponse(MailgunHttpResponse response)
		{
			return new CreateRouteCommandResult(response.Message, ((object) response.Body.route).ToRoute());
		}
	}
}
