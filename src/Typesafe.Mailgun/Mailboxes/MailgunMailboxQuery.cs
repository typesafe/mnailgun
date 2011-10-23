using System;

namespace Typesafe.Mailgun.Mailboxes
{
	internal class MailgunMailboxQuery : MailgunQuery<Mailbox>
	{
		public MailgunMailboxQuery(IMailgunAccountInfo accountInfo) : base(accountInfo, "mailboxes") { }
		
		public override Mailbox MapJsonItem(dynamic item)
		{
			return new Mailbox{ CreatedAt = DateTime.Parse(item.created_at.Value), Id = item.id.Value, Name= item.mailbox.Value };
		}
	}
}