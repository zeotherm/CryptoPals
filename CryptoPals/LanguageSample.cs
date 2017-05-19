using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals {
	internal class LanguageSample {
		private Dictionary<string, Dictionary<char, int>> _refs;
		private readonly double _length;
		private string _pair;

		public LanguageSample(Assembly a, string resource_file) {
			//var assembly = Assembly.GetExecutingAssembly();
			//var resourceName = "en-corpus.txt";
			_pair = "  ";
			// default to using english language for now
			using (Stream stream = a.GetManifestResourceStream(resource_file))
			using (StreamReader file = new StreamReader(stream)) {
				string line;
				while( (line = file.ReadLine()) != null ) {
					ProcessStringIntoRefDictionary(line);
				}
			}
			_length = ComputeLength();
		}

		private double ComputeLength() {
			double l = 0.0;
			foreach( var y in _refs.Values) {
				l += y.Sum(x => Math.Pow(x.Value, 2.0));
			}
			return Math.Sqrt(l);
		}

		public LanguageSample(string s) {
			_pair = "  ";
			ProcessStringIntoRefDictionary(s);
			_length = ComputeLength();
		}

		private void ProcessStringIntoRefDictionary( string s) {
			foreach (char c in s.Where(l => l != '\n')) {
				Dictionary<char, int> val;
				if (_refs.ContainsKey(_pair)) {
					val = _refs[_pair];
				} else {
					val = new Dictionary<char, int>();
				}
				if (val.ContainsKey(c)) {
					val[c] += 1;
				} else {
					val.Add(c, 1);
				}
				_pair = String.Concat(_pair[1], c);
			}
			return;
		}

		public double Length {
			get { return _length; }
		}
	}
}
