using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Game.Kernel
{
	public class PagerManager
	{
		private DbHelper m_dbHelper;

		private IDictionary<int, PagerSet> m_fixedCacher;

		private PagerParameters m_prams;

		public PagerManager(DbHelper dbHelper)
		{
			m_dbHelper = dbHelper;
		}

		public PagerManager(string connectionString)
		{
			m_dbHelper = new DbHelper(connectionString);
		}

		public PagerManager(PagerParameters prams, DbHelper dbHelper)
		{
			m_prams = prams;
			m_dbHelper = dbHelper;
		}

		public PagerManager(PagerParameters prams, string connectionString)
		{
			m_prams = prams;
			m_dbHelper = new DbHelper(connectionString);
			if (prams.CacherSize > 0)
			{
				m_fixedCacher = new Dictionary<int, PagerSet>(prams.CacherSize);
			}
		}

		private void CacheObject(int index, PagerSet pagerSet)
		{
			if (m_fixedCacher != null)
			{
				m_fixedCacher.Add(index, pagerSet);
			}
			else if (m_prams.CacherSize > 0)
			{
				m_fixedCacher = new Dictionary<int, PagerSet>(m_prams.CacherSize);
				m_fixedCacher.Add(index, pagerSet);
			}
		}

		private PagerSet GetCachedObject(int index)
		{
			if (m_fixedCacher == null)
			{
				return null;
			}
			if (!m_fixedCacher.ContainsKey(index))
			{
				return null;
			}
			return m_fixedCacher[index];
		}

		protected string GetFieldString(string[] fields, string[] fieldAlias)
		{
			if (fields == null)
			{
				fields = new string[1]
				{
					"*"
				};
			}
			string text = "";
			if (fieldAlias == null)
			{
				for (int i = 0; i < fields.Length; i++)
				{
					text = text + " " + fields[i];
					text = ((i == fields.Length - 1) ? (text + " ") : (text + " , "));
				}
				return text;
			}
			for (int i = 0; i < fields.Length; i++)
			{
				text = text + " " + fields[i];
				if (fieldAlias[i] != null)
				{
					text = text + " as " + fieldAlias[i];
				}
				text = ((i == fields.Length - 1) ? (text + " ") : (text + " , "));
			}
			return text;
		}

		public PagerSet GetPagerSet()
		{
			return GetPagerSet(m_prams);
		}

		public PagerSet GetPagerSet(PagerParameters pramsPager)
		{
			if (m_prams == null)
			{
				m_prams = pramsPager;
			}
			if (pramsPager.PageIndex < 0)
			{
				return null;
			}
			List<DbParameter> list = new List<DbParameter>();
			list.Add(m_dbHelper.MakeInParam("TableName", pramsPager.Table));
			list.Add(m_dbHelper.MakeInParam("ReturnFields", GetFieldString(pramsPager.Fields, pramsPager.FieldAlias)));
			list.Add(m_dbHelper.MakeInParam("PageSize", pramsPager.PageSize));
			list.Add(m_dbHelper.MakeInParam("PageIndex", pramsPager.PageIndex));
			list.Add(m_dbHelper.MakeInParam("Where", pramsPager.WhereStr));
			list.Add(m_dbHelper.MakeInParam("Orderfld", pramsPager.PKey));
			list.Add(m_dbHelper.MakeInParam("OrderType", (!pramsPager.Ascending) ? 1 : 0));
			list.Add(m_dbHelper.MakeOutParam("PageCount", typeof(int)));
			list.Add(m_dbHelper.MakeOutParam("RecordCount", typeof(int)));
			List<DbParameter> list2 = list;
			DataSet pageSet = new DataSet();
			PagerSet pagerSet = new PagerSet(pramsPager.PageIndex, pramsPager.PageSize, Convert.ToInt32(list2[list2.Count - 3].Value), Convert.ToInt32(list2[list2.Count - 2].Value), pageSet);
			pagerSet.PageSet.DataSetName = "PagerSet_" + pramsPager.Table;
			return pagerSet;
		}

		public PagerSet GetPagerSet2(PagerParameters pramsPager)
		{
			if (m_prams == null)
			{
				m_prams = pramsPager;
			}
			if (pramsPager.PageIndex < 0)
			{
				return null;
			}
			List<DbParameter> list = new List<DbParameter>();
			list.Add(m_dbHelper.MakeInParam("TableName", pramsPager.Table));
			list.Add(m_dbHelper.MakeInParam("ReturnFields", GetFieldString(pramsPager.Fields, pramsPager.FieldAlias)));
			list.Add(m_dbHelper.MakeInParam("PageSize", pramsPager.PageSize));
			list.Add(m_dbHelper.MakeInParam("PageIndex", pramsPager.PageIndex));
			list.Add(m_dbHelper.MakeInParam("Where", pramsPager.WhereStr));
			list.Add(m_dbHelper.MakeInParam("Orderby", pramsPager.PKey));
			list.Add(m_dbHelper.MakeOutParam("PageCount", typeof(int)));
			list.Add(m_dbHelper.MakeOutParam("RecordCount", typeof(int)));
			List<DbParameter> list2 = list;
			DataSet ds;
			m_dbHelper.RunProc("WEB_PageView", list2, out ds);
			PagerSet pagerSet = new PagerSet(pramsPager.PageIndex, pramsPager.PageSize, Convert.ToInt32(list2[list2.Count - 3].Value), Convert.ToInt32(list2[list2.Count - 2].Value), ds);
			pagerSet.PageSet.DataSetName = "PagerSet_" + pramsPager.Table;
			return pagerSet;
		}
	}
}
