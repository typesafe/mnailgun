using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using System.Text;
using Typesafe.Mailgun.Extensions.HttpWebRequest;
using Typesafe.Mailgun.Extensions.HttpWebResponse;

namespace Typesafe.Mailgun
{
	public class MailgunClient
	{
		private readonly string apiKey;
		private readonly Uri baseUrl;

		public MailgunClient(string domain, string apiKey)
		{
			this.apiKey = apiKey;

			baseUrl = new Uri("https://api.mailgun.net/v2/" + domain + "/");
		}

		public void SendMail(MailMessage mailMessage)
		{
			var request = CreateRequest("POST");

			var formParts = new List<FormPart>
			{
				new SimpleFormPart("from", mailMessage.From.Address),
				new SimpleFormPart("to", string.Join(",", mailMessage.To)),
				new SimpleFormPart("subject", mailMessage.Subject),
				new SimpleFormPart(mailMessage.IsBodyHtml ? "html" : "text", mailMessage.Body),
			};

			if(mailMessage.CC.Any()) new SimpleFormPart("cc", string.Join(",", mailMessage.CC));
			if (mailMessage.Bcc.Any()) new SimpleFormPart("bcc", string.Join(",", mailMessage.Bcc));

			formParts.AddRange(mailMessage.Attachments.Select(attachment => new AttachmentSimpleFormPart(attachment)));

			request.SetFormParts(formParts);

			var response = ExecuteRequest(request);

			if (response.StatusCode == HttpStatusCode.Unauthorized) throw new AuthenticationException();

			if (response.StatusCode == HttpStatusCode.NotFound) throw new EntryPointNotFoundException(response.BodyAsJson().message.Value);
		}

		private HttpWebResponse ExecuteRequest(WebRequest request)
		{
			try
			{
				return request.GetResponse() as HttpWebResponse;
			}
			catch (WebException ex)
			{
				return ex.Response as HttpWebResponse;
			}
		}

		private HttpWebRequest CreateRequest(string method)
		{
			var request = WebRequest.Create(new Uri(baseUrl, "messages")) as HttpWebRequest;
			request.Method = method;

			// request.PreAuthenticate does not work as you might expect
			request.Headers.Add("Authorization", "basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(String.Format("api:{0}", apiKey))));

			return request;
		}
	}
}
