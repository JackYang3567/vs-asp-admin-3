using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Game.Facade.Tools
{
	public class ModelHandler<T> where T : new()
	{
		public List<T> FillModel(DataSet ds)
		{
			if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
			{
				return null;
			}
			return FillModel(ds.Tables[0]);
		}

		public List<T> FillModel(DataSet ds, int index)
		{
			if (ds == null || ds.Tables.Count <= index || ds.Tables[index].Rows.Count == 0)
			{
				return null;
			}
			return FillModel(ds.Tables[index]);
		}

		public List<T> FillModel(DataTable dt)
		{
			if (dt == null || dt.Rows.Count == 0)
			{
				return null;
			}
			List<T> list = new List<T>();
			foreach (DataRow row in dt.Rows)
			{
				T val = new T();
				for (int i = 0; i < row.Table.Columns.Count; i++)
				{
					PropertyInfo property = val.GetType().GetProperty(row.Table.Columns[i].ColumnName);
					if (property != (PropertyInfo)null && row[i] != DBNull.Value)
					{
						property.SetValue(val, row[i], null);
					}
				}
				list.Add(val);
			}
			return list;
		}

		public T FillModel(DataRow dr)
		{
			if (dr == null)
			{
				return default(T);
			}
			T val = new T();
			for (int i = 0; i < dr.Table.Columns.Count; i++)
			{
				PropertyInfo property = val.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
				if (property != (PropertyInfo)null && dr[i] != DBNull.Value)
				{
					property.SetValue(val, dr[i], null);
				}
			}
			return val;
		}

		public DataSet FillDataSet(List<T> modelList)
		{
			if (modelList == null || modelList.Count == 0)
			{
				return null;
			}
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add(FillDataTable(modelList));
			return dataSet;
		}

		public DataTable FillDataTable(List<T> modelList)
		{
			if (modelList == null || modelList.Count == 0)
			{
				return null;
			}
			DataTable dataTable = CreateData(modelList[0]);
			foreach (T model in modelList)
			{
				DataRow dataRow = dataTable.NewRow();
				PropertyInfo[] properties = typeof(T).GetProperties();
				foreach (PropertyInfo propertyInfo in properties)
				{
					dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		private DataTable CreateData(T model)
		{
			DataTable dataTable = new DataTable(typeof(T).Name);
			PropertyInfo[] properties = typeof(T).GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
			}
			return dataTable;
		}
	}
}
