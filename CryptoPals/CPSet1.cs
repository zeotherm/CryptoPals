using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
	public class CPSet1
	{
		public static void Challenge4()
		{
			LanguageSample EnglishReference = new LanguageSample(Assembly.GetExecutingAssembly(), "en-corpus.txt");

			var bbs = new List<BestByteScore>();
			string[] lines;
			try {
				lines = File.ReadAllLines(@"C:\Users\Matt\Documents\Visual Studio 2015\Projects\CryptoPals\CryptoPals\S1C4.txt");
			} catch( DirectoryNotFoundException) {
				lines = File.ReadAllLines(@"C:\Users\fordm\Source\Repos\CryptoPals\CryptoPals\S1C4.txt");
			}
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
			for (int i = 0; i < bbs.Count(); i++)
			{
				if (bbs[i].Score > max_score)
				{
					i_best = i;
					max_score = bbs[i].Score;
				}
			}
			Console.WriteLine(lines[i_best]);
			var e_best = new EnhancedByte(lines[i_best]);
			Console.WriteLine("Decoding with {0:X2} ({1}) gives \"{2}\"", bbs[i_best].BestByte, Encoding.ASCII.GetString(new[] { bbs[i_best].BestByte }), (e_best ^ bbs[i_best].BestByte).ToASCII());
			return;
		}

		public static void Challenge5()
		{
			var line1 = @"Burning 'em, if you ain't quick and nimble I go crazy when I hear a cymbal";
			var eb1 = new EnhancedByte(line1, bytemode.ASCII);
			var key = "ICE";
			var cryptor = new RepeatingKeyXORCryptor(eb1);
			var encrypted = cryptor.Encrypt(key);
			Console.WriteLine("Encrypted: {0}", encrypted);
			Console.WriteLine("Decrypted: {0}", new RepeatingKeyXORCryptor(encrypted).Decrypt(key).ToASCII());
		}

		public static void Challenge6()
		{
			string[] lines;
			try {
				lines = File.ReadAllLines(@"C:\Users\Matt\Documents\Visual Studio 2015\Projects\CryptoPals\CryptoPals\S1C6.txt");
			} catch(DirectoryNotFoundException) {
				lines = File.ReadAllLines(@"C:\Users\fordm\Source\Repos\CryptoPals\CryptoPals\S1C6.txt");
			}
			var EncryptedData = new EnhancedByte(Convert.FromBase64String(String.Join("", lines)));
			List<Tuple<int, double>> keydists = new List<Tuple<int, double>>();
			for (int keysize = 2; keysize < 41; keysize++)
			{
				var datachunks = new[] {
					EncryptedData.Take(keysize),
					EncryptedData.Skip(keysize).Take(keysize),
					EncryptedData.Skip(keysize * 2).Take(keysize),
					EncryptedData.Skip(keysize * 3).Take(keysize)
				};
				var datacombos = datachunks.DifferentCombinations(2);
				var distance = datacombos.Select(i => new { e1 = i.First(), e2 = i.Last() })
									     .Average(pair => (double)pair.e1.HammingDistance(pair.e2) / (double)keysize);
				Console.WriteLine("KEYSIZE = {0}, ||distance|| = {1}", keysize, distance);
				keydists.Add(new Tuple<int, double>(keysize, distance));
			}
			var optKeysizes = keydists.OrderBy(kdp => kdp.Item2).Take(3);
			foreach( var opt in optKeysizes)
			{
				Console.WriteLine("Potential KEYSIZE = {0}", opt.Item1);
			}
			
		}
	}
}
