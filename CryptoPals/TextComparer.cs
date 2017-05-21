using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals {
	public sealed class TextComparer {
		private readonly string _s1;
		private readonly string _s2;

		public TextComparer( string s1, string s2) {
			_s1 = s1;
			_s2 = s2;
		}

		public int HammingDistance() {
			if( _s1.Length != _s2.Length) {
				throw new InvalidOperationException("Hamming distance can only be computed for equal length strings");
			}
			return _s1.Zip(_s2, (l, r) => l - r == 0 ? 0 : 1).Sum();
		}

		public int LevenshteinDistance() {
			throw new NotImplementedException();

			// https://en.wikipedia.org/wiki/Levenshtein_distance
			// https://en.wikipedia.org/wiki/Edit_distance
			// https://en.wikipedia.org/wiki/Hirschberg%27s_algorithm
			// http://www.geeksforgeeks.org/dynamic-programming-set-5-edit-distance/
			// http://users.monash.edu/~lloyd/tildeAlgDS/Dynamic/Hirsch/
		}
	}
}
