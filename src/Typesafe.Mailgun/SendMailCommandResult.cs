namespace Typesafe.Mailgun
{
	public class SendMailCommandResult : CommandResult
	{
		public SendMailCommandResult(string id, string message) : base(message)
		{
			Id = id;
		}

		public string Id { get; }
	}
}
