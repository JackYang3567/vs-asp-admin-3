using System;
using System.Linq;

namespace RestSharp
{
	public class OAuth2AuthorizationRequestHeaderAuthenticator : OAuth2Authenticator
	{
		private readonly string _authorizationValue;

		public OAuth2AuthorizationRequestHeaderAuthenticator(string accessToken)
			: this(accessToken, "OAuth")
		{
		}

		public OAuth2AuthorizationRequestHeaderAuthenticator(string accessToken, string tokenType)
			: base(accessToken)
		{
			_authorizationValue = tokenType + " " + accessToken;
		}

		public override void Authenticate(IRestClient client, IRestRequest request)
		{
			if (!request.Parameters.Any((Parameter p) => p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase)))
			{
				request.AddParameter("Authorization", _authorizationValue, ParameterType.HttpHeader);
			}
		}
	}
}
