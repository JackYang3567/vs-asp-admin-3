using System;
using System.IO;

namespace RestSharp
{
	public class FileParameter
	{
		public long ContentLength
		{
			get;
			set;
		}

		public Action<Stream> Writer
		{
			get;
			set;
		}

		public string FileName
		{
			get;
			set;
		}

		public string ContentType
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public static FileParameter Create(string name, byte[] data, string filename, string contentType)
		{
			long longLength = data.LongLength;
			FileParameter fileParameter = new FileParameter();
			fileParameter.Writer = delegate(Stream s)
			{
				s.Write(data, 0, data.Length);
			};
			fileParameter.FileName = filename;
			fileParameter.ContentType = contentType;
			fileParameter.ContentLength = longLength;
			fileParameter.Name = name;
			return fileParameter;
		}

		public static FileParameter Create(string name, byte[] data, string filename)
		{
			return Create(name, data, filename, null);
		}
	}
}
