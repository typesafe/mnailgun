namespace Typesafe.Mailgun.Routing
{
	public class RouteFilter
	{
		protected internal RouteFilter(string expression)
		{
			Expression = expression;
		}

		public static RouteFilter MatchRecipient(string mailPattern)
		{
			return new RouteFilter(string.Format("match_recipient(\"{0}\")", mailPattern));
		}

		public static RouteFilter MatchHeader(string header, string pattern)
		{
			return new RouteFilter(string.Format("match_header(\"{0}\", \"{1}\")", header, pattern));
		}

		public static RouteFilter CatchAll()
		{
			return new RouteFilter("catch_all()");
		}

		public string Expression { get; private set; }

		public override string ToString() { return Expression; }
	}
}