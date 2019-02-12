using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Net;

namespace RestSharp
{
	public abstract class RestResponseBase
	{
		private string _content;

		private ResponseStatus _responseStatus = ResponseStatus.None;

		public IRestRequest Request
		{
			get;
			set;
		}

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
				if (_content == null)
				{
					_content = RawBytes.AsString();
				}
				return _content;
			}
			set
			{
				_content = value;
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

		public IList<RestResponseCookie> Cookies
		{
			get;
			protected internal set;
		}

		public IList<Parameter> Headers
		{
			get;
			protected internal set;
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

		public RestResponseBase()
		{
			Headers = new List<Parameter>();
			Cookies = new List<RestResponseCookie>();
		}
	}
}
