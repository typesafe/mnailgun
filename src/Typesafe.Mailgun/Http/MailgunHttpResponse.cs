using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Typesafe.Mailgun.Http
{
	public class MailgunHttpResponse
	{
		private readonly HttpWebResponse _httpResponse;

		public MailgunHttpResponse(HttpWebResponse httpResponse)
		{
			_httpResponse = httpResponse;

			ReadJson();
		}

		public string Message => Body == null ? null : Body.message.Value;

		public dynamic Body { get; private set; }

		public HttpStatusCode StatusCode => _httpResponse.StatusCode;

		private void ReadJson()
		{
			//Sometimes the server returns application/json; charset=utf-8
			if (_httpResponse == null || !_httpResponse.ContentType.StartsWith("application/json"))
			{
				return;
			}

			using (var reader = new StreamReader(_httpResponse.GetResponseStream()))
			{
				Body = JToken.ReadFrom(new JsonTextReader(reader));
			}
		}
	}
}
