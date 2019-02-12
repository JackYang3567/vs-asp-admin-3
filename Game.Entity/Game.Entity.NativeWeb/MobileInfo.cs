using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class MobileInfo
	{
		public const string Tablename = "MobileInfo";

		public const string _MobileID = "MobileID";

		public const string _MobileType = "MobileType";

		public const string _MobileSerial = "MobileSerial";

		public const string _MobileModel = "MobileModel";

		public const string _Size = "Size";

		public const string _Resolution = "Resolution";

		public const string _Screen = "Screen";

		public const string _OperatingSystem = "OperatingSystem";

		public const string _IsHorizontal = "IsHorizontal";

		public const string _Remark = "Remark";

		private int m_mobileID;

		private string m_mobileType;

		private string m_mobileSerial;

		private string m_mobileModel;

		private string m_size;

		private string m_resolution;

		private string m_screen;

		private string m_operatingSystem;

		private byte m_isHorizontal;

		private string m_remark;

		public int MobileID
		{
			get
			{
				return m_mobileID;
			}
			set
			{
				m_mobileID = value;
			}
		}

		public string MobileType
		{
			get
			{
				return m_mobileType;
			}
			set
			{
				m_mobileType = value;
			}
		}

		public string MobileSerial
		{
			get
			{
				return m_mobileSerial;
			}
			set
			{
				m_mobileSerial = value;
			}
		}

		public string MobileModel
		{
			get
			{
				return m_mobileModel;
			}
			set
			{
				m_mobileModel = value;
			}
		}

		public string Size
		{
			get
			{
				return m_size;
			}
			set
			{
				m_size = value;
			}
		}

		public string Resolution
		{
			get
			{
				return m_resolution;
			}
			set
			{
				m_resolution = value;
			}
		}

		public string Screen
		{
			get
			{
				return m_screen;
			}
			set
			{
				m_screen = value;
			}
		}

		public string OperatingSystem
		{
			get
			{
				return m_operatingSystem;
			}
			set
			{
				m_operatingSystem = value;
			}
		}

		public byte IsHorizontal
		{
			get
			{
				return m_isHorizontal;
			}
			set
			{
				m_isHorizontal = value;
			}
		}

		public string Remark
		{
			get
			{
				return m_remark;
			}
			set
			{
				m_remark = value;
			}
		}

		public MobileInfo()
		{
			m_mobileID = 0;
			m_mobileType = "";
			m_mobileSerial = "";
			m_mobileModel = "";
			m_size = "";
			m_resolution = "";
			m_screen = "";
			m_operatingSystem = "";
			m_isHorizontal = 0;
			m_remark = "";
		}
	}
}
