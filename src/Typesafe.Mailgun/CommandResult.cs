namespace Typesafe.Mailgun
{
	public class CommandResult
	{
		public CommandResult(string message)
		{
			Message = message;
		}

		public string Message { get; }

		public override string ToString()
		{
			return Message;
		}
	}
}
