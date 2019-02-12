using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class GameMobileInfo
	{
		public const string Tablename = "GameMobileInfo";

		public const string _KindID = "KindID";

		public const string _MobileID = "MobileID";

		public const string _DownloadUrl = "DownloadUrl";

		private int m_kindID;

		private int m_mobileID;

		private string m_downloadUrl;

		public int KindID
		{
			get
			{
				return m_kindID;
			}
			set
			{
				m_kindID = value;
			}
		}

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

		public string DownloadUrl
		{
			get
			{
				return m_downloadUrl;
			}
			set
			{
				m_downloadUrl = value;
			}
		}

		public GameMobileInfo()
		{
			m_kindID = 0;
			m_mobileID = 0;
			m_downloadUrl = "";
		}
	}
}
