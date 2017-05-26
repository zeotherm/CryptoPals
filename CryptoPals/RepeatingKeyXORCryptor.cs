using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals {
	public class RepeatingKeyXORCryptor {

		private readonly EnhancedByte _ct;

		public RepeatingKeyXORCryptor( EnhancedByte eb) {
			_ct = eb;
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

		public EnhancedByte DecipherKey(int keysize) {
			List<EnhancedByte> chunks = new List<EnhancedByte>();
			// break the byte array up into keysize chunks
			var num_chunks = (_ct.Length + keysize - 1) / keysize;
			for (int i = 0; i < num_chunks; i++) {
				chunks.Add(_ct.Skip(keysize * i).Take(keysize));
			}

			return new EnhancedByte("");
		}
	}
}
