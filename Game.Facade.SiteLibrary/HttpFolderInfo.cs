using System;

namespace Game.Facade.SiteLibrary
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

		public string ExtName
		{
			get
			{
				return this.m_extName;
			}
		}

		public string FormatModifyDate
		{
			get
			{
				DateTime dateTime = DateTime.Parse(this.m_modifyDate.ToString("U"));
				return dateTime.AddHours(8).ToString("yyyy-MM-dd hh:mm:ss");
			}
		}

		public string FormatName
		{
			get
			{
				return this.m_formatName;
			}
		}

		public string FormatSize
		{
			get
			{
				if (this.m_size == (long)0)
				{
					return string.Empty;
				}
				if (this.m_size.ToString().Length < 8)
				{
					return string.Concat(this.m_size / (long)1024, " KB");
				}
				return string.Concat(this.m_size / (long)1024 / (long)1024, " MB");
			}
		}

		public string FullName
		{
			get
			{
				return this.m_fullName;
			}
		}

		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		public string Type
		{
			get
			{
				return this.m_type;
			}
		}

		public HttpFolderInfo()
		{
		}

		public HttpFolderInfo(string p_name, string p_fullName, string p_formatName, string p_ext, long p_size, string p_type, DateTime p_modifyDate)
		{
			this.m_name = p_name;
			this.m_fullName = p_fullName;
			this.m_formatName = p_formatName;
			this.m_extName = p_ext;
			this.m_size = p_size;
			this.m_type = p_type;
			this.m_modifyDate = p_modifyDate;
		}
	}
}