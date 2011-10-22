using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Typesafe.Mailgun.Extensions.HttpWebResponse
{
	public static class JsonExtensions
	{
		public static dynamic BodyAsJson(this System.Net.HttpWebResponse response)
		{
			return JToken.ReadFrom(new JsonTextReader(new StreamReader(response.GetResponseStream())));
		}
	}
}