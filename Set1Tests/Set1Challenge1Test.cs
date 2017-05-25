using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoPals;

namespace Set1Tests {
	[TestClass]
	public class Set1Challenge1Test {
		private EnhancedByte b;
		private string input = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
		private string output = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";
		[TestMethod]
		public void VerifyChallenge1() {
			b = new EnhancedByte(input);
			Assert.AreEqual(output, b.ToBase64());
		}
	}

	[TestClass]
	public class Set1Challenge2Test {
		[TestMethod]
		public void VerifyChallenge2() {
			var b1 = new EnhancedByte("1c0111001f010100061a024b53535009181c");
			var b2 = new EnhancedByte("686974207468652062756c6c277320657965");
			var correct = "746865206b696420646f6e277420706c6179";
			
			Assert.AreEqual(correct, (b1^b2).ToString());
		}
	}
}
