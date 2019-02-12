using System;
using System.Linq;
using System.Text;

namespace RestSharp
{
	public class HttpBasicAuthenticator : IAuthenticator
	{
		private readonly string _authHeader;

		public HttpBasicAuthenticator(string username, string password)
		{
			string arg = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password)));
			_authHeader = string.Format("Basic {0}", arg);
		}

		public void Authenticate(IRestClient client, IRestRequest request)
		{
			if (!request.Parameters.Any((Parameter p) => p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase)))
			{
				request.AddParameter("Authorization", _authHeader, ParameterType.HttpHeader);
			}
		}
	}
}
