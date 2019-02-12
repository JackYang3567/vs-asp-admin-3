using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Game.Utils
{
	public static class TextLogger
	{
		public class SortDateTime : IComparable
		{
			public DateTime dateTime;

			public SortDateTime(DateTime oneDateTime)
			{
				dateTime = oneDateTime;
			}

			public int CompareTo(object obj)
			{
				SortDateTime sortDateTime = obj as SortDateTime;
				if (sortDateTime.dateTime > dateTime)
				{
					return 1;
				}
				if (sortDateTime.dateTime < dateTime)
				{
					return -1;
				}
				return 0;
			}
		}

		public static readonly string APP_LOG_DIRECTORY = Utility.GetAppLogDirectory;

		private static string LOG_SUFFIX = "Log.config";

		private static readonly bool WRITE_APP_LOG = Utility.GetWriteAppLog;

		public static bool DeleteFile(string path)
		{
			bool result = false;
			try
			{
				new FileInfo(path).Delete();
				result = true;
				return result;
			}
			catch (Exception ex)
			{
				Write(ex.ToString());
				return result;
			}
		}

		public static SortedList<SortDateTime, string> GetFileList()
		{
			SortedList<SortDateTime, string> sortedList = new SortedList<SortDateTime, string>();
			DirectoryInfo directoryInfo = new DirectoryInfo(APP_LOG_DIRECTORY);
			FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
			foreach (FileSystemInfo fileSystemInfo in fileSystemInfos)
			{
				if (fileSystemInfo.Attributes != FileAttributes.Directory)
				{
					sortedList.Add(new SortDateTime(fileSystemInfo.LastWriteTime), fileSystemInfo.Name);
				}
			}
			return sortedList;
		}

		public static List<TextLoggerEntity> GetTextLogger()
		{
			string filePath = "";
			if (GetFileList().Count > 0)
			{
				filePath = Path.Combine(APP_LOG_DIRECTORY, GetFileList().Values[0]);
			}
			return GetTextLogger(filePath);
		}

		public static List<TextLoggerEntity> GetTextLogger(string filePath)
		{
			List<TextLoggerEntity> list = new List<TextLoggerEntity>();
			string[] array = LoadFile(filePath).Split(new char[1]
			{
				'\n'
			}, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split('|');
				list.Add(new TextLoggerEntity(DateTime.Parse(array2[0].Trim()), array2[1], array2[2], array2[3]));
			}
			return list;
		}

		public static string LoadFile(string path)
		{
			if (!File.Exists(path))
			{
				return "";
			}
			FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			if (fileStream == null)
			{
				throw new IOException("Unable to open the file: " + path);
			}
			StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8);
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			return result;
		}

		public static bool Write(string logContent)
		{
			return Write(logContent, "AppError");
		}

		public static bool Write(string logContent, string fileName)
		{
			bool result = true;
			if (string.IsNullOrEmpty(fileName))
			{
				fileName = "AppError";
			}
			try
			{
				string text = Path.Combine(APP_LOG_DIRECTORY, DateTime.Now.ToString("yyyyMMdd") + fileName + LOG_SUFFIX);
				FileInfo fileInfo = new FileInfo(text);
				if (fileInfo.Exists && fileInfo.Length >= 800000)
				{
					fileInfo.CopyTo(text.Replace(LOG_SUFFIX, TextUtility.CreateRandomNum(5) + LOG_SUFFIX));
					File.Delete(text);
				}
				FileStream fileStream = new FileStream(text, FileMode.Append, FileAccess.Write, FileShare.Read);
				StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0:yyyy'/'MM'/'dd' 'HH':'mm':'ss}", DateTime.Now);
				stringBuilder.Append("|");
				if (WRITE_APP_LOG)
				{
					stringBuilder.Append(logContent.Replace("\r", "").Replace("\n", "<br />"));
					stringBuilder.Append("|");
					stringBuilder.Append(GameRequest.GetUserIP());
					stringBuilder.Append("|");
					stringBuilder.Append(GameRequest.GetUrl());
				}
				else
				{
					stringBuilder.Append(logContent);
				}
				stringBuilder.Append("\r\n");
				streamWriter.Write(stringBuilder.ToString());
				streamWriter.Flush();
				streamWriter.Close();
				streamWriter.Dispose();
				fileStream.Close();
				fileStream.Dispose();
				return result;
			}
			catch
			{
				return false;
			}
		}

		public static bool Write(string logContent, string classUrl, string funcName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(classUrl))
			{
				stringBuilder.Append(classUrl);
			}
			if (!string.IsNullOrEmpty(funcName))
			{
				stringBuilder.Append("/");
				stringBuilder.Append(funcName);
			}
			if (!string.IsNullOrEmpty(logContent))
			{
				if (WRITE_APP_LOG)
				{
					stringBuilder.Append("<br />");
				}
				else
				{
					stringBuilder.Append("\r\n\t");
				}
				stringBuilder.Append(logContent);
			}
			return Write(stringBuilder.ToString(), "AppDebug");
		}

		public static bool Write(Type cType, string funcName, string text)
		{
			return Write(cType.Namespace + cType.Name, funcName, text);
		}
	}
}
