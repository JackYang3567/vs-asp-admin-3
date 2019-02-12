using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class GlobalAppInfo
	{
		public const string Tablename = "GlobalAppInfo";

		public const string _AppID = "AppID";

		public const string _ProductID = "ProductID";

		public const string _ProductName = "ProductName";

		public const string _Description = "Description";

		public const string _Price = "Price";

		public const string _AttachCurrency = "AttachCurrency";

		public const string _TagID = "TagID";

		public const string _CollectDate = "CollectDate";

		private int m_appID;

		private string m_productID;

		private string m_productName;

		private string m_description;

		private decimal m_price;

		private decimal m_attachCurrency;

		private int m_tagID;

		private DateTime m_collectDate;

		public int AppID
		{
			get
			{
				return m_appID;
			}
			set
			{
				m_appID = value;
			}
		}

		public string ProductID
		{
			get
			{
				return m_productID;
			}
			set
			{
				m_productID = value;
			}
		}

		public string ProductName
		{
			get
			{
				return m_productName;
			}
			set
			{
				m_productName = value;
			}
		}

		public string Description
		{
			get
			{
				return m_description;
			}
			set
			{
				m_description = value;
			}
		}

		public decimal Price
		{
			get
			{
				return m_price;
			}
			set
			{
				m_price = value;
			}
		}

		public decimal AttachCurrency
		{
			get
			{
				return m_attachCurrency;
			}
			set
			{
				m_attachCurrency = value;
			}
		}

		public int TagID
		{
			get
			{
				return m_tagID;
			}
			set
			{
				m_tagID = value;
			}
		}

		public DateTime CollectDate
		{
			get
			{
				return m_collectDate;
			}
			set
			{
				m_collectDate = value;
			}
		}

		public GlobalAppInfo()
		{
			m_appID = 0;
			m_productID = "";
			m_productName = "";
			m_description = "";
			m_price = 0m;
			m_attachCurrency = 0m;
			m_tagID = 0;
			m_collectDate = DateTime.Now;
		}
	}
}
