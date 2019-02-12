using Game.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Game.Kernel
{
	public class DbHelper
	{
		private enum DbConnectionOwnership
		{
			Internal,
			External
		}

		private object lockHelper = new object();

		protected string m_connectionstring;

		private DbProviderFactory m_factory;

		private Hashtable m_paramcache = Hashtable.Synchronized(new Hashtable());

		private IDbProvider m_provider;

		private int m_querycount;

		private static string m_querydetail = "";

		protected internal string ConnectionString
		{
			get
			{
				return m_connectionstring;
			}
			set
			{
				m_connectionstring = value;
			}
		}

		public DbProviderFactory Factory
		{
			get
			{
				if (m_factory == null)
				{
					m_factory = Provider.Instance();
				}
				return m_factory;
			}
		}

		public IDbProvider Provider
		{
			get
			{
				if (m_provider == null)
				{
					lock (lockHelper)
					{
						if (m_provider == null)
						{
							try
							{
								m_provider = (IDbProvider)Activator.CreateInstance(Type.GetType("Game.Kernel.SqlServerProvider, Game.Kernel", false, true));
							}
							catch
							{
								new Terminator().Throw("SqlServerProvider 数据库访问器创建失败！");
							}
						}
					}
				}
				return m_provider;
			}
		}

		public int QueryCount
		{
			get
			{
				return m_querycount;
			}
			set
			{
				m_querycount = value;
			}
		}

		public static string QueryDetail
		{
			get
			{
				return m_querydetail;
			}
			set
			{
				m_querydetail = value;
			}
		}

		public DbHelper(string connString)
		{
			BuildConnection(connString);
		}

		public void BuildConnection(string connectionString)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				new Terminator().Throw("请检查数据库连接信息，当前数据库连接信息为空。");
			}
			m_connectionstring = connectionString;
			m_querycount = 0;
		}

		private void AssignParameterValues(DbParameter[] commandParameters, DataRow dataRow)
		{
			if (commandParameters != null && dataRow != null)
			{
				int num = 0;
				foreach (DbParameter dbParameter in commandParameters)
				{
					if (dbParameter.ParameterName == null || dbParameter.ParameterName.Length <= 1)
					{
						new Terminator().Throw(string.Format("请提供参数{0}一个有效的名称{1}.", num, dbParameter.ParameterName));
					}
					if (dataRow.Table.Columns.IndexOf(dbParameter.ParameterName.Substring(1)) != -1)
					{
						dbParameter.Value = dataRow[dbParameter.ParameterName.Substring(1)];
					}
					num++;
				}
			}
		}

		private void AssignParameterValues(DbParameter[] commandParameters, object[] parameterValues)
		{
			if (commandParameters != null && parameterValues != null)
			{
				if (commandParameters.Length != parameterValues.Length)
				{
					new Terminator().Throw("参数值个数与参数不匹配。");
				}
				int i = 0;
				for (int num = commandParameters.Length; i < num; i++)
				{
					if (parameterValues[i] is IDbDataParameter)
					{
						IDbDataParameter dbDataParameter = (IDbDataParameter)parameterValues[i];
						if (dbDataParameter.Value == null)
						{
							commandParameters[i].Value = DBNull.Value;
						}
						else
						{
							commandParameters[i].Value = dbDataParameter.Value;
						}
					}
					else if (parameterValues[i] == null)
					{
						commandParameters[i].Value = DBNull.Value;
					}
					else
					{
						commandParameters[i].Value = parameterValues[i];
					}
				}
			}
		}

		private void AttachParameters(DbCommand command, DbParameter[] commandParameters)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (commandParameters != null)
			{
				foreach (DbParameter dbParameter in commandParameters)
				{
					if (dbParameter != null)
					{
						if ((dbParameter.Direction == ParameterDirection.InputOutput || dbParameter.Direction == ParameterDirection.Input) && dbParameter.Value == null)
						{
							dbParameter.Value = DBNull.Value;
						}
						command.Parameters.Add(dbParameter);
					}
				}
			}
		}

		public void CacheParameterSet(string commandText, params DbParameter[] commandParameters)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			if (commandText == null || commandText.Length == 0)
			{
				throw new ArgumentNullException("commandText");
			}
			string key = ConnectionString + ":" + commandText;
			m_paramcache[key] = commandParameters;
		}

		private DbParameter[] CloneParameters(DbParameter[] originalParameters)
		{
			DbParameter[] array = new DbParameter[originalParameters.Length];
			int i = 0;
			for (int num = originalParameters.Length; i < num; i++)
			{
				array[i] = (DbParameter)((ICloneable)originalParameters[i]).Clone();
			}
			return array;
		}

		public DbCommand CreateCommand(DbConnection connection, string spName, params string[] sourceColumns)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			DbCommand dbCommand = Factory.CreateCommand();
			dbCommand.CommandText = spName;
			dbCommand.Connection = connection;
			dbCommand.CommandType = CommandType.StoredProcedure;
			if (sourceColumns != null && sourceColumns.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(connection, spName);
				for (int i = 0; i < sourceColumns.Length; i++)
				{
					spParameterSet[i].SourceColumn = sourceColumns[i];
				}
				AttachParameters(dbCommand, spParameterSet);
			}
			return dbCommand;
		}

		private DbParameter[] DiscoverSpParameterSet(DbConnection connection, string spName, bool includeReturnValueParameter)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			DbCommand dbCommand = connection.CreateCommand();
			dbCommand.CommandText = spName;
			dbCommand.CommandType = CommandType.StoredProcedure;
			connection.Open();
			Provider.DeriveParameters(dbCommand);
			connection.Close();
			if (!includeReturnValueParameter)
			{
				dbCommand.Parameters.RemoveAt(0);
			}
			DbParameter[] array = new DbParameter[dbCommand.Parameters.Count];
			dbCommand.Parameters.CopyTo(array, 0);
			DbParameter[] array2 = array;
			foreach (DbParameter dbParameter in array2)
			{
				dbParameter.Value = DBNull.Value;
			}
			return array;
		}

		public void ExecuteCommandWithSplitter(string commandText)
		{
			ExecuteCommandWithSplitter(commandText, "\r\nGO\r\n");
		}

		public void ExecuteCommandWithSplitter(string commandText, string splitter)
		{
			int num = 0;
			do
			{
				int num2 = commandText.IndexOf(splitter, num);
				int length = ((num2 > num) ? num2 : commandText.Length) - num;
				string text = commandText.Substring(num, length);
				if (text.Trim().Length > 0)
				{
					ExecuteNonQuery(CommandType.Text, text);
				}
				if (num2 == -1)
				{
					break;
				}
				num = num2 + splitter.Length;
			}
			while (num < commandText.Length);
		}

		public DataSet ExecuteDataset(string commandText)
		{
			return ExecuteDataset(CommandType.Text, commandText, (DbParameter[])null);
		}

		public DataSet ExecuteDataset(CommandType commandType, string commandText)
		{
			return ExecuteDataset(commandType, commandText, (DbParameter[])null);
		}

		public DataSet ExecuteDataset(CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			using (DbConnection dbConnection = Factory.CreateConnection())
			{
				dbConnection.ConnectionString = ConnectionString;
				dbConnection.Open();
				return ExecuteDataset(dbConnection, commandType, commandText, commandParameters);
			}
		}

		public DataSet ExecuteDataset(DbConnection connection, CommandType commandType, string commandText)
		{
			return ExecuteDataset(connection, commandType, commandText, (DbParameter[])null);
		}

		public DataSet ExecuteDataset(DbConnection connection, string spName, params object[] parameterValues)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (parameterValues != null && parameterValues.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(connection, spName);
				AssignParameterValues(spParameterSet, parameterValues);
				return ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
		}

		public DataSet ExecuteDataset(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (connection != null)
			{
				DbCommand dbCommand = Factory.CreateCommand();
				bool mustCloseConnection = false;
				PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
				DbDataAdapter dbDataAdapter = Factory.CreateDataAdapter();
				try
				{
					dbDataAdapter.SelectCommand = dbCommand;
					DataSet dataSet = new DataSet();
					DateTime now = DateTime.Now;
					dbDataAdapter.Fill(dataSet);
					DateTime now2 = DateTime.Now;
					m_querydetail += GetQueryDetail(dbCommand.CommandText, now, now2, commandParameters);
					m_querycount++;
					dbCommand.Parameters.Clear();
					if (mustCloseConnection)
					{
						connection.Close();
					}
					return dataSet;
				}
				finally
				{
					if (dbDataAdapter != null)
					{
						((IDisposable)dbDataAdapter).Dispose();
					}
				}
			}
			throw new ArgumentNullException("connection");
		}

		public DataSet ExecuteDataset(DbTransaction transaction, CommandType commandType, string commandText)
		{
			return ExecuteDataset(transaction, commandType, commandText, (DbParameter[])null);
		}

		public DataSet ExecuteDataset(DbTransaction transaction, string spName, params object[] parameterValues)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (parameterValues != null && parameterValues.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(transaction.Connection, spName);
				AssignParameterValues(spParameterSet, parameterValues);
				return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
		}

		public DataSet ExecuteDataset(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction == null || transaction.Connection != null)
			{
				DbCommand dbCommand = Factory.CreateCommand();
				bool mustCloseConnection = false;
				PrepareCommand(dbCommand, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
				DbDataAdapter dbDataAdapter = Factory.CreateDataAdapter();
				try
				{
					dbDataAdapter.SelectCommand = dbCommand;
					DataSet dataSet = new DataSet();
					dbDataAdapter.Fill(dataSet);
					dbCommand.Parameters.Clear();
					return dataSet;
				}
				finally
				{
					if (dbDataAdapter != null)
					{
						((IDisposable)dbDataAdapter).Dispose();
					}
				}
			}
			throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
		}

		public DataSet ExecuteDatasetTypedParams(string spName, DataRow dataRow)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteDataset(CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteDataset(CommandType.StoredProcedure, spName);
		}

		public DataSet ExecuteDatasetTypedParams(DbConnection connection, string spName, DataRow dataRow)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(connection, spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
		}

		public DataSet ExecuteDatasetTypedParams(DbTransaction transaction, string spName, DataRow dataRow)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(transaction.Connection, spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
		}

		public int ExecuteNonQuery(string commandText)
		{
			return ExecuteNonQuery(CommandType.Text, commandText, (DbParameter[])null);
		}

		public int ExecuteNonQuery(CommandType commandType, string commandText)
		{
			return ExecuteNonQuery(commandType, commandText, (DbParameter[])null);
		}

		public int ExecuteNonQuery(CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			using (DbConnection dbConnection = Factory.CreateConnection())
			{
				dbConnection.ConnectionString = ConnectionString;
				dbConnection.Open();
				return ExecuteNonQuery(dbConnection, commandType, commandText, commandParameters);
			}
		}

		public int ExecuteNonQuery(DbConnection connection, string spName, params object[] parameterValues)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (parameterValues != null && parameterValues.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(connection, spName);
				AssignParameterValues(spParameterSet, parameterValues);
				return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
		}

		public int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText)
		{
			return ExecuteNonQuery(connection, commandType, commandText, (DbParameter[])null);
		}

		public int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			DbCommand dbCommand = Factory.CreateCommand();
			bool mustCloseConnection = false;
			PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
			DateTime now = DateTime.Now;
			int result = dbCommand.ExecuteNonQuery();
			DateTime now2 = DateTime.Now;
			m_querydetail += GetQueryDetail(dbCommand.CommandText, now, now2, commandParameters);
			m_querycount++;
			dbCommand.Parameters.Clear();
			if (mustCloseConnection)
			{
				connection.Close();
			}
			return result;
		}

		public int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText)
		{
			return ExecuteNonQuery(transaction, commandType, commandText, (DbParameter[])null);
		}

		public int ExecuteNonQuery(DbTransaction transaction, string spName, params object[] parameterValues)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (parameterValues != null && parameterValues.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(transaction.Connection, spName);
				AssignParameterValues(spParameterSet, parameterValues);
				return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
		}

		public int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			DbCommand dbCommand = Factory.CreateCommand();
			bool mustCloseConnection = false;
			PrepareCommand(dbCommand, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
			int result = dbCommand.ExecuteNonQuery();
			dbCommand.Parameters.Clear();
			return result;
		}

		public int ExecuteNonQuery(out int id, CommandType commandType, string commandText)
		{
			return ExecuteNonQuery(out id, commandType, commandText, (DbParameter[])null);
		}

		public int ExecuteNonQuery(out int id, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			using (DbConnection dbConnection = Factory.CreateConnection())
			{
				dbConnection.ConnectionString = ConnectionString;
				dbConnection.Open();
				return ExecuteNonQuery(out id, dbConnection, commandType, commandText, commandParameters);
			}
		}

		public int ExecuteNonQuery(out int id, string commandText)
		{
			return ExecuteNonQuery(out id, CommandType.Text, commandText, (DbParameter[])null);
		}

		public int ExecuteNonQuery(out int id, DbConnection connection, CommandType commandType, string commandText)
		{
			return ExecuteNonQuery(out id, connection, commandType, commandText, (DbParameter[])null);
		}

		public int ExecuteNonQuery(out int id, DbTransaction transaction, CommandType commandType, string commandText)
		{
			return ExecuteNonQuery(out id, transaction, commandType, commandText, (DbParameter[])null);
		}

		public int ExecuteNonQuery(out int id, DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (Provider.GetLastIdSql().Trim() == "")
			{
				throw new ArgumentNullException("GetLastIdSql is \"\"");
			}
			DbCommand dbCommand = Factory.CreateCommand();
			bool mustCloseConnection = false;
			PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
			int result = dbCommand.ExecuteNonQuery();
			dbCommand.Parameters.Clear();
			dbCommand.CommandType = CommandType.Text;
			dbCommand.CommandText = Provider.GetLastIdSql();
			id = int.Parse(dbCommand.ExecuteScalar().ToString());
			DateTime now = DateTime.Now;
			id = int.Parse(dbCommand.ExecuteScalar().ToString());
			DateTime now2 = DateTime.Now;
			m_querydetail += GetQueryDetail(dbCommand.CommandText, now, now2, commandParameters);
			m_querycount++;
			if (mustCloseConnection)
			{
				connection.Close();
			}
			return result;
		}

		public int ExecuteNonQuery(out int id, DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			DbCommand dbCommand = Factory.CreateCommand();
			bool mustCloseConnection = false;
			PrepareCommand(dbCommand, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
			int result = dbCommand.ExecuteNonQuery();
			dbCommand.Parameters.Clear();
			dbCommand.CommandType = CommandType.Text;
			dbCommand.CommandText = Provider.GetLastIdSql();
			id = int.Parse(dbCommand.ExecuteScalar().ToString());
			return result;
		}

		public int ExecuteNonQueryTypedParams(string spName, DataRow dataRow)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteNonQuery(CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteNonQuery(CommandType.StoredProcedure, spName);
		}

		public int ExecuteNonQueryTypedParams(DbConnection connection, string spName, DataRow dataRow)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(connection, spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
		}

		public int ExecuteNonQueryTypedParams(DbTransaction transaction, string spName, DataRow dataRow)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(transaction.Connection, spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
		}

		public T ExecuteObject<T>(string commandText)
		{
			DataSet dataSet = ExecuteDataset(commandText);
			if (Validate.CheckedDataSet(dataSet))
			{
				return DataHelper.ConvertRowToObject<T>(dataSet.Tables[0].Rows[0]);
			}
			return default(T);
		}

		public T ExecuteObject<T>(string commandText, List<DbParameter> prams)
		{
			DataSet dataSet = ExecuteDataset(CommandType.Text, commandText, prams.ToArray());
			if (Validate.CheckedDataSet(dataSet))
			{
				return DataHelper.ConvertRowToObject<T>(dataSet.Tables[0].Rows[0]);
			}
			return default(T);
		}

		public IList<T> ExecuteObjectList<T>(string commandText)
		{
			DataSet dataSet = ExecuteDataset(commandText);
			if (Validate.CheckedDataSet(dataSet))
			{
				return DataHelper.ConvertDataTableToObjects<T>(dataSet.Tables[0]);
			}
			return null;
		}

		public IList<T> ExecuteObjectList<T>(string commandText, List<DbParameter> prams)
		{
			DataSet dataSet = ExecuteDataset(CommandType.Text, commandText, prams.ToArray());
			if (Validate.CheckedDataSet(dataSet))
			{
				return DataHelper.ConvertDataTableToObjects<T>(dataSet.Tables[0]);
			}
			return null;
		}

		public DbDataReader ExecuteReader(CommandType commandType, string commandText)
		{
			return ExecuteReader(commandType, commandText, (DbParameter[])null);
		}

		public DbDataReader ExecuteReader(string spName, params object[] parameterValues)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (parameterValues != null && parameterValues.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(spName);
				AssignParameterValues(spParameterSet, parameterValues);
				return ExecuteReader(ConnectionString, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteReader(ConnectionString, CommandType.StoredProcedure, spName);
		}

		public DbDataReader ExecuteReader(CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (ConnectionString != null && ConnectionString.Length != 0)
			{
				DbConnection dbConnection = null;
				try
				{
					dbConnection = Factory.CreateConnection();
					dbConnection.ConnectionString = ConnectionString;
					dbConnection.Open();
					return ExecuteReader(dbConnection, null, commandType, commandText, commandParameters, DbConnectionOwnership.Internal);
				}
				catch
				{
					if (dbConnection != null)
					{
						dbConnection.Close();
					}
					throw;
				}
			}
			throw new ArgumentNullException("ConnectionString");
		}

		public DbDataReader ExecuteReader(DbConnection connection, CommandType commandType, string commandText)
		{
			return ExecuteReader(connection, commandType, commandText, (DbParameter[])null);
		}

		public DbDataReader ExecuteReader(DbConnection connection, string spName, params object[] parameterValues)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (parameterValues != null && parameterValues.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(connection, spName);
				AssignParameterValues(spParameterSet, parameterValues);
				return ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteReader(connection, CommandType.StoredProcedure, spName);
		}

		public DbDataReader ExecuteReader(DbTransaction transaction, CommandType commandType, string commandText)
		{
			return ExecuteReader(transaction, commandType, commandText, (DbParameter[])null);
		}

		public DbDataReader ExecuteReader(DbTransaction transaction, string spName, params object[] parameterValues)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (parameterValues != null && parameterValues.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(transaction.Connection, spName);
				AssignParameterValues(spParameterSet, parameterValues);
				return ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
		}

		public DbDataReader ExecuteReader(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			return ExecuteReader(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.External);
		}

		public DbDataReader ExecuteReader(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, DbConnectionOwnership.External);
		}

		private DbDataReader ExecuteReader(DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters, DbConnectionOwnership connectionOwnership)
		{
			if (connection != null)
			{
				bool mustCloseConnection = false;
				DbCommand dbCommand = Factory.CreateCommand();
				try
				{
					PrepareCommand(dbCommand, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
					DateTime now = DateTime.Now;
					DbDataReader result = (connectionOwnership != DbConnectionOwnership.External) ? dbCommand.ExecuteReader(CommandBehavior.CloseConnection) : dbCommand.ExecuteReader();
					DateTime now2 = DateTime.Now;
					m_querydetail += GetQueryDetail(dbCommand.CommandText, now, now2, commandParameters);
					m_querycount++;
					bool flag = true;
					foreach (DbParameter parameter in dbCommand.Parameters)
					{
						if (parameter.Direction != ParameterDirection.Input)
						{
							flag = false;
						}
					}
					if (flag)
					{
						dbCommand.Parameters.Clear();
					}
					return result;
				}
				catch
				{
					if (mustCloseConnection)
					{
						connection.Close();
					}
					throw;
				}
			}
			throw new ArgumentNullException("connection");
		}

		public DbDataReader ExecuteReaderTypedParams(string spName, DataRow dataRow)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteReader(ConnectionString, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteReader(ConnectionString, CommandType.StoredProcedure, spName);
		}

		public DbDataReader ExecuteReaderTypedParams(DbConnection connection, string spName, DataRow dataRow)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(connection, spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteReader(connection, CommandType.StoredProcedure, spName);
		}

		public DbDataReader ExecuteReaderTypedParams(DbTransaction transaction, string spName, DataRow dataRow)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(transaction.Connection, spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
		}

		public object ExecuteScalar(CommandType commandType, string commandText)
		{
			return ExecuteScalar(commandType, commandText, (DbParameter[])null);
		}

		public object ExecuteScalar(CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			using (DbConnection dbConnection = Factory.CreateConnection())
			{
				dbConnection.ConnectionString = ConnectionString;
				dbConnection.Open();
				return ExecuteScalar(dbConnection, commandType, commandText, commandParameters);
			}
		}

		public object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText)
		{
			return ExecuteScalar(connection, commandType, commandText, (DbParameter[])null);
		}

		public object ExecuteScalar(DbConnection connection, string spName, params object[] parameterValues)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (parameterValues != null && parameterValues.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(connection, spName);
				AssignParameterValues(spParameterSet, parameterValues);
				return ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
		}

		public object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText)
		{
			return ExecuteScalar(transaction, commandType, commandText, (DbParameter[])null);
		}

		public object ExecuteScalar(DbTransaction transaction, string spName, params object[] parameterValues)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (parameterValues != null && parameterValues.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(transaction.Connection, spName);
				AssignParameterValues(spParameterSet, parameterValues);
				return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
		}

		public object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			DbCommand dbCommand = Factory.CreateCommand();
			bool mustCloseConnection = false;
			PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
			object result = dbCommand.ExecuteScalar();
			dbCommand.Parameters.Clear();
			if (mustCloseConnection)
			{
				connection.Close();
			}
			return result;
		}

		public object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			DbCommand dbCommand = Factory.CreateCommand();
			bool mustCloseConnection = false;
			PrepareCommand(dbCommand, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
			DateTime now = DateTime.Now;
			object result = dbCommand.ExecuteScalar();
			DateTime now2 = DateTime.Now;
			m_querydetail += GetQueryDetail(dbCommand.CommandText, now, now2, commandParameters);
			m_querycount++;
			dbCommand.Parameters.Clear();
			return result;
		}

		public string ExecuteScalarToStr(CommandType commandType, string commandText)
		{
			object obj = ExecuteScalar(commandType, commandText);
			if (obj == null)
			{
				return "";
			}
			return obj.ToString();
		}

		public string ExecuteScalarToStr(CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			object obj = ExecuteScalar(commandType, commandText, commandParameters);
			if (obj == null)
			{
				return "";
			}
			return obj.ToString();
		}

		public object ExecuteScalarTypedParams(string spName, DataRow dataRow)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteScalar(CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteScalar(CommandType.StoredProcedure, spName);
		}

		public object ExecuteScalarTypedParams(DbConnection connection, string spName, DataRow dataRow)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(connection, spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
		}

		public object ExecuteScalarTypedParams(DbTransaction transaction, string spName, DataRow dataRow)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (dataRow != null && dataRow.ItemArray.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(transaction.Connection, spName);
				AssignParameterValues(spParameterSet, dataRow);
				return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, spParameterSet);
			}
			return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
		}

		public void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			using (DbConnection dbConnection = Factory.CreateConnection())
			{
				dbConnection.ConnectionString = ConnectionString;
				dbConnection.Open();
				FillDataset(dbConnection, commandType, commandText, dataSet, tableNames);
			}
		}

		public void FillDataset(string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			using (DbConnection dbConnection = Factory.CreateConnection())
			{
				dbConnection.ConnectionString = ConnectionString;
				dbConnection.Open();
				FillDataset(dbConnection, spName, dataSet, tableNames, parameterValues);
			}
		}

		public void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params DbParameter[] commandParameters)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			using (DbConnection dbConnection = Factory.CreateConnection())
			{
				dbConnection.ConnectionString = ConnectionString;
				dbConnection.Open();
				FillDataset(dbConnection, commandType, commandText, dataSet, tableNames, commandParameters);
			}
		}

		public void FillDataset(DbConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
		{
			FillDataset(connection, commandType, commandText, dataSet, tableNames, (DbParameter[])null);
		}

		public void FillDataset(DbConnection connection, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (parameterValues != null && parameterValues.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(connection, spName);
				AssignParameterValues(spParameterSet, parameterValues);
				FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, spParameterSet);
			}
			else
			{
				FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
			}
		}

		public void FillDataset(DbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
		{
			FillDataset(transaction, commandType, commandText, dataSet, tableNames, (DbParameter[])null);
		}

		public void FillDataset(DbTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			if (parameterValues != null && parameterValues.Length > 0)
			{
				DbParameter[] spParameterSet = GetSpParameterSet(transaction.Connection, spName);
				AssignParameterValues(spParameterSet, parameterValues);
				FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, spParameterSet);
			}
			else
			{
				FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
			}
		}

		public void FillDataset(DbConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params DbParameter[] commandParameters)
		{
			FillDataset(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
		}

		public void FillDataset(DbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params DbParameter[] commandParameters)
		{
			FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
		}

		private void FillDataset(DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params DbParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			DbCommand dbCommand = Factory.CreateCommand();
			bool mustCloseConnection = false;
			PrepareCommand(dbCommand, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
			DbDataAdapter dbDataAdapter = Factory.CreateDataAdapter();
			try
			{
				dbDataAdapter.SelectCommand = dbCommand;
				if (tableNames != null && tableNames.Length > 0)
				{
					string text = "Table";
					for (int i = 0; i < tableNames.Length; i++)
					{
						if (tableNames[i] == null || tableNames[i].Length == 0)
						{
							throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
						}
						dbDataAdapter.TableMappings.Add(text, tableNames[i]);
						text += (i + 1).ToString();
					}
				}
				dbDataAdapter.Fill(dataSet);
				dbCommand.Parameters.Clear();
			}
			finally
			{
				if (dbDataAdapter != null)
				{
					((IDisposable)dbDataAdapter).Dispose();
				}
			}
			if (mustCloseConnection)
			{
				connection.Close();
			}
		}

		public DbParameter[] GetCachedParameterSet(string commandText)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			if (commandText == null || commandText.Length == 0)
			{
				throw new ArgumentNullException("commandText");
			}
			string key = ConnectionString + ":" + commandText;
			DbParameter[] array = m_paramcache[key] as DbParameter[];
			if (array == null)
			{
				return null;
			}
			return CloneParameters(array);
		}

		public DataTable GetEmptyTable(string tableName)
		{
			string commandText = string.Format("SELECT * FROM {0} WHERE 1=0", tableName);
			return ExecuteDataset(commandText).Tables[0];
		}

		private static string GetQueryDetail(string commandText, DateTime dtStart, DateTime dtEnd, DbParameter[] cmdParams)
		{
			string text = "<tr style=\"background: rgb(255, 255, 255) none repeat scroll 0%; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial;\">";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string arg = "";
			if (cmdParams != null && cmdParams.Length > 0)
			{
				foreach (DbParameter dbParameter in cmdParams)
				{
					if (dbParameter != null)
					{
						text2 = text2 + "<td>" + dbParameter.ParameterName + "</td>";
						text3 = text3 + "<td>" + dbParameter.DbType.ToString() + "</td>";
						text4 = text4 + "<td>" + dbParameter.Value.ToString() + "</td>";
					}
				}
				arg = string.Format("<table width=\"100%\" cellspacing=\"1\" cellpadding=\"0\" style=\"background: rgb(255, 255, 255) none repeat scroll 0%; margin-top: 5px; font-size: 12px; display: block; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial;\">{0}{1}</tr>{0}{2}</tr>{0}{3}</tr></table>", text, text2, text3, text4);
			}
			return string.Format("<center><div style=\"border: 1px solid black; margin: 2px; padding: 1em; text-align: left; width: 96%; clear: both;\"><div style=\"font-size: 12px; float: right; width: 100px; margin-bottom: 5px;\"><b>TIME:</b> {0}</div><span style=\"font-size: 12px;\">{1}{2}</span></div><br /></center>", dtEnd.Subtract(dtStart).TotalMilliseconds / 1000.0, commandText, arg);
		}

		public DbParameter[] GetSpParameterSet(string spName)
		{
			return GetSpParameterSet(spName, false);
		}

		public DbParameter[] GetSpParameterSet(string spName, bool includeReturnValueParameter)
		{
			if (ConnectionString == null || ConnectionString.Length == 0)
			{
				throw new ArgumentNullException("ConnectionString");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			using (DbConnection dbConnection = Factory.CreateConnection())
			{
				dbConnection.ConnectionString = ConnectionString;
				return GetSpParameterSetInternal(dbConnection, spName, includeReturnValueParameter);
			}
		}

		internal DbParameter[] GetSpParameterSet(DbConnection connection, string spName)
		{
			return GetSpParameterSet(connection, spName, false);
		}

		internal DbParameter[] GetSpParameterSet(DbConnection connection, string spName, bool includeReturnValueParameter)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			using (DbConnection connection2 = (DbConnection)((ICloneable)connection).Clone())
			{
				return GetSpParameterSetInternal(connection2, spName, includeReturnValueParameter);
			}
		}

		private DbParameter[] GetSpParameterSetInternal(DbConnection connection, string spName, bool includeReturnValueParameter)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (spName == null || spName.Length == 0)
			{
				throw new ArgumentNullException("spName");
			}
			string key = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
			DbParameter[] array = m_paramcache[key] as DbParameter[];
			if (array == null)
			{
				DbParameter[] array2 = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
				m_paramcache[key] = array2;
				array = array2;
			}
			return CloneParameters(array);
		}

		public DbParameter MakeInParam(string paraName, object paraValue)
		{
			return MakeParam(paraName, paraValue, ParameterDirection.Input);
		}

		public DbParameter MakeOutParam(string paraName, Type paraType)
		{
			return MakeParam(paraName, null, ParameterDirection.Output, paraType, "");
		}

		public DbParameter MakeOutParam(string paraName, Type paraType, int size)
		{
			return MakeParam(paraName, null, ParameterDirection.Output, paraType, "", size);
		}

		public DbParameter MakeOutParam(string paraName, object paraValue, Type paraType, int size)
		{
			return MakeParam(paraName, paraValue, ParameterDirection.Output, paraType, "", size);
		}

		public DbParameter MakeParam(string paraName, object paraValue, ParameterDirection direction)
		{
			return Provider.MakeParam(paraName, paraValue, direction);
		}

		public DbParameter MakeParam(string paraName, object paraValue, ParameterDirection direction, Type paraType, string sourceColumn)
		{
			return Provider.MakeParam(paraName, paraValue, direction, paraType, sourceColumn);
		}

		public DbParameter MakeParam(string paraName, object paraValue, ParameterDirection direction, Type paraType, string sourceColumn, int size)
		{
			return Provider.MakeParam(paraName, paraValue, direction, paraType, sourceColumn, size);
		}

		public DbParameter MakeReturnParam()
		{
			return MakeReturnParam("ReturnValue");
		}

		public DbParameter MakeReturnParam(string paraName)
		{
			return MakeParam(paraName, 0, ParameterDirection.ReturnValue);
		}

		private void PrepareCommand(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters, out bool mustCloseConnection)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (commandText == null || commandText.Length == 0)
			{
				throw new ArgumentNullException("commandText");
			}
			if (connection.State != ConnectionState.Open)
			{
				mustCloseConnection = true;
				connection.Open();
			}
			else
			{
				mustCloseConnection = false;
			}
			command.Connection = connection;
			command.CommandText = commandText;
			if (transaction != null)
			{
				if (transaction.Connection == null)
				{
					throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
				}
				command.Transaction = transaction;
			}
			command.CommandType = commandType;
			if (commandParameters != null)
			{
				AttachParameters(command, commandParameters);
			}
		}

		public void ResetDbProvider()
		{
			m_connectionstring = null;
			m_factory = null;
			m_provider = null;
		}

		public int RunProc(string procName)
		{
			return ExecuteNonQuery(CommandType.StoredProcedure, procName, (DbParameter[])null);
		}

		public void RunProc(string procName, out DbDataReader reader)
		{
			reader = ExecuteReader(CommandType.StoredProcedure, procName, (DbParameter[])null);
		}

		public void RunProc(string procName, out DataSet ds)
		{
			ds = ExecuteDataset(CommandType.StoredProcedure, procName, (DbParameter[])null);
		}

		public void RunProc(string procName, out object obj)
		{
			obj = ExecuteScalar(CommandType.StoredProcedure, procName, (DbParameter[])null);
		}

		public int RunProc(string procName, List<DbParameter> prams)
		{
			prams.Add(MakeReturnParam());
			return ExecuteNonQuery(CommandType.StoredProcedure, procName, prams.ToArray());
		}

		public void RunProc(string procName, List<DbParameter> prams, out DbDataReader reader)
		{
			prams.Add(MakeReturnParam());
			reader = ExecuteReader(CommandType.StoredProcedure, procName, prams.ToArray());
		}

		public void RunProc(string procName, List<DbParameter> prams, out DataSet ds)
		{
			prams.Add(MakeReturnParam());
			ds = ExecuteDataset(CommandType.StoredProcedure, procName, prams.ToArray());
		}

		public void RunProc(string procName, List<DbParameter> prams, out object obj)
		{
			prams.Add(MakeReturnParam());
			obj = ExecuteScalar(CommandType.StoredProcedure, procName, prams.ToArray());
		}

		public T RunProcObject<T>(string procName)
		{
			DataSet ds = null;
			RunProc(procName, out ds);
			if (Validate.CheckedDataSet(ds))
			{
				return DataHelper.ConvertRowToObject<T>(ds.Tables[0].Rows[0]);
			}
			return default(T);
		}

		public T RunProcObject<T>(string procName, List<DbParameter> prams)
		{
			DataSet ds = null;
			RunProc(procName, prams, out ds);
			if (Validate.CheckedDataSet(ds))
			{
				return DataHelper.ConvertRowToObject<T>(ds.Tables[0].Rows[0]);
			}
			return default(T);
		}

		public IList<T> RunProcObjectList<T>(string procName)
		{
			DataSet ds = null;
			RunProc(procName, out ds);
			if (Validate.CheckedDataSet(ds))
			{
				return DataHelper.ConvertDataTableToObjects<T>(ds.Tables[0]);
			}
			return null;
		}

		public IList<T> RunProcObjectList<T>(string procName, List<DbParameter> prams)
		{
			DataSet ds = null;
			RunProc(procName, prams, out ds);
			if (Validate.CheckedDataSet(ds))
			{
				return DataHelper.ConvertDataTableToObjects<T>(ds.Tables[0]);
			}
			return null;
		}

		public void UpdateByDataSet(DataSet dataSet, string tableName)
		{
			DbDataAdapter dbDataAdapter = Factory.CreateDataAdapter();
			dbDataAdapter.SelectCommand.CommandText = string.Format("Select * from {0} ORDER BY DayID DESC", tableName);
			DbCommandBuilder dbCommandBuilder = Factory.CreateCommandBuilder();
			dbCommandBuilder.DataAdapter.SelectCommand.Connection = Factory.CreateConnection();
			DataSet dataSet2 = new DataSet();
			dbDataAdapter.Fill(dataSet2);
			dataSet2.Tables[0].Rows[0][1] = "107";
			dbDataAdapter.Update(dataSet2);
		}

		public void UpdateDataSet(DataSet dataSet, string tableName)
		{
			string commandText = string.Format("Select * from {0} where 1=0", tableName);
			DbCommandBuilder dbCommandBuilder = Factory.CreateCommandBuilder();
			dbCommandBuilder.DataAdapter = Factory.CreateDataAdapter();
			dbCommandBuilder.DataAdapter.SelectCommand = Factory.CreateCommand();
			dbCommandBuilder.DataAdapter.DeleteCommand = Factory.CreateCommand();
			dbCommandBuilder.DataAdapter.InsertCommand = Factory.CreateCommand();
			dbCommandBuilder.DataAdapter.UpdateCommand = Factory.CreateCommand();
			dbCommandBuilder.DataAdapter.SelectCommand.CommandText = commandText;
			dbCommandBuilder.DataAdapter.SelectCommand.Connection = Factory.CreateConnection();
			dbCommandBuilder.DataAdapter.DeleteCommand.Connection = Factory.CreateConnection();
			dbCommandBuilder.DataAdapter.InsertCommand.Connection = Factory.CreateConnection();
			dbCommandBuilder.DataAdapter.UpdateCommand.Connection = Factory.CreateConnection();
			dbCommandBuilder.DataAdapter.SelectCommand.Connection.ConnectionString = ConnectionString;
			dbCommandBuilder.DataAdapter.DeleteCommand.Connection.ConnectionString = ConnectionString;
			dbCommandBuilder.DataAdapter.InsertCommand.Connection.ConnectionString = ConnectionString;
			dbCommandBuilder.DataAdapter.UpdateCommand.Connection.ConnectionString = ConnectionString;
			UpdateDataSet(dbCommandBuilder.GetInsertCommand(), dbCommandBuilder.GetDeleteCommand(), dbCommandBuilder.GetUpdateCommand(), dataSet, tableName);
		}

		public void UpdateDataSet(DbCommand insertCommand, DbCommand deleteCommand, DbCommand updateCommand, DataSet dataSet, string tableName)
		{
			if (insertCommand == null)
			{
				throw new ArgumentNullException("insertCommand");
			}
			if (deleteCommand == null)
			{
				throw new ArgumentNullException("deleteCommand");
			}
			if (updateCommand == null)
			{
				throw new ArgumentNullException("updateCommand");
			}
			if (tableName == null || tableName.Length == 0)
			{
				throw new ArgumentNullException("tableName");
			}
			DbDataAdapter dbDataAdapter = Factory.CreateDataAdapter();
			try
			{
				dbDataAdapter.UpdateCommand = updateCommand;
				dbDataAdapter.InsertCommand = insertCommand;
				dbDataAdapter.DeleteCommand = deleteCommand;
				dbDataAdapter.Update(dataSet, tableName);
				dataSet.AcceptChanges();
			}
			finally
			{
				if (dbDataAdapter != null)
				{
					((IDisposable)dbDataAdapter).Dispose();
				}
			}
		}
	}
}
