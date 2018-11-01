using System;

namespace Typesafe.Mailgun
{
	internal interface IMailgunAccountInfo
	{
		Uri DomainBaseUrl { get; }

		string ApiKey { get; }
	}
}
