using RestSharp.Deserializers;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestSharp
{
	public class RestClient : IRestClient
	{
		private static readonly Version version = new AssemblyName(Assembly.GetExecutingAssembly().FullName).Version;

		public IHttpFactory HttpFactory = new SimpleFactory<Http>();

		private Encoding encoding = Encoding.UTF8;

		public int? MaxRedirects
		{
			get;
			set;
		}

		public X509CertificateCollection ClientCertificates
		{
			get;
			set;
		}

		public IWebProxy Proxy
		{
			get;
			set;
		}

		public bool FollowRedirects
		{
			get;
			set;
		}

		public CookieContainer CookieContainer
		{
			get;
			set;
		}

		public string UserAgent
		{
			get;
			set;
		}

		public int Timeout
		{
			get;
			set;
		}

		public int ReadWriteTimeout
		{
			get;
			set;
		}

		public bool UseSynchronizationContext
		{
			get;
			set;
		}

		public IAuthenticator Authenticator
		{
			get;
			set;
		}

		public virtual Uri BaseUrl
		{
			get;
			set;
		}

		public Encoding Encoding
		{
			get
			{
				return encoding;
			}
			set
			{
				encoding = value;
			}
		}

		public bool PreAuthenticate
		{
			get;
			set;
		}

		private IDictionary<string, IDeserializer> ContentHandlers
		{
			get;
			set;
		}

		private IList<string> AcceptTypes
		{
			get;
			set;
		}

		public IList<Parameter> DefaultParameters
		{
			get;
			private set;
		}

		public byte[] DownloadData(IRestRequest request)
		{
			IRestResponse restResponse = Execute(request);
			return restResponse.RawBytes;
		}

		public virtual IRestResponse Execute(IRestRequest request)
		{
			string name = Enum.GetName(typeof(Method), request.Method);
			switch (request.Method)
			{
			case Method.POST:
			case Method.PUT:
			case Method.PATCH:
			case Method.MERGE:
				return Execute(request, name, DoExecuteAsPost);
			default:
				return Execute(request, name, DoExecuteAsGet);
			}
		}

		public IRestResponse ExecuteAsGet(IRestRequest request, string httpMethod)
		{
			return Execute(request, httpMethod, DoExecuteAsGet);
		}

		public IRestResponse ExecuteAsPost(IRestRequest request, string httpMethod)
		{
			request.Method = Method.POST;
			return Execute(request, httpMethod, DoExecuteAsPost);
		}

		private IRestResponse Execute(IRestRequest request, string httpMethod, Func<IHttp, string, HttpResponse> getResponse)
		{
			AuthenticateIfNeeded(this, request);
			IRestResponse restResponse = new RestResponse();
			try
			{
				IHttp http = HttpFactory.Create();
				ConfigureHttp(request, http);
				restResponse = ConvertToRestResponse(request, getResponse(http, httpMethod));
				restResponse.Request = request;
				restResponse.Request.IncreaseNumAttempts();
			}
			catch (Exception ex)
			{
				restResponse.ResponseStatus = ResponseStatus.Error;
				restResponse.ErrorMessage = ex.Message;
				restResponse.ErrorException = ex;
			}
			return restResponse;
		}

		private static HttpResponse DoExecuteAsGet(IHttp http, string method)
		{
			return http.AsGet(method);
		}

		private static HttpResponse DoExecuteAsPost(IHttp http, string method)
		{
			return http.AsPost(method);
		}

		public virtual IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
		{
			return Deserialize<T>(request, Execute(request));
		}

		public IRestResponse<T> ExecuteAsGet<T>(IRestRequest request, string httpMethod) where T : new()
		{
			return Deserialize<T>(request, ExecuteAsGet(request, httpMethod));
		}

		public IRestResponse<T> ExecuteAsPost<T>(IRestRequest request, string httpMethod) where T : new()
		{
			return Deserialize<T>(request, ExecuteAsPost(request, httpMethod));
		}

		public RestClient()
		{
			ContentHandlers = new Dictionary<string, IDeserializer>();
			AcceptTypes = new List<string>();
			DefaultParameters = new List<Parameter>();
			AddHandler("application/json", new JsonDeserializer());
			AddHandler("application/xml", new XmlDeserializer());
			AddHandler("text/json", new JsonDeserializer());
			AddHandler("text/x-json", new JsonDeserializer());
			AddHandler("text/javascript", new JsonDeserializer());
			AddHandler("text/xml", new XmlDeserializer());
			AddHandler("*", new XmlDeserializer());
			FollowRedirects = true;
		}

		public RestClient(Uri baseUrl)
			: this()
		{
			BaseUrl = baseUrl;
		}

		public RestClient(string baseUrl)
			: this()
		{
			if (string.IsNullOrEmpty(baseUrl))
			{
				throw new ArgumentNullException("baseUrl");
			}
			BaseUrl = new Uri(baseUrl);
		}

		public void AddHandler(string contentType, IDeserializer deserializer)
		{
			ContentHandlers[contentType] = deserializer;
			if (contentType != "*")
			{
				AcceptTypes.Add(contentType);
				string value = string.Join(", ", AcceptTypes.ToArray());
				this.RemoveDefaultParameter("Accept");
				this.AddDefaultParameter("Accept", value, ParameterType.HttpHeader);
			}
		}

		public void RemoveHandler(string contentType)
		{
			ContentHandlers.Remove(contentType);
			AcceptTypes.Remove(contentType);
			this.RemoveDefaultParameter("Accept");
		}

		public void ClearHandlers()
		{
			ContentHandlers.Clear();
			AcceptTypes.Clear();
			this.RemoveDefaultParameter("Accept");
		}

		private IDeserializer GetHandler(string contentType)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (!string.IsNullOrEmpty(contentType) || !ContentHandlers.ContainsKey("*"))
			{
				int num = contentType.IndexOf(';');
				if (num > -1)
				{
					contentType = contentType.Substring(0, num);
				}
				IDeserializer result = null;
				if (ContentHandlers.ContainsKey(contentType))
				{
					result = ContentHandlers[contentType];
				}
				else if (ContentHandlers.ContainsKey("*"))
				{
					result = ContentHandlers["*"];
				}
				return result;
			}
			return ContentHandlers["*"];
		}

		private void AuthenticateIfNeeded(RestClient client, IRestRequest request)
		{
			if (Authenticator != null)
			{
				Authenticator.Authenticate(client, request);
			}
		}

		public Uri BuildUri(IRestRequest request)
		{
			if (BaseUrl == (Uri)null)
			{
				throw new NullReferenceException("RestClient must contain a value for BaseUrl");
			}
			string text = request.Resource;
			IEnumerable<Parameter> enumerable = from p in request.Parameters
			where p.Type == ParameterType.UrlSegment
			select p;
			UriBuilder uriBuilder = new UriBuilder(BaseUrl);
			foreach (Parameter item in enumerable)
			{
				if (item.Value == null)
				{
					throw new ArgumentException(string.Format("Cannot build uri when url segment parameter '{0}' value is null.", item.Name), "request");
				}
				if (!string.IsNullOrEmpty(text))
				{
					text = text.Replace("{" + item.Name + "}", item.Value.ToString().UrlEncode());
				}
				uriBuilder.Path = uriBuilder.Path.UrlDecode().Replace("{" + item.Name + "}", item.Value.ToString().UrlEncode());
			}
			BaseUrl = new Uri(uriBuilder.ToString());
			if (!string.IsNullOrEmpty(text) && text.StartsWith("/"))
			{
				text = text.Substring(1);
			}
			if (BaseUrl != (Uri)null && !string.IsNullOrEmpty(BaseUrl.AbsoluteUri))
			{
				if (!BaseUrl.AbsoluteUri.EndsWith("/") && !string.IsNullOrEmpty(text))
				{
					text = "/" + text;
				}
				text = (string.IsNullOrEmpty(text) ? BaseUrl.AbsoluteUri : string.Format("{0}{1}", BaseUrl, text));
			}
			IEnumerable<Parameter> enumerable2 = (request.Method == Method.POST || request.Method == Method.PUT || request.Method == Method.PATCH) ? (from p in request.Parameters
			where p.Type == ParameterType.QueryString
			select p).ToList() : (from p in request.Parameters
			where p.Type == ParameterType.GetOrPost || p.Type == ParameterType.QueryString
			select p).ToList();
			if (enumerable2.Any())
			{
				string str = EncodeParameters(enumerable2);
				string str2 = text.Contains("?") ? "&" : "?";
				text = text + str2 + str;
				return new Uri(text);
			}
			return new Uri(text);
		}

		private static string EncodeParameters(IEnumerable<Parameter> parameters)
		{
			return string.Join("&", parameters.Select(EncodeParameter).ToArray());
		}

		private static string EncodeParameter(Parameter parameter)
		{
			return (parameter.Value == null) ? (parameter.Name.UrlEncode() + "=") : (parameter.Name.UrlEncode() + "=" + parameter.Value.ToString().UrlEncode());
		}

		private void ConfigureHttp(IRestRequest request, IHttp http)
		{
			http.Encoding = Encoding;
			http.AlwaysMultipartFormData = request.AlwaysMultipartFormData;
			http.UseDefaultCredentials = request.UseDefaultCredentials;
			http.ResponseWriter = request.ResponseWriter;
			http.CookieContainer = CookieContainer;
			using (IEnumerator<Parameter> enumerator = DefaultParameters.GetEnumerator())
			{
				Parameter p3;
				while (enumerator.MoveNext())
				{
					p3 = enumerator.Current;
					if (!request.Parameters.Any((Parameter p2) => p2.Name == p3.Name && p2.Type == p3.Type))
					{
						request.AddParameter(p3);
					}
				}
			}
			if (request.Parameters.All((Parameter p2) => p2.Name.ToLowerInvariant() != "accept"))
			{
				string value = string.Join(", ", AcceptTypes.ToArray());
				request.AddParameter("Accept", value, ParameterType.HttpHeader);
			}
			http.Url = BuildUri(request);
			http.PreAuthenticate = PreAuthenticate;
			string text = UserAgent ?? http.UserAgent;
			http.UserAgent = (text.HasValue() ? text : ("RestSharp/" + version));
			int num = (request.Timeout > 0) ? request.Timeout : Timeout;
			if (num > 0)
			{
				http.Timeout = num;
			}
			int num2 = (request.ReadWriteTimeout > 0) ? request.ReadWriteTimeout : ReadWriteTimeout;
			if (num2 > 0)
			{
				http.ReadWriteTimeout = num2;
			}
			http.FollowRedirects = FollowRedirects;
			if (ClientCertificates != null)
			{
				http.ClientCertificates = ClientCertificates;
			}
			http.MaxRedirects = MaxRedirects;
			if (request.Credentials != null)
			{
				http.Credentials = request.Credentials;
			}
			IEnumerable<HttpHeader> enumerable = from p in request.Parameters
			where p.Type == ParameterType.HttpHeader
			select new HttpHeader
			{
				Name = p.Name,
				Value = Convert.ToString(p.Value)
			};
			foreach (HttpHeader item in enumerable)
			{
				http.Headers.Add(item);
			}
			IEnumerable<HttpCookie> enumerable2 = from p in request.Parameters
			where p.Type == ParameterType.Cookie
			select new HttpCookie
			{
				Name = p.Name,
				Value = Convert.ToString(p.Value)
			};
			foreach (HttpCookie item2 in enumerable2)
			{
				http.Cookies.Add(item2);
			}
			IEnumerable<HttpParameter> enumerable3 = from p in request.Parameters
			where p.Type == ParameterType.GetOrPost && p.Value != null
			select new HttpParameter
			{
				Name = p.Name,
				Value = Convert.ToString(p.Value)
			};
			foreach (HttpParameter item3 in enumerable3)
			{
				http.Parameters.Add(item3);
			}
			foreach (FileParameter file in request.Files)
			{
				http.Files.Add(new HttpFile
				{
					Name = file.Name,
					ContentType = file.ContentType,
					Writer = file.Writer,
					FileName = file.FileName,
					ContentLength = file.ContentLength
				});
			}
			Parameter parameter = (from p in request.Parameters
			where p.Type == ParameterType.RequestBody
			select p).FirstOrDefault();
			if (parameter != null)
			{
				http.RequestContentType = parameter.Name;
				if (!http.Files.Any())
				{
					object value2 = parameter.Value;
					if (value2 is byte[])
					{
						http.RequestBodyBytes = (byte[])value2;
					}
					else
					{
						http.RequestBody = Convert.ToString(parameter.Value);
					}
				}
				else
				{
					http.Parameters.Add(new HttpParameter
					{
						Name = parameter.Name,
						Value = Convert.ToString(parameter.Value)
					});
				}
			}
			ConfigureProxy(http);
		}

		private void ConfigureProxy(IHttp http)
		{
			if (Proxy != null)
			{
				http.Proxy = Proxy;
			}
		}

		private static RestResponse ConvertToRestResponse(IRestRequest request, HttpResponse httpResponse)
		{
			RestResponse restResponse = new RestResponse();
			restResponse.Content = httpResponse.Content;
			restResponse.ContentEncoding = httpResponse.ContentEncoding;
			restResponse.ContentLength = httpResponse.ContentLength;
			restResponse.ContentType = httpResponse.ContentType;
			restResponse.ErrorException = httpResponse.ErrorException;
			restResponse.ErrorMessage = httpResponse.ErrorMessage;
			restResponse.RawBytes = httpResponse.RawBytes;
			restResponse.ResponseStatus = httpResponse.ResponseStatus;
			restResponse.ResponseUri = httpResponse.ResponseUri;
			restResponse.Server = httpResponse.Server;
			restResponse.StatusCode = httpResponse.StatusCode;
			restResponse.StatusDescription = httpResponse.StatusDescription;
			restResponse.Request = request;
			RestResponse restResponse2 = restResponse;
			foreach (HttpHeader header in httpResponse.Headers)
			{
				restResponse2.Headers.Add(new Parameter
				{
					Name = header.Name,
					Value = header.Value,
					Type = ParameterType.HttpHeader
				});
			}
			foreach (HttpCookie cooky in httpResponse.Cookies)
			{
				restResponse2.Cookies.Add(new RestResponseCookie
				{
					Comment = cooky.Comment,
					CommentUri = cooky.CommentUri,
					Discard = cooky.Discard,
					Domain = cooky.Domain,
					Expired = cooky.Expired,
					Expires = cooky.Expires,
					HttpOnly = cooky.HttpOnly,
					Name = cooky.Name,
					Path = cooky.Path,
					Port = cooky.Port,
					Secure = cooky.Secure,
					TimeStamp = cooky.TimeStamp,
					Value = cooky.Value,
					Version = cooky.Version
				});
			}
			return restResponse2;
		}

		private IRestResponse<T> Deserialize<T>(IRestRequest request, IRestResponse raw)
		{
			request.OnBeforeDeserialization(raw);
			IRestResponse<T> restResponse = new RestResponse<T>();
			try
			{
				restResponse = raw.ToAsyncResponse<T>();
				restResponse.Request = request;
				if (restResponse.ErrorException == null)
				{
					IDeserializer handler = GetHandler(raw.ContentType);
					if (handler != null)
					{
						handler.RootElement = request.RootElement;
						handler.DateFormat = request.DateFormat;
						handler.Namespace = request.XmlNamespace;
						restResponse.Data = handler.Deserialize<T>(raw);
					}
				}
			}
			catch (Exception ex)
			{
				restResponse.ResponseStatus = ResponseStatus.Error;
				restResponse.ErrorMessage = ex.Message;
				restResponse.ErrorException = ex;
			}
			return restResponse;
		}

		public virtual RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback)
		{
			string name = Enum.GetName(typeof(Method), request.Method);
			switch (request.Method)
			{
			case Method.POST:
			case Method.PUT:
			case Method.PATCH:
			case Method.MERGE:
				return ExecuteAsync(request, callback, name, DoAsPostAsync);
			default:
				return ExecuteAsync(request, callback, name, DoAsGetAsync);
			}
		}

		public virtual RestRequestAsyncHandle ExecuteAsyncGet(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
		{
			return ExecuteAsync(request, callback, httpMethod, DoAsGetAsync);
		}

		public virtual RestRequestAsyncHandle ExecuteAsyncPost(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod)
		{
			request.Method = Method.POST;
			return ExecuteAsync(request, callback, httpMethod, DoAsPostAsync);
		}

		private RestRequestAsyncHandle ExecuteAsync(IRestRequest request, Action<IRestResponse, RestRequestAsyncHandle> callback, string httpMethod, Func<IHttp, Action<HttpResponse>, string, HttpWebRequest> getWebRequest)
		{
			IHttp http = HttpFactory.Create();
			AuthenticateIfNeeded(this, request);
			ConfigureHttp(request, http);
			RestRequestAsyncHandle asyncHandle = new RestRequestAsyncHandle();
			Action<HttpResponse> action = delegate(HttpResponse r)
			{
				ProcessResponse(request, r, asyncHandle, callback);
			};
			if (UseSynchronizationContext && SynchronizationContext.Current != null)
			{
				SynchronizationContext ctx = SynchronizationContext.Current;
				Action<HttpResponse> cb = action;
				action = delegate(HttpResponse resp)
				{
					ctx.Post(delegate
					{
						cb(resp);
					}, null);
				};
			}
			asyncHandle.WebRequest = getWebRequest(http, action, httpMethod);
			return asyncHandle;
		}

		private static HttpWebRequest DoAsGetAsync(IHttp http, Action<HttpResponse> response_cb, string method)
		{
			return http.AsGetAsync(response_cb, method);
		}

		private static HttpWebRequest DoAsPostAsync(IHttp http, Action<HttpResponse> response_cb, string method)
		{
			return http.AsPostAsync(response_cb, method);
		}

		private void ProcessResponse(IRestRequest request, HttpResponse httpResponse, RestRequestAsyncHandle asyncHandle, Action<IRestResponse, RestRequestAsyncHandle> callback)
		{
			RestResponse arg = ConvertToRestResponse(request, httpResponse);
			callback(arg, asyncHandle);
		}

		public virtual RestRequestAsyncHandle ExecuteAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback)
		{
			return ExecuteAsync(request, delegate(IRestResponse response, RestRequestAsyncHandle asyncHandle)
			{
				this.DeserializeResponse<T>(request, callback, response, asyncHandle);
			});
		}

		public virtual RestRequestAsyncHandle ExecuteAsyncGet<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
		{
			return ExecuteAsyncGet(request, delegate(IRestResponse response, RestRequestAsyncHandle asyncHandle)
			{
				this.DeserializeResponse<T>(request, callback, response, asyncHandle);
			}, httpMethod);
		}

		public virtual RestRequestAsyncHandle ExecuteAsyncPost<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, string httpMethod)
		{
			return ExecuteAsyncPost(request, delegate(IRestResponse response, RestRequestAsyncHandle asyncHandle)
			{
				this.DeserializeResponse<T>(request, callback, response, asyncHandle);
			}, httpMethod);
		}

		private void DeserializeResponse<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback, IRestResponse response, RestRequestAsyncHandle asyncHandle)
		{
			IRestResponse<T> arg;
			try
			{
				arg = Deserialize<T>(request, response);
			}
			catch (Exception ex)
			{
				RestResponse<T> restResponse = new RestResponse<T>();
				restResponse.Request = request;
				restResponse.ResponseStatus = ResponseStatus.Error;
				restResponse.ErrorMessage = ex.Message;
				restResponse.ErrorException = ex;
				arg = restResponse;
			}
			callback(arg, asyncHandle);
		}

		public virtual Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request)
		{
			return ExecuteGetTaskAsync<T>(request, CancellationToken.None);
		}

		public virtual Task<IRestResponse<T>> ExecuteGetTaskAsync<T>(IRestRequest request, CancellationToken token)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			request.Method = Method.GET;
			return ExecuteTaskAsync<T>(request, token);
		}

		public virtual Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request)
		{
			return ExecutePostTaskAsync<T>(request, CancellationToken.None);
		}

		public virtual Task<IRestResponse<T>> ExecutePostTaskAsync<T>(IRestRequest request, CancellationToken token)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			request.Method = Method.POST;
			return ExecuteTaskAsync<T>(request, token);
		}

		public virtual Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
		{
			return ExecuteTaskAsync<T>(request, CancellationToken.None);
		}

		public virtual Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request, CancellationToken token)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			TaskCompletionSource<IRestResponse<T>> taskCompletionSource = (TaskCompletionSource<IRestResponse<T>>)new TaskCompletionSource<IRestResponse<T>>();
			try
			{
				RestRequestAsyncHandle async = ExecuteAsync(request, delegate(IRestResponse<T> response, RestRequestAsyncHandle _)
				{
					if (token.IsCancellationRequested)
					{
						((TaskCompletionSource<IRestResponse<T>>)taskCompletionSource).TrySetCanceled();
					}
					else
					{
						((TaskCompletionSource<IRestResponse<T>>)taskCompletionSource).TrySetResult(response);
					}
				});
				token.Register(delegate
				{
					async.Abort();
					((TaskCompletionSource<IRestResponse<T>>)taskCompletionSource).TrySetCanceled();
				});
			}
			catch (Exception exception)
			{
				((TaskCompletionSource<IRestResponse<T>>)taskCompletionSource).TrySetException(exception);
			}
			return ((TaskCompletionSource<IRestResponse<T>>)taskCompletionSource).Task;
		}

		public virtual Task<IRestResponse> ExecuteTaskAsync(IRestRequest request)
		{
			return ExecuteTaskAsync(request, CancellationToken.None);
		}

		public virtual Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request)
		{
			return ExecuteGetTaskAsync(request, CancellationToken.None);
		}

		public virtual Task<IRestResponse> ExecuteGetTaskAsync(IRestRequest request, CancellationToken token)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			request.Method = Method.GET;
			return ExecuteTaskAsync(request, token);
		}

		public virtual Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request)
		{
			return ExecutePostTaskAsync(request, CancellationToken.None);
		}

		public virtual Task<IRestResponse> ExecutePostTaskAsync(IRestRequest request, CancellationToken token)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			request.Method = Method.POST;
			return ExecuteTaskAsync(request, token);
		}

		public virtual Task<IRestResponse> ExecuteTaskAsync(IRestRequest request, CancellationToken token)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			TaskCompletionSource<IRestResponse> taskCompletionSource = new TaskCompletionSource<IRestResponse>();
			try
			{
				RestRequestAsyncHandle async = ExecuteAsync(request, delegate(IRestResponse response, RestRequestAsyncHandle _)
				{
					if (token.IsCancellationRequested)
					{
						taskCompletionSource.TrySetCanceled();
					}
					else
					{
						taskCompletionSource.TrySetResult(response);
					}
				});
				token.Register(delegate
				{
					async.Abort();
					taskCompletionSource.TrySetCanceled();
				});
			}
			catch (Exception exception)
			{
				taskCompletionSource.TrySetException(exception);
			}
			return taskCompletionSource.Task;
		}
	}
}
