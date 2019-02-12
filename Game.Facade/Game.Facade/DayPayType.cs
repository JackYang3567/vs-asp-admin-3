using System.Xml;

namespace Game.Facade
{
	public class DayPayType
	{
		private string path;

		public DayPayType(string path)
		{
			this.path = path;
		}

		public string GetDayPayType(string typeID)
		{
			string result = "未知支付方式";
			XmlReader xmlReader = XmlReader.Create(path);
			while (xmlReader.Read())
			{
				if (xmlReader.HasAttributes && xmlReader.GetAttribute("payTypeID") == typeID)
				{
					xmlReader.Read();
					xmlReader.Read();
					result = xmlReader.ReadString();
					break;
				}
			}
			xmlReader.Close();
			return result;
		}
	}
}
