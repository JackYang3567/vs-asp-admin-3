using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class Ads
	{
		public const string Tablename = "Ads";

		public const string _ID = "ID";

		public const string _Title = "Title";

		public const string _ResourceURL = "ResourceURL";

		public const string _LinkURL = "LinkURL";

		public const string _Type = "Type";

		public const string _SortID = "SortID";

		public const string _Remark = "Remark";

		private int m_iD;

		private string m_title;

		private string m_resourceURL;

		private string m_linkURL;

		private byte m_type;

		private int m_sortID;

		private string m_remark;

		public int ID
		{
			get
			{
				return m_iD;
			}
			set
			{
				m_iD = value;
			}
		}

		public string Title
		{
			get
			{
				return m_title;
			}
			set
			{
				m_title = value;
			}
		}

		public string ResourceURL
		{
			get
			{
				return m_resourceURL;
			}
			set
			{
				m_resourceURL = value;
			}
		}

		public string LinkURL
		{
			get
			{
				return m_linkURL;
			}
			set
			{
				m_linkURL = value;
			}
		}

		public byte Type
		{
			get
			{
				return m_type;
			}
			set
			{
				m_type = value;
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

		public Ads()
		{
			m_iD = 0;
			m_title = "";
			m_resourceURL = "";
			m_linkURL = "";
			m_type = 0;
			m_sortID = 0;
			m_remark = "";
		}
	}
}
