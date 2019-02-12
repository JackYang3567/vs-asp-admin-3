namespace RestSharp
{
	public abstract class OAuth2Authenticator : IAuthenticator
	{
		private readonly string _accessToken;

		public string AccessToken
		{
			get
			{
				return _accessToken;
			}
		}

		protected OAuth2Authenticator(string accessToken)
		{
			_accessToken = accessToken;
		}

		public abstract void Authenticate(IRestClient client, IRestRequest request);
	}
}
