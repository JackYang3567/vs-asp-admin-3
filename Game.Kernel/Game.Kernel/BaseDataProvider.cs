namespace Game.Kernel
{
	public abstract class BaseDataProvider
	{
		private string m_connectionString;

		private DbHelper m_database;

		private PagerManager m_pagerHelper;

		protected internal string ConnectionString
		{
			get
			{
				return m_connectionString;
			}
		}

		protected internal DbHelper Database
		{
			get
			{
				return m_database;
			}
		}

		protected internal PagerManager PagerHelper
		{
			get
			{
				return m_pagerHelper;
			}
		}

		protected internal BaseDataProvider()
		{
		}

		protected internal BaseDataProvider(DbHelper database)
		{
			m_database = database;
			m_connectionString = database.ConnectionString;
			m_pagerHelper = new PagerManager(m_database);
		}

		protected internal BaseDataProvider(string connectionString)
		{
			m_connectionString = connectionString;
			m_database = new DbHelper(connectionString);
			m_pagerHelper = new PagerManager(m_database);
		}

		protected virtual PagerSet GetPagerSet(PagerParameters prams)
		{
			return PagerHelper.GetPagerSet(prams);
		}

		protected virtual PagerSet GetPagerSet2(PagerParameters prams)
		{
			return PagerHelper.GetPagerSet2(prams);
		}

		protected virtual ITableProvider GetTableProvider(string tableName)
		{
			return new TableProvider(Database, tableName);
		}
	}
}
