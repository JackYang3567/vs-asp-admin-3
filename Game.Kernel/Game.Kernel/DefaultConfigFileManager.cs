using System;
using System.IO;
using System.Xml.Serialization;

namespace Game.Kernel
{
	public class DefaultConfigFileManager
	{
		private static string m_configfilepath;

		private static IConfigInfo m_configinfo = null;

		private static object m_lockHelper = new object();

		public static string ConfigFilePath
		{
			get
			{
				return m_configfilepath;
			}
			set
			{
				m_configfilepath = value;
			}
		}

		public static IConfigInfo ConfigInfo
		{
			get
			{
				return m_configinfo;
			}
			set
			{
				m_configinfo = value;
			}
		}

		public static IConfigInfo DeserializeInfo(string configfilepath, Type configtype)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(configfilepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				XmlSerializer xmlSerializer = new XmlSerializer(configtype);
				return (IConfigInfo)xmlSerializer.Deserialize(fileStream);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo)
		{
			return LoadConfig(ref fileoldchange, configFilePath, configinfo, true);
		}

		protected static IConfigInfo LoadConfig(ref DateTime fileoldchange, string configFilePath, IConfigInfo configinfo, bool checkTime)
		{
			m_configfilepath = configFilePath;
			if (checkTime)
			{
				DateTime lastWriteTime = File.GetLastWriteTime(configFilePath);
				if (fileoldchange != lastWriteTime)
				{
					fileoldchange = lastWriteTime;
					lock (m_lockHelper)
					{
						return DeserializeInfo(configFilePath, configinfo.GetType());
					}
				}
				return configinfo;
			}
			lock (m_lockHelper)
			{
				return DeserializeInfo(configFilePath, configinfo.GetType());
			}
		}

		public virtual bool SaveConfig()
		{
			return true;
		}

		public bool SaveConfig(string configFilePath, IConfigInfo configinfo)
		{
			bool flag = false;
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(configFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
				new XmlSerializer(configinfo.GetType()).Serialize(fileStream, configinfo);
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}
	}
}
