using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun
{
	internal class SendMailCommand : MailgunCommand
	{
		private readonly MailMessage mailMessage;

		public SendMailCommand(IMailgunAccountInfo accountInfo, MailMessage mailMessage) : base(accountInfo, "messages")
		{
			this.mailMessage = mailMessage;
		}

		protected internal override IEnumerable<FormPart> CreateFormParts()
		{
			var formParts = new List<FormPart>
			{
				new SimpleFormPart("from", mailMessage.From.ToString()),
				new SimpleFormPart("to", string.Join(",", mailMessage.To)),
				new SimpleFormPart("subject", mailMessage.Subject),
				new SimpleFormPart(mailMessage.IsBodyHtml ? "html" : "text", mailMessage.Body),
			};

			if (mailMessage.CC.Any()) new SimpleFormPart("cc", string.Join(",", mailMessage.CC));
			if (mailMessage.Bcc.Any()) new SimpleFormPart("bcc", string.Join(",", mailMessage.Bcc));

			formParts.AddRange(mailMessage.Attachments.Select(attachment => new AttachmentFormPart(attachment)));

			return formParts;
		}
	}
}