using System;

namespace Game.Facade.Files
{
	public class HttpFolderInfo
	{
		private string m_name;

		private string m_fullName;

		private string m_formatName;

		private string m_extName;

		private long m_size;

		private string m_type;

		private DateTime m_modifyDate;

		public string Name
		{
			get
			{
				return m_name;
			}
		}

		public string FullName
		{
			get
			{
				return m_fullName;
			}
		}

		public string FormatName
		{
			get
			{
				return m_formatName;
			}
		}

		public string ExtName
		{
			get
			{
				return m_extName;
			}
		}

		public string FormatSize
		{
			get
			{
				if (m_size == 0)
				{
					return string.Empty;
				}
				if (m_size.ToString().Length < 8)
				{
					return m_size / 1024 + " KB";
				}
				return m_size / 1024 / 1024 + " MB";
			}
		}

		public string Type
		{
			get
			{
				return m_type;
			}
		}

		public string FormatModifyDate
		{
			get
			{
				return DateTime.Parse(m_modifyDate.ToString("U")).AddHours(8.0).ToString("yyyy-MM-dd hh:mm:ss");
			}
		}

		public HttpFolderInfo()
		{
		}

		public HttpFolderInfo(string p_name, string p_fullName, string p_formatName, string p_ext, long p_size, string p_type, DateTime p_modifyDate)
		{
			m_name = p_name;
			m_fullName = p_fullName;
			m_formatName = p_formatName;
			m_extName = p_ext;
			m_size = p_size;
			m_type = p_type;
			m_modifyDate = p_modifyDate;
		}
	}
}
