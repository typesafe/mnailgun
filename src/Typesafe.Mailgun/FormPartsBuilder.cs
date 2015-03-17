using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using Newtonsoft.Json;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun
{
	public static class FormPartsBuilder
	{
		public static List<FormPart> Build(MailMessage message)
		{
			return Build(message, null);
		}

		public static List<FormPart> Build(MailMessage message, IDictionary<string, IDictionary<string, object>> recipientVariables)
		{
			if (message == null)
				return new List<FormPart>();

			var result = new List<FormPart>
			{
				new SimpleFormPart("from", message.From.ToString()),
				new SimpleFormPart("to",string.Join(", ", message.To)),
				new SimpleFormPart("subject", message.Subject),
			};

			if (recipientVariables != null)
			{
				result.Add(new SimpleFormPart("recipient-variables", JsonConvert.SerializeObject(recipientVariables)));
			}

			if (message.CC.Any())
				result.Add(new SimpleFormPart("cc", string.Join(", ", message.CC)));

			if (message.Bcc.Any())
				result.Add(new SimpleFormPart("bcc", string.Join(", ", message.Bcc)));

			if(message.ReplyToList.Any())
				result.Add(new SimpleFormPart("h:Reply-To", string.Join(", ", message.ReplyToList)));

			result.AddRange(message.GetBodyParts());

			result.AddRange(message.Attachments.Select(attachment => new AttachmentFormPart(attachment)));

			return result;
		}

		private static IEnumerable<FormPart> GetBodyParts(this MailMessage message)
		{
			if (!string.IsNullOrWhiteSpace(message.Body))
			{
				yield return new SimpleFormPart(message.IsBodyHtml ? "html" : "text", message.Body);

				var alternateContentType = message.IsBodyHtml ? MediaTypeNames.Text.Plain : MediaTypeNames.Text.Html;
				var alt = message.GetAlternatePart(alternateContentType);

				if (alt != null) yield return alt;
			}
			else
			{
				var alt = message.GetAlternatePart(MediaTypeNames.Text.Plain);
				if (alt != null) yield return alt;

				alt = message.GetAlternatePart(MediaTypeNames.Text.Html);
				if (alt != null) yield return alt;
			}
		}

		private static FormPart GetAlternatePart(this MailMessage message, string contentType)
		{
			var alt = message.AlternateViews.FirstOrDefault(v => v.ContentType.MediaType == contentType);

			if (alt != null)
			{
				using (var sr = new StreamReader(alt.ContentStream))
					return new SimpleFormPart(contentType == MediaTypeNames.Text.Plain ? "text" : "html", sr.ReadToEnd());
			}

			return null;
		}
	}
}
