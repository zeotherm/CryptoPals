using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
	class Program
	{
		static void Main(string[] args)
		{
			LanguageSample EnglishReference = new LanguageSample(Assembly.GetExecutingAssembly(), "en-corpus.txt");

			var bbs = new List<BestByteScore>();

			var lines = File.ReadAllLines(@"C:\Users\Matt\Documents\Visual Studio 2015\Projects\CryptoPals\CryptoPals\S1C4.txt");
			foreach (var line in lines)
			{
				var e = new EnhancedByte(line);
				Dictionary<byte, double> scores = new Dictionary<byte, double>();
				for (byte b = 0; b < 255; b++)
				{
					var ls = new LanguageSample((e ^ b).ToASCII());
					scores.Add(b, ls.CompareTo(EnglishReference));
				}
				var best_byte = scores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
				bbs.Add(new BestByteScore(best_byte, scores[best_byte]));
			}
			double max_score = -1.0;
			int i_best = -1;
			for( int i = 0; i < bbs.Count(); i++)
			{
				if( bbs[i].Score > max_score)
				{
					i_best = i;
					max_score = bbs[i].Score;
				}
			}
			Console.WriteLine(lines[i_best]);
			var e_best = new EnhancedByte(lines[i_best]);
			Console.WriteLine("Decoding with {0:X2} ({1}) gives \"{2}\"", bbs[i_best].BestByte, Encoding.ASCII.GetString(new[] { bbs[i_best].BestByte }), (e_best ^ bbs[i_best].BestByte).ToASCII());

			//var eb1 = new EnhancedByte("this is a test", bytemode.ASCII);
			//var eb2 = new EnhancedByte("wokka wokka!!!", bytemode.ASCII);

			//Console.WriteLine("Weird Hamming Distance is {0}", eb1.HammingDistance(eb2));
		}
	}
}
