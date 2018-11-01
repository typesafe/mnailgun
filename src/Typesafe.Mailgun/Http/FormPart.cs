using System.IO;

namespace Typesafe.Mailgun.Http
{
	/// <summary>
	/// Represents an multipart form part.
	/// </summary>
	public abstract class FormPart
	{
		public abstract void WriteTo(StreamWriter writer, string boundary);
	}
}
