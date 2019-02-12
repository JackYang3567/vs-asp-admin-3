using System.IO;

namespace RestSharp.Authenticators.OAuth
{
	internal class HttpPostParameter : WebParameter
	{
		public virtual HttpPostParameterType Type
		{
			get;
			private set;
		}

		public virtual string FileName
		{
			get;
			private set;
		}

		public virtual string FilePath
		{
			get;
			private set;
		}

		public virtual Stream FileStream
		{
			get;
			set;
		}

		public virtual string ContentType
		{
			get;
			private set;
		}

		public HttpPostParameter(string name, string value)
			: base(name, value)
		{
		}

		public static HttpPostParameter CreateFile(string name, string fileName, string filePath, string contentType)
		{
			HttpPostParameter httpPostParameter = new HttpPostParameter(name, string.Empty);
			httpPostParameter.Type = HttpPostParameterType.File;
			httpPostParameter.FileName = fileName;
			httpPostParameter.FilePath = filePath;
			httpPostParameter.ContentType = contentType;
			return httpPostParameter;
		}

		public static HttpPostParameter CreateFile(string name, string fileName, Stream fileStream, string contentType)
		{
			HttpPostParameter httpPostParameter = new HttpPostParameter(name, string.Empty);
			httpPostParameter.Type = HttpPostParameterType.File;
			httpPostParameter.FileName = fileName;
			httpPostParameter.FileStream = fileStream;
			httpPostParameter.ContentType = contentType;
			return httpPostParameter;
		}
	}
}
