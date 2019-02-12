using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RestSharp.Serializers
{
	public class DotNetXmlSerializer : ISerializer
	{
		private class EncodingStringWriter : StringWriter
		{
			private readonly Encoding encoding;

			public override Encoding Encoding
			{
				get
				{
					return encoding;
				}
			}

			public EncodingStringWriter(Encoding encoding)
			{
				this.encoding = encoding;
			}
		}

		public string RootElement
		{
			get;
			set;
		}

		public string Namespace
		{
			get;
			set;
		}

		public string DateFormat
		{
			get;
			set;
		}

		public string ContentType
		{
			get;
			set;
		}

		public Encoding Encoding
		{
			get;
			set;
		}

		public DotNetXmlSerializer()
		{
			ContentType = "application/xml";
			Encoding = Encoding.UTF8;
		}

		public DotNetXmlSerializer(string @namespace)
			: this()
		{
			Namespace = @namespace;
		}

		public string Serialize(object obj)
		{
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			xmlSerializerNamespaces.Add(string.Empty, Namespace);
			System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
			EncodingStringWriter encodingStringWriter = new EncodingStringWriter(Encoding);
			xmlSerializer.Serialize(encodingStringWriter, obj, xmlSerializerNamespaces);
			return encodingStringWriter.ToString();
		}
	}
}
