using System;

namespace RestSharp.Authenticators.OAuth
{
	[Serializable]
	public enum OAuthSignatureTreatment
	{
		Escaped,
		Unescaped
	}
}
