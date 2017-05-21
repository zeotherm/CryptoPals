using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
	public class EnhancedByte
	{
		private byte[] _data;

		public EnhancedByte(string val) {
			System.Diagnostics.Debug.Assert(val.Length % 2 == 0);
			_data = new byte[val.Length / 2];
			for (int i = 0; i < val.Length / 2; i++) {
				_data[i] = byte.Parse(val.Substring(i * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
			}
		}

		public EnhancedByte(byte[] data) {
			this._data = data;
		}

		public void PrintData() {
			foreach (var b in _data) {
				Console.Write("{0:X2} ", b);
			}
			Console.WriteLine("");
		}

		public string ToBase64() {
			return Convert.ToBase64String(_data);
		}

		public int Length {
			get { return _data.Length; }
		}

		public byte this[int index] {
			get { if (index < 0 || index > _data.Length) throw new ArgumentOutOfRangeException();
				  return _data[index];
			}
		}

		public override string ToString() {
			string hex = BitConverter.ToString(_data);
			return hex.Replace("-", "");
		}

		public string ToASCII() {
			return System.Text.Encoding.ASCII.GetString(_data);
		}

		private EnhancedByte XOR( EnhancedByte e) {
			System.Diagnostics.Debug.Assert(e.Length == _data.Length);
			var ret = new byte[_data.Length];
			int i = 0;
			foreach ( var b in _data) {
				ret[i] = (byte)((e[i]) ^ b);
				i++;
			}

			return new EnhancedByte(ret);
		}

		private EnhancedByte XOR( byte b) {
			var ret = new byte[_data.Length];
			for( int i = 0; i < _data.Length; i++) {
				ret[i] = (byte)(_data[i] ^ b);
			}
			return new EnhancedByte(ret);
		}

		public static EnhancedByte operator ^(EnhancedByte e1, EnhancedByte e2) {
			return e1.XOR(e2);
		}

		public static EnhancedByte operator ^(EnhancedByte e, byte b) {
			return e.XOR(b);
		}
	}
}
