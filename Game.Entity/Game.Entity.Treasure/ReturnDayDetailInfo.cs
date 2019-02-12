using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class ReturnDayDetailInfo
	{
		public const string Tablename = "ReturnDayDetailInfo";

		public const string _DetailID = "DetailID";

		public const string _OrderID = "OrderID";

		public const string _MerID = "MerID";

		public const string _PayMoney = "PayMoney";

		public const string _UserName = "UserName";

		public const string _PayType = "PayType";

		public const string _Status = "Status";

		public const string _Sign = "Sign";

		public const string _InputDate = "InputDate";

		private int m_detailID;

		private string m_orderID;

		private string m_merID;

		private decimal m_payMoney;

		private string m_userName;

		private int m_payType;

		private string m_status;

		private string m_sign;

		private DateTime m_inputDate;

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

		public string MerID
		{
			get
			{
				return m_merID;
			}
			set
			{
				m_merID = value;
			}
		}

		public decimal PayMoney
		{
			get
			{
				return m_payMoney;
			}
			set
			{
				m_payMoney = value;
			}
		}

		public string UserName
		{
			get
			{
				return m_userName;
			}
			set
			{
				m_userName = value;
			}
		}

		public int PayType
		{
			get
			{
				return m_payType;
			}
			set
			{
				m_payType = value;
			}
		}

		public string Status
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

		public string Sign
		{
			get
			{
				return m_sign;
			}
			set
			{
				m_sign = value;
			}
		}

		public DateTime InputDate
		{
			get
			{
				return m_inputDate;
			}
			set
			{
				m_inputDate = value;
			}
		}

		public ReturnDayDetailInfo()
		{
			m_detailID = 0;
			m_orderID = "";
			m_merID = "";
			m_payMoney = 0m;
			m_userName = "";
			m_payType = 0;
			m_status = "";
			m_sign = "";
			m_inputDate = DateTime.Now;
		}
	}
}
