using System.Collections.Generic;
using System.Net;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun.Mailboxes
{
	internal class CreateMailboxCommand : MailgunCommand
	{
		private readonly string account;
		private readonly string password;

		public CreateMailboxCommand(IMailgunAccountInfo accountInfo, string account, string password) : base(accountInfo, "mailboxes")
		{
			this.account = account;
			this.password = password;
		}

		protected override IEnumerable<FormPart> CreateFormParts()
		{
			return new List<FormPart> { new SimpleFormPart("mailbox", account), new SimpleFormPart("password", password) };
		}
	}
}