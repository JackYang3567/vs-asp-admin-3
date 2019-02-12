using System;

namespace Game.Utils
{
	public class FolderInfo
	{
		private string m_contentType;

		private FsoMethod m_fsoType;

		private string m_fullName;

		private DateTime m_lastWriteTime;

		private long m_length;

		private string m_name;

		private string m_path;

		private byte m_type;

		public string ContentType
		{
			get
			{
				return m_contentType;
			}
			set
			{
				m_contentType = value;
			}
		}

		public FsoMethod FsoType
		{
			get
			{
				return m_fsoType;
			}
			set
			{
				m_fsoType = value;
				m_type = (byte)value;
			}
		}

		public string FullName
		{
			get
			{
				return m_fullName;
			}
			set
			{
				m_fullName = value;
			}
		}

		public DateTime LastWriteTime
		{
			get
			{
				return m_lastWriteTime;
			}
			set
			{
				m_lastWriteTime = value;
			}
		}

		public long Length
		{
			get
			{
				return m_length;
			}
			set
			{
				m_length = value;
			}
		}

		public string Name
		{
			get
			{
				return m_name;
			}
			set
			{
				m_name = value;
			}
		}

		public string Path
		{
			get
			{
				return m_path;
			}
			set
			{
				m_path = value;
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
				m_fsoType = (FsoMethod)value;
			}
		}

		public FolderInfo()
		{
			m_name = "";
			m_fullName = "";
			m_contentType = "";
			m_type = 0;
			m_fsoType = FsoMethod.Folder;
			m_path = "";
			m_lastWriteTime = DateTime.Now;
			m_length = 0L;
		}

		public FolderInfo(string name, string fullName, string contentType, byte type, string path, DateTime lastWriteTime, long length)
		{
			m_name = name;
			m_fullName = fullName;
			m_contentType = contentType;
			m_type = type;
			m_path = path;
			m_lastWriteTime = lastWriteTime;
			m_length = length;
		}
	}
}
