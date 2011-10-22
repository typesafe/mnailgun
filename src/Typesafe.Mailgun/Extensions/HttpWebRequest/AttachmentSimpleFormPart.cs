using System;
using System.IO;
using System.Net.Mail;

namespace Typesafe.Mailgun.Extensions.HttpWebRequest
{
	public class AttachmentSimpleFormPart : FormPart
	{
		private readonly Attachment attachment;

		public AttachmentSimpleFormPart(Attachment attachment)
		{
			this.attachment = attachment;
		}

		public override void WriteTo(StreamWriter writer, string boundary)
		{
			writer.Write("--{0}\r\nContent-Disposition: form-data; name=\"attachment\"; filename=\"{1}\"\r\nContent-Type: {2}\r\nContent-Transfer-Encoding: base64\r\n\r\n",
				boundary, 
				attachment.Name,
				attachment.ContentType.MediaType);

			var bytes = new byte[attachment.ContentStream.Length];
			attachment.ContentStream.Read(bytes, 0, (int) attachment.ContentStream.Length);

			writer.Write(Convert.ToBase64String(bytes));
			writer.Write("\r\n");
		}
	}
}