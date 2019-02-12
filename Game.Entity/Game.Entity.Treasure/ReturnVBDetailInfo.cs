using System;

namespace Game.Entity.Treasure
{
	[Serializable]
	public class ReturnVBDetailInfo
	{
		public const string Tablename = "ReturnVBDetailInfo";

		public const string _DetailID = "DetailID";

		public const string _OperStationID = "OperStationID";

		public const string _Rtmd5 = "Rtmd5";

		public const string _Rtka = "Rtka";

		public const string _Rtmi = "Rtmi";

		public const string _Rtmz = "Rtmz";

		public const string _Rtlx = "Rtlx";

		public const string _Rtoid = "Rtoid";

		public const string _OrderID = "OrderID";

		public const string _Rtuserid = "Rtuserid";

		public const string _Rtcustom = "Rtcustom";

		public const string _Rtflag = "Rtflag";

		public const string _EcryptStr = "EcryptStr";

		public const string _SignMsg = "SignMsg";

		public const string _CollectDate = "CollectDate";

		private int m_detailID;

		private int m_operStationID;

		private string m_rtmd5;

		private string m_rtka;

		private string m_rtmi;

		private int m_rtmz;

		private int m_rtlx;

		private string m_rtoid;

		private string m_orderID;

		private string m_rtuserid;

		private string m_rtcustom;

		private int m_rtflag;

		private string m_ecryptStr;

		private string m_signMsg;

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

		public int OperStationID
		{
			get
			{
				return m_operStationID;
			}
			set
			{
				m_operStationID = value;
			}
		}

		public string Rtmd5
		{
			get
			{
				return m_rtmd5;
			}
			set
			{
				m_rtmd5 = value;
			}
		}

		public string Rtka
		{
			get
			{
				return m_rtka;
			}
			set
			{
				m_rtka = value;
			}
		}

		public string Rtmi
		{
			get
			{
				return m_rtmi;
			}
			set
			{
				m_rtmi = value;
			}
		}

		public int Rtmz
		{
			get
			{
				return m_rtmz;
			}
			set
			{
				m_rtmz = value;
			}
		}

		public int Rtlx
		{
			get
			{
				return m_rtlx;
			}
			set
			{
				m_rtlx = value;
			}
		}

		public string Rtoid
		{
			get
			{
				return m_rtoid;
			}
			set
			{
				m_rtoid = value;
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

		public string Rtuserid
		{
			get
			{
				return m_rtuserid;
			}
			set
			{
				m_rtuserid = value;
			}
		}

		public string Rtcustom
		{
			get
			{
				return m_rtcustom;
			}
			set
			{
				m_rtcustom = value;
			}
		}

		public int Rtflag
		{
			get
			{
				return m_rtflag;
			}
			set
			{
				m_rtflag = value;
			}
		}

		public string EcryptStr
		{
			get
			{
				return m_ecryptStr;
			}
			set
			{
				m_ecryptStr = value;
			}
		}

		public string SignMsg
		{
			get
			{
				return m_signMsg;
			}
			set
			{
				m_signMsg = value;
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

		public ReturnVBDetailInfo()
		{
			m_detailID = 0;
			m_operStationID = 0;
			m_rtmd5 = "";
			m_rtka = "";
			m_rtmi = "";
			m_rtmz = 0;
			m_rtlx = 0;
			m_rtoid = "";
			m_orderID = "";
			m_rtuserid = "";
			m_rtcustom = "";
			m_rtflag = 0;
			m_ecryptStr = "";
			m_signMsg = "";
			m_collectDate = DateTime.Now;
		}
	}
}
