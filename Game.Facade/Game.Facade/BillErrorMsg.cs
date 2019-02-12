using System.Xml;

namespace Game.Facade
{
	public class BillErrorMsg
	{
		private string path;

		public BillErrorMsg(string path)
		{
			this.path = path;
		}

		public string GetBillErrorMsgByErrorID(string errorID)
		{
			string result = "未知错误";
			XmlReader xmlReader = XmlReader.Create(path);
			while (xmlReader.Read())
			{
				if (xmlReader.HasAttributes && xmlReader.GetAttribute("ErrorID") == errorID)
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
