using System;

namespace RestSharp.Authenticators.OAuth
{
	[Serializable]
	public enum OAuthSignatureMethod
	{
		HmacSha1,
		PlainText,
		RsaSha1
	}
}
