using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class ConfigInfo
	{
		public const string Tablename = "ConfigInfo";

		public const string _ConfigID = "ConfigID";

		public const string _ConfigKey = "ConfigKey";

		public const string _ConfigName = "ConfigName";

		public const string _ConfigString = "ConfigString";

		public const string _Field1 = "Field1";

		public const string _Field2 = "Field2";

		public const string _Field3 = "Field3";

		public const string _Field4 = "Field4";

		public const string _Field5 = "Field5";

		public const string _Field6 = "Field6";

		public const string _Field7 = "Field7";

		public const string _Field8 = "Field8";

		public const string _SortID = "SortID";

		private int m_configID;

		private string m_configKey;

		private string m_configName;

		private string m_configString;

		private string m_field1;

		private string m_field2;

		private string m_field3;

		private string m_field4;

		private string m_field5;

		private string m_field6;

		private string m_field7;

		private string m_field8;

		private int m_sortID;

		public int ConfigID
		{
			get
			{
				return m_configID;
			}
			set
			{
				m_configID = value;
			}
		}

		public string ConfigKey
		{
			get
			{
				return m_configKey;
			}
			set
			{
				m_configKey = value;
			}
		}

		public string ConfigName
		{
			get
			{
				return m_configName;
			}
			set
			{
				m_configName = value;
			}
		}

		public string ConfigString
		{
			get
			{
				return m_configString;
			}
			set
			{
				m_configString = value;
			}
		}

		public string Field1
		{
			get
			{
				return m_field1;
			}
			set
			{
				m_field1 = value;
			}
		}

		public string Field2
		{
			get
			{
				return m_field2;
			}
			set
			{
				m_field2 = value;
			}
		}

		public string Field3
		{
			get
			{
				return m_field3;
			}
			set
			{
				m_field3 = value;
			}
		}

		public string Field4
		{
			get
			{
				return m_field4;
			}
			set
			{
				m_field4 = value;
			}
		}

		public string Field5
		{
			get
			{
				return m_field5;
			}
			set
			{
				m_field5 = value;
			}
		}

		public string Field6
		{
			get
			{
				return m_field6;
			}
			set
			{
				m_field6 = value;
			}
		}

		public string Field7
		{
			get
			{
				return m_field7;
			}
			set
			{
				m_field7 = value;
			}
		}

		public string Field8
		{
			get
			{
				return m_field8;
			}
			set
			{
				m_field8 = value;
			}
		}

		public int SortID
		{
			get
			{
				return m_sortID;
			}
			set
			{
				m_sortID = value;
			}
		}

		public ConfigInfo()
		{
			m_configID = 0;
			m_configKey = "";
			m_configName = "";
			m_configString = "";
			m_field1 = "";
			m_field2 = "";
			m_field3 = "";
			m_field4 = "";
			m_field5 = "";
			m_field6 = "";
			m_field7 = "";
			m_field8 = "";
			m_sortID = 0;
		}
	}
}
