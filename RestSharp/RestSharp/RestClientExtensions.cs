using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestSharp
{
	public static class RestClientExtensions
	{
		public static RestRequestAsyncHandle ExecuteAsync(this IRestClient client, IRestRequest request, Action<IRestResponse> callback)
		{
			return client.ExecuteAsync(request, delegate(IRestResponse response, RestRequestAsyncHandle handle)
			{
				callback(response);
			});
		}

		public static RestRequestAsyncHandle ExecuteAsync<T>(this IRestClient client, IRestRequest request, Action<IRestResponse<T>> callback) where T : new()
		{
			return client.ExecuteAsync(request, delegate(IRestResponse<T> response, RestRequestAsyncHandle asyncHandle)
			{
				callback(response);
			});
		}

		public static RestRequestAsyncHandle GetAsync<T>(this IRestClient client, IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback) where T : new()
		{
			request.Method = Method.GET;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle PostAsync<T>(this IRestClient client, IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback) where T : new()
		{
			request.Method = Method.POST;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle PutAsync<T>(this IRestClient client, IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback) where T : new()
		{
			request.Method = Method.PUT;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle HeadAsync<T>(this IRestClient client, IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback) where T : new()
		{
			request.Method = Method.HEAD;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle OptionsAsync<T>(this IRestClient client, IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback) where T : new()
		{
			request.Method = Method.OPTIONS;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle PatchAsync<T>(this IRestClient client, IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback) where T : new()
		{
			request.Method = Method.PATCH;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle DeleteAsync<T>(this IRestClient client, IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback) where T : new()
		{
			request.Method = Method.DELETE;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle GetAsync(this IRestClient client, IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
		{
			request.Method = Method.GET;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle PostAsync(this IRestClient client, IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
		{
			request.Method = Method.POST;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle PutAsync(this IRestClient client, IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
		{
			request.Method = Method.PUT;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle HeadAsync(this IRestClient client, IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
		{
			request.Method = Method.HEAD;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle OptionsAsync(this IRestClient client, IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
		{
			request.Method = Method.OPTIONS;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle PatchAsync(this IRestClient client, IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
		{
			request.Method = Method.PATCH;
			return client.ExecuteAsync(request, callback);
		}

		public static RestRequestAsyncHandle DeleteAsync(this IRestClient client, IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
		{
			request.Method = Method.DELETE;
			return client.ExecuteAsync(request, callback);
		}

		public static Task<T> GetTaskAsync<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			return client.ExecuteGetTaskAsync<T>(request).ContinueWith((Task<IRestResponse<T>> x) => x.Result.Data);
		}

		public static Task<T> PostTaskAsync<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			return client.ExecutePostTaskAsync<T>(request).ContinueWith((Task<IRestResponse<T>> x) => x.Result.Data);
		}

		public static Task<T> PutTaskAsync<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.PUT;
			return client.ExecuteTaskAsync<T>(request).ContinueWith((Task<IRestResponse<T>> x) => x.Result.Data);
		}

		public static Task<T> HeadTaskAsync<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.HEAD;
			return client.ExecuteTaskAsync<T>(request).ContinueWith((Task<IRestResponse<T>> x) => x.Result.Data);
		}

		public static Task<T> OptionsTaskAsync<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.OPTIONS;
			return client.ExecuteTaskAsync<T>(request).ContinueWith((Task<IRestResponse<T>> x) => x.Result.Data);
		}

		public static Task<T> PatchTaskAsync<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.PATCH;
			return client.ExecuteTaskAsync<T>(request).ContinueWith((Task<IRestResponse<T>> x) => x.Result.Data);
		}

		public static Task<T> DeleteTaskAsync<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.DELETE;
			return client.ExecuteTaskAsync<T>(request).ContinueWith((Task<IRestResponse<T>> x) => x.Result.Data);
		}

		public static IRestResponse<T> Get<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.GET;
			return client.Execute<T>(request);
		}

		public static IRestResponse<T> Post<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.POST;
			return client.Execute<T>(request);
		}

		public static IRestResponse<T> Put<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.PUT;
			return client.Execute<T>(request);
		}

		public static IRestResponse<T> Head<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.HEAD;
			return client.Execute<T>(request);
		}

		public static IRestResponse<T> Options<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.OPTIONS;
			return client.Execute<T>(request);
		}

		public static IRestResponse<T> Patch<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.PATCH;
			return client.Execute<T>(request);
		}

		public static IRestResponse<T> Delete<T>(this IRestClient client, IRestRequest request) where T : new()
		{
			request.Method = Method.DELETE;
			return client.Execute<T>(request);
		}

		public static IRestResponse Get(this IRestClient client, IRestRequest request)
		{
			request.Method = Method.GET;
			return client.Execute(request);
		}

		public static IRestResponse Post(this IRestClient client, IRestRequest request)
		{
			request.Method = Method.POST;
			return client.Execute(request);
		}

		public static IRestResponse Put(this IRestClient client, IRestRequest request)
		{
			request.Method = Method.PUT;
			return client.Execute(request);
		}

		public static IRestResponse Head(this IRestClient client, IRestRequest request)
		{
			request.Method = Method.HEAD;
			return client.Execute(request);
		}

		public static IRestResponse Options(this IRestClient client, IRestRequest request)
		{
			request.Method = Method.OPTIONS;
			return client.Execute(request);
		}

		public static IRestResponse Patch(this IRestClient client, IRestRequest request)
		{
			request.Method = Method.PATCH;
			return client.Execute(request);
		}

		public static IRestResponse Delete(this IRestClient client, IRestRequest request)
		{
			request.Method = Method.DELETE;
			return client.Execute(request);
		}

		public static void AddDefaultParameter(this IRestClient restClient, Parameter p)
		{
			if (p.Type == ParameterType.RequestBody)
			{
				throw new NotSupportedException("Cannot set request body from default headers. Use Request.AddBody() instead.");
			}
			restClient.DefaultParameters.Add(p);
		}

		public static void RemoveDefaultParameter(this IRestClient restClient, string name)
		{
			Parameter parameter = restClient.DefaultParameters.SingleOrDefault((Parameter p) => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
			if (parameter != null)
			{
				restClient.DefaultParameters.Remove(parameter);
			}
		}

		public static void AddDefaultParameter(this IRestClient restClient, string name, object value)
		{
			restClient.AddDefaultParameter(new Parameter
			{
				Name = name,
				Value = value,
				Type = ParameterType.GetOrPost
			});
		}

		public static void AddDefaultParameter(this IRestClient restClient, string name, object value, ParameterType type)
		{
			restClient.AddDefaultParameter(new Parameter
			{
				Name = name,
				Value = value,
				Type = type
			});
		}

		public static void AddDefaultHeader(this IRestClient restClient, string name, string value)
		{
			restClient.AddDefaultParameter(name, value, ParameterType.HttpHeader);
		}

		public static void AddDefaultUrlSegment(this IRestClient restClient, string name, string value)
		{
			restClient.AddDefaultParameter(name, value, ParameterType.UrlSegment);
		}
	}
}
