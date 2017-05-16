using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
	public class BasicByteMethods
	{
		private byte[] _data;

		public BasicByteMethods(string val) {
			System.Diagnostics.Debug.Assert(val.Length % 2 == 0);
			_data = new byte[val.Length / 2];
			for (int i = 0; i < val.Length / 2; i++) {
				_data[i] = byte.Parse(val.Substring(i * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
			}
		}

		public void PrintData() {
			foreach (var b in _data) {
				Console.Write("{0:X} ", b);
			}
			Console.WriteLine("");
		}

		public string ToBase64() {
			return Convert.ToBase64String(_data);
		}
	}
}
