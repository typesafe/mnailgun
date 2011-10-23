using System;

namespace Typesafe.Mailgun.Mailboxes
{
	public class Mailbox : MailgunResource
	{
		public DateTime CreatedAt { get; set; }

		public string Name { get; set; }

		public int Size { get; set; }
	}
}