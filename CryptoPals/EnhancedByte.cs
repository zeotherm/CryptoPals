using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
	public enum bytemode { HEX, ASCII };
	public class EnhancedByte
	{
		private byte[] _data;

		public EnhancedByte(string val, bytemode bm = bytemode.HEX) {
			if (bm == bytemode.HEX) {
				System.Diagnostics.Debug.Assert(val.Length % 2 == 0);
				_data = new byte[val.Length / 2];
				for (int i = 0; i < val.Length / 2; i++) {
					_data[i] = byte.Parse(val.Substring(i * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
				}
			} else {
				_data = Encoding.ASCII.GetBytes(val);
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
			//StringBuilder sb = new StringBuilder();
			//for( int i = 0; i < _data.Length-1; i++) { 
			//	sb.AppendFormat(_data[i].ToString().PadLeft(3, '0'));
			//	sb.Append("-");
			//}
			//sb.AppendFormat(_data[_data.Length - 1].ToString().PadLeft(3, '0'));

			//String.Join("-", _data);
			//string hex = String.Join("-", _data); //BitConverter.ToString(_data);
			//return sb.ToString();//hex;//.ToLower().Replace("-", "");
			return BitConverter.ToString(_data).ToLower().Replace("-", "");
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

		private int DifferingBits( byte l, byte r) {
			int count = 0;
			for( int i = 0; i < 8; i++) {
				if ((l & (1 << i)) != (r & (1 << i))) count++;
			}
			return count;
		}

		public int HammingDistance( EnhancedByte r) {
			if (this.Length != r.Length) throw new InvalidOperationException("cannot compute Hamming distance between unequal length EnhancedBytes");
			int count = 0;
			for (int i = 0; i < this.Length; i++) {
				count += DifferingBits(_data[i], r[i]);
			}
			return count;
		}

		public static int EditDistance( EnhancedByte l, EnhancedByte r) {
			return l.HammingDistance(r);
		}



		public EnhancedByte Skip( int n)
		{
			// return a new EnhancedByte that excludes the first n bytes
			return new EnhancedByte(_data.Skip(n).ToArray());
		}

		public EnhancedByte Take( int n, bool pad = false)
		{
			// returns a new EnhancedByte that contains only the first n bytes
			var ret = new EnhancedByte(_data.Take(n).ToArray());
			if( pad && ret.Length < n) {
				ret.Pad(0, n-ret.Length);
			}
			return ret;
		}

		private void Pad(byte v1, int v2) {
			byte[] new_data = new byte[_data.Length + v2];
			Array.Copy(_data, new_data, _data.Length);
			for( int i = _data.Length; i < _data.Length + v2; i++) {
				new_data[i] = v1;
			}
			_data = new_data;
			return;
		}
	}
}
