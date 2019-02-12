using Game.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Game.Kernel
{
	public class TableProvider : BaseDataProvider, ITableProvider
	{
		private string m_tableName;

		public string TableName
		{
			get
			{
				return m_tableName;
			}
		}

		public TableProvider(DbHelper database, string tableName)
			: base(database)
		{
			m_tableName = "";
			m_tableName = tableName;
		}

		public TableProvider(string connectionString, string tableName)
			: base(connectionString)
		{
			m_tableName = "";
			m_tableName = tableName;
		}

		public void BatchCommitData(DataSet dataSet, string[][] columnMapArray)
		{
			BatchCommitData(dataSet.Tables[0], columnMapArray);
		}

		public void BatchCommitData(DataTable table, string[][] columnMapArray)
		{
			using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(base.Database.ConnectionString))
			{
				sqlBulkCopy.DestinationTableName = TableName;
				foreach (string[] array in columnMapArray)
				{
					sqlBulkCopy.ColumnMappings.Add(array[0], array[1]);
				}
				sqlBulkCopy.WriteToServer(table);
				sqlBulkCopy.Close();
			}
		}

		public void CommitData(DataTable dt)
		{
			DataSet dataSet = ConstructDataSet(dt);
			base.Database.UpdateDataSet(dataSet, TableName);
		}

		private DataSet ConstructDataSet(DataTable dt)
		{
			DataSet dataSet = null;
			if (dt.DataSet != null)
			{
				return dt.DataSet;
			}
			dataSet = new DataSet();
			dataSet.Tables.Add(dt);
			return dataSet;
		}

		public void Delete(string where)
		{
			string commandText = string.Format("DELETE FROM {0} {1}", TableName, where);
			base.Database.ExecuteNonQuery(commandText);
		}

		public DataSet Get(string where)
		{
			string commandText = string.Format("SELECT * FROM {0} {1}", TableName, where);
			return base.Database.ExecuteDataset(commandText);
		}

		public DataTable GetEmptyTable()
		{
			DataTable emptyTable = base.Database.GetEmptyTable(TableName);
			emptyTable.TableName = TableName;
			return emptyTable;
		}

		public DataRow NewRow()
		{
			DataTable emptyTable = GetEmptyTable();
			DataRow dataRow = emptyTable.NewRow();
			for (int i = 0; i < emptyTable.Columns.Count; i++)
			{
				dataRow[i] = DBNull.Value;
			}
			return dataRow;
		}

		public T GetObject<T>(string where)
		{
			DataRow one = GetOne(where);
			if (one == null)
			{
				return default(T);
			}
			return DataHelper.ConvertRowToObject<T>(one);
		}

		public IList<T> GetObjectList<T>(string where)
		{
			DataSet dataSet = Get(where);
			if (Validate.CheckedDataSet(dataSet))
			{
				return DataHelper.ConvertDataTableToObjects<T>(dataSet.Tables[0]);
			}
			return null;
		}

		public DataRow GetOne(string where)
		{
			DataSet dataSet = Get(where);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				return dataSet.Tables[0].Rows[0];
			}
			return null;
		}

		public int GetRecordsCount(string where)
		{
			if (where == null)
			{
				where = "";
			}
			string commandText = string.Format("SELECT COUNT(*) FROM {0} {1}", TableName, where);
			return int.Parse(base.Database.ExecuteScalarToStr(CommandType.Text, commandText));
		}

		public void Insert(DataRow row)
		{
			DataTable emptyTable = GetEmptyTable();
			try
			{
				DataRow dataRow = emptyTable.NewRow();
				for (int i = 0; i < emptyTable.Columns.Count; i++)
				{
					dataRow[i] = row[i];
				}
				emptyTable.Rows.Add(dataRow);
				CommitData(emptyTable);
			}
			catch
			{
				throw;
			}
			finally
			{
				emptyTable.Rows.Clear();
				emptyTable.AcceptChanges();
			}
		}
	}
}
