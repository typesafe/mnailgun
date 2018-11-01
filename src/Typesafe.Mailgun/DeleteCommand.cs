namespace Typesafe.Mailgun
{
	internal class DeleteCommand : MailgunCommand
	{
		public DeleteCommand(IMailgunAccountInfo accountInfo, string path) : base(accountInfo, path, "DELETE") { }
	}
}
