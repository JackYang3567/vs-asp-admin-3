using System;
using System.Net;

namespace RestSharp
{
	public class NtlmAuthenticator : IAuthenticator
	{
		private readonly ICredentials credentials;

		public NtlmAuthenticator()
			: this(CredentialCache.DefaultCredentials)
		{
		}

		public NtlmAuthenticator(string username, string password)
			: this(new NetworkCredential(username, password))
		{
		}

		public NtlmAuthenticator(ICredentials credentials)
		{
			if (credentials == null)
			{
				throw new ArgumentNullException("credentials");
			}
			this.credentials = credentials;
		}

		public void Authenticate(IRestClient client, IRestRequest request)
		{
			request.Credentials = credentials;
		}
	}
}
