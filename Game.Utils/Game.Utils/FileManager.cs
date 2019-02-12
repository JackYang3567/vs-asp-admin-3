using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Game.Utils
{
	public abstract class FileManager
	{
		public static void CopyDirectories(string srcDir, string desDir)
		{
			try
			{
				DirectoryInfo dInfo = new DirectoryInfo(srcDir);
				CopyDirectoryInfo(dInfo, srcDir, desDir);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

		private static void CopyDirectoryInfo(DirectoryInfo dInfo, string srcDir, string desDir)
		{
			if (!Exists(desDir, FsoMethod.Folder))
			{
				Create(desDir, FsoMethod.Folder);
			}
			DirectoryInfo[] directories = dInfo.GetDirectories();
			DirectoryInfo[] array = directories;
			foreach (DirectoryInfo directoryInfo in array)
			{
				CopyDirectoryInfo(directoryInfo, directoryInfo.FullName, desDir + directoryInfo.FullName.Replace(srcDir, ""));
			}
			FileInfo[] files = dInfo.GetFiles();
			FileInfo[] array2 = files;
			foreach (FileInfo fileInfo in array2)
			{
				CopyFile(fileInfo.FullName, desDir + fileInfo.FullName.Replace(srcDir, ""));
			}
		}

		public static void CopyFile(string srcFile, string desFile)
		{
			try
			{
				File.Copy(srcFile, desFile, true);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

		public static bool CopyFileStream(string srcFile, string desFile)
		{
			try
			{
				FileStream fileStream = new FileStream(srcFile, FileMode.Open, FileAccess.Read);
				FileStream fileStream2 = new FileStream(desFile, FileMode.Create, FileAccess.Write);
				BinaryReader binaryReader = new BinaryReader(fileStream);
				BinaryWriter binaryWriter = new BinaryWriter(fileStream2);
				binaryReader.BaseStream.Seek(0L, SeekOrigin.Begin);
				binaryReader.BaseStream.Seek(0L, SeekOrigin.End);
				while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
				{
					binaryWriter.Write(binaryReader.ReadByte());
				}
				binaryReader.Close();
				binaryWriter.Close();
				fileStream.Flush();
				fileStream.Close();
				fileStream2.Flush();
				fileStream2.Close();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static void Create(string file, FsoMethod method)
		{
			try
			{
				switch (method)
				{
				case FsoMethod.File:
					WriteFile(file, "");
					break;
				case FsoMethod.Folder:
					Directory.CreateDirectory(file);
					break;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

		public static void Delete(string file, FsoMethod method)
		{
			try
			{
				if (method == FsoMethod.File)
				{
					File.Delete(file);
				}
				if (method == FsoMethod.Folder)
				{
					Directory.Delete(file, true);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

		private static long[] DirInfo(DirectoryInfo directory)
		{
			long[] array = new long[3];
			long num = 0L;
			long num2 = 0L;
			long num3 = 0L;
			FileInfo[] files = directory.GetFiles();
			num3 += files.Length;
			FileInfo[] array2 = files;
			foreach (FileInfo fileInfo in array2)
			{
				num += fileInfo.Length;
			}
			DirectoryInfo[] directories = directory.GetDirectories();
			num2 += directories.Length;
			DirectoryInfo[] array3 = directories;
			foreach (DirectoryInfo directory2 in array3)
			{
				num += DirInfo(directory2)[0];
				num2 += DirInfo(directory2)[1];
				num3 += DirInfo(directory2)[2];
			}
			array[0] = num;
			array[1] = num2;
			array[2] = num3;
			return array;
		}

		public static bool Exists(string file, FsoMethod method)
		{
			try
			{
				switch (method)
				{
				case FsoMethod.File:
					return File.Exists(file);
				case FsoMethod.Folder:
					return Directory.Exists(file);
				default:
					return false;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

		private static string[] GetDirectories(string directory)
		{
			return Directory.GetDirectories(directory);
		}

		public static DataTable GetDirectoryFilesList(string directory, FsoMethod method)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("FullName");
			dataTable.Columns.Add("ContentType");
			dataTable.Columns.Add("Type");
			dataTable.Columns.Add("Path");
			dataTable.Columns.Add("LastWriteTime");
			dataTable.Columns.Add("Length");
			if (method != FsoMethod.File)
			{
				for (int i = 0; i < GetDirectories(directory).Length; i++)
				{
					DataRow dataRow = dataTable.NewRow();
					DirectoryInfo directoryInfo = new DirectoryInfo(GetDirectories(directory)[i]);
					dataRow[0] = directoryInfo.Name;
					dataRow[1] = directoryInfo.FullName;
					dataRow[2] = "";
					dataRow[3] = 0;
					dataRow[4] = directoryInfo.FullName.Replace(directoryInfo.Name, "");
					dataRow[5] = directoryInfo.LastWriteTime;
					dataRow[6] = "";
					dataTable.Rows.Add(dataRow);
				}
			}
			if (method != 0)
			{
				for (int i = 0; i < GetFiles(directory).Length; i++)
				{
					DataRow dataRow = dataTable.NewRow();
					FileInfo fileInfo = new FileInfo(GetFiles(directory)[i]);
					dataRow[0] = fileInfo.Name;
					dataRow[1] = fileInfo.FullName;
					dataRow[2] = fileInfo.Extension.Replace(".", "");
					dataRow[3] = 1;
					dataRow[4] = fileInfo.DirectoryName + "\\";
					dataRow[5] = fileInfo.LastWriteTime;
					dataRow[6] = fileInfo.Length;
					dataTable.Rows.Add(dataRow);
				}
			}
			return dataTable;
		}

		public static IList<FolderInfo> GetDirectoryFilesListForObject(string directory, FsoMethod method)
		{
			return DataHelper.ConvertDataTableToObjects<FolderInfo>(GetDirectoryFilesList(directory, method));
		}

		public static IList<FolderInfo> GetDirectoryListForObject(string directory, FsoMethod method)
		{
			return DataHelper.ConvertDataTableToObjects<FolderInfo>(GetDirectoryList(directory, method));
		}

		public static long[] GetDirectoryInfo(string directory)
		{
			DirectoryInfo directory2 = new DirectoryInfo(directory);
			return DirInfo(directory2);
		}

		private static DataTable GetDirectoryList(DirectoryInfo directoryInfo, FsoMethod method)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("FullName");
			dataTable.Columns.Add("ContentType");
			dataTable.Columns.Add("Type");
			dataTable.Columns.Add("Path");
			dataTable.Columns.Add("LastWriteTime");
			dataTable.Columns.Add("Length");
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			DirectoryInfo[] array = directories;
			foreach (DirectoryInfo directoryInfo2 in array)
			{
				if (method == FsoMethod.File)
				{
					dataTable = Merge(dataTable, GetDirectoryList(directoryInfo2, method));
				}
				else
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow[0] = directoryInfo2.Name;
					dataRow[1] = directoryInfo2.FullName;
					dataRow[2] = "";
					dataRow[3] = 0;
					dataRow[4] = directoryInfo2.FullName.Replace(directoryInfo2.Name, "");
					dataRow[5] = directoryInfo2.LastWriteTime;
					dataRow[6] = "";
					dataTable.Rows.Add(dataRow);
					dataTable = Merge(dataTable, GetDirectoryList(directoryInfo2, method));
				}
			}
			if (method != 0)
			{
				FileInfo[] files = directoryInfo.GetFiles();
				FileInfo[] array2 = files;
				foreach (FileInfo fileInfo in array2)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow[0] = fileInfo.Name;
					dataRow[1] = fileInfo.FullName;
					dataRow[2] = fileInfo.Extension.Replace(".", "");
					dataRow[3] = 1;
					dataRow[4] = fileInfo.DirectoryName + "\\";
					dataRow[5] = fileInfo.LastWriteTime;
					dataRow[6] = fileInfo.Length;
					dataTable.Rows.Add(dataRow);
				}
			}
			return dataTable;
		}

		public static DataTable GetDirectoryList(string directory, FsoMethod method)
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(directory);
				return GetDirectoryList(directoryInfo, method);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

		private static string[] GetFiles(string directory)
		{
			return Directory.GetFiles(directory);
		}

		private static DataTable Merge(DataTable parent, DataTable child)
		{
			for (int i = 0; i < child.Rows.Count; i++)
			{
				DataRow dataRow = parent.NewRow();
				for (int j = 0; j < parent.Columns.Count; j++)
				{
					dataRow[j] = child.Rows[i][j];
				}
				parent.Rows.Add(dataRow);
			}
			return parent;
		}

		public static void Move(string srcFile, string desFile, FsoMethod method)
		{
			try
			{
				if (method == FsoMethod.File)
				{
					File.Move(srcFile, desFile);
				}
				if (method == FsoMethod.Folder)
				{
					Directory.Move(srcFile, desFile);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
		}

		public static string ReadFile(string file)
		{
			UTF8Encoding encoding = new UTF8Encoding();
			return ReadFile(file, encoding);
		}

		public static string ReadFile(string file, Encoding encoding)
		{
			string result = "";
			FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
			StreamReader streamReader = new StreamReader(fileStream, encoding);
			try
			{
				result = streamReader.ReadToEnd();
				return result;
			}
			catch
			{
				return result;
			}
			finally
			{
				fileStream.Flush();
				fileStream.Close();
				streamReader.Close();
			}
		}

		public static byte[] ReadFileReturnBytes(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return null;
			}
			FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			byte[] result = binaryReader.ReadBytes((int)fileStream.Length);
			fileStream.Flush();
			fileStream.Close();
			binaryReader.Close();
			return result;
		}

		public static void WriteBuffToFile(byte[] buff, string filePath)
		{
			WriteBuffToFile(buff, 0, buff.Length, filePath);
		}

		public static void WriteBuffToFile(byte[] buff, int offset, int len, string filePath)
		{
			string directoryName = Path.GetDirectoryName(filePath);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(buff, offset, len);
			binaryWriter.Flush();
			binaryWriter.Close();
			fileStream.Close();
		}

		public static void WriteFile(string file, string fileContent)
		{
			UTF8Encoding encoding = new UTF8Encoding();
			WriteFile(file, fileContent, encoding);
		}

		public static void WriteFile(string file, string fileContent, Encoding encoding)
		{
			FileInfo fileInfo = new FileInfo(file);
			if (!Directory.Exists(fileInfo.DirectoryName))
			{
				Directory.CreateDirectory(fileInfo.DirectoryName);
			}
			FileStream fileStream = new FileStream(file, FileMode.Create, FileAccess.Write);
			StreamWriter streamWriter = new StreamWriter(fileStream, encoding);
			try
			{
				streamWriter.Write(fileContent);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
			finally
			{
				streamWriter.Flush();
				fileStream.Flush();
				streamWriter.Close();
				fileStream.Close();
			}
		}

		public static void WriteFile(string file, string fileContent, bool append)
		{
			UTF8Encoding encoding = new UTF8Encoding();
			WriteFile(file, fileContent, append, encoding);
		}

		public static void WriteFile(string file, string fileContent, bool append, Encoding encoding)
		{
			FileInfo fileInfo = new FileInfo(file);
			if (!Directory.Exists(fileInfo.DirectoryName))
			{
				Directory.CreateDirectory(fileInfo.DirectoryName);
			}
			StreamWriter streamWriter = new StreamWriter(file, append, encoding);
			try
			{
				streamWriter.Write(fileContent);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
			finally
			{
				streamWriter.Flush();
				streamWriter.Close();
			}
		}

		public static string GetCurrentLogName(string directory, string path, ref int i)
		{
			string empty = string.Empty;
			empty = ((i == 0) ? path : (directory + "ErrorLog" + DateTime.Now.ToString("yyyy-MM") + "[" + i + "].log"));
			if (File.Exists(empty))
			{
				i++;
				return GetCurrentLogName(directory, empty, ref i);
			}
			return path;
		}
	}
}
