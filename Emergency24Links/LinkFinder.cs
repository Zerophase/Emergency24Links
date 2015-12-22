using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace Emergency24Links
{
	public static class LinkFinder
	{
		private static MatchCollection topMatch;  

		private const string CP = "/cp/";

		private static string newLine = Environment.NewLine;

		public static List<string> Find(string file)
		{
			Console.WriteLine("Finding Links...");

			topMatch = Regex.Matches(file, @"(<body.*?>.*?</body>)",
				RegexOptions.Singleline);

			List<string> links = new List<string> ();

			var cpList = extractLink (
				@"href=""(https\://www\.emergency24\.com/cp/)?(/cp/)?(([A-Za-z0-9\-_]+(\.aspx)?|(#.*?))|(val/)|(subresp/))""", 3);

			StringBuilder stringBuilder = new StringBuilder (cpList.Count);
			foreach (var link in cpList)
			{
				stringBuilder.Append (CP);
				stringBuilder.AppendLine(link);
			}
			links.AddRange (stringBuilder.ToString ().Split(newLine.ToCharArray(), 
				StringSplitOptions.RemoveEmptyEntries));

			var httpList = extractLink (@"href=""(?!https://www.emergency24.com/cp/subresp/)(https?.*?)""", 1);
			links.AddRange (httpList);

			return links;
		}

		public static List<string> extractLink(string regex, int group)
		{
			List<string> strings = new List<string> ();
			foreach (Match match in topMatch)
			{
				MatchCollection m = Regex.Matches (match.Value, 
					regex, RegexOptions.Singleline);
				for (int i = 0; i < m.Count; i++) 
				{
					strings.Add (m[i].Groups[group].Value);
				}
			}

			return strings;
		}
	}
}


