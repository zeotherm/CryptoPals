﻿using System;
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
			Console.WriteLine("============= Challenge 3 ===============");
			CPSet1.Challenge3();
			Console.WriteLine("============= Challenge 4 ===============");
			CPSet1.Challenge4();
			Console.WriteLine("============= Challenge 5 ===============");
			CPSet1.Challenge5();
			Console.WriteLine("============= Challenge 6 ===============");
			CPSet1.Challenge6();
			//var eb1 = new EnhancedByte("this is a test", bytemode.ASCII);
			//var eb2 = new EnhancedByte("wokka wokka!!!", bytemode.ASCII);

			//Console.WriteLine("Weird Hamming Distance is {0}", eb1.HammingDistance(eb2));
		}
	}
}
