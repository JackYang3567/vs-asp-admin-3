using Game.Utils;
using System;
using System.Data;

namespace Game.Kernel
{
	[Serializable]
	public class PagerSet
	{
		public DataSet PageSet
		{
			get;
			set;
		}

		public int PageCount
		{
			get;
			set;
		}

		public int PageIndex
		{
			get;
			set;
		}

		public int PageSize
		{
			get;
			set;
		}

		public int RecordCount
		{
			get;
			set;
		}

		public PagerSet()
		{
			PageIndex = 1;
			PageSize = 10;
			PageCount = 0;
			RecordCount = 0;
			PageSet = new DataSet("PagerSet");
		}

		public PagerSet(int pageIndex, int pageSize, int pageCount, int recordCount, DataSet pageSet)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			PageCount = pageCount;
			RecordCount = recordCount;
			PageSet = pageSet;
		}

		public bool CheckedPageSet()
		{
			return Validate.CheckedDataSet(PageSet);
		}
	}
}
