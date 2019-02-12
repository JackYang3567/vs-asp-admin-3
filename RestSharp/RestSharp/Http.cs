using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace RestSharp
{
	public class Http : IHttp, IHttpFactory
	{
		private class TimeOutState
		{
			public bool TimedOut
			{
				get;
				set;
			}

			public HttpWebRequest Request
			{
				get;
				set;
			}
		}

		private const string LINE_BREAK = "\r\n";

		private const string FORM_BOUNDARY = "-----------------------------28947758029299";

		private TimeOutState timeoutState;

		private Encoding encoding = Encoding.UTF8;

		private readonly IDictionary<string, Action<HttpWebRequest, string>> restrictedHeaderActions;

		protected bool HasParameters
		{
			get
			{
				return Parameters.Any();
			}
		}

		protected bool HasCookies
		{
			get
			{
				return Cookies.Any();
			}
		}

		protected bool HasBody
		{
			get
			{
				return RequestBodyBytes != null || !string.IsNullOrEmpty(RequestBody);
			}
		}

		protected bool HasFiles
		{
			get
			{
				return Files.Any();
			}
		}

		public bool AlwaysMultipartFormData
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

		public ICredentials Credentials
		{
			get;
			set;
		}

		public CookieContainer CookieContainer
		{
			get;
			set;
		}

		public Action<Stream> ResponseWriter
		{
			get;
			set;
		}

		public IList<HttpFile> Files
		{
			get;
			private set;
		}

		public bool FollowRedirects
		{
			get;
			set;
		}

		public X509CertificateCollection ClientCertificates
		{
			get;
			set;
		}

		public int? MaxRedirects
		{
			get;
			set;
		}

		public bool UseDefaultCredentials
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

		public IList<HttpHeader> Headers
		{
			get;
			private set;
		}

		public IList<HttpParameter> Parameters
		{
			get;
			private set;
		}

		public IList<HttpCookie> Cookies
		{
			get;
			private set;
		}

		public string RequestBody
		{
			get;
			set;
		}

		public string RequestContentType
		{
			get;
			set;
		}

		public byte[] RequestBodyBytes
		{
			get;
			set;
		}

		public Uri Url
		{
			get;
			set;
		}

		public bool PreAuthenticate
		{
			get;
			set;
		}

		public IWebProxy Proxy
		{
			get;
			set;
		}

		public HttpWebRequest DeleteAsync(Action<HttpResponse> action)
		{
			return GetStyleMethodInternalAsync("DELETE", action);
		}

		public HttpWebRequest GetAsync(Action<HttpResponse> action)
		{
			return GetStyleMethodInternalAsync("GET", action);
		}

		public HttpWebRequest HeadAsync(Action<HttpResponse> action)
		{
			return GetStyleMethodInternalAsync("HEAD", action);
		}

		public HttpWebRequest OptionsAsync(Action<HttpResponse> action)
		{
			return GetStyleMethodInternalAsync("OPTIONS", action);
		}

		public HttpWebRequest PostAsync(Action<HttpResponse> action)
		{
			return PutPostInternalAsync("POST", action);
		}

		public HttpWebRequest PutAsync(Action<HttpResponse> action)
		{
			return PutPostInternalAsync("PUT", action);
		}

		public HttpWebRequest PatchAsync(Action<HttpResponse> action)
		{
			return PutPostInternalAsync("PATCH", action);
		}

		public HttpWebRequest MergeAsync(Action<HttpResponse> action)
		{
			return PutPostInternalAsync("MERGE", action);
		}

		public HttpWebRequest AsPostAsync(Action<HttpResponse> action, string httpMethod)
		{
			return PutPostInternalAsync(httpMethod.ToUpperInvariant(), action);
		}

		public HttpWebRequest AsGetAsync(Action<HttpResponse> action, string httpMethod)
		{
			return GetStyleMethodInternalAsync(httpMethod.ToUpperInvariant(), action);
		}

		private HttpWebRequest GetStyleMethodInternalAsync(string method, Action<HttpResponse> callback)
		{
			HttpWebRequest httpWebRequest = null;
			try
			{
				Uri url = Url;
				httpWebRequest = ConfigureAsyncWebRequest(method, url);
				if (HasBody && (method == "DELETE" || method == "OPTIONS"))
				{
					httpWebRequest.ContentType = RequestContentType;
					WriteRequestBodyAsync(httpWebRequest, callback);
				}
				else
				{
					timeoutState = new TimeOutState
					{
						Request = httpWebRequest
					};
					IAsyncResult asyncResult = httpWebRequest.BeginGetResponse(delegate(IAsyncResult result)
					{
						ResponseCallback(result, callback);
					}, httpWebRequest);
					SetTimeout(asyncResult, timeoutState);
				}
			}
			catch (Exception ex)
			{
				ExecuteCallback(CreateErrorResponse(ex), callback);
			}
			return httpWebRequest;
		}

		private HttpResponse CreateErrorResponse(Exception ex)
		{
			HttpResponse httpResponse = new HttpResponse();
			WebException ex2 = ex as WebException;
			if (ex2 == null || ex2.Status != WebExceptionStatus.RequestCanceled)
			{
				httpResponse.ErrorMessage = ex.Message;
				httpResponse.ErrorException = ex;
				httpResponse.ResponseStatus = ResponseStatus.Error;
				return httpResponse;
			}
			httpResponse.ResponseStatus = (timeoutState.TimedOut ? ResponseStatus.TimedOut : ResponseStatus.Aborted);
			return httpResponse;
		}

		private HttpWebRequest PutPostInternalAsync(string method, Action<HttpResponse> callback)
		{
			HttpWebRequest httpWebRequest = null;
			try
			{
				httpWebRequest = ConfigureAsyncWebRequest(method, Url);
				PreparePostBody(httpWebRequest);
				WriteRequestBodyAsync(httpWebRequest, callback);
			}
			catch (Exception ex)
			{
				ExecuteCallback(CreateErrorResponse(ex), callback);
			}
			return httpWebRequest;
		}

		private void WriteRequestBodyAsync(HttpWebRequest webRequest, Action<HttpResponse> callback)
		{
			timeoutState = new TimeOutState
			{
				Request = webRequest
			};
			IAsyncResult asyncResult;
			if (HasBody || HasFiles || AlwaysMultipartFormData)
			{
				webRequest.ContentLength = CalculateContentLength();
				asyncResult = webRequest.BeginGetRequestStream(delegate(IAsyncResult result)
				{
					RequestStreamCallback(result, callback);
				}, webRequest);
			}
			else
			{
				asyncResult = webRequest.BeginGetResponse(delegate(IAsyncResult r)
				{
					ResponseCallback(r, callback);
				}, webRequest);
			}
			SetTimeout(asyncResult, timeoutState);
		}

		private long CalculateContentLength()
		{
			if (RequestBodyBytes == null)
			{
				if (HasFiles || AlwaysMultipartFormData)
				{
					long num = 0L;
					foreach (HttpFile file in Files)
					{
						num += Encoding.GetByteCount(GetMultipartFileHeader(file));
						num += file.ContentLength;
						num += Encoding.GetByteCount("\r\n");
					}
					num = Parameters.Aggregate(num, (long current, HttpParameter param) => current + Encoding.GetByteCount(GetMultipartFormData(param)));
					return num + Encoding.GetByteCount(GetMultipartFooter());
				}
				return encoding.GetByteCount(RequestBody);
			}
			return RequestBodyBytes.Length;
		}

		private void RequestStreamCallback(IAsyncResult result, Action<HttpResponse> callback)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
			if (timeoutState.TimedOut)
			{
				HttpResponse httpResponse = new HttpResponse();
				httpResponse.ResponseStatus = ResponseStatus.TimedOut;
				HttpResponse response = httpResponse;
				ExecuteCallback(response, callback);
			}
			else
			{
				try
				{
					using (Stream stream = httpWebRequest.EndGetRequestStream(result))
					{
						if (HasFiles || AlwaysMultipartFormData)
						{
							WriteMultipartFormData(stream);
						}
						else if (RequestBodyBytes != null)
						{
							stream.Write(RequestBodyBytes, 0, RequestBodyBytes.Length);
						}
						else
						{
							WriteStringTo(stream, RequestBody);
						}
					}
				}
				catch (Exception ex)
				{
					ExecuteCallback(CreateErrorResponse(ex), callback);
					return;
				}
				IAsyncResult asyncResult = httpWebRequest.BeginGetResponse(delegate(IAsyncResult r)
				{
					ResponseCallback(r, callback);
				}, httpWebRequest);
				SetTimeout(asyncResult, timeoutState);
			}
		}

		private void SetTimeout(IAsyncResult asyncResult, TimeOutState timeOutState)
		{
			if (Timeout != 0)
			{
				ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, TimeoutCallback, timeOutState, Timeout, true);
			}
		}

		private static void TimeoutCallback(object state, bool timedOut)
		{
			if (timedOut)
			{
				TimeOutState timeOutState = state as TimeOutState;
				if (timeOutState != null)
				{
					lock (timeOutState)
					{
						timeOutState.TimedOut = true;
					}
					if (timeOutState.Request != null)
					{
						timeOutState.Request.Abort();
					}
				}
			}
		}

		private static void GetRawResponseAsync(IAsyncResult result, Action<HttpWebResponse> callback)
		{
			HttpWebResponse httpWebResponse;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
				httpWebResponse = (httpWebRequest.EndGetResponse(result) as HttpWebResponse);
			}
			catch (WebException ex)
			{
				if (ex.Status == WebExceptionStatus.RequestCanceled)
				{
					throw;
				}
				if (!(ex.Response is HttpWebResponse))
				{
					throw;
				}
				httpWebResponse = (ex.Response as HttpWebResponse);
			}
			callback(httpWebResponse);
			if (httpWebResponse != null)
			{
				httpWebResponse.Close();
			}
		}

		private void ResponseCallback(IAsyncResult result, Action<HttpResponse> callback)
		{
			HttpResponse response = new HttpResponse
			{
				ResponseStatus = ResponseStatus.None
			};
			try
			{
				if (timeoutState.TimedOut)
				{
					response.ResponseStatus = ResponseStatus.TimedOut;
					ExecuteCallback(response, callback);
				}
				else
				{
					GetRawResponseAsync(result, delegate(HttpWebResponse webResponse)
					{
						ExtractResponseData(response, webResponse);
						ExecuteCallback(response, callback);
					});
				}
			}
			catch (Exception ex)
			{
				ExecuteCallback(CreateErrorResponse(ex), callback);
			}
		}

		private static void ExecuteCallback(HttpResponse response, Action<HttpResponse> callback)
		{
			PopulateErrorForIncompleteResponse(response);
			callback(response);
		}

		private static void PopulateErrorForIncompleteResponse(HttpResponse response)
		{
			if (response.ResponseStatus != ResponseStatus.Completed && response.ErrorException == null)
			{
				response.ErrorException = response.ResponseStatus.ToWebException();
				response.ErrorMessage = response.ErrorException.Message;
			}
		}

		private void AddAsyncHeaderActions()
		{
		}

		private HttpWebRequest ConfigureAsyncWebRequest(string method, Uri url)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.UseDefaultCredentials = UseDefaultCredentials;
			httpWebRequest.PreAuthenticate = PreAuthenticate;
			AppendHeaders(httpWebRequest);
			AppendCookies(httpWebRequest);
			httpWebRequest.Method = method;
			if (!HasFiles && !AlwaysMultipartFormData)
			{
				httpWebRequest.ContentLength = 0L;
			}
			if (Credentials != null)
			{
				httpWebRequest.Credentials = Credentials;
			}
			if (UserAgent.HasValue())
			{
				httpWebRequest.UserAgent = UserAgent;
			}
			if (ClientCertificates != null)
			{
				httpWebRequest.ClientCertificates.AddRange(ClientCertificates);
			}
			httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
			httpWebRequest.ServicePoint.Expect100Continue = false;
			if (Timeout != 0)
			{
				httpWebRequest.Timeout = Timeout;
			}
			if (ReadWriteTimeout != 0)
			{
				httpWebRequest.ReadWriteTimeout = ReadWriteTimeout;
			}
			if (Proxy != null)
			{
				httpWebRequest.Proxy = Proxy;
			}
			if (FollowRedirects && MaxRedirects.HasValue)
			{
				httpWebRequest.MaximumAutomaticRedirections = MaxRedirects.Value;
			}
			httpWebRequest.AllowAutoRedirect = FollowRedirects;
			return httpWebRequest;
		}

		public IHttp Create()
		{
			return new Http();
		}

		public Http()
		{
			Headers = new List<HttpHeader>();
			Files = new List<HttpFile>();
			Parameters = new List<HttpParameter>();
			Cookies = new List<HttpCookie>();
			restrictedHeaderActions = new Dictionary<string, Action<HttpWebRequest, string>>(StringComparer.OrdinalIgnoreCase);
			AddSharedHeaderActions();
			AddSyncHeaderActions();
		}

		private void AddSharedHeaderActions()
		{
			restrictedHeaderActions.Add("Accept", delegate(HttpWebRequest r, string v)
			{
				r.Accept = v;
			});
			restrictedHeaderActions.Add("Content-Type", delegate(HttpWebRequest r, string v)
			{
				r.ContentType = v;
			});
			restrictedHeaderActions.Add("Date", delegate(HttpWebRequest r, string v)
			{
				DateTime result;
				if (DateTime.TryParse(v, out result))
				{
					r.Date = result;
				}
			});
			restrictedHeaderActions.Add("Host", delegate(HttpWebRequest r, string v)
			{
				r.Host = v;
			});
			restrictedHeaderActions.Add("Range", delegate(HttpWebRequest r, string v)
			{
				AddRange(r, v);
			});
		}

		private static string GetMultipartFormContentType()
		{
			return string.Format("multipart/form-data; boundary={0}", "-----------------------------28947758029299");
		}

		private static string GetMultipartFileHeader(HttpFile file)
		{
			return string.Format("--{0}{4}Content-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"{4}Content-Type: {3}{4}{4}", "-----------------------------28947758029299", file.Name, file.FileName, file.ContentType ?? "application/octet-stream", "\r\n");
		}

		private string GetMultipartFormData(HttpParameter param)
		{
			string format = (param.Name == RequestContentType) ? "--{0}{3}Content-Type: {1}{3}Content-Disposition: form-data; name=\"{1}\"{3}{3}{2}{3}" : "--{0}{3}Content-Disposition: form-data; name=\"{1}\"{3}{3}{2}{3}";
			return string.Format(format, "-----------------------------28947758029299", param.Name, param.Value, "\r\n");
		}

		private static string GetMultipartFooter()
		{
			return string.Format("--{0}--{1}", "-----------------------------28947758029299", "\r\n");
		}

		private void AppendHeaders(HttpWebRequest webRequest)
		{
			foreach (HttpHeader header in Headers)
			{
				if (restrictedHeaderActions.ContainsKey(header.Name))
				{
					restrictedHeaderActions[header.Name](webRequest, header.Value);
				}
				else
				{
					webRequest.Headers.Add(header.Name, header.Value);
				}
			}
		}

		private void AppendCookies(HttpWebRequest webRequest)
		{
			webRequest.CookieContainer = (CookieContainer ?? new CookieContainer());
			foreach (HttpCookie cooky in Cookies)
			{
				Cookie cookie = new Cookie();
				cookie.Name = cooky.Name;
				cookie.Value = cooky.Value;
				cookie.Domain = webRequest.RequestUri.Host;
				Cookie cookie2 = cookie;
				webRequest.CookieContainer.Add(cookie2);
			}
		}

		private string EncodeParameters()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (HttpParameter parameter in Parameters)
			{
				if (stringBuilder.Length > 1)
				{
					stringBuilder.Append("&");
				}
				stringBuilder.AppendFormat("{0}={1}", parameter.Name.UrlEncode(), parameter.Value.UrlEncode());
			}
			return stringBuilder.ToString();
		}

		private void PreparePostBody(HttpWebRequest webRequest)
		{
			if (HasFiles || AlwaysMultipartFormData)
			{
				webRequest.ContentType = GetMultipartFormContentType();
			}
			else if (HasParameters)
			{
				webRequest.ContentType = "application/x-www-form-urlencoded";
				RequestBody = EncodeParameters();
			}
			else if (HasBody)
			{
				webRequest.ContentType = RequestContentType;
			}
		}

		private void WriteStringTo(Stream stream, string toWrite)
		{
			byte[] bytes = Encoding.GetBytes(toWrite);
			stream.Write(bytes, 0, bytes.Length);
		}

		private void WriteMultipartFormData(Stream requestStream)
		{
			foreach (HttpParameter parameter in Parameters)
			{
				WriteStringTo(requestStream, GetMultipartFormData(parameter));
			}
			foreach (HttpFile file in Files)
			{
				WriteStringTo(requestStream, GetMultipartFileHeader(file));
				file.Writer(requestStream);
				WriteStringTo(requestStream, "\r\n");
			}
			WriteStringTo(requestStream, GetMultipartFooter());
		}

		private void ExtractResponseData(HttpResponse response, HttpWebResponse webResponse)
		{
			using (webResponse)
			{
				response.ContentEncoding = webResponse.ContentEncoding;
				response.Server = webResponse.Server;
				response.ContentType = webResponse.ContentType;
				response.ContentLength = webResponse.ContentLength;
				Stream responseStream = webResponse.GetResponseStream();
				ProcessResponseStream(responseStream, response);
				response.StatusCode = webResponse.StatusCode;
				response.StatusDescription = webResponse.StatusDescription;
				response.ResponseUri = webResponse.ResponseUri;
				response.ResponseStatus = ResponseStatus.Completed;
				if (webResponse.Cookies != null)
				{
					foreach (Cookie cooky in webResponse.Cookies)
					{
						response.Cookies.Add(new HttpCookie
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
				}
				string[] allKeys = webResponse.Headers.AllKeys;
				foreach (string name in allKeys)
				{
					string value = webResponse.Headers[name];
					response.Headers.Add(new HttpHeader
					{
						Name = name,
						Value = value
					});
				}
				webResponse.Close();
			}
		}

		private void ProcessResponseStream(Stream webResponseStream, HttpResponse response)
		{
			if (ResponseWriter == null)
			{
				response.RawBytes = webResponseStream.ReadAsBytes();
			}
			else
			{
				ResponseWriter(webResponseStream);
			}
		}

		private void AddRange(HttpWebRequest r, string range)
		{
			Match match = Regex.Match(range, "=(\\d+)-(\\d+)$");
			if (match.Success)
			{
				int from = Convert.ToInt32(match.Groups[1].Value);
				int to = Convert.ToInt32(match.Groups[2].Value);
				r.AddRange(from, to);
			}
		}

		public HttpResponse Post()
		{
			return PostPutInternal("POST");
		}

		public HttpResponse Put()
		{
			return PostPutInternal("PUT");
		}

		public HttpResponse Get()
		{
			return GetStyleMethodInternal("GET");
		}

		public HttpResponse Head()
		{
			return GetStyleMethodInternal("HEAD");
		}

		public HttpResponse Options()
		{
			return GetStyleMethodInternal("OPTIONS");
		}

		public HttpResponse Delete()
		{
			return GetStyleMethodInternal("DELETE");
		}

		public HttpResponse Patch()
		{
			return PostPutInternal("PATCH");
		}

		public HttpResponse Merge()
		{
			return PostPutInternal("MERGE");
		}

		public HttpResponse AsGet(string httpMethod)
		{
			return GetStyleMethodInternal(httpMethod.ToUpperInvariant());
		}

		public HttpResponse AsPost(string httpMethod)
		{
			return PostPutInternal(httpMethod.ToUpperInvariant());
		}

		private HttpResponse GetStyleMethodInternal(string method)
		{
			HttpWebRequest httpWebRequest = ConfigureWebRequest(method, Url);
			if (HasBody && (method == "DELETE" || method == "OPTIONS"))
			{
				httpWebRequest.ContentType = RequestContentType;
				WriteRequestBody(httpWebRequest);
			}
			return GetResponse(httpWebRequest);
		}

		private HttpResponse PostPutInternal(string method)
		{
			HttpWebRequest httpWebRequest = ConfigureWebRequest(method, Url);
			PreparePostData(httpWebRequest);
			WriteRequestBody(httpWebRequest);
			return GetResponse(httpWebRequest);
		}

		private void AddSyncHeaderActions()
		{
			restrictedHeaderActions.Add("Connection", delegate(HttpWebRequest r, string v)
			{
				r.Connection = v;
			});
			restrictedHeaderActions.Add("Content-Length", delegate(HttpWebRequest r, string v)
			{
				r.ContentLength = Convert.ToInt64(v);
			});
			restrictedHeaderActions.Add("Expect", delegate(HttpWebRequest r, string v)
			{
				r.Expect = v;
			});
			restrictedHeaderActions.Add("If-Modified-Since", delegate(HttpWebRequest r, string v)
			{
				r.IfModifiedSince = Convert.ToDateTime(v);
			});
			restrictedHeaderActions.Add("Referer", delegate(HttpWebRequest r, string v)
			{
				r.Referer = v;
			});
			restrictedHeaderActions.Add("Transfer-Encoding", delegate(HttpWebRequest r, string v)
			{
				r.TransferEncoding = v;
				r.SendChunked = true;
			});
			restrictedHeaderActions.Add("User-Agent", delegate(HttpWebRequest r, string v)
			{
				r.UserAgent = v;
			});
		}

		private void ExtractErrorResponse(HttpResponse httpResponse, Exception ex)
		{
			WebException ex2 = ex as WebException;
			if (ex2 != null && ex2.Status == WebExceptionStatus.Timeout)
			{
				httpResponse.ResponseStatus = ResponseStatus.TimedOut;
				httpResponse.ErrorMessage = ex.Message;
				httpResponse.ErrorException = ex2;
			}
			else
			{
				httpResponse.ErrorMessage = ex.Message;
				httpResponse.ErrorException = ex;
				httpResponse.ResponseStatus = ResponseStatus.Error;
			}
		}

		private HttpResponse GetResponse(HttpWebRequest request)
		{
			HttpResponse httpResponse = new HttpResponse();
			httpResponse.ResponseStatus = ResponseStatus.None;
			HttpResponse httpResponse2 = httpResponse;
			try
			{
				HttpWebResponse rawResponse = GetRawResponse(request);
				ExtractResponseData(httpResponse2, rawResponse);
			}
			catch (Exception ex)
			{
				ExtractErrorResponse(httpResponse2, ex);
			}
			return httpResponse2;
		}

		private static HttpWebResponse GetRawResponse(HttpWebRequest request)
		{
			try
			{
				return (HttpWebResponse)request.GetResponse();
			}
			catch (WebException ex)
			{
				if (!(ex.Response is HttpWebResponse))
				{
					throw;
				}
				return ex.Response as HttpWebResponse;
			}
		}

		private void PreparePostData(HttpWebRequest webRequest)
		{
			if (HasFiles || AlwaysMultipartFormData)
			{
				webRequest.ContentType = GetMultipartFormContentType();
				using (Stream requestStream = webRequest.GetRequestStream())
				{
					WriteMultipartFormData(requestStream);
				}
			}
			PreparePostBody(webRequest);
		}

		private void WriteRequestBody(HttpWebRequest webRequest)
		{
			if (HasBody)
			{
				byte[] array = RequestBodyBytes ?? Encoding.GetBytes(RequestBody);
				webRequest.ContentLength = array.Length;
				using (Stream stream = webRequest.GetRequestStream())
				{
					stream.Write(array, 0, array.Length);
				}
			}
		}

		private HttpWebRequest ConfigureWebRequest(string method, Uri url)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.UseDefaultCredentials = UseDefaultCredentials;
			httpWebRequest.PreAuthenticate = PreAuthenticate;
			httpWebRequest.ServicePoint.Expect100Continue = false;
			AppendHeaders(httpWebRequest);
			AppendCookies(httpWebRequest);
			httpWebRequest.Method = method;
			if (!HasFiles && !AlwaysMultipartFormData)
			{
				httpWebRequest.ContentLength = 0L;
			}
			httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
			if (ClientCertificates != null)
			{
				httpWebRequest.ClientCertificates.AddRange(ClientCertificates);
			}
			if (UserAgent.HasValue())
			{
				httpWebRequest.UserAgent = UserAgent;
			}
			if (Timeout != 0)
			{
				httpWebRequest.Timeout = Timeout;
			}
			if (ReadWriteTimeout != 0)
			{
				httpWebRequest.ReadWriteTimeout = ReadWriteTimeout;
			}
			if (Credentials != null)
			{
				httpWebRequest.Credentials = Credentials;
			}
			if (Proxy != null)
			{
				httpWebRequest.Proxy = Proxy;
			}
			httpWebRequest.AllowAutoRedirect = FollowRedirects;
			if (FollowRedirects && MaxRedirects.HasValue)
			{
				httpWebRequest.MaximumAutomaticRedirections = MaxRedirects.Value;
			}
			return httpWebRequest;
		}
	}
}
