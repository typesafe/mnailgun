using System.IO;

namespace Typesafe.Mailgun.Extensions.HttpWebRequest
{
	public abstract class FormPart
	{
		public abstract void WriteTo(StreamWriter writer, string boundary);
	}
}