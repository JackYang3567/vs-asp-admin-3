namespace RestSharp
{
	public interface IAuthenticator
	{
		void Authenticate(IRestClient client, IRestRequest request);
	}
}
