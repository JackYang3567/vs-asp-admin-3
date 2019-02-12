using System;
using System.Collections.Generic;
using System.Net;

namespace RestSharp
{
	public interface IRestResponse
	{
		IRestRequest Request
		{
			get;
			set;
		}

		string ContentType
		{
			get;
			set;
		}

		long ContentLength
		{
			get;
			set;
		}

		string ContentEncoding
		{
			get;
			set;
		}

		string Content
		{
			get;
			set;
		}

		HttpStatusCode StatusCode
		{
			get;
			set;
		}

		string StatusDescription
		{
			get;
			set;
		}

		byte[] RawBytes
		{
			get;
			set;
		}

		Uri ResponseUri
		{
			get;
			set;
		}

		string Server
		{
			get;
			set;
		}

		IList<RestResponseCookie> Cookies
		{
			get;
		}

		IList<Parameter> Headers
		{
			get;
		}

		ResponseStatus ResponseStatus
		{
			get;
			set;
		}

		string ErrorMessage
		{
			get;
			set;
		}

		Exception ErrorException
		{
			get;
			set;
		}
	}
	public interface IRestResponse<T> : IRestResponse
	{
		T Data
		{
			get;
			set;
		}
	}
}
