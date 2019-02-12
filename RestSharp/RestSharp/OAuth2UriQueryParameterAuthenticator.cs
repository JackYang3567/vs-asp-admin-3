namespace RestSharp
{
	public class OAuth2UriQueryParameterAuthenticator : OAuth2Authenticator
	{
		public OAuth2UriQueryParameterAuthenticator(string accessToken)
			: base(accessToken)
		{
		}

		public override void Authenticate(IRestClient client, IRestRequest request)
		{
			request.AddParameter("oauth_token", base.AccessToken, ParameterType.GetOrPost);
		}
	}
}
