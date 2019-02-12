using System;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Game.Facade
{
	public class FileOperate
	{
		private string sException;

		public static void ExportDataGrid(string FileType, string FileName, DataGrid DG)
		{
			HttpContext.Current.Response.Charset = "GB2312";
			HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
			HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
			HttpContext.Current.Response.ContentType = FileType;
			Page page = new Page();
			page.EnableViewState = false;
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
			DG.RenderControl(writer);
			HttpContext.Current.Response.Write(stringWriter.ToString());
			HttpContext.Current.Response.End();
		}

		public static void FolderCreate(string filepath)
		{
			if (!Directory.Exists(filepath))
			{
				Directory.CreateDirectory(filepath);
			}
		}

		public static void FolderDelete(string filepath)
		{
			if (Directory.Exists(filepath))
			{
				Directory.Delete(filepath);
			}
		}

		private static Hashtable getAllFies(string filesdirectorypath, out int dirnamelength)
		{
			Hashtable hashtable = new Hashtable();
			DirectoryInfo directoryInfo = new DirectoryInfo(filesdirectorypath);
			if (!directoryInfo.Exists)
			{
				throw new FileNotFoundException("目录:" + directoryInfo.FullName + "没有找到!");
			}
			dirnamelength = directoryInfo.Name.Length;
			getAllDirFiles(directoryInfo, hashtable);
			getAllDirsFiles(directoryInfo.GetDirectories(), hashtable);
			return hashtable;
		}

		private static void getAllDirsFiles(DirectoryInfo[] dirs, Hashtable filesList)
		{
			foreach (DirectoryInfo directoryInfo in dirs)
			{
				FileInfo[] files = directoryInfo.GetFiles("*.*");
				foreach (FileInfo fileInfo in files)
				{
					filesList.Add(fileInfo.FullName, fileInfo.LastWriteTime);
				}
				getAllDirsFiles(directoryInfo.GetDirectories(), filesList);
			}
		}

		private static void getAllDirFiles(DirectoryInfo dir, Hashtable filesList)
		{
			FileInfo[] files = dir.GetFiles("*.*");
			foreach (FileInfo fileInfo in files)
			{
				filesList.Add(fileInfo.FullName, fileInfo.LastWriteTime);
			}
		}

		public static string GetFileNames(string path)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DirectoryInfo directoryInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(path));
			FileInfo[] files = directoryInfo.GetFiles();
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				stringBuilder.Append(fileInfo.Name + "|");
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			DirectoryInfo[] array2 = directories;
			foreach (DirectoryInfo directoryInfo2 in array2)
			{
				FileInfo[] files2 = directoryInfo2.GetFiles();
				FileInfo[] array3 = files2;
				foreach (FileInfo fileInfo2 in array3)
				{
					stringBuilder.Append(fileInfo2.Name + "|");
				}
			}
			return stringBuilder.ToString();
		}

		public static string ReadHtmlFile(string temp)
		{
			StreamReader streamReader = null;
			string text = "";
			try
			{
				streamReader = new StreamReader(temp);
				return streamReader.ReadToEnd();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				streamReader.Dispose();
				streamReader.Close();
			}
		}

		public static bool WriteHtmlFile(string str, string htmlfilename)
		{
			StreamWriter streamWriter = null;
			try
			{
				streamWriter = new StreamWriter(htmlfilename, false, Encoding.UTF8);
				streamWriter.Write(str);
				streamWriter.Flush();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				streamWriter.Dispose();
				streamWriter.Close();
			}
			return true;
		}

		public static string GetXmlValue(string Target, string attributes, string xmlPath)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlPath);
			XmlNode documentElement = xmlDocument.DocumentElement;
			XmlNode xmlNode = documentElement.SelectSingleNode(Target);
			if (xmlNode != null)
			{
				return xmlNode.Attributes[attributes].Value;
			}
			return string.Empty;
		}

		public static void SetXmlValue(string Target, string attributes, string TargetValue, string xmlPath)
		{
			SetXmlTargetValue(Target, attributes, TargetValue, xmlPath);
		}

		private static void SetXmlTargetValue(string Target, string attributes, string TargetValue, string xmlPath)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlPath);
			XmlElement documentElement = xmlDocument.DocumentElement;
			XmlNode xmlNode = documentElement.SelectSingleNode(Target);
			if (xmlNode != null)
			{
				XmlElement xmlElement = (XmlElement)xmlNode;
				xmlElement.SetAttribute(attributes, TargetValue);
			}
			else
			{
				xmlNode = xmlDocument.CreateElement(Target);
				documentElement.AppendChild(xmlNode);
				xmlNode.InnerText = TargetValue;
			}
			xmlDocument.Save(xmlPath);
		}

		public string GetText(string str)
		{
			return Regex.Replace(str, "src[^>]*[^/].(?:jpg|bmp|gif|png|jpeg|JPG|BMP|GIF|JPEG)(?:\\\"|\\')", SaveYuanFile);
		}

		protected string OverrideFileName(string filePath, string fileName)
		{
			string result = fileName;
			if (File.Exists(filePath + fileName))
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
				string extension = Path.GetExtension(fileName);
				string empty = string.Empty;
				int num = 1;
				while (true)
				{
					empty = fileNameWithoutExtension + "(" + num.ToString() + ")" + extension;
					if (!File.Exists(filePath + empty))
					{
						break;
					}
					num++;
				}
				result = empty;
			}
			return result;
		}

		public static string GetFileName()
		{
			Thread.Sleep(1000);
			string str = DateTime.Now.Year.ToString() + "-";
			str = ((DateTime.Now.Month.ToString().Length >= 2) ? (str + DateTime.Now.Month.ToString() + "-") : (str + "0" + DateTime.Now.Month.ToString() + "-"));
			str = ((DateTime.Now.Day.ToString().Length >= 2) ? (str + DateTime.Now.Day.ToString() + "-") : (str + "0" + DateTime.Now.Day.ToString() + "-"));
			str = ((DateTime.Now.Hour.ToString().Length >= 2) ? (str + DateTime.Now.Hour.ToString() + "-") : (str + "0" + DateTime.Now.Hour.ToString() + "-"));
			str = ((DateTime.Now.Minute.ToString().Length >= 2) ? (str + DateTime.Now.Minute.ToString() + "-") : (str + "0" + DateTime.Now.Minute.ToString() + "-"));
			if (DateTime.Now.Second.ToString().Length >= 2)
			{
				return str + DateTime.Now.Second.ToString();
			}
			return str + "0" + DateTime.Now.Second.ToString();
		}

		public static string UpLoadFile(FileUpload fileupload, string Folders)
		{
			string fileName = fileupload.PostedFile.FileName;
			if (fileName == null || fileName.Equals(""))
			{
				return "";
			}
			string str = fileName.Substring(fileName.LastIndexOf("."));
			string fileName2 = GetFileName();
			string text = Folders + fileName2 + str;
			string text2 = HttpContext.Current.Server.MapPath(text);
			if (File.Exists(text2))
			{
				File.Delete(text2);
			}
			fileupload.PostedFile.SaveAs(text2);
			return text;
		}

		private bool GetHttpFile(string sUrl, string sSavePath)
		{
			bool result = false;
			WebResponse webResponse = null;
			WebRequest webRequest = WebRequest.Create(sUrl);
			webRequest.Timeout = 100000;
			try
			{
				webResponse = webRequest.GetResponse();
				return result;
			}
			catch (WebException ex)
			{
				sException = ex.Message.ToString();
				return result;
			}
			catch (Exception ex2)
			{
				sException = ex2.ToString();
				return result;
			}
			finally
			{
				if (webResponse != null)
				{
					BinaryReader binaryReader = new BinaryReader(webResponse.GetResponseStream(), Encoding.GetEncoding("GB2312"));
					int num = Convert.ToInt32(webResponse.ContentLength);
					try
					{
						FileStream fileStream = (!File.Exists(HttpContext.Current.Request.MapPath("RecievedData.tmp"))) ? File.Create(sSavePath) : File.OpenWrite(sSavePath);
						fileStream.SetLength(num);
						fileStream.Write(binaryReader.ReadBytes(num), 0, num);
						fileStream.Close();
					}
					catch (Exception)
					{
					}
					finally
					{
						binaryReader.Close();
						webResponse.Close();
					}
					result = true;
				}
			}
		}

		private string SaveYuanFile(Match m)
		{
			string text = "";
			string value = m.Value;
			string text2 = "";
			text2 = value.Substring(5);
			text2 = text2.Substring(0, text2.IndexOf("\""));
			Regex regex = new Regex("^http://*");
			if (!regex.Match(text2).Success)
			{
				return value;
			}
			value = value.Substring(5);
			value = value.Substring(0, value.IndexOf("\""));
			string str = ConfigurationManager.AppSettings["yuanimg"].ToString();
			string text3 = value;
			string str2 = text3.Substring(text3.LastIndexOf("."));
			string fileName = GetFileName();
			string text4 = str + fileName + str2;
			if (File.Exists(HttpContext.Current.Request.MapPath(text4)))
			{
				File.Delete(HttpContext.Current.Request.MapPath(text4));
			}
			GetHttpFile(value, HttpContext.Current.Request.MapPath(text4));
			return "src=\"/" + text4.Replace("~/", "") + "\"";
		}

		public static bool MoveFiles(string oldpath, string newpath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(oldpath));
			FileInfo[] files = directoryInfo.GetFiles();
			try
			{
				FileInfo[] array = files;
				foreach (FileInfo fileInfo in array)
				{
					fileInfo.MoveTo(HttpContext.Current.Server.MapPath(newpath + fileInfo.Name));
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool MoveFile(string oldpath, string newpath)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(oldpath));
				fileInfo.MoveTo(HttpContext.Current.Server.MapPath(newpath));
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static void DeleteFile(string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string defVal, byte[] retVal, int size, string filePath);

		public static void IniWriteValue(string section, string key, string iValue, string path)
		{
			WritePrivateProfileString(section, key, iValue, path);
		}

		public static string IniReadValue(string section, string key, string path)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			GetPrivateProfileString(section, key, "", stringBuilder, 255, path);
			return stringBuilder.ToString();
		}

		public static byte[] IniReadValues(string section, string key, string path)
		{
			byte[] array = new byte[255];
			GetPrivateProfileString(section, key, "", array, 255, path);
			return array;
		}

		[DllImport("kernel32.dll")]
		public static extern int GetPrivateProfileSectionNamesA(byte[] buffer, int iLen, string fileName);

		[DllImport("kernel32.dll")]
		public static extern int GetPrivateProfileStringA(string segName, string keyName, string sDefault, byte[] buffer, int iLen, string fileName);

		public static ArrayList ReadSections(string path)
		{
			byte[] array = new byte[65535];
			int privateProfileSectionNamesA = GetPrivateProfileSectionNamesA(array, array.GetUpperBound(0), path);
			ArrayList arrayList = new ArrayList();
			if (privateProfileSectionNamesA > 0)
			{
				int num = 0;
				int num2 = 0;
				for (num = 0; num < privateProfileSectionNamesA; num++)
				{
					if (array[num] == 0)
					{
						string text = Encoding.Default.GetString(array, num2, num - num2).Trim();
						num2 = num + 1;
						if (text != "")
						{
							arrayList.Add(text);
						}
					}
				}
			}
			return arrayList;
		}

		public static ArrayList ReadKeys(string sectionName, string path)
		{
			byte[] array = new byte[5120];
			int privateProfileStringA = GetPrivateProfileStringA(sectionName, null, "", array, array.GetUpperBound(0), path);
			ArrayList arrayList = new ArrayList();
			if (privateProfileStringA > 0)
			{
				int num = 0;
				int num2 = 0;
				for (num = 0; num < privateProfileStringA; num++)
				{
					if (array[num] == 0)
					{
						string text = Encoding.Default.GetString(array, num2, num - num2).Trim();
						num2 = num + 1;
						if (text != "")
						{
							arrayList.Add(text);
						}
					}
				}
			}
			return arrayList;
		}

		public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(originalImagePath);
			int num = width;
			int num2 = height;
			int x = 0;
			int y = 0;
			int num3 = image.Width;
			int num4 = image.Height;
			string text = "Auto";
			switch (text)
			{
			case "W":
				num2 = image.Height * width / image.Width;
				break;
			case "H":
				num = image.Width * height / image.Height;
				break;
			case "Cut":
				if ((double)image.Width / (double)image.Height > (double)num / (double)num2)
				{
					num4 = image.Height;
					num3 = image.Height * num / num2;
					y = 0;
					x = (image.Width - num3) / 2;
				}
				else
				{
					num3 = image.Width;
					num4 = image.Width * height / num;
					x = 0;
					y = (image.Height - num4) / 2;
				}
				break;
			case "Auto":
				if ((double)image.Width / (double)image.Height > 1.0)
				{
					num2 = image.Height * width / image.Width;
				}
				else
				{
					num = image.Width * height / image.Height;
				}
				break;
			}
			System.Drawing.Image image2 = new Bitmap(num, num2);
			Graphics graphics = Graphics.FromImage(image2);
			graphics.InterpolationMode = InterpolationMode.High;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.Clear(Color.Transparent);
			graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num3, num4), GraphicsUnit.Pixel);
			try
			{
				image2.Save(thumbnailPath, ImageFormat.Jpeg);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				image.Dispose();
				image2.Dispose();
				graphics.Dispose();
			}
		}

		public static void MarkWaterText(string Path, string NewPath, string WaterText, int x, int y, bool isDelOld)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(Path));
			try
			{
				Graphics graphics = Graphics.FromImage(image);
				graphics.DrawImage(image, 0, 0, image.Width, image.Height);
				Font font = new Font("System", 12f);
				Brush brush = new SolidBrush(Color.Blue);
				graphics.DrawString(WaterText, font, brush, (float)x, (float)y);
				graphics.Dispose();
				image.Save(HttpContext.Current.Server.MapPath(NewPath));
				image.Dispose();
			}
			catch
			{
				image.Save(HttpContext.Current.Server.MapPath(NewPath));
				image.Dispose();
			}
			finally
			{
				if (isDelOld)
				{
					File.Delete(HttpContext.Current.Server.MapPath(Path));
				}
			}
		}

		public static void MarkWaterText(string Path, string NewPath, string WaterText, bool isDelOld)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(Path));
			try
			{
				Graphics graphics = Graphics.FromImage(image);
				graphics.DrawImage(image, 0, 0, image.Width, image.Height);
				int num = 12;
				num = ((image.Height <= image.Width) ? (image.Width / 10) : (image.Height / 10));
				if (num > 16)
				{
					num = 16;
				}
				Font font = new Font("Arial", (float)num);
				Brush brush = new SolidBrush(Color.Blue);
				int num2 = WaterText.Length * font.Height;
				int num3 = 15;
				int num4 = 15;
				StringFormat stringFormat = new StringFormat();
				stringFormat.FormatFlags = StringFormatFlags.NoWrap;
				graphics.DrawString(WaterText, font, brush, (float)(image.Width - num3 - num2), (float)(image.Height - num4 - font.Height), stringFormat);
				graphics.Dispose();
				image.Save(HttpContext.Current.Server.MapPath(NewPath));
				image.Dispose();
			}
			catch
			{
				image.Save(HttpContext.Current.Server.MapPath(NewPath));
				image.Dispose();
			}
			finally
			{
				if (isDelOld)
				{
					File.Delete(HttpContext.Current.Server.MapPath(Path));
				}
			}
		}

		public static void MarkWaterImage(string Path, string NewPath, string ImagePath, int x, int y, bool isDelOld)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(Path));
			System.Drawing.Image image2 = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(ImagePath));
			try
			{
				Graphics graphics = Graphics.FromImage(image);
				graphics.DrawImage(image2, new Rectangle(image.Width - image2.Width - x, image.Height - image2.Height - y, image2.Width, image2.Height), new Rectangle(0, 0, image2.Width, image2.Height), GraphicsUnit.Pixel);
				graphics.Dispose();
				image.Save(HttpContext.Current.Server.MapPath(NewPath));
				image.Dispose();
			}
			catch
			{
				image.Save(HttpContext.Current.Server.MapPath(NewPath));
				image.Dispose();
			}
			finally
			{
				if (isDelOld)
				{
					File.Delete(HttpContext.Current.Server.MapPath(Path));
				}
			}
		}

		public static void MarkWaterImage(string Path, string NewPath, string ImagePath, bool isDelOld)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(Path));
			System.Drawing.Image image2 = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(ImagePath));
			try
			{
				Graphics graphics = Graphics.FromImage(image);
				int num = 15;
				int num2 = 15;
				graphics.DrawImage(image2, new Rectangle(image.Width - image2.Width - num, image.Height - image2.Height - num2, image2.Width, image2.Height), new Rectangle(0, 0, image2.Width, image2.Height), GraphicsUnit.Pixel);
				graphics.Dispose();
				image.Save(HttpContext.Current.Server.MapPath(NewPath));
				image.Dispose();
			}
			catch
			{
				image.Save(HttpContext.Current.Server.MapPath(NewPath));
				image.Dispose();
			}
			finally
			{
				if (isDelOld)
				{
					File.Delete(HttpContext.Current.Server.MapPath(Path));
				}
			}
		}

		public static string GetRemotImage(string Path, string Url)
		{
			string[] array = new string[4]
			{
				"image/gif",
				"image/jpeg",
				"image/bmp",
				"image/png"
			};
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url.ToLower());
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			string text = httpWebResponse.ContentType.ToString();
			string str = "";
			bool flag = false;
			for (int i = 0; i <= array.Length - 1; i++)
			{
				if (text == array[i].ToString().ToLower())
				{
					switch (text)
					{
					case "image/gif":
						str = "gif";
						break;
					case "image/jpeg":
						str = "jpg";
						break;
					case "image/bmp":
						str = "bmp";
						break;
					case "image/png":
						str = "png";
						break;
					}
					flag = true;
					break;
				}
			}
			if (flag)
			{
				System.Drawing.Image image = System.Drawing.Image.FromStream(httpWebResponse.GetResponseStream());
				string empty = string.Empty;
				string text2 = DateTime.Now.Year.ToString();
				string text3 = DateTime.Now.Month.ToString();
				string text4 = DateTime.Now.Day.ToString();
				string text5 = DateTime.Now.Hour.ToString();
				string text6 = DateTime.Now.Minute.ToString();
				string text7 = DateTime.Now.Second.ToString();
				empty = text2 + text3 + text4 + text5 + text6 + text7;
				Random random = new Random();
				empty += random.Next(1000);
				empty = empty + "." + str;
				string text8 = Path + empty;
				image.Save(HttpContext.Current.Server.MapPath(text8));
				httpWebResponse.Close();
				return text8;
			}
			httpWebResponse.Close();
			return "";
		}

		private static string GetChartset(string url)
		{
			string hTML = getHTML(url);
			Regex regex = new Regex("charset\\b\\s*=\\s*(?<charset>[^\"]*)");
			string text = null;
			text = ((!regex.IsMatch(hTML)) ? Encoding.Default.EncodingName : regex.Match(hTML).Groups["charset"].Value);
			if (text.ToLower().Contains("gb2312"))
			{
				text = "gb2312";
			}
			if (text.ToLower().Contains("utf-8"))
			{
				text = "utf-8";
			}
			return text;
		}

		private static string getHTML(string url)
		{
			try
			{
				WebRequest webRequest = WebRequest.Create(url);
				WebResponse response = webRequest.GetResponse();
				Stream responseStream = response.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding(Encoding.ASCII.EncodingName));
				return streamReader.ReadToEnd();
			}
			catch (UriFormatException ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
			catch (WebException ex2)
			{
				Console.WriteLine(ex2.Message);
				return null;
			}
		}

		public static string GetRemotUrl(string url, int Type)
		{
			string text = url.Trim();
			string empty = string.Empty;
			string text2 = string.Empty;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text);
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				string contentEncoding = httpWebResponse.ContentEncoding;
				Stream responseStream = httpWebResponse.GetResponseStream();
				Encoding encoding = Encoding.GetEncoding(GetChartset(text));
				if (contentEncoding.ToLower() == "gzip")
				{
					GZipStream stream = new GZipStream(responseStream, CompressionMode.Decompress);
					using (StreamReader streamReader = new StreamReader(stream, encoding))
					{
						text2 = streamReader.ReadToEnd();
					}
				}
				else
				{
					using (StreamReader streamReader2 = new StreamReader(responseStream, encoding))
					{
						text2 = streamReader2.ReadToEnd();
					}
				}
				switch (Type)
				{
				case 1:
					return text2;
				case 2:
					return wipeScript(text2);
				case 3:
					return ClearHTML(text2);
				case 4:
					return getImages(text, text2);
				case 5:
					return getLink(text, text2);
				default:
					return empty;
				}
			}
			catch
			{
				return "Error";
			}
		}

		public static string getImages(string Url, string html)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			Regex regex = new Regex("<IMG[^>]+src=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>", RegexOptions.IgnoreCase);
			Match match = regex.Match(html);
			while (match.Success)
			{
				text2 = match.Groups["src"].Value.ToLower();
				if (text2.IndexOf("http") == 0)
				{
					text = text + match.Value + "<br />";
				}
				else
				{
					string[] array = Url.Trim().Split('/');
					try
					{
						text3 = ((array.Length <= 3) ? Url.Trim() : Url.Trim().Replace(array[array.Length - 1], ""));
					}
					catch
					{
						text3 = Url.Trim();
					}
					text = ((text2.IndexOf("/") != 0) ? (text + match.Value.Replace(match.Groups["src"].Value, text3 + match.Groups["src"].Value) + "<br/>") : (text + match.Value.Replace(match.Groups["src"].Value, "http://" + array[2] + match.Groups["src"].Value) + "<br/>"));
				}
				match = match.NextMatch();
			}
			return text;
		}

		public static string ClearHTML(string Htmlstring)
		{
			Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<style[\\s\\S]+</style *>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
			Htmlstring.Replace("<", "");
			Htmlstring.Replace(">", "");
			Htmlstring.Replace("\r\n", "");
			Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
			return Htmlstring;
		}

		public static string wipeScript(string html)
		{
			Regex regex = new Regex("<script[\\s\\S]+</script *>", RegexOptions.IgnoreCase);
			Regex regex2 = new Regex(" href *= *[\\s\\S]*script *:", RegexOptions.IgnoreCase);
			Regex regex3 = new Regex(" on[\\s\\S]*=", RegexOptions.IgnoreCase);
			Regex regex4 = new Regex("<iframe[\\s\\S]+</iframe *>", RegexOptions.IgnoreCase);
			Regex regex5 = new Regex("<frameset[\\s\\S]+</frameset *>", RegexOptions.IgnoreCase);
			html = regex.Replace(html, "");
			html = regex2.Replace(html, "");
			html = regex3.Replace(html, " _disibledevent=");
			html = regex4.Replace(html, "");
			html = regex5.Replace(html, "");
			return html;
		}

		public static string getLink(string Url, string html)
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			Regex regex = new Regex("<a[^>]+href=\\s*(?:'(?<href>[^']+)'|\"(?<href>[^\"]+)\"|(?<href>[^>\\s]+))\\s*[^>]*>(?<text>.*?)</a>", RegexOptions.IgnoreCase);
			MatchCollection matchCollection = regex.Matches(html);
			foreach (Match item in matchCollection)
			{
				text2 = item.Groups["href"].Value.ToLower();
				if (text2.IndexOf("http") == 0)
				{
					text = text + item.Value + "<br/>";
				}
				else
				{
					string[] array = Url.Trim().Split('/');
					try
					{
						text3 = ((array.Length <= 1) ? Url.Trim() : Url.Trim().Replace(array[array.Length - 1], ""));
					}
					catch
					{
						text3 = Url.Trim();
					}
					text = ((text2.IndexOf("/") != 0) ? ((text2.IndexOf("mailto") != 0) ? (text + item.Value.Replace(item.Groups["href"].Value, text3 + item.Groups["href"].Value) + "<br/>") : (text + item.Value + "<br/>")) : (text + item.Value.Replace(item.Groups["href"].Value, "http://" + array[2] + item.Groups["href"].Value) + "<br/>"));
				}
			}
			return text;
		}

		public static string GetChinaString(string stringToSub, int length)
		{
			Regex regex = new Regex("[一-龥]+", RegexOptions.Compiled);
			char[] array = stringToSub.ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				if (regex.IsMatch(array[i].ToString()))
				{
					stringBuilder.Append(array[i]);
					num += 2;
				}
				else
				{
					stringBuilder.Append(array[i]);
					num++;
				}
				if (num > length)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				return stringBuilder.ToString() + "..";
			}
			return stringBuilder.ToString();
		}

		public static string FiltSQL(string str)
		{
			str = str.Replace("select", "");
			str = str.Replace("insert", "");
			str = str.Replace("update", "");
			str = str.Replace("delete", "");
			str = str.Replace("create", "");
			str = str.Replace("drop", "");
			str = str.Replace("delcare", "");
			str = str.Replace("   ", "&nbsp;");
			str = str.Replace("<script>", "");
			str = str.Replace("</script>", "");
			str = str.Trim();
			return str;
		}

		public static bool IsNumeric(string str)
		{
			Regex regex = new Regex("^\\d+(\\.)?\\d*$");
			if (regex.IsMatch(str))
			{
				return true;
			}
			return false;
		}

		public static bool IsPic(string Ext)
		{
			bool result = false;
			string[] array = new string[5]
			{
				".jpg",
				".jpeg",
				".gif",
				".png",
				".bmp"
			};
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (text.Equals(Ext, StringComparison.InvariantCultureIgnoreCase))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public static void CreateImage(string checkCode)
		{
			int width = checkCode.Length * 11;
			Bitmap bitmap = new Bitmap(width, 19);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.Clear(Color.White);
			Color[] array = new Color[8]
			{
				Color.Black,
				Color.Red,
				Color.DarkBlue,
				Color.Green,
				Color.Chocolate,
				Color.Brown,
				Color.DarkCyan,
				Color.Purple
			};
			Random random = new Random();
			for (int i = 0; i < checkCode.Length; i++)
			{
				int num = random.Next(7);
				Font font = new Font("Microsoft Sans Serif", 11f);
				Brush brush = new SolidBrush(array[num]);
				graphics.DrawString(checkCode.Substring(i, 1), font, brush, (float)(i * 10 + 1), 0f, StringFormat.GenericDefault);
			}
			graphics.DrawRectangle(new Pen(Color.Black, 0f), 0, 0, bitmap.Width - 1, bitmap.Height - 1);
			MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, ImageFormat.Jpeg);
			HttpContext.Current.Response.ClearContent();
			HttpContext.Current.Response.ContentType = "image/Jpeg";
			HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
			graphics.Dispose();
			bitmap.Dispose();
		}

		public static string GetValidateCode(int num)
		{
			char[] array = "023456789".ToCharArray();
			Random random = new Random();
			string text = string.Empty;
			for (int i = 0; i < num; i++)
			{
				char c = array[random.Next(0, array.Length)];
				if (text.IndexOf(c) > -1)
				{
					i--;
				}
				else
				{
					text += c;
				}
			}
			return text;
		}
	}
}
