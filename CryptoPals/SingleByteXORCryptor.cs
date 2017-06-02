﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals {
	class SingleByteXORCryptor {
		private readonly EnhancedByte _ct;

		public SingleByteXORCryptor(EnhancedByte eb) {
			_ct = eb;
		}

		private EnhancedByte XORHelper(byte key) {
			var ret = new byte[_ct.Length];
			for (int i = 0; i < _ct.Length; i++) {
				ret[i] = (byte)(_ct[i] ^ key);
			}
			return new EnhancedByte(ret);
		}

		public EnhancedByte Decrypt(byte key) {
			return XORHelper(key);
		}

		public EnhancedByte Encrypt(byte key) {
			return XORHelper(key);
		}

		public BestByteScore DecypherKey( Func<string, double> f) {
			Dictionary<byte, double> scores = new Dictionary<byte, double>();
			for (byte b = 1; b < 255; b++) {
				if( b == 107) {
					var foo = 1;
				}
				if( b == 51) {
					var bar = 2;
				}
				if( b == 121) {
					var baz = 3;
				}
				var test_string = (_ct ^ b).ToASCII();
				var string_score = f(test_string);
				scores.Add(b, string_score);
			}
			var best_byte = scores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
			return new BestByteScore(best_byte, scores[best_byte]);
		}

		
	}
}
