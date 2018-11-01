using System.IO;

namespace Typesafe.Mailgun.Http
{
	public class SimpleFormPart : FormPart
	{
		public SimpleFormPart(string name, string value)
		{
			Name = name;
			Value = value;
		}

		public string Name { get; }

		public string Value { get; }

		public override void WriteTo(StreamWriter writer, string boundary)
		{
			writer.Write("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n", boundary, Name);
			writer.Write(Value);
			writer.Write("\r\n");
		}

		public override string ToString()
		{
			return string.Format("{0}: {1}", Name, Value);
		}
	}
}
