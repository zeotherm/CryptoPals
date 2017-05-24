using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPals
{
	public class BestByteScore
	{
		public byte BestByte { get; private set; }
		public double Score { get; private set; }

		public BestByteScore(byte b, double d)
		{
			BestByte = b;
			Score = d;
		}
	}
}
