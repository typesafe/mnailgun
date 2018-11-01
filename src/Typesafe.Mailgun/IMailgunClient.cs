using System.Collections.Generic;
using System.Net.Mail;
using Typesafe.Mailgun.Routing;
using Typesafe.Mailgun.Statistics;

namespace Typesafe.Mailgun
{
	/// <summary>
	/// Provides access to the Mailgun REST API.
	/// </summary>
	public interface IMailgunClient
	{
		/// <summary>
		/// Sends email through the mailgun client.
		/// </summary>
		/// <param name="mailMessage"></param>
		/// <returns></returns>
		SendMailCommandResult SendMail(MailMessage mailMessage);

		IEnumerable<Route> GetRoutes(int skip, int take, out int count);

		Route CreateRoute(int priority, string description, RouteFilter expression, params RouteAction[] actions);

		CommandResult DeleteRoute(string routeId);

		IEnumerable<MailgunStatEntry> GetStats(int skip, int take, MailgunEventTypes eventTypes, out int count);
	}
}
