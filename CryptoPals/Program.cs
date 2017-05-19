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
			var b = new EnhancedByte("1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736");

			b.PrintData();

			Console.WriteLine(b.ToASCII());

			LanguageSample EnglishReference = new LanguageSample(Assembly.GetExecutingAssembly(), "en-corpus.txt");
			LanguageSample EnglishReference2 = new LanguageSample(Assembly.GetExecutingAssembly(), "Resources.en-corpus2.txt");
			LanguageSample EnglishSample = new LanguageSample("The quick brown fox jumps over the lazy dog");
			LanguageSample SpanishSample = new LanguageSample("El zorro marrón rápido salta sobre el perro perezoso");
			LanguageSample FrenchSample = new LanguageSample("Le renard brun rapide saute sur le chien paresseux");

			Console.WriteLine("en-en {0}", EnglishSample.CompareTo(EnglishReference));
			Console.WriteLine("en-en2 {0}", EnglishReference.CompareTo(EnglishReference2));
			Console.WriteLine("en2-ens {0}", EnglishSample.CompareTo(EnglishReference2));
			Console.WriteLine("en-es {0}", SpanishSample.CompareTo(EnglishReference));
			Console.WriteLine("en-fr {0}", FrenchSample.CompareTo(EnglishReference));
		}
	}
}
