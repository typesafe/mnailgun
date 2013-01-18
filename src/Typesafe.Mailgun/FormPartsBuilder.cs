using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
                    new SimpleFormPart("subject", message.Subject)
                };

            if (message.CC.Any())
                result.Add(new SimpleFormPart("cc",string.Join(", ", message.CC)));
            
            if (message.Bcc.Any())
                result.Add(new SimpleFormPart("bcc",string.Join(", ", message.Bcc)));

            var htmlPart = PartForTextContent(message, html: true);
            if (htmlPart != null) result.Add(htmlPart);
            
            var textPart = PartForTextContent(message, html: false);
            if (textPart != null) result.Add(textPart);
            
            result.AddRange(message.Attachments.Select(attachment => new AttachmentFormPart(attachment)));

            return result;
        }

        private static FormPart PartForTextContent(MailMessage message, bool html)
        {
            var contentType = html ? "text/html" : "text/plain";
            var partName = html ? "html" : "text";
            
            if (!string.IsNullOrWhiteSpace(message.Body))
            {
                return new SimpleFormPart(partName, message.Body);
            }

            // Check to See if AlternateView Specified for Content Type
            var view = message.AlternateViews.FirstOrDefault(x => x.ContentType.MediaType == contentType);
            if (view != null)
            {
                var content = new StreamReader(view.ContentStream).ReadToEnd();
                return new SimpleFormPart(partName, content);
            }

            return null;
        }
    }
}
