using System.Net.Mail;

namespace Typesafe.Mailgun.Extensions
{
	/// <summary>
	/// Extensions to the <see cref="System.Net.Mail.MailMessage"/> class
	/// </summary>
	public static class MailMessageExtensions
	{
		/// <summary>
		/// Adds a tag to the <see cref="System.Net.Mail.MailMessage" />.
		/// </summary>
		/// <param name="mailMessage">The <see cref="System.Net.Mail.MailMessage" /> instance.</param>
		/// <param name="tag">The tag to add to the <see cref="System.Net.Mail.MailMessage" />.</param>
		public static void AddTag(this MailMessage mailMessage, string tag)
		{
			mailMessage.Headers.Add("X-Mailgun-Tag", tag);
		}

		public static void AddVariable(this MailMessage mailMessage, string name, string value)
		{
			mailMessage.Headers.Add("X-Mailgun-Variables", $"{{\"{name}\": \"{value}\"}}");
		}
	}
}
