using System;

namespace Game.Utils
{
	public class UCException : Exception
	{
		public UCException()
		{
		}

		public UCException(string msg)
			: base(msg)
		{
		}
	}
}
