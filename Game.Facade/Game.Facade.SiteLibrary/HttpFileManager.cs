using Game.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace Game.Facade.SiteLibrary
{
	public class HttpFileManager
	{
		private string m_value;

		private bool m_access;

		private string m_folderPath;

		private string m_uploadFilePath;

		private int m_folderCount;

		private int m_FileCount;

		public string Value
		{
			get
			{
				return m_value;
			}
		}

		public string UploadFilePath
		{
			get
			{
				return m_uploadFilePath;
			}
		}

		public bool Access
		{
			get
			{
				return m_access;
			}
		}

		public int FolderCount
		{
			get
			{
				return m_folderCount;
			}
		}

		public int FileCount
		{
			get
			{
				return m_FileCount;
			}
		}

		public HttpFileManager()
		{
			m_value = "";
			m_access = false;
			m_folderPath = "";
			m_folderCount = 0;
			m_FileCount = 0;
			HttpRequest request = HttpContext.Current.Request;
			m_folderPath = request.QueryString["path"];
		}

		public HttpFileManager(string p_act)
		{
			HttpRequest request = HttpContext.Current.Request;
			m_folderPath = request.QueryString["path"];
			string a;
			if ((a = p_act) != null)
			{
				if (!(a == "create"))
				{
					if (!(a == "delete"))
					{
						if (a == "upload")
						{
							UploadFile();
						}
					}
					else
					{
						DeleteFileFolder();
					}
				}
				else
				{
					CreateFolder();
				}
			}
		}

		public List<HttpFolderInfo> GetDirectories(string folderPath, string rootPath)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string queryString = GameRequest.GetQueryString("order");
			List<HttpFolderInfo> list = new List<HttpFolderInfo>();
			IList<FolderInfo> directoryFilesListForObject = FileManager.GetDirectoryFilesListForObject(folderPath, FsoMethod.All);
			if (directoryFilesListForObject == null || directoryFilesListForObject.Count <= 0)
			{
				m_access = false;
				return list;
			}
			foreach (FolderInfo item3 in directoryFilesListForObject)
			{
				stringBuilder.Remove(0, stringBuilder.Length);
				if (item3.FsoType == FsoMethod.Folder)
				{
					stringBuilder.AppendFormat("<a href=\"Web_FilesManager.aspx?path={0}\">", Utility.UrlEncode(rootPath + "/" + item3.Name)).AppendFormat("<img src=\"images/attach/files/folder.gif\" alt=\"文件夹\" align=\"absmiddle\" /> {0} </a>", item3.Name).AppendFormat(" <a href=\"Web_FilesManager.aspx?act=compress&amp;path={0}&amp;objfolder={1}\" onclick=\"javascript:compressMsg();\">", Utility.UrlEncode(m_folderPath), Utility.UrlEncode(item3.FullName));
					HttpFolderInfo item = new HttpFolderInfo(item3.Name, Utility.UrlEncode(item3.FullName), stringBuilder.ToString(), "", 0L, "folder", item3.LastWriteTime);
					list.Add(item);
					m_folderCount++;
				}
				if (item3.FsoType == FsoMethod.File)
				{
					if (TextUtility.InArray(item3.ContentType, "jpg,gif,png,bmp,psd", ",", true))
					{
						stringBuilder.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"showPopWin('Web_FilesView.aspx?url=file.axd?file={0}',700,433,null);\">", Utility.UrlEncode(rootPath + "/" + item3.Name)).AppendFormat("<img src=\"images/attach/files/{1}.gif\" alt=\"文件\" align=\"absmiddle\" /> {0}</a>", item3.Name, item3.ContentType);
					}
					else
					{
						stringBuilder.AppendFormat("<a href=\"file.axd?file={0}\" target=\"_new\">", Utility.UrlEncode(rootPath + "/" + item3.Name));
						string arg = "default";
						switch (item3.ContentType)
						{
						case "mp3":
						case "wav":
						case "wma":
						case "wmv":
							arg = "mp3";
							break;
						case "zip":
						case "7z":
						case "rar":
							arg = "zip";
							break;
						}
						if (!TextUtility.EmptyTrimOrNull(item3.ContentType) && TextUtility.InArray(item3.ContentType, "css,dll,doc,docx,swf,txt,xls,xlsx,xml", ",", true))
						{
							arg = item3.ContentType;
						}
						stringBuilder.AppendFormat("<img src=\"images/attach/files/{1}.gif\" alt=\"文件\" align=\"absmiddle\" /> {0}</a>", item3.Name, arg);
					}
					HttpFolderInfo item2 = new HttpFolderInfo(item3.Name, Utility.UrlEncode(item3.FullName), stringBuilder.ToString(), item3.ContentType, item3.Length, "file", item3.LastWriteTime);
					list.Add(item2);
					m_FileCount++;
				}
			}
			m_access = true;
			if (!TextUtility.EmptyTrimOrNull(queryString))
			{
				list.Sort(new FilesComparer(queryString));
			}
			return list;
		}

		private void CreateFolder()
		{
			string formString = GameRequest.GetFormString("txtFolderName");
			string realPath = TextUtility.GetRealPath(m_folderPath);
			if (TextUtility.EmptyTrimOrNull(formString))
			{
				m_value = "目录名不能为空";
			}
			else
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(realPath + "\\" + formString);
				if (!directoryInfo.Exists)
				{
					try
					{
						directoryInfo.Create();
						m_value = "创建目录成功, 目录名称为: " + formString;
					}
					catch
					{
						m_value = "创建目录失败, 权限不足";
					}
				}
				else
				{
					m_value = "目录名已存在";
				}
			}
		}

		private void DeleteFileFolder()
		{
			string queryString = GameRequest.GetQueryString(HttpContext.Current.Request, "file");
			string realPath = TextUtility.GetRealPath(queryString);
			string queryString2 = GameRequest.GetQueryString(HttpContext.Current.Request, "type");
			if (TextUtility.EmptyTrimOrNull(queryString) || !queryString.StartsWith("/upload"))
			{
				m_value = "要删除的文件不存在";
			}
			else if (queryString2 == "file")
			{
				if (File.Exists(realPath))
				{
					try
					{
						File.Delete(realPath);
						m_value = "删除文件成功, 被删除的文件为: " + Path.GetFileName(realPath);
					}
					catch
					{
						m_value = "删除文件失败, 权限不足";
					}
				}
				else
				{
					m_value = "要删除的文件不存在";
				}
			}
			else if (queryString2 == "folder")
			{
				if (Directory.Exists(realPath))
				{
					try
					{
						Directory.Delete(realPath, true);
						m_value = "删除目录成功, 被删除的目录为: " + Path.GetFileName(realPath);
					}
					catch
					{
						m_value = "删除目录失败, 权限不足";
					}
				}
				else
				{
					m_value = "要删除的目录不存在";
				}
			}
		}

		private void UploadFile()
		{
			HttpRequest request = HttpContext.Current.Request;
			HttpPostedFile httpPostedFile = request.Files["fileUpload"];
			string realPath = TextUtility.GetRealPath(m_folderPath);
			if (httpPostedFile.ContentLength == 0)
			{
				m_value = "请先选择文件";
			}
			else if (httpPostedFile.ContentLength > 4096000)
			{
				m_value = "文件过大，无法进行上传";
			}
			else if (httpPostedFile.ContentType.ToUpper().IndexOf("IMAGE") == -1)
			{
				m_value = "请选择图片文件";
			}
			else
			{
				string fileName = Path.GetFileName(httpPostedFile.FileName);
				if (File.Exists(realPath + "\\" + fileName))
				{
					Random random = new Random();
					string text = Path.GetFileNameWithoutExtension(fileName) + "_" + random.Next(1, 1000) + Path.GetExtension(fileName);
					try
					{
						httpPostedFile.SaveAs(realPath + "\\" + text);
						m_value = "上传的文件名已存在, 自动重命名为: " + text;
						m_uploadFilePath = TextUtility.AddLast(m_folderPath, "/") + text;
					}
					catch
					{
						m_value = "写入文件失败, 权限不足";
					}
				}
				else
				{
					try
					{
						httpPostedFile.SaveAs(realPath + "\\" + fileName);
						m_value = "上传文件完毕, 文件名为: " + fileName;
						m_uploadFilePath = TextUtility.AddLast(m_folderPath, "/") + fileName;
					}
					catch
					{
						m_value = "写入文件失败, 权限不足";
					}
				}
			}
		}
	}
}
