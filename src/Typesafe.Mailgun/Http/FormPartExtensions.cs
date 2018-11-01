using System;
using System.Collections.Generic;
using System.IO;

namespace Typesafe.Mailgun.Http
{
	public static class FormPartExtensions
	{
		private static readonly string boundary = Guid.NewGuid().ToString("N");

		public static void SetFormParts(this System.Net.HttpWebRequest request, IEnumerable<FormPart> parts)
		{
			request.ContentType = "multipart/form-data; boundary=" + boundary;

			using (var writer = new StreamWriter(request.GetRequestStream()))
			{
				foreach (var part in parts) part.WriteTo(writer, boundary);

				writer.Write("--" + boundary + "--");
			}
		}
	}
}
