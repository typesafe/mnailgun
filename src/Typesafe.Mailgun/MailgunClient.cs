﻿using System;
using System.Collections.Generic;
using System.Net.Mail;
using Typesafe.Mailgun.Routing;
using Typesafe.Mailgun.Statistics;

namespace Typesafe.Mailgun
{
	/// <summary>
	/// Provides access to the Mailgun REST API.
	/// </summary>
	public class MailgunClient : IMailgunAccountInfo, IMailgunClient
	{
		/// <summary>
		/// Initializes a new client for the specified domain and api key.
		/// </summary>
		public MailgunClient(string domain, string apiKey, int version, string region = "us")
		{            
			DomainBaseUrl = new Uri($"https://api.{(!region.Equals("us") ? $"{region}." : "")}mailgun.net/v{version}/{domain}/");
            ApiKey = apiKey;
		}

		public Uri DomainBaseUrl { get; private set; }

		public string ApiKey { get; private set; }

		public SendMailCommandResult SendMail(MailMessage mailMessage)
		{
			return new SendMailCommand(this, mailMessage).Invoke();
		}

		public SendMailCommandResult SendBatchMail(MailMessage mailMessage)
		{
			return SendBatchMail(mailMessage, new Dictionary<string, IDictionary<string, object>>());
		}

		public SendMailCommandResult SendBatchMail(MailMessage mailMessage, IDictionary<string, IDictionary<string, object>> recipientVariables)
		{
			return new SendMailCommand(this, mailMessage, recipientVariables).Invoke();
		}

		public IEnumerable<Route> GetRoutes(int skip, int take, out int count)
		{
			return new MailgunRouteQuery(this).Execute(skip, take, out count);
		}

		public Route CreateRoute(int priority, string description, RouteFilter expression, params RouteAction[] actions)
		{
			return new CreateRouteCommand(this, priority, description, expression, actions).Invoke().Route;
		}

		public CommandResult DeleteRoute(string routeId)
		{
			return new DeleteCommand(this, "../routes/" + routeId).Invoke();
		}

		public IEnumerable<MailgunStatEntry> GetStats(int skip, int take, MailgunEventTypes eventTypes, out int count)
		{
			return new MailgunStatsQuery(this, eventTypes).Execute(skip, take, out count);
		}
	}
}
