
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
			return new RouteFilter($"match_recipient(\"{mailPattern}\")");
		}

		public static RouteFilter MatchHeader(string header, string pattern)
		{
			return new RouteFilter($"match_header(\"{header}\", \"{pattern}\")");
		}

		public static RouteFilter CatchAll()
		{
			return new RouteFilter("catch_all()");
		}

		public string Expression { get; }

		public override string ToString()
		{
			return Expression;
		}
	}
}
