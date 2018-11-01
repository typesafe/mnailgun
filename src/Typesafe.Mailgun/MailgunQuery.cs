using System.Collections.Generic;
using System.Linq;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun
{
	internal abstract class MailgunQuery<T> where T : MailgunResource
	{
		private readonly string path;

		protected MailgunQuery(IMailgunAccountInfo accountInfo, string path)
		{
			this.path = path;
			AccountInfo = accountInfo;
		}

		public abstract T MapJsonItem(dynamic item);

		public IEnumerable<T> Execute(int skip, int take, out int count)
		{
			var json = ExecuteRequest(skip, take).Body;

			count = (int) json.total_count.Value;

			var ret = new List<T>();

			foreach (var item in json.items) ret.Add(MapJsonItem(item));

			return ret;
		}

		protected IMailgunAccountInfo AccountInfo { get; }

		protected virtual IEnumerable<KeyValuePair<string, string>> AdditionalParameters => Enumerable.Empty<KeyValuePair<string, string>>();

		private MailgunHttpResponse ExecuteRequest(int skip, int take)
		{
			var url = $"{path}?skip={skip}&take={take}";
			foreach (var additionalParameter in AdditionalParameters)
			{
				url += $"&{additionalParameter.Key}={additionalParameter.Value}";
			}

			return new MailgunHttpRequest(AccountInfo, "GET", url).GetResponse();
		}
	}
}
