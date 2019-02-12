using System;

namespace RestSharp.Authenticators.OAuth
{
	[Serializable]
	public enum OAuthParameterHandling
	{
		HttpAuthorizationHeader,
		UrlOrPostParameters
	}
}
