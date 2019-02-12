using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace RestSharp
{
	public interface IRestRequest
	{
		bool AlwaysMultipartFormData
		{
			get;
			set;
		}

		ISerializer JsonSerializer
		{
			get;
			set;
		}

		ISerializer XmlSerializer
		{
			get;
			set;
		}

		Action<Stream> ResponseWriter
		{
			get;
			set;
		}

		List<Parameter> Parameters
		{
			get;
		}

		List<FileParameter> Files
		{
			get;
		}

		Method Method
		{
			get;
			set;
		}

		string Resource
		{
			get;
			set;
		}

		DataFormat RequestFormat
		{
			get;
			set;
		}

		string RootElement
		{
			get;
			set;
		}

		string DateFormat
		{
			get;
			set;
		}

		string XmlNamespace
		{
			get;
			set;
		}

		ICredentials Credentials
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

		int Attempts
		{
			get;
		}

		bool UseDefaultCredentials
		{
			get;
			set;
		}

		Action<IRestResponse> OnBeforeDeserialization
		{
			get;
			set;
		}

		IRestRequest AddFile(string name, string path);

		IRestRequest AddFile(string name, byte[] bytes, string fileName);

		IRestRequest AddFile(string name, byte[] bytes, string fileName, string contentType);

		IRestRequest AddBody(object obj, string xmlNamespace);

		IRestRequest AddBody(object obj);

		IRestRequest AddJsonBody(object obj);

		IRestRequest AddXmlBody(object obj);

		IRestRequest AddXmlBody(object obj, string xmlNamespace);

		IRestRequest AddObject(object obj, params string[] includedProperties);

		IRestRequest AddObject(object obj);

		IRestRequest AddParameter(Parameter p);

		IRestRequest AddParameter(string name, object value);

		IRestRequest AddParameter(string name, object value, ParameterType type);

		IRestRequest AddHeader(string name, string value);

		IRestRequest AddCookie(string name, string value);

		IRestRequest AddUrlSegment(string name, string value);

		IRestRequest AddQueryParameter(string name, string value);

		void IncreaseNumAttempts();
	}
}
