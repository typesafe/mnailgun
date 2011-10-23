using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Typesafe.Mailgun.Http
{
	public class MailgunHttpResponse
	{
		private readonly HttpWebResponse httpResonse;

		public MailgunHttpResponse(HttpWebResponse httpResonse)
		{
			this.httpResonse = httpResonse;

			ReadJson();
		}

		public string Message { get { return Body == null ? null : Body.message.Value; } }

		public dynamic Body { get; private set; }

		public HttpStatusCode StatusCode { get { return httpResonse.StatusCode; } }

		private void ReadJson()
		{
			if (httpResonse.ContentType != "application/json") return;

			using (var reader = new StreamReader(httpResonse.GetResponseStream()))
				Body = JToken.ReadFrom(new JsonTextReader(reader));
		}
	}
}