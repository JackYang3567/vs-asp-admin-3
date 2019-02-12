using System.Xml;

namespace Game.Facade
{
	public class BillBanks
	{
		private string path;

		public BillBanks(string path)
		{
			this.path = path;
		}

		public string GetBillBanksByCode(string bCode)
		{
			string result = "未知银行";
			XmlReader xmlReader = XmlReader.Create(path);
			while (xmlReader.Read())
			{
				if (xmlReader.HasAttributes && xmlReader.GetAttribute("Code") == bCode)
				{
					xmlReader.Read();
					xmlReader.Read();
					result = xmlReader.GetAttribute("CnName");
					break;
				}
			}
			xmlReader.Close();
			return result;
		}
	}
}
