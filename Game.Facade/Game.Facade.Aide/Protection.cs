using System.Collections.Generic;
using System.Data;

namespace Game.Facade.Aide
{
	public class Protection
	{
		private string path;

		public Protection(string path)
		{
			this.path = path;
		}

		public List<string> GetProtectionQuestions()
		{
			List<string> list = new List<string>();
			DataSet dataSet = new DataSet();
			dataSet.ReadXml(path);
			DataRow[] array = dataSet.Tables["Item"].Select("Questions_ID=0");
			DataRow[] array2 = array;
			foreach (DataRow dataRow in array2)
			{
				list.Add(dataRow[0].ToString());
			}
			return list;
		}
	}
}
