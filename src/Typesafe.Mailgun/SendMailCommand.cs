using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun
{
    internal class SendMailCommand : MailgunCommand<SendMailCommandResult>
    {
        private readonly MailMessage mailMessage;
		private readonly IDictionary<string, IDictionary<string, object>> _recipientVariables;
        private readonly IDictionary<string, object> _customVariables;

        public SendMailCommand(IMailgunAccountInfo accountInfo, MailMessage mailMessage)
            : base(accountInfo, "messages")
        {
            this.mailMessage = mailMessage;
        }

        public SendMailCommand(IMailgunAccountInfo accountInfo, MailMessage mailMessage, IDictionary<string, IDictionary<string, object>> recipientVariables, IDictionary<string, object> customVariables = null)
			: base(accountInfo, "messages")
		{
			this.mailMessage = mailMessage;
			_recipientVariables = recipientVariables;
            _customVariables = customVariables;
		}

        protected internal override IEnumerable<FormPart> CreateFormParts()
        {
			return FormPartsBuilder.Build(mailMessage, _recipientVariables);
        }

	    public override SendMailCommandResult TranslateResponse(MailgunHttpResponse response)
	    {
		    return new SendMailCommandResult(response.Body.id.ToString(), response.Message);
	    }
    }
}