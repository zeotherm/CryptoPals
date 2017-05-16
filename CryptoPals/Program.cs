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
			string test = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";

			var b = new BasicByteMethods(test);

			b.PrintData();

			Console.WriteLine(b.ToBase64());
		}
	}
}
