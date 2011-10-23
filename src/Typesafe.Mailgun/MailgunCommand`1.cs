using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using Typesafe.Mailgun.Extensions.HttpWebResponse;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun
{
	internal abstract class MailgunCommand<T> where T : CommandResult
	{
		private readonly string path;
		private readonly string httpVerb;

		protected MailgunCommand(IMailgunAccountInfo accountInfo, string path, string httpVerb = "POST")
		{
			this.path = path;
			this.httpVerb = httpVerb;
			AccountInfo = accountInfo;
		}

		protected IMailgunAccountInfo AccountInfo { get; private set; }

		public T Invoke()
		{
			var request = new MailgunHttpRequest(AccountInfo, httpVerb, path);

			request.SetFormParts(CreateFormParts());

			var response = request.GetResponse();

			ThrowIfBadStatusCode(response);

			return TranslateResponse(response);
		}

		private static void ThrowIfBadStatusCode(HttpWebResponse response)
		{
			if (response.StatusCode == HttpStatusCode.Unauthorized) throw new AuthenticationException();

			if (response.StatusCode >= HttpStatusCode.InternalServerError) throw new Exception("Internal Server Error");

			if (response.StatusCode >= HttpStatusCode.BadRequest) throw new InvalidOperationException(response.BodyAsJson().message.Value);
		}

		protected virtual IEnumerable<FormPart> CreateFormParts()
		{
			return Enumerable.Empty<FormPart>();
		}

		public abstract T TranslateResponse(HttpWebResponse response);
	}
}