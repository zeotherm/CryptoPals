using System;
using System.Collections.Generic;
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
			var e = new EnhancedByte("1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736");

			e.PrintData();

			Console.WriteLine(e.ToASCII());

			LanguageSample EnglishReference = new LanguageSample(Assembly.GetExecutingAssembly(), "en-corpus.txt");

			Dictionary<byte, double> scores = new Dictionary<byte, double>();
			for( byte b = 0; b < 255; b++) {
				var ls = new LanguageSample((e ^ b).ToASCII());
				scores.Add(b, ls.CompareTo(EnglishReference));
			}

			var best_byte = scores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
			Console.WriteLine("Decoding with {0:X2} ({1}) gives \"{2}\"", best_byte, Encoding.ASCII.GetString(new[] { best_byte }), (e ^ best_byte).ToASCII());

			var eb1 = new EnhancedByte("this is a test", bytemode.ASCII);
			var eb2 = new EnhancedByte("wokka wokka!!!", bytemode.ASCII);

			Console.WriteLine("Weird Hamming Distance is {0}", eb1.HammingDistance(eb2));
		}
	}
}
