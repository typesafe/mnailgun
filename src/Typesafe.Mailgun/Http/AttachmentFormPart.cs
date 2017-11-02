using System;
using System.IO;
using System.Net.Mail;

namespace Typesafe.Mailgun.Http
{
    /// <summary>
    /// Represents an multipart form part for a file attachment.
    /// </summary>
    public class AttachmentFormPart : FormPart
    {
        private readonly AttachmentBase attachment;

        public AttachmentFormPart(AttachmentBase attachment)
        {
            this.attachment = attachment;
        }

        public AttachmentBase Attachment { get { return attachment; } }

        public override void WriteTo(StreamWriter writer, string boundary)
        {
            if (attachment is Attachment regularAttachment)
            {
                writer.Write("--{0}\r\nContent-Disposition: form-data; name=\"attachment\"; filename=\"{1}\"\r\nContent-Type: {2}\r\nContent-Transfer-Encoding: base64\r\n\r\n",
                    boundary,
                    regularAttachment.Name,
                    regularAttachment.ContentType.MediaType);
            }

            if (attachment is LinkedResource linkedResource)
            {
                writer.Write("--{0}\r\nContent-Disposition: form-data; name=\"inline\"; filename=\"{1}\"\r\nContent-Type: {2}\r\nContent-Transfer-Encoding: base64\r\n\r\n",
                    boundary,
                    linkedResource.ContentId,
                    linkedResource.ContentType.MediaType);
            }

            var bytes = new byte[Attachment.ContentStream.Length];
            Attachment.ContentStream.Read(bytes, 0, (int)Attachment.ContentStream.Length);

            writer.Write(Convert.ToBase64String(bytes));
            writer.Write("\r\n");
        }
    }
}