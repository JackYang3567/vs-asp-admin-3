using System.Collections.Generic;
using System.Data;

namespace Game.Kernel
{
	public interface ITableProvider
	{
		string TableName
		{
			get;
		}

		void BatchCommitData(DataSet dataSet, string[][] columnMapArray);

		void BatchCommitData(DataTable table, string[][] columnMapArray);

		void CommitData(DataTable dt);

		void Delete(string where);

		DataSet Get(string where);

		DataTable GetEmptyTable();

		T GetObject<T>(string where);

		IList<T> GetObjectList<T>(string where);

		DataRow GetOne(string where);

		int GetRecordsCount(string where);

		void Insert(DataRow row);

		DataRow NewRow();
	}
}
