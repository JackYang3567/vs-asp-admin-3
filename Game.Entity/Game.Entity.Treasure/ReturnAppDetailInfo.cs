using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class ReturnAppDetailInfo
	{
		public const string Tablename = "ReturnAppDetailInfo";

		public const string _DetailID = "DetailID";

		public const string _UserID = "UserID";

		public const string _OrderID = "OrderID";

		public const string _PayAmount = "PayAmount";

		public const string _Status = "Status";

		public const string _Quantity = "quantity";

		public const string _Product_id = "product_id";

		public const string _Transaction_id = "transaction_id";

		public const string _Purchase_date = "purchase_date";

		public const string _Original_transaction_id = "original_transaction_id";

		public const string _Original_purchase_date = "original_purchase_date";

		public const string _App_item_id = "app_item_id";

		public const string _Version_external_identifier = "version_external_identifier";

		public const string _Bid = "bid";

		public const string _Bvrs = "bvrs";

		public const string _CollectDate = "CollectDate";

		private int m_detailID;

		private int m_userID;

		private string m_orderID;

		private decimal m_payAmount;

		private int m_status;

		private int m_quantity;

		private string m_product_id;

		private string m_transaction_id;

		private string m_purchase_date;

		private string m_original_transaction_id;

		private string m_original_purchase_date;

		private string m_app_item_id;

		private string m_version_external_identifier;

		private string m_bid;

		private string m_bvrs;

		private DateTime m_collectDate;

		public int DetailID
		{
			get
			{
				return m_detailID;
			}
			set
			{
				m_detailID = value;
			}
		}

		public int UserID
		{
			get
			{
				return m_userID;
			}
			set
			{
				m_userID = value;
			}
		}

		public string OrderID
		{
			get
			{
				return m_orderID;
			}
			set
			{
				m_orderID = value;
			}
		}

		public decimal PayAmount
		{
			get
			{
				return m_payAmount;
			}
			set
			{
				m_payAmount = value;
			}
		}

		public int Status
		{
			get
			{
				return m_status;
			}
			set
			{
				m_status = value;
			}
		}

		public int Quantity
		{
			get
			{
				return m_quantity;
			}
			set
			{
				m_quantity = value;
			}
		}

		public string Product_id
		{
			get
			{
				return m_product_id;
			}
			set
			{
				m_product_id = value;
			}
		}

		public string Transaction_id
		{
			get
			{
				return m_transaction_id;
			}
			set
			{
				m_transaction_id = value;
			}
		}

		public string Purchase_date
		{
			get
			{
				return m_purchase_date;
			}
			set
			{
				m_purchase_date = value;
			}
		}

		public string Original_transaction_id
		{
			get
			{
				return m_original_transaction_id;
			}
			set
			{
				m_original_transaction_id = value;
			}
		}

		public string Original_purchase_date
		{
			get
			{
				return m_original_purchase_date;
			}
			set
			{
				m_original_purchase_date = value;
			}
		}

		public string App_item_id
		{
			get
			{
				return m_app_item_id;
			}
			set
			{
				m_app_item_id = value;
			}
		}

		public string Version_external_identifier
		{
			get
			{
				return m_version_external_identifier;
			}
			set
			{
				m_version_external_identifier = value;
			}
		}

		public string Bid
		{
			get
			{
				return m_bid;
			}
			set
			{
				m_bid = value;
			}
		}

		public string Bvrs
		{
			get
			{
				return m_bvrs;
			}
			set
			{
				m_bvrs = value;
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

		public ReturnAppDetailInfo()
		{
			m_detailID = 0;
			m_userID = 0;
			m_orderID = "";
			m_payAmount = 0m;
			m_status = 0;
			m_quantity = 0;
			m_product_id = "";
			m_transaction_id = "";
			m_purchase_date = "";
			m_original_transaction_id = "";
			m_original_purchase_date = "";
			m_app_item_id = "";
			m_version_external_identifier = "";
			m_bid = "";
			m_bvrs = "";
			m_collectDate = DateTime.Now;
		}
	}
}
