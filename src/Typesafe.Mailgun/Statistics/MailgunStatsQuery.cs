using System;
using System.Linq;
using System.Collections.Generic;

namespace Typesafe.Mailgun.Statistics
{
	internal class MailgunStatsQuery : MailgunQuery<MailgunStatEntry>
	{
		private readonly MailgunEventTypes _eventTypes;

		public MailgunStatsQuery(IMailgunAccountInfo accountInfo, MailgunEventTypes eventTypes) : base(accountInfo, "stats")
		{
			_eventTypes = eventTypes;
		}

		protected override IEnumerable<KeyValuePair<string, string>> AdditionalParameters
		{
			get
			{
				var events = Enum.GetValues(typeof(MailgunEventTypes)).OfType<MailgunEventTypes>()
					.Where(value => _eventTypes.HasFlag(value))
					.Select(value => value.ToString().ToLower());

				return new[] {new KeyValuePair<string, string>("event", string.Join(",", events))};
			}
		}

		public override MailgunStatEntry MapJsonItem(dynamic item)
		{
			return new MailgunStatEntry {Count = (int) item.total_count.Value};
		}
	}
}
