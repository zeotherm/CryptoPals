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
			//var t = _s1.Zip(_s2, (s1, s2) => new Tuple<char, char>(s1, s2));
			//var u = t.Select(tu => tu.Item1 - tu.Item2 == 0 ? 0 : 1);
			//var v = u.Sum();
			//return v;
			return _s1.Zip(_s2, (l, r) => l - r == 0 ? 0 : 1).Sum();
		}

		public int LevenshteinDistance() {
			// https://en.wikipedia.org/wiki/Levenshtein_distance
			// https://en.wikipedia.org/wiki/Edit_distance
			// https://en.wikipedia.org/wiki/Hirschberg%27s_algorithm
			// http://www.geeksforgeeks.org/dynamic-programming-set-5-edit-distance/
			// http://users.monash.edu/~lloyd/tildeAlgDS/Dynamic/Hirsch/

			int i_max = _s1.Length;
			int j_max = _s2.Length;

			int[,] m = new int[i_max+1,j_max+1];
			m[0,0] = 0;
			for (int i = 1; i <= i_max; i++) {
				m[i, 0] = i;
			}
			for( int j = 1; j <= j_max; j++) {
				m[0, j] = j;
			}

			for (int i = 1; i <= i_max; i++) {
				for (int j = 1; j <= j_max; j++) {
					int u = m[i - 1, j - 1] + (_s1[i - 1] == _s2[j - 1] ? 0 : 1);
					int v = m[i - 1, j] + 1;
					int w = m[i, j - 1] + 1;
					m[i, j] = Math.Min(u, Math.Min(v, w));
				}
			}

			return m[i_max, j_max];
		}
	}
}
