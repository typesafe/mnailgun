using System.Collections.Generic;
using System.Net;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun
{
	internal abstract class MailgunCommand<T> where T : CommandResult
	{
		private readonly string path;

		protected MailgunCommand(IMailgunAccountInfo accountInfo, string path)
		{
			this.path = path;
			AccountInfo = accountInfo;
		}

		protected IMailgunAccountInfo AccountInfo { get; private set; }

		public T Invoke()
		{
			var request = new MailgunHttpRequest(AccountInfo, "POST", path);

			request.SetFormParts(CreateFormParts());
			
			return TranslateResponse(request.GetResponse());
		}

		protected abstract IEnumerable<FormPart> CreateFormParts();

		public abstract T TranslateResponse(HttpWebResponse response);
	}
}