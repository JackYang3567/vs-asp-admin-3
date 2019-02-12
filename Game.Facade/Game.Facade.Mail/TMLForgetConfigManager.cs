using Game.Kernel;
using System;
using System.IO;

namespace Game.Facade.Mail
{
	public class TMLForgetConfigManager : DefaultConfigFileManager
	{
		private static MailTMLConfigInfo m_configinfo;

		private static DateTime m_fileoldchange;

		public static string filename;

		public new static IConfigInfo ConfigInfo
		{
			get
			{
				return m_configinfo;
			}
			set
			{
				m_configinfo = (MailTMLConfigInfo)value;
			}
		}

		static TMLForgetConfigManager()
		{
			m_configinfo = null;
			filename = null;
			m_fileoldchange = File.GetLastWriteTime(DefaultConfigFileManager.ConfigFilePath);
			m_configinfo = (MailTMLConfigInfo)DefaultConfigFileManager.DeserializeInfo(DefaultConfigFileManager.ConfigFilePath, typeof(MailTMLConfigInfo));
		}

		public static MailTMLConfigInfo LoadConfig()
		{
			ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, DefaultConfigFileManager.ConfigFilePath, ConfigInfo, false);
			return ConfigInfo as MailTMLConfigInfo;
		}

		public override bool SaveConfig()
		{
			return SaveConfig(DefaultConfigFileManager.ConfigFilePath, ConfigInfo);
		}
	}
}
