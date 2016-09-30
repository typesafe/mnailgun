using System.Net.Mail;

namespace Typesafe.Mailgun.Extensions
{
	/// <summary>
	/// Extensions to the <see cref="System.Net.Mail.MailMessage"/> class
	/// </summary>
	public static class MailMessageExtensions
	{ 
		public static void AddVariable(this MailMessage mailMessage, string name, string value)
		{
			mailMessage.Headers.Add("X-Mailgun-Variables", string.Format("{{\"{0}\": \"{1}\"}}", name, value));
		}
	}
}