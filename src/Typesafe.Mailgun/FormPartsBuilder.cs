using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun
{
    public class FormPartsBuilder
    {
        public static List<FormPart> Build(MailMessage message)
        {
            if (message == null)
                return new List<FormPart>();

            var result = new List<FormPart>
                {
                    new SimpleFormPart("from", message.From.ToString()),
                    new SimpleFormPart("to",string.Join(", ", message.To)),
                    new SimpleFormPart("subject", message.Subject),
                    new SimpleFormPart(message.IsBodyHtml ? "html" : "text", message.Body)
                };

            if (message.CC.Any())
                result.Add(new SimpleFormPart("cc",string.Join(", ", message.CC)));
            
            if (message.Bcc.Any())
                result.Add(new SimpleFormPart("bcc",string.Join(", ", message.Bcc)));

            result.AddRange(message.Attachments.Select(attachment => new AttachmentFormPart(attachment)));

            return result;
        }
    }
}
