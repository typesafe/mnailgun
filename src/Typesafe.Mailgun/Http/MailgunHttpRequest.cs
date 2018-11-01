using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Typesafe.Mailgun.Http
{
	internal class MailgunHttpRequest
	{
		private readonly string boundary = Guid.NewGuid().ToString("N");

		private readonly HttpWebRequest request;

		public MailgunHttpRequest(IMailgunAccountInfo accountInfo, string method, string relativePath)
		{
			request = (HttpWebRequest)WebRequest.Create(new Uri(accountInfo.DomainBaseUrl, relativePath));
			request.Method = method;

			// Note: ensure no preceding 401, request.PreAuthenticate does not work as you might expect
			request.Headers.Add("Authorization", "basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{accountInfo.ApiKey}")));
		}

		public MailgunHttpResponse GetResponse()
		{
			try
			{
				return new MailgunHttpResponse(request.GetResponse() as HttpWebResponse);
			}
			catch (WebException ex)
			{
				return new MailgunHttpResponse(ex.Response as HttpWebResponse);
			}
		}

		public void SetFormParts(IEnumerable<FormPart> parts)
		{
			request.ContentType = "multipart/form-data; boundary=" + boundary;

			using (var writer = new StreamWriter(request.GetRequestStream()))
			{
				foreach (var part in parts) part.WriteTo(writer, boundary);

				writer.Write("--{0}--", boundary);
			}
		}
	}
}
