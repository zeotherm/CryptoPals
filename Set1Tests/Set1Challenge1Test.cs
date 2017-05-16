using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoPals;

namespace Set1Tests {
	[TestClass]
	public class Set1Challenge1Test {
		private BasicByteMethods b;
		private string input = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
		private string output = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";
		[TestMethod]
		public void VerifyChallenge1() {
			b = new BasicByteMethods(input);
			Assert.AreEqual(output, b.ToBase64());
		}
	}
}
