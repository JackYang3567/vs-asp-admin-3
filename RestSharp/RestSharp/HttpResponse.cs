using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Net;

namespace RestSharp
{
	public class HttpResponse : IHttpResponse
	{
		private string _content;

		private ResponseStatus _responseStatus = ResponseStatus.None;

		public string ContentType
		{
			get;
			set;
		}

		public long ContentLength
		{
			get;
			set;
		}

		public string ContentEncoding
		{
			get;
			set;
		}

		public string Content
		{
			get
			{
				return _content ?? (_content = RawBytes.AsString());
			}
		}

		public HttpStatusCode StatusCode
		{
			get;
			set;
		}

		public string StatusDescription
		{
			get;
			set;
		}

		public byte[] RawBytes
		{
			get;
			set;
		}

		public Uri ResponseUri
		{
			get;
			set;
		}

		public string Server
		{
			get;
			set;
		}

		public IList<HttpHeader> Headers
		{
			get;
			private set;
		}

		public IList<HttpCookie> Cookies
		{
			get;
			private set;
		}

		public ResponseStatus ResponseStatus
		{
			get
			{
				return _responseStatus;
			}
			set
			{
				_responseStatus = value;
			}
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public Exception ErrorException
		{
			get;
			set;
		}

		public HttpResponse()
		{
			Headers = new List<HttpHeader>();
			Cookies = new List<HttpCookie>();
		}
	}
}
