using System;

namespace Game.Entity.NativeWeb
{
	[Serializable]
	public class Activity
	{
		public const string Tablename = "Activity";

		public const string _ActivityID = "ActivityID";

		public const string _Title = "Title";

		public const string _SortID = "SortID";

		public const string _ImageUrl = "ImageUrl";

		public const string _Time = "Time";

		public const string _Describe = "Describe";

		public const string _IsRecommend = "IsRecommend";

		public const string _InputDate = "InputDate";

		private int m_activityID;

		private string m_title;

		private int m_sortID;

		private string m_imageUrl;

		private string m_time;

		private string m_describe;

		private bool m_isRecommend;

		private DateTime m_inputDate;

		public int ActivityID
		{
			get
			{
				return m_activityID;
			}
			set
			{
				m_activityID = value;
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

		public string ImageUrl
		{
			get
			{
				return m_imageUrl;
			}
			set
			{
				m_imageUrl = value;
			}
		}

		public string Time
		{
			get
			{
				return m_time;
			}
			set
			{
				m_time = value;
			}
		}

		public string Describe
		{
			get
			{
				return m_describe;
			}
			set
			{
				m_describe = value;
			}
		}

		public bool IsRecommend
		{
			get
			{
				return m_isRecommend;
			}
			set
			{
				m_isRecommend = value;
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

		public Activity()
		{
			m_activityID = 0;
			m_title = "";
			m_sortID = 0;
			m_imageUrl = "";
			m_time = "";
			m_describe = "";
			m_isRecommend = false;
			m_inputDate = DateTime.Now;
		}
	}
}
