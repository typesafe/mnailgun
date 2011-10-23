using System.Collections.Generic;
using System.Linq;
using System.Net;
using Typesafe.Mailgun.Extensions.HttpWebResponse;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun
{
	internal abstract class MailgunQuery<T>
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
			var json = ExecuteRequest(skip, take).BodyAsJson();

			count = (int)json.total_count.Value;

			var ret = new List<T>();

			foreach (var item in json.items) ret.Add(MapJsonItem(item));

			return ret;
		}

		protected IMailgunAccountInfo AccountInfo { get; private set; }

		protected virtual IEnumerable<KeyValuePair<string, string>> AdditionalParameters
		{
			get { return Enumerable.Empty<KeyValuePair<string, string>>(); }
		}

		private HttpWebResponse ExecuteRequest(int skip, int take)
		{
			var url = string.Format("{0}?skip={1}&take={2}", path, skip, take);
			foreach (var additionalParameter in AdditionalParameters)
			{
				url += string.Format("&{0}={1}", additionalParameter.Key, additionalParameter.Value);
			}

			return new MailgunHttpRequest(AccountInfo, "GET", url).GetResponse();
		}
	}
}