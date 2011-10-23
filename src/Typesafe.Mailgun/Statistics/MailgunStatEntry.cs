using System;

namespace Typesafe.Mailgun.Statistics
{
	/// <summary>
	/// 
	/// </summary>
	public class MailgunStatEntry : MailgunResource
	{
		public DateTime Date { get; set; }

		public MailgunEventTypes EventType { get; set; }

		public int Count { get; set; }
	}
}