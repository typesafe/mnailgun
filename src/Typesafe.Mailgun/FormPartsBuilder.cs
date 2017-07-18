using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using Newtonsoft.Json;
using Typesafe.Mailgun.Http;

namespace Typesafe.Mailgun
{
	using Newtonsoft.Json.Linq;

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

			// Check for the existence of any Mailgun-Variables headers
			if (message.Headers.AllKeys.Contains("X-Mailgun-Variables"))
			{
				// Grab the Mailgun variables header values
				var variableHeaders = message.Headers.GetValues("X-Mailgun-Variables");
				if (variableHeaders != null)
				{
					// Iterate over the collection and add each tag header to the result
					foreach (var variable in variableHeaders)
					{
						JObject customVar = JObject.Parse(variable);
						foreach (var item in customVar)
						{
							result.Add(new SimpleFormPart(string.Format("v:{0}", item.Key), item.Value.ToString()));
						}
					}
				}
			}

			result.AddRange(message.GetBodyParts());

			// Check for the existense of any Mailgun Tag headers
			if (message.Headers.AllKeys.Contains("X-Mailgun-Tag"))
			{
				// Grab the Mailgun tag header values
				var tagHeaders = message.Headers.GetValues("X-Mailgun-Tag");
				if (tagHeaders != null)
				{
					// Iterate over the collection and add each tag header to the result
					foreach (var tag in tagHeaders)
					{
						result.Add(new SimpleFormPart("o:tag", tag));
					}
				}
			}

            // Check for the existense of Mailgun delayed send headers
            if (message.Headers.AllKeys.Contains("X-Mailgun-Deliver-By"))
            {
                // Grab the Mailgun tag header values
                var tagHeaders = message.Headers.GetValues("X-Mailgun-Deliver-By");
                if (tagHeaders != null)
                {
                    // Iterate over the collection and add each tag header to the result
                    foreach (var tag in tagHeaders)
                    {
                        result.Add(new SimpleFormPart("o:deliverytime", tag));
                    }
                }
            }

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
