using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestSharp
{
	public interface IRestClient
	{
		CookieContainer CookieContainer
		{
			get;
			set;
		}

		string UserAgent
		{
			get;
			set;
		}

		int Timeout
		{
			get;
			set;
		}

		int ReadWriteTimeout
		{
			get;
			set;
		}

		bool UseSynchronizationContext
		{
			get;
			set;
		}

		IAuthenticator Authenticator
		{
			get;
			set;
		}

		Uri BaseUrl
		{
			get;
			set;
		}

		Encoding Encoding
		{
			get;
			set;
		}

		bool PreAuthenticate
		{
			get;
			set;
		}

		IList<Parameter> DefaultParameters
		{
			get;
		}

		X509CertificateCollection ClientCertificates
		{
			get;
			set;
		}

		IWebProxy Proxy
		{
			get;
			set;
		}

		RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback);

		RestRequestAsyncHandle ExecuteAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback);

		IRestResponse Execute(IRestRequest request);

		IRestResponse<T> Execute<T>(IRestRequest request) where T : new();

		Uri BuildUri(IRestRequest request);

		RestRequestAsyncHandle ExecuteAsyncGet(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod);

		RestRequestAsyncHandle ExecuteAsyncPost(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod);

		RestRequestAsyncHandle ExecuteAsyncGet<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod);

		RestRequestAsyncHandle ExecuteAsyncPost<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod);

		IRestResponse ExecuteAsGet(IRestRequest request, string httpMethod);

		IRestResponse ExecuteAsPost(IRestRequest request, string httpMethod);

		IRestResponse<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod) where T : new();

		IRestResponse<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod) where T : new();

		Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token);

		Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request);

		Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request);

		Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token);

		Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request);

		Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token);

		Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token);

		Task<IRestResponse> ExecuteTaskAsync(IRestRequest request);

		Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request);

		Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token);

		Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request);

		Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request, CancellationToken token);
	}
}
