using System.IO;

namespace Typesafe.Mailgun.Extensions.HttpWebRequest
{
	public class SimpleFormPart : FormPart
	{
		public SimpleFormPart(string name, string value)
		{
			Name = name;
			Value = value;
		}

		private string Name { get; set; }

		private string Value { get; set; }

		public override void WriteTo(StreamWriter writer, string boundary)
		{
			writer.Write("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n", boundary, Name);
			writer.Write(Value);
			writer.Write("\r\n");
		}
	}
}