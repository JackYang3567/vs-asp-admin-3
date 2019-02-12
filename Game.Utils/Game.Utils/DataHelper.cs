using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Game.Utils
{
	public class DataHelper
	{
		public class PropertyNotFoundException : UCException
		{
			private string targetPropertyName;

			public string TargetPropertyName
			{
				get
				{
					return targetPropertyName;
				}
				set
				{
					targetPropertyName = value;
				}
			}

			public PropertyNotFoundException()
			{
			}

			public PropertyNotFoundException(string propertyName)
				: base(string.Format("The property named '{0}' not found in Entity definition.", propertyName))
			{
				targetPropertyName = propertyName;
			}
		}

		public static IList<TEntity> ConvertDataTableToObjects<TEntity>(DataTable dt)
		{
			if (dt == null)
			{
				return null;
			}
			IList<TEntity> list = new List<TEntity>();
			foreach (DataRow row in dt.Rows)
			{
				((ICollection<TEntity>)list).Add(ConvertRowToObject<TEntity>(row));
			}
			return list;
		}

		public static TEntity ConvertRowToObject<TEntity>(DataRow row)
		{
			if (row == null)
			{
				return default(TEntity);
			}
			Type typeFromHandle = typeof(TEntity);
			return (TEntity)ConvertRowToObject(typeFromHandle, row);
		}

		public static object ConvertRowToObject(Type objType, DataRow row)
		{
			if (row == null)
			{
				return null;
			}
			DataTable table = row.Table;
			object obj = Activator.CreateInstance(objType);
			foreach (DataColumn column in table.Columns)
			{
				PropertyInfo property = objType.GetProperty(column.ColumnName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (property != null)
				{
					Type propertyType = property.PropertyType;
					object obj2 = null;
					bool flag = true;
					try
					{
						obj2 = TypeHelper.ChangeType(propertyType, row[column.ColumnName]);
					}
					catch
					{
						flag = false;
					}
					if (flag)
					{
						object[] args = new object[1]
						{
							obj2
						};
						objType.InvokeMember(column.ColumnName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, null, obj, args);
					}
				}
			}
			return obj;
		}

		public static IList<string> DistillCommandParameter(string sqlStatement, string paraPrefix)
		{
			sqlStatement += " ";
			IList<string> list = new List<string>();
			DoDistill(sqlStatement, list, paraPrefix);
			if (list.Count > 0)
			{
				string text = list[list.Count - 1].Trim();
				if (text.EndsWith("\""))
				{
					text.TrimEnd('"');
					list.RemoveAt(list.Count - 1);
					list.Add(text);
				}
			}
			return list;
		}

		private static void DoDistill(string sqlStatement, IList<string> paraList, string paraPrefix)
		{
			sqlStatement.TrimStart(' ');
			int num = sqlStatement.IndexOf(paraPrefix);
			if (num >= 0)
			{
				int num2 = sqlStatement.IndexOf(" ", num);
				int length = num2 - num;
				string text = sqlStatement.Substring(num, length);
				paraList.Add(text.Replace(paraPrefix, ""));
				DoDistill(sqlStatement.Substring(num2), paraList, paraPrefix);
			}
		}

		public static void FillCommandParameterValue(IDbCommand command, object entityOrRow)
		{
			foreach (IDbDataParameter parameter in command.Parameters)
			{
				parameter.Value = GetColumnValue(entityOrRow, parameter.SourceColumn);
				if (parameter.Value == null)
				{
					parameter.Value = DBNull.Value;
				}
			}
		}

		public static object GetColumnValue(object entityOrRow, string columnName)
		{
			DataRow dataRow = entityOrRow as DataRow;
			if (dataRow != null)
			{
				return dataRow[columnName];
			}
			return entityOrRow.GetType().InvokeMember(columnName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty, null, entityOrRow, null);
		}

		public static object GetSafeDbValue(object val)
		{
			if (val != null)
			{
				return val;
			}
			return DBNull.Value;
		}

		public static void RefreshEntityFields(object entity, DataRow row)
		{
			DataTable table = row.Table;
			IList<string> list = new List<string>();
			foreach (DataColumn column in table.Columns)
			{
				list.Add(column.ColumnName);
			}
			RefreshEntityFields(entity, row, list);
		}

		public static void RefreshEntityFields(object entity, DataRow row, IList<string> refreshFields)
		{
			Type type = entity.GetType();
			foreach (string refreshField in refreshFields)
			{
				string text = refreshField;
				PropertyInfo property = type.GetProperty(text, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (property == null)
				{
					throw new PropertyNotFoundException(text);
				}
				Type propertyType = property.PropertyType;
				object obj = null;
				bool flag = true;
				try
				{
					obj = TypeHelper.ChangeType(propertyType, row[text]);
				}
				catch
				{
					flag = false;
				}
				if (flag)
				{
					object[] args = new object[1]
					{
						obj
					};
					type.InvokeMember(text, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, null, entity, args);
				}
			}
		}
	}
}
