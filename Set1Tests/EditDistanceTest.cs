using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoPals;

namespace Set1Tests {
	[TestClass]

	public class EditDistanceTest {
		[TestMethod]
		public void HammingTestIdentical() {
			string s1 = "ABCDEFG";
			string s2 = "ABCDEFG";
			var tc = new TextComparer(s1, s2);
			Assert.AreEqual(0, tc.HammingDistance());
		}

		[TestMethod]
		public void LevenshteinTestIdentical() {
			string s1 = "ABCDEFG";
			string s2 = "ABCDEFG";
			var tc = new TextComparer(s1, s2);
			Assert.AreEqual(0, tc.LevenshteinDistance());
		}

		[TestMethod]
		public void LevenshteinTestIdenticalSimple() {
			string s1 = "A";
			string s2 = "A";
			var tc = new TextComparer(s1, s2);
			Assert.AreEqual(0, tc.LevenshteinDistance());
		}

		[TestMethod]
		public void HammingTestDifferent() {
			string s1 = "ABCDEFF";
			string s2 = "ABCDEFG";
			var tc = new TextComparer(s1, s2);
			Assert.AreEqual(1, tc.HammingDistance());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void HammingThrow() {
			string s1 = "ABCDEF";
			string s2 = "ABCDEFG";
			var tc = new TextComparer(s1, s2);
			tc.HammingDistance();
		}

		[TestMethod]
		public void EditLengthTest1() {
			string s1 = "appropriate meaning";
			string s2 = "approximate matching";
			var tc = new TextComparer(s1, s2);
			Assert.AreEqual(7, tc.LevenshteinDistance());
		}

		[TestMethod]
		public void EditLengthTest2() {
			string s1 = "this is a test";
			string s2 = "wokka wokka!!!";
			var tc = new TextComparer(s1, s2);
			Assert.AreEqual(14, tc.LevenshteinDistance());
		}

		[TestMethod]
		public void BitwiseEditLength()
		{
			EnhancedByte s1 = new EnhancedByte("this is a test", bytemode.ASCII);
			EnhancedByte s2 = new EnhancedByte("wokka wokka!!!", bytemode.ASCII);
			Assert.AreEqual(37, s1.HammingDistance(s2));
		}
	}
}
