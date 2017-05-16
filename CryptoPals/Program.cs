using System;
using System.Collections.Generic;
using System.Linq;
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

		}
	}
}
