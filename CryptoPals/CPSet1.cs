//using OpenSSL.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
	public class CPSet1
	{
		public static void Challenge3() {
			LanguageSample EnglishReference = new LanguageSample(Assembly.GetExecutingAssembly(), "en-corpus.txt");
			var e = new EnhancedByte("1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736");

			var best_byte = new SingleByteXORCryptor(e).DecypherKey(s => new LanguageSample(s).TriadComparison(EnglishReference));
			var decoded = (e ^ best_byte.BestByte).ToASCII();
			Console.WriteLine("\t====== Using Triad Compares ======");
			Console.WriteLine("Decoding with {0:X2} ({1}) gives \"{2}\"", best_byte.BestByte, Encoding.ASCII.GetString(new[] { best_byte.BestByte }), (e ^ best_byte.BestByte).ToASCII());

			best_byte = new SingleByteXORCryptor(e).DecypherKey(s => new LanguageSample(s).PercentEnglishASCII());
			decoded = (e ^ best_byte.BestByte).ToASCII();
			Console.WriteLine("\t====== Using English ASCII ======");
			Console.WriteLine("Decoding with {0:X2} ({1}) gives \"{2}\"", best_byte.BestByte, Encoding.ASCII.GetString(new[] { best_byte.BestByte }), (e ^ best_byte.BestByte).ToASCII());

			best_byte = new SingleByteXORCryptor(e).DecypherKey(s => new LanguageSample(s).BhattacharyyaCoeff(EnglishReference));
			decoded = (e ^ best_byte.BestByte).ToASCII();
			Console.WriteLine("\t====== Using Bhattacharyya Coefficient ======");
			Console.WriteLine("Decoding with {0:X2} ({1}) gives \"{2}\"", best_byte.BestByte, Encoding.ASCII.GetString(new[] { best_byte.BestByte }), (e ^ best_byte.BestByte).ToASCII());
		}

		public static void Challenge4()
		{
			LanguageSample EnglishReference = new LanguageSample(Assembly.GetExecutingAssembly(), "en-corpus.txt");

			var bbs = new List<BestByteScore>();
			var bbs_b = new List<BestByteScore>();
			var bbs_e = new List<BestByteScore>();
			string[] lines;
			try {
				lines = File.ReadAllLines(@"C:\Users\Matt\Documents\Visual Studio 2015\Projects\CryptoPals\CryptoPals\S1C4.txt");
			} catch( DirectoryNotFoundException) {
				lines = File.ReadAllLines(@"C:\Users\fordm\Source\Repos\CryptoPals\CryptoPals\S1C4.txt");
			}
			foreach (var line in lines)
			{
				var e = new EnhancedByte(line);
				var best_byte = new SingleByteXORCryptor(e).DecypherKey(s => new LanguageSample(s).TriadComparison(EnglishReference));
				bbs.Add(best_byte);
				var best_byte_e = new SingleByteXORCryptor(e).DecypherKey(s => new LanguageSample(s).PercentEnglishASCII());
				bbs_e.Add(best_byte_e);
				var best_byte_b = new SingleByteXORCryptor(e).DecypherKey(s => new LanguageSample(s).BhattacharyyaCoeff(EnglishReference));
				bbs_b.Add(best_byte_b);
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
			Console.WriteLine("\t====== Using Triad Compares ======");
			Console.WriteLine("Decoding with {0:X2} ({1}) gives \"{2}\"", bbs[i_best].BestByte, Encoding.ASCII.GetString(new[] { bbs[i_best].BestByte }), (e_best ^ bbs[i_best].BestByte).ToASCII());

			max_score = -1.0;
			i_best = -1;
			for (int i = 0; i < bbs_e.Count(); i++) {
				if (bbs_e[i].Score > max_score) {
					i_best = i;
					max_score = bbs_e[i].Score;
				}
			}
			Console.WriteLine(lines[i_best]);
			e_best = new EnhancedByte(lines[i_best]);
			Console.WriteLine("\t====== Using English ASCII ======");
			Console.WriteLine("Decoding with {0:X2} ({1}) gives \"{2}\"", bbs_e[i_best].BestByte, Encoding.ASCII.GetString(new[] { bbs_e[i_best].BestByte }), (e_best ^ bbs_e[i_best].BestByte).ToASCII());

			max_score = -1.0;
			i_best = -1;
			for (int i = 0; i < bbs_b.Count(); i++) {
				if (bbs_b[i].Score > max_score) {
					i_best = i;
					max_score = bbs_b[i].Score;
				}
			}
			Console.WriteLine(lines[i_best]);
			e_best = new EnhancedByte(lines[i_best]);
			Console.WriteLine("\t====== Using Bhattacharyya Coefficient ======");
			Console.WriteLine("Decoding with {0:X2} ({1}) gives \"{2}\"", bbs_b[i_best].BestByte, Encoding.ASCII.GetString(new[] { bbs_b[i_best].BestByte }), (e_best ^ bbs_b[i_best].BestByte).ToASCII());
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
			} catch (DirectoryNotFoundException) {
				lines = File.ReadAllLines(@"C:\Users\fordm\Source\Repos\CryptoPals\CryptoPals\S1C6.txt");
			}

			var EncryptedData = new EnhancedByte(Convert.FromBase64String(String.Join("", lines)));
			//TODO: Need to be cautious about what to do when data size is small and what maximum keysize can be...
			List<Tuple<int, double>> keydists = new List<Tuple<int, double>>();
			for (int keysize = 2; keysize < 41; keysize++) {
				var datachunks = new[] {
					EncryptedData.Take(keysize),
					EncryptedData.Skip(keysize).Take(keysize),
					EncryptedData.Skip(keysize * 2).Take(keysize),
					EncryptedData.Skip(keysize * 3).Take(keysize)
				};
				var datacombos = datachunks.DifferentCombinations(2);
				var distance = datacombos.Select(i => new { e1 = i.First(), e2 = i.Last() })
										 .Average(pair => (double)pair.e1.HammingDistance(pair.e2) / (double)keysize);
				keydists.Add(new Tuple<int, double>(keysize, distance));
			}
			var optKeysizes = keydists.OrderBy(kdp => kdp.Item2).Take(3);
			var EnglishReference = new LanguageSample(System.Reflection.Assembly.GetExecutingAssembly(), "en-corpus.txt");
			var bestLangDistance = 0.0;
			var bestKey = String.Empty;
			var Cryptor = new RepeatingKeyXORCryptor(EncryptedData);
			foreach (var opt in optKeysizes) {
				var possibleKey = Cryptor.DecipherKey(opt.Item1);
				var potentialAnswer = Cryptor.Decrypt(possibleKey).ToASCII();
				var langDistance = EnglishReference.TriadComparison(new LanguageSample(potentialAnswer));
				if (langDistance > bestLangDistance) {
					bestLangDistance = langDistance;
					bestKey = possibleKey;
				}
				Console.WriteLine($@"Potential KEYSIZE = {opt.Item1}, Decrypted key = ""{possibleKey}"", English Score = {langDistance:G5}");
			}
			Console.WriteLine("The decrypted text is:\n{0} ", Cryptor.Decrypt(bestKey).ToASCII());
		}

		public static void Challenge7() {
			string[] lines;
			try {
				lines = File.ReadAllLines(@"C:\Users\Matt\Documents\Visual Studio 2015\Projects\CryptoPals\CryptoPals\S1C7.txt");
			} catch (DirectoryNotFoundException) {
				lines = File.ReadAllLines(@"C:\Users\fordm\Source\Repos\CryptoPals\CryptoPals\S1C7.txt");
			}

			var data = Convert.FromBase64String(String.Join("", lines));

			var password = Encoding.ASCII.GetBytes("YELLOW SUBMARINE");
			Aes Decryptor = Aes.Create();
			Decryptor.Mode = CipherMode.ECB;
			Decryptor.BlockSize = 128;
			Decryptor.KeySize = password.Length * 8;
			Decryptor.Key = password;

			Decryptor.Padding = PaddingMode.None;
			ICryptoTransform Decr = Decryptor.CreateDecryptor();
			Byte[] plainText = null;
			using (MemoryStream ms = new MemoryStream()) { 
				using (CryptoStream cs = new CryptoStream(ms, Decr, CryptoStreamMode.Write)) {
					cs.Write(data, 0, data.Length);
				}
				plainText = ms.ToArray();
			}

			Console.WriteLine($"The decrypted file is:\n{Encoding.ASCII.GetString(plainText)}");
		}
	}
}
