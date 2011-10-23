namespace Typesafe.Mailgun
{
	/// <summary>
	/// 
	/// </summary>
	public class CommandResult
	{
		public CommandResult(string message)
		{
			Message = message;
		}

		public string Message { get; private set; }

		public override string ToString() { return Message; }
	}
}