using System;
using System.Diagnostics;

namespace RestSharp.Authenticators.OAuth
{
	[Serializable]
	[DebuggerDisplay("{Name}:{Value}")]
	internal class WebParameter : WebPair
	{
		public WebParameter(string name, string value)
			: base(name, value)
		{
		}
	}
}
