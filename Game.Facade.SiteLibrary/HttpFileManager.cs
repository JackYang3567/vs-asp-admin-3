using Game.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
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

		public bool Access
		{
			get
			{
				return this.m_access;
			}
		}

		public int FileCount
		{
			get
			{
				return this.m_FileCount;
			}
		}

		public int FolderCount
		{
			get
			{
				return this.m_folderCount;
			}
		}

		public string UploadFilePath
		{
			get
			{
				return this.m_uploadFilePath;
			}
		}

		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		public HttpFileManager()
		{
			this.m_value = "";
			this.m_access = false;
			this.m_folderPath = "";
			this.m_folderCount = 0;
			this.m_FileCount = 0;
			this.m_folderPath = HttpContext.Current.Request.QueryString["path"];
		}

		public HttpFileManager(string p_act)
		{
			this.m_folderPath = HttpContext.Current.Request.QueryString["path"];
			string pAct = p_act;
			string str = pAct;
			if (pAct != null)
			{
				if (str == "create")
				{
					this.CreateFolder();
					return;
				}
				if (str == "delete")
				{
					this.DeleteFileFolder();
					return;
				}
				if (str != "upload")
				{
					return;
				}
				this.UploadFile();
			}
		}

		private void CreateFolder()
		{
			string formString = GameRequest.GetFormString("txtFolderName");
			string realPath = TextUtility.GetRealPath(this.m_folderPath);
			if (TextUtility.EmptyTrimOrNull(formString))
			{
				this.m_value = "目录名不能为空";
				return;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(realPath, "\\", formString));
			if (directoryInfo.Exists)
			{
				this.m_value = "目录名已存在";
				return;
			}
			try
			{
				directoryInfo.Create();
				this.m_value = string.Concat("创建目录成功, 目录名称为: ", formString);
			}
			catch
			{
				this.m_value = "创建目录失败, 权限不足";
			}
		}

		private void DeleteFileFolder()
		{
			string queryString = GameRequest.GetQueryString(HttpContext.Current.Request, "file");
			string realPath = TextUtility.GetRealPath(queryString);
			string str = GameRequest.GetQueryString(HttpContext.Current.Request, "type");
			if (TextUtility.EmptyTrimOrNull(queryString) || !queryString.StartsWith("/upload"))
			{
				this.m_value = "要删除的文件不存在";
				return;
			}
			if (str == "file")
			{
				if (!File.Exists(realPath))
				{
					this.m_value = "要删除的文件不存在";
					return;
				}
				try
				{
					File.Delete(realPath);
					this.m_value = string.Concat("删除文件成功, 被删除的文件为: ", Path.GetFileName(realPath));
				}
				catch
				{
					this.m_value = "删除文件失败, 权限不足";
				}
			}
			else if (str == "folder")
			{
				if (!Directory.Exists(realPath))
				{
					this.m_value = "要删除的目录不存在";
				}
				else
				{
					try
					{
						Directory.Delete(realPath, true);
						this.m_value = string.Concat("删除目录成功, 被删除的目录为: ", Path.GetFileName(realPath));
					}
					catch
					{
						this.m_value = "删除目录失败, 权限不足";
					}
				}
			}
		}

		public List<HttpFolderInfo> GetDirectories(string folderPath, string rootPath)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string queryString = GameRequest.GetQueryString("order");
			List<HttpFolderInfo> httpFolderInfos = new List<HttpFolderInfo>();
			IList<FolderInfo> directoryFilesListForObject = FileManager.GetDirectoryFilesListForObject(folderPath, FsoMethod.All);
			if (directoryFilesListForObject == null || directoryFilesListForObject.Count <= 0)
			{
				this.m_access = false;
				return httpFolderInfos;
			}
			foreach (FolderInfo folderInfo in directoryFilesListForObject)
			{
				stringBuilder.Remove(0, stringBuilder.Length);
				if (folderInfo.FsoType == FsoMethod.Folder)
				{
					stringBuilder.AppendFormat("<a href=\"Web_FilesManager.aspx?path={0}\">", Utility.UrlEncode(string.Concat(rootPath, "/", folderInfo.Name))).AppendFormat("<img src=\"images/attach/files/folder.gif\" alt=\"文件夹\" align=\"absmiddle\" /> {0} </a>", folderInfo.Name).AppendFormat(" <a href=\"Web_FilesManager.aspx?act=compress&amp;path={0}&amp;objfolder={1}\" onclick=\"javascript:compressMsg();\">", Utility.UrlEncode(this.m_folderPath), Utility.UrlEncode(folderInfo.FullName));
					HttpFolderInfo httpFolderInfo = new HttpFolderInfo(folderInfo.Name, Utility.UrlEncode(folderInfo.FullName), stringBuilder.ToString(), "", (long)0, "folder", folderInfo.LastWriteTime);
					httpFolderInfos.Add(httpFolderInfo);
					HttpFileManager mFolderCount = this;
					mFolderCount.m_folderCount = mFolderCount.m_folderCount + 1;
				}
				if (folderInfo.FsoType != FsoMethod.File)
				{
					continue;
				}
				if (!TextUtility.InArray(folderInfo.ContentType, "jpg,gif,png,bmp,psd", ",", true))
				{
					stringBuilder.AppendFormat("<a href=\"file.axd?file={0}\" target=\"_new\">", Utility.UrlEncode(string.Concat(rootPath, "/", folderInfo.Name)));
					string contentType = "default";
					string str = folderInfo.ContentType;
					string str1 = str;
					if (str != null)
					{
						switch (str1)
						{
							case "mp3":
							case "wav":
							case "wma":
							case "wmv":
							{
								contentType = "mp3";
								break;
							}
							case "zip":
							case "7z":
							case "rar":
							{
								contentType = "zip";
								break;
							}
						}
					}
					if (!TextUtility.EmptyTrimOrNull(folderInfo.ContentType) && TextUtility.InArray(folderInfo.ContentType, "css,dll,doc,docx,swf,txt,xls,xlsx,xml", ",", true))
					{
						contentType = folderInfo.ContentType;
					}
					stringBuilder.AppendFormat("<img src=\"images/attach/files/{1}.gif\" alt=\"文件\" align=\"absmiddle\" /> {0}</a>", folderInfo.Name, contentType);
				}
				else
				{
					stringBuilder.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"showPopWin('Web_FilesView.aspx?url=file.axd?file={0}',700,433,null);\">", Utility.UrlEncode(string.Concat(rootPath, "/", folderInfo.Name))).AppendFormat("<img src=\"images/attach/files/{1}.gif\" alt=\"文件\" align=\"absmiddle\" /> {0}</a>", folderInfo.Name, folderInfo.ContentType);
				}
				HttpFolderInfo httpFolderInfo1 = new HttpFolderInfo(folderInfo.Name, Utility.UrlEncode(folderInfo.FullName), stringBuilder.ToString(), folderInfo.ContentType, folderInfo.Length, "file", folderInfo.LastWriteTime);
				httpFolderInfos.Add(httpFolderInfo1);
				HttpFileManager mFileCount = this;
				mFileCount.m_FileCount = mFileCount.m_FileCount + 1;
			}
			this.m_access = true;
			if (!TextUtility.EmptyTrimOrNull(queryString))
			{
				httpFolderInfos.Sort(new FilesComparer(queryString));
			}
			return httpFolderInfos;
		}

		private void UploadFile()
		{
			HttpPostedFile item = HttpContext.Current.Request.Files["fileUpload"];
			string realPath = TextUtility.GetRealPath(this.m_folderPath);
			if (item.ContentLength == 0)
			{
				this.m_value = "请先选择文件";
				return;
			}
			if (item.ContentLength > 4096000)
			{
				this.m_value = "文件过大，无法进行上传";
				return;
			}
			if (item.ContentType.ToUpper().IndexOf("IMAGE") == -1)
			{
				this.m_value = "请选择图片文件";
				return;
			}
			string fileName = Path.GetFileName(item.FileName);
			if (!File.Exists(string.Concat(realPath, "\\", fileName)))
			{
				try
				{
					item.SaveAs(string.Concat(realPath, "\\", fileName));
					this.m_value = string.Concat("上传文件完毕, 文件名为: ", fileName);
					this.m_uploadFilePath = string.Concat(TextUtility.AddLast(this.m_folderPath, "/"), fileName);
				}
				catch
				{
					this.m_value = "写入文件失败, 权限不足";
				}
			}
			else
			{
				Random random = new Random();
				object[] fileNameWithoutExtension = new object[] { Path.GetFileNameWithoutExtension(fileName), "_", random.Next(1, 1000), Path.GetExtension(fileName) };
				string str = string.Concat(fileNameWithoutExtension);
				try
				{
					item.SaveAs(string.Concat(realPath, "\\", str));
					this.m_value = string.Concat("上传的文件名已存在, 自动重命名为: ", str);
					this.m_uploadFilePath = string.Concat(TextUtility.AddLast(this.m_folderPath, "/"), str);
				}
				catch
				{
					this.m_value = "写入文件失败, 权限不足";
				}
			}
		}
	}
}