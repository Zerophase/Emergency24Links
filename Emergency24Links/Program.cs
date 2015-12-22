using System;
using System.Net;
using System.IO;
using System.Linq;

namespace Emergency24Links
{
	class MainClass
	{
		const string EMERGENCY_24 = "http://www.emergency24.com";
		const string FILE_NAME = "links.txt";

		public static void Main (string[] args)
		{
			WebClient w = new WebClient ();
			string s = w.DownloadString (EMERGENCY_24);


			var links = LinkFinder.Find(s).Distinct().ToList ();
			Console.WriteLine ("Alphabetizing Links...");
			for (int i = 0; i < links.Count; i++) 
			{
				if (links[i].Contains ("/cp/"))
					links[i] = links[i].Insert (0, EMERGENCY_24);
			}

			links.Sort ();

			using (StreamWriter writer = new StreamWriter (FILE_NAME)) 
			{
				Console.WriteLine ("Writing links to file...");
				foreach (var link in links) 
				{
					writer.WriteLine (link);
				}

				Console.WriteLine ("Path to text file: {0}.", Path.GetFullPath(FILE_NAME));
			}
		}
	}
}
