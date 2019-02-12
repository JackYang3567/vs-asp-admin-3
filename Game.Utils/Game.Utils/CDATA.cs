using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Game.Utils
{
	public class CDATA : IXmlSerializable
	{
		private string text;

		public string Text
		{
			get
			{
				return text;
			}
		}

		public CDATA()
		{
		}

		public CDATA(string text)
		{
			this.text = text;
		}

		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			text = reader.ReadElementContentAsString();
		}

		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteCData(text);
		}
	}
}
