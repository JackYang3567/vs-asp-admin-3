using System;

namespace RestSharp.Authenticators.OAuth
{
	[Serializable]
	public enum OAuthType
	{
		RequestToken,
		AccessToken,
		ProtectedResource,
		ClientAuthentication
	}
}
