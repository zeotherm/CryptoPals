using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals {
	public class RepeatingKeyXORCryptor {

		private readonly EnhancedByte _ct;
		private readonly LanguageSample EnglishReference;
		public RepeatingKeyXORCryptor( EnhancedByte eb) {
			_ct = eb;
			EnglishReference = new LanguageSample(System.Reflection.Assembly.GetExecutingAssembly(), "en-corpus.txt");
		}

		private EnhancedByte RepeatingKeyXORHelper(string key) {
			var key_bytes = Encoding.ASCII.GetBytes(key);
			var len = key_bytes.Length;
			var ret = new byte[_ct.Length];
			for (int i = 0; i < _ct.Length; i++) {
				ret[i] = (byte)(_ct[i] ^ key_bytes[i % len]);
			}
			return new EnhancedByte(ret);
		}

		public EnhancedByte Decrypt(string key) {
			return RepeatingKeyXORHelper(key);
		}

		public EnhancedByte Encrypt(string key) {
			return RepeatingKeyXORHelper(key);
		}

		public string DecipherKey(int keysize) {
			var chunks = new List<EnhancedByte>();
			// break the byte array up into keysize chunks
			var num_chunks = (_ct.Length + keysize - 1) / keysize;
			for (int i = 0; i < num_chunks; i++) {
				chunks.Add(_ct.Skip(keysize * i).Take(keysize));
			}

			var transposed_chunks = new List<EnhancedByte>();

			for( int i = 0; i < chunks[0].Length; i++) {
				var nth_byte_collection = new List<byte>();
				foreach( var eb in chunks) {
					if( eb.Length > i) nth_byte_collection.Add(eb[i]); // If it isn't then just skip it
				}
				transposed_chunks.Add(new EnhancedByte(nth_byte_collection.ToArray()));
			}
			var potential_key = new List<byte>();
			foreach( var c in transposed_chunks) {
				var singleXOR = new SingleByteXORCryptor(c);
				var guess = singleXOR.DecypherKey(s => {
					var l = new LanguageSample(s);
					return l.BhattacharyyaCoeff(EnglishReference) * l.PercentEnglishASCII();
				});
				potential_key.Add(guess.BestByte);
			}

			return Encoding.ASCII.GetString(potential_key.ToArray());
		}

	}
}
