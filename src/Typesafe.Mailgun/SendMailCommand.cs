using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun
{
    internal class SendMailCommand : MailgunCommand
    {
        private readonly MailMessage mailMessage;

        public SendMailCommand(IMailgunAccountInfo accountInfo, MailMessage mailMessage)
            : base(accountInfo, "messages")
        {
            this.mailMessage = mailMessage;
        }

        protected internal override IEnumerable<FormPart> CreateFormParts()
        {
            return FormPartsBuilder.Build(mailMessage);
        }
    }
}