using System.Net.Mail;

namespace Typesafe.Mailgun
{
    /// <summary>
    /// Provides access to the Mailgun REST API.
    /// </summary>
    public interface IMailgunClient
    {
        /// <summary>
        /// Sends email through the mailgun client.
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        CommandResult SendMail(MailMessage mailMessage);
    }
}