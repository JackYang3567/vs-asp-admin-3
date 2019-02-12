using System;

namespace Game.Utils
{
	public class TextLoggerEntity : IComparable
	{
		public string LogContent
		{
			get;
			set;
		}

		public DateTime LogDateTime
		{
			get;
			set;
		}

		public string LogErrorUrl
		{
			get;
			set;
		}

		public string LogIp
		{
			get;
			set;
		}

		public TextLoggerEntity(DateTime logDateTime, string logContent, string logIp, string logErrorUrl)
		{
			LogDateTime = logDateTime;
			LogContent = logContent;
			LogIp = logIp;
			LogErrorUrl = logErrorUrl;
		}

		public int CompareTo(object obj)
		{
			TextLoggerEntity textLoggerEntity = obj as TextLoggerEntity;
			if (textLoggerEntity.LogDateTime > LogDateTime)
			{
				return 1;
			}
			if (textLoggerEntity.LogDateTime < LogDateTime)
			{
				return -1;
			}
			return 0;
		}
	}
}
