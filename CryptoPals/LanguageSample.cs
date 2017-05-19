using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals {
	internal sealed class AlphabetCounter {
		private Dictionary<char, int> _freq;

		public AlphabetCounter() {
			_freq = new Dictionary<char, int>();
		}

		public bool Contains( char c) {
			return _freq.ContainsKey(c);
		}

		public void Add(char c) {
			_freq.Add(c, 0);
			return;
		}

		public void Add( char c, int i) {
			_freq.Add(c, i);
		}

		public int this[char c] {
			get {
				if (_freq.ContainsKey(c)) return _freq[c];
				else return 0;
			}
			set {
				if (_freq.ContainsKey(c)) _freq[c] = value;
				else _freq.Add(c, 1);
			}
		}

		public IEnumerable<char> Keys {
			get {
				return _freq.Keys;
			}
		}		 

		public IEnumerable<int> Values {
			get {
				return _freq.Values;
			}
		}
	}

	internal sealed class LanguageSample {
		private Dictionary<string, AlphabetCounter> _n_gram;
		private readonly double _length;
		private string _triad;

		private LanguageSample() {
			_n_gram = new Dictionary<string, AlphabetCounter>();
			_triad = "   ";
		}

		public LanguageSample(Assembly a, string resource_file) : this(){
			//var assembly = Assembly.GetExecutingAssembly();
			//var resourceName = "en-corpus.txt";
			
			// default to using english language for now
			var l = a.GetManifestResourceNames();

			using (Stream stream = a.GetManifestResourceStream(String.Concat("CryptoPals.",resource_file)))
			using (StreamReader file = new StreamReader(stream)) {
				string line;
				while( (line = file.ReadLine()) != null ) {
					ProcessStringIntoRefDictionary(line);
				}
			}
			_length = ComputeLength();
		}

		public LanguageSample(string s) : this() {
			ProcessStringIntoRefDictionary(s);
			_length = ComputeLength();
		}

		private double ComputeLength() {
			double l = 0.0;
			foreach( var y in _n_gram.Values) {
				foreach( var z in y.Values) {
					l += Math.Pow(z, 2);
				}
			}
			return Math.Sqrt(l);
		}

		private void ProcessStringIntoRefDictionary( string s) {
			foreach (char c in s.Where(l => l != '\n')) {
				AlphabetCounter val;
				if (_n_gram.ContainsKey(_triad)) {
					val = _n_gram[_triad];
				} else {
					val = new AlphabetCounter();
					_n_gram.Add(_triad, val);
				}
				if (val.Contains(c)) {
					val[c] += 1;
				} else {
					val.Add(c, 1);
				}
				_triad = String.Concat(_triad[1], _triad[2], c);
			}
			return;
		}

		public double Length {
			get { return _length; }
		}

		public double CompareTo(LanguageSample ls) {
			double total = 0.0;
			foreach( var k in _n_gram.Keys) {
				if( ls.ContainsNGram(k)) {
					var a = _n_gram[k];
					var b = ls.GetNGram(k);
					foreach( var x in a.Keys) {
						if (b.Contains(x)) total += a[x] * b[x];
					}
				}
			}
			return total/(this.Length * ls.Length);
		}

		public bool ContainsNGram(string s) {
			return _n_gram.ContainsKey(s);
		}

		public AlphabetCounter GetNGram(string s) {
			return _n_gram[s];
		}
	}
}
