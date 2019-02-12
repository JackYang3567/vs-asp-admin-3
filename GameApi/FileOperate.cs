using System;
using System.Collections;
using System.Collections.Specialized;
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

namespace GameApi
{
	public class FileOperate
	{
		private string sException;

		public FileOperate()
		{
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

		public static void CreateImage(string checkCode)
		{
			Bitmap bitmap = new Bitmap(checkCode.Length * 11, 19);
			Graphics graphic = Graphics.FromImage(bitmap);
			graphic.Clear(Color.White);
			Color[] black = new Color[] { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Chocolate, Color.Brown, Color.DarkCyan, Color.Purple };
			Color[] colorArray = black;
			Random random = new Random();
			for (int i = 0; i < checkCode.Length; i++)
			{
				int num = random.Next(7);
				Font font = new Font("Microsoft Sans Serif", 11f);
				Brush solidBrush = new SolidBrush(colorArray[num]);
				graphic.DrawString(checkCode.Substring(i, 1), font, solidBrush, (float)(i * 10 + 1), 0f, StringFormat.GenericDefault);
			}
			graphic.DrawRectangle(new Pen(Color.Black, 0f), 0, 0, bitmap.Width - 1, bitmap.Height - 1);
			MemoryStream memoryStream = new MemoryStream();
			bitmap.Save(memoryStream, ImageFormat.Jpeg);
			HttpContext.Current.Response.ClearContent();
			HttpContext.Current.Response.ContentType = "image/Jpeg";
			HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
			graphic.Dispose();
			bitmap.Dispose();
		}

		public static void DeleteFile(string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		public static void ExportDataGrid(string FileType, string FileName, DataGrid DG)
		{
			HttpContext.Current.Response.Charset = "GB2312";
			HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
			HttpContext.Current.Response.AppendHeader("Content-Disposition", string.Concat("attachment;filename=", HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString()));
			HttpContext.Current.Response.ContentType = FileType;
			(new Page()).EnableViewState = false;
			StringWriter stringWriter = new StringWriter();
			DG.RenderControl(new HtmlTextWriter(stringWriter));
			HttpContext.Current.Response.Write(stringWriter.ToString());
			HttpContext.Current.Response.End();
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

		private static void getAllDirFiles(DirectoryInfo dir, Hashtable filesList)
		{
			FileInfo[] files = dir.GetFiles("*.*");
			for (int i = 0; i < (int)files.Length; i++)
			{
				FileInfo fileInfo = files[i];
				filesList.Add(fileInfo.FullName, fileInfo.LastWriteTime);
			}
		}

		private static void getAllDirsFiles(DirectoryInfo[] dirs, Hashtable filesList)
		{
			DirectoryInfo[] directoryInfoArray = dirs;
			for (int i = 0; i < (int)directoryInfoArray.Length; i++)
			{
				DirectoryInfo directoryInfo = directoryInfoArray[i];
				FileInfo[] files = directoryInfo.GetFiles("*.*");
				for (int j = 0; j < (int)files.Length; j++)
				{
					FileInfo fileInfo = files[j];
					filesList.Add(fileInfo.FullName, fileInfo.LastWriteTime);
				}
				FileOperate.getAllDirsFiles(directoryInfo.GetDirectories(), filesList);
			}
		}

		private static Hashtable getAllFies(string filesdirectorypath, out int dirnamelength)
		{
			Hashtable hashtables = new Hashtable();
			DirectoryInfo directoryInfo = new DirectoryInfo(filesdirectorypath);
			if (!directoryInfo.Exists)
			{
				throw new FileNotFoundException(string.Concat("目录:", directoryInfo.FullName, "没有找到!"));
			}
			dirnamelength = directoryInfo.Name.Length;
			FileOperate.getAllDirFiles(directoryInfo, hashtables);
			FileOperate.getAllDirsFiles(directoryInfo.GetDirectories(), hashtables);
			return hashtables;
		}

		private static string GetChartset(string url)
		{
			string hTML = FileOperate.getHTML(url);
			Regex regex = new Regex("charset\\b\\s*=\\s*(?<charset>[^\"]*)");
			string str = null;
			str = (!regex.IsMatch(hTML) ? Encoding.Default.EncodingName : regex.Match(hTML).Groups["charset"].Value);
			if (str.ToLower().Contains("gb2312"))
			{
				str = "gb2312";
			}
			if (str.ToLower().Contains("utf-8"))
			{
				str = "utf-8";
			}
			return str;
		}

		public static string GetChinaString(string stringToSub, int length)
		{
			Regex regex = new Regex("[一-龥]+", RegexOptions.Compiled);
			char[] charArray = stringToSub.ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			bool flag = false;
			int num1 = 0;
			while (num1 < (int)charArray.Length)
			{
				if (!regex.IsMatch(charArray[num1].ToString()))
				{
					stringBuilder.Append(charArray[num1]);
					num++;
				}
				else
				{
					stringBuilder.Append(charArray[num1]);
					num = num + 2;
				}
				if (num <= length)
				{
					num1++;
				}
				else
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return stringBuilder.ToString();
			}
			return string.Concat(stringBuilder.ToString(), "..");
		}

		public static string GetFileName()
		{
			Thread.Sleep(1000);
			int year = DateTime.Now.Year;
			string str = string.Concat(year.ToString(), "-");
			if (DateTime.Now.Month.ToString().Length >= 2)
			{
				int month = DateTime.Now.Month;
				str = string.Concat(str, month.ToString(), "-");
			}
			else
			{
				int num = DateTime.Now.Month;
				str = string.Concat(str, "0", num.ToString(), "-");
			}
			if (DateTime.Now.Day.ToString().Length >= 2)
			{
				int day = DateTime.Now.Day;
				str = string.Concat(str, day.ToString(), "-");
			}
			else
			{
				int day1 = DateTime.Now.Day;
				str = string.Concat(str, "0", day1.ToString(), "-");
			}
			if (DateTime.Now.Hour.ToString().Length >= 2)
			{
				int hour = DateTime.Now.Hour;
				str = string.Concat(str, hour.ToString(), "-");
			}
			else
			{
				int hour1 = DateTime.Now.Hour;
				str = string.Concat(str, "0", hour1.ToString(), "-");
			}
			if (DateTime.Now.Minute.ToString().Length >= 2)
			{
				int minute = DateTime.Now.Minute;
				str = string.Concat(str, minute.ToString(), "-");
			}
			else
			{
				int minute1 = DateTime.Now.Minute;
				str = string.Concat(str, "0", minute1.ToString(), "-");
			}
			if (DateTime.Now.Second.ToString().Length >= 2)
			{
				int second = DateTime.Now.Second;
				str = string.Concat(str, second.ToString());
			}
			else
			{
				int second1 = DateTime.Now.Second;
				str = string.Concat(str, "0", second1.ToString());
			}
			return str;
		}

		public static string GetFileNames(string path)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DirectoryInfo directoryInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(path));
			FileInfo[] files = directoryInfo.GetFiles();
			for (int i = 0; i < (int)files.Length; i++)
			{
				FileInfo fileInfo = files[i];
				stringBuilder.Append(string.Concat(fileInfo.Name, "|"));
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			for (int j = 0; j < (int)directories.Length; j++)
			{
				FileInfo[] fileInfoArray = directories[j].GetFiles();
				for (int k = 0; k < (int)fileInfoArray.Length; k++)
				{
					FileInfo fileInfo1 = fileInfoArray[k];
					stringBuilder.Append(string.Concat(fileInfo1.Name, "|"));
				}
			}
			return stringBuilder.ToString();
		}

		private static string getHTML(string url)
		{
			string end;
			try
			{
				Stream responseStream = WebRequest.Create(url).GetResponse().GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding(Encoding.ASCII.EncodingName));
				end = streamReader.ReadToEnd();
			}
			catch (UriFormatException uriFormatException)
			{
				Console.WriteLine(uriFormatException.Message);
				end = null;
			}
			catch (WebException webException)
			{
				Console.WriteLine(webException.Message);
				end = null;
			}
			return end;
		}

		private bool GetHttpFile(string sUrl, string sSavePath)
		{
			FileStream fileStream;
			bool flag = false;
			WebResponse response = null;
			WebRequest webRequest = WebRequest.Create(sUrl);
			webRequest.Timeout = 100000;
			try
			{
				try
				{
					response = webRequest.GetResponse();
				}
				catch (WebException webException)
				{
					this.sException = webException.Message.ToString();
				}
				catch (Exception exception)
				{
					this.sException = exception.ToString();
				}
			}
			finally
			{
				if (response != null)
				{
					BinaryReader binaryReader = new BinaryReader(response.GetResponseStream(), Encoding.GetEncoding("GB2312"));
					int num = Convert.ToInt32(response.ContentLength);
					try
					{
						try
						{
							fileStream = (!File.Exists(HttpContext.Current.Request.MapPath("RecievedData.tmp")) ? File.Create(sSavePath) : File.OpenWrite(sSavePath));
							fileStream.SetLength((long)num);
							fileStream.Write(binaryReader.ReadBytes(num), 0, num);
							fileStream.Close();
						}
						catch (Exception exception1)
						{
						}
					}
					finally
					{
						binaryReader.Close();
						response.Close();
					}
					flag = true;
				}
			}
			return flag;
		}

		public static string getImages(string Url, string html)
		{
			string str = "";
			string lower = "";
			string str1 = "";
			for (System.Text.RegularExpressions.Match i = (new Regex("<IMG[^>]+src=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>", RegexOptions.IgnoreCase)).Match(html); i.Success; i = i.NextMatch())
			{
				lower = i.Groups["src"].Value.ToLower();
				if (lower.IndexOf("http") != 0)
				{
					string[] strArrays = Url.Trim().Split(new char[] { '/' });
					try
					{
						str1 = ((int)strArrays.Length <= 3 ? Url.Trim() : Url.Trim().Replace(strArrays[(int)strArrays.Length - 1], ""));
					}
					catch
					{
						str1 = Url.Trim();
					}
					str = (lower.IndexOf("/") != 0 ? string.Concat(str, i.Value.Replace(i.Groups["src"].Value, string.Concat(str1, i.Groups["src"].Value)), "<br/>") : string.Concat(str, i.Value.Replace(i.Groups["src"].Value, string.Concat("http://", strArrays[2], i.Groups["src"].Value)), "<br/>"));
				}
				else
				{
					str = string.Concat(str, i.Value, "<br />");
				}
			}
			return str;
		}

		public static string getLink(string Url, string html)
		{
			string str = "";
			string lower = "";
			string str1 = "";
			foreach (System.Text.RegularExpressions.Match match in (new Regex("<a[^>]+href=\\s*(?:'(?<href>[^']+)'|\"(?<href>[^\"]+)\"|(?<href>[^>\\s]+))\\s*[^>]*>(?<text>.*?)</a>", RegexOptions.IgnoreCase)).Matches(html))
			{
				lower = match.Groups["href"].Value.ToLower();
				if (lower.IndexOf("http") != 0)
				{
					string[] strArrays = Url.Trim().Split(new char[] { '/' });
					try
					{
						str1 = ((int)strArrays.Length <= 1 ? Url.Trim() : Url.Trim().Replace(strArrays[(int)strArrays.Length - 1], ""));
					}
					catch
					{
						str1 = Url.Trim();
					}
					if (lower.IndexOf("/") != 0)
					{
						str = (lower.IndexOf("mailto") != 0 ? string.Concat(str, match.Value.Replace(match.Groups["href"].Value, string.Concat(str1, match.Groups["href"].Value)), "<br/>") : string.Concat(str, match.Value, "<br/>"));
					}
					else
					{
						str = string.Concat(str, match.Value.Replace(match.Groups["href"].Value, string.Concat("http://", strArrays[2], match.Groups["href"].Value)), "<br/>");
					}
				}
				else
				{
					str = string.Concat(str, match.Value, "<br/>");
				}
			}
			return str;
		}

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int GetPrivateProfileSectionNamesA(byte[] buffer, int iLen, string fileName);

		[DllImport("kernel32", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);

		[DllImport("kernel32", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern int GetPrivateProfileString(string section, string key, string defVal, byte[] retVal, int size, string filePath);

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern int GetPrivateProfileStringA(string segName, string keyName, string sDefault, byte[] buffer, int iLen, string fileName);

		public static string GetRemotImage(string Path, string Url)
		{
			string[] strArrays = new string[] { "image/gif", "image/jpeg", "image/bmp", "image/png" };
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url.ToLower());
			HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
			string str = response.ContentType.ToString();
			string str1 = "";
			bool flag = false;
			int num = 0;
			while (num <= (int)strArrays.Length - 1)
			{
				if (str != strArrays[num].ToString().ToLower())
				{
					num++;
				}
				else
				{
					string str2 = str;
					string str3 = str2;
					if (str2 != null)
					{
						if (str3 == "image/gif")
						{
							str1 = "gif";
						}
						else if (str3 == "image/jpeg")
						{
							str1 = "jpg";
						}
						else if (str3 == "image/bmp")
						{
							str1 = "bmp";
						}
						else if (str3 == "image/png")
						{
							str1 = "png";
						}
					}
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				response.Close();
				return "";
			}
			System.Drawing.Image image = System.Drawing.Image.FromStream(response.GetResponseStream());
			string empty = string.Empty;
			string str4 = DateTime.Now.Year.ToString();
			string str5 = DateTime.Now.Month.ToString();
			string str6 = DateTime.Now.Day.ToString();
			string str7 = DateTime.Now.Hour.ToString();
			string str8 = DateTime.Now.Minute.ToString();
			string str9 = DateTime.Now.Second.ToString();
			string[] strArrays1 = new string[] { str4, str5, str6, str7, str8, str9 };
			empty = string.Concat(strArrays1);
			Random random = new Random();
			empty = string.Concat(empty, random.Next(1000));
			empty = string.Concat(empty, ".", str1);
			string str10 = string.Concat(Path, empty);
			image.Save(HttpContext.Current.Server.MapPath(str10));
			response.Close();
			return str10;
		}

		public static string GetRemotUrl(string url, int Type)
		{
			string str = url.Trim();
			string empty = string.Empty;
			string end = string.Empty;
			try
			{
				HttpWebResponse response = (HttpWebResponse)((HttpWebRequest)WebRequest.Create(str)).GetResponse();
				string contentEncoding = response.ContentEncoding;
				Stream responseStream = response.GetResponseStream();
				Encoding encoding = Encoding.GetEncoding(FileOperate.GetChartset(str));
				if (contentEncoding.ToLower() != "gzip")
				{
					using (StreamReader streamReader = new StreamReader(responseStream, encoding))
					{
						end = streamReader.ReadToEnd();
					}
				}
				else
				{
					using (StreamReader streamReader1 = new StreamReader(new GZipStream(responseStream, CompressionMode.Decompress), encoding))
					{
						end = streamReader1.ReadToEnd();
					}
				}
				switch (Type)
				{
					case 1:
					{
						empty = end;
						break;
					}
					case 2:
					{
						empty = FileOperate.wipeScript(end);
						break;
					}
					case 3:
					{
						empty = FileOperate.ClearHTML(end);
						break;
					}
					case 4:
					{
						empty = FileOperate.getImages(str, end);
						break;
					}
					case 5:
					{
						empty = FileOperate.getLink(str, end);
						break;
					}
				}
			}
			catch
			{
				empty = "Error";
			}
			return empty;
		}

		public string GetText(string str)
		{
			string str1 = Regex.Replace(str, "src[^>]*[^/].(?:jpg|bmp|gif|png|jpeg|JPG|BMP|GIF|JPEG)(?:\\\"|\\')", new MatchEvaluator(this.SaveYuanFile));
			return str1;
		}

		public static string GetValidateCode(int num)
		{
			char[] charArray = "023456789".ToCharArray();
			Random random = new Random();
			string empty = string.Empty;
			for (int i = 0; i < num; i++)
			{
				char chr = charArray[random.Next(0, (int)charArray.Length)];
				if (empty.IndexOf(chr) <= -1)
				{
					empty = string.Concat(empty, chr);
				}
				else
				{
					i--;
				}
			}
			return empty;
		}

		public static string GetXmlValue(string Target, string attributes, string xmlPath)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlPath);
			XmlNode xmlNodes = xmlDocument.DocumentElement.SelectSingleNode(Target);
			if (xmlNodes == null)
			{
				return string.Empty;
			}
			return xmlNodes.Attributes[attributes].Value;
		}

		public static string IniReadValue(string section, string key, string path)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			FileOperate.GetPrivateProfileString(section, key, "", stringBuilder, 255, path);
			return stringBuilder.ToString();
		}

		public static byte[] IniReadValues(string section, string key, string path)
		{
			byte[] numArray = new byte[255];
			FileOperate.GetPrivateProfileString(section, key, "", numArray, 255, path);
			return numArray;
		}

		public static void IniWriteValue(string section, string key, string iValue, string path)
		{
			FileOperate.WritePrivateProfileString(section, key, iValue, path);
		}

		public static bool IsNumeric(string str)
		{
			if ((new Regex("^\\d+(\\.)?\\d*$")).IsMatch(str))
			{
				return true;
			}
			return false;
		}

		public static bool IsPic(string Ext)
		{
			bool flag = false;
			string[] strArrays = new string[] { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };
			string[] strArrays1 = strArrays;
			int num = 0;
			while (num < (int)strArrays1.Length)
			{
				if (!strArrays1[num].Equals(Ext, StringComparison.InvariantCultureIgnoreCase))
				{
					num++;
				}
				else
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(originalImagePath);
			int num = width;
			int num1 = height;
			int num2 = 0;
			int num3 = 0;
			int num4 = image.Width;
			int num5 = image.Height;
			string str = "Auto";
			string str1 = str;
			if (str != null && !(str1 == "HW"))
			{
				if (str1 == "W")
				{
					num1 = image.Height * width / image.Width;
				}
				else if (str1 == "H")
				{
					num = image.Width * height / image.Height;
				}
				else if (str1 != "Cut")
				{
					if (str1 == "Auto")
					{
						if ((double)image.Width / (double)image.Height <= 1)
						{
							num = image.Width * height / image.Height;
						}
						else
						{
							num1 = image.Height * width / image.Width;
						}
					}
				}
				else if ((double)image.Width / (double)image.Height <= (double)num / (double)num1)
				{
					num4 = image.Width;
					num5 = image.Width * height / num;
					num2 = 0;
					num3 = (image.Height - num5) / 2;
				}
				else
				{
					num5 = image.Height;
					num4 = image.Height * num / num1;
					num3 = 0;
					num2 = (image.Width - num4) / 2;
				}
			}
			System.Drawing.Image bitmap = new Bitmap(num, num1);
			Graphics graphic = Graphics.FromImage(bitmap);
			graphic.InterpolationMode = InterpolationMode.High;
			graphic.SmoothingMode = SmoothingMode.HighQuality;
			graphic.Clear(Color.Transparent);
			graphic.DrawImage(image, new Rectangle(0, 0, num, num1), new Rectangle(num2, num3, num4, num5), GraphicsUnit.Pixel);
			try
			{
				try
				{
					bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			finally
			{
				image.Dispose();
				bitmap.Dispose();
				graphic.Dispose();
			}
		}

		public static void MarkWaterImage(string Path, string NewPath, string ImagePath, int x, int y, bool isDelOld)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(Path));
			System.Drawing.Image image1 = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(ImagePath));
			try
			{
				try
				{
					Graphics graphic = Graphics.FromImage(image);
					graphic.DrawImage(image1, new Rectangle(image.Width - image1.Width - x, image.Height - image1.Height - y, image1.Width, image1.Height), new Rectangle(0, 0, image1.Width, image1.Height), GraphicsUnit.Pixel);
					graphic.Dispose();
					image.Save(HttpContext.Current.Server.MapPath(NewPath));
					image.Dispose();
				}
				catch
				{
					image.Save(HttpContext.Current.Server.MapPath(NewPath));
					image.Dispose();
				}
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
			System.Drawing.Image image1 = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(ImagePath));
			try
			{
				try
				{
					Graphics graphic = Graphics.FromImage(image);
					int num = 15;
					int num1 = 15;
					graphic.DrawImage(image1, new Rectangle(image.Width - image1.Width - num, image.Height - image1.Height - num1, image1.Width, image1.Height), new Rectangle(0, 0, image1.Width, image1.Height), GraphicsUnit.Pixel);
					graphic.Dispose();
					image.Save(HttpContext.Current.Server.MapPath(NewPath));
					image.Dispose();
				}
				catch
				{
					image.Save(HttpContext.Current.Server.MapPath(NewPath));
					image.Dispose();
				}
			}
			finally
			{
				if (isDelOld)
				{
					File.Delete(HttpContext.Current.Server.MapPath(Path));
				}
			}
		}

		public static void MarkWaterText(string Path, string NewPath, string WaterText, int x, int y, bool isDelOld)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(Path));
			try
			{
				try
				{
					Graphics graphic = Graphics.FromImage(image);
					graphic.DrawImage(image, 0, 0, image.Width, image.Height);
					Font font = new Font("System", 12f);
					Brush solidBrush = new SolidBrush(Color.Blue);
					graphic.DrawString(WaterText, font, solidBrush, (float)x, (float)y);
					graphic.Dispose();
					image.Save(HttpContext.Current.Server.MapPath(NewPath));
					image.Dispose();
				}
				catch
				{
					image.Save(HttpContext.Current.Server.MapPath(NewPath));
					image.Dispose();
				}
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
				try
				{
					Graphics graphic = Graphics.FromImage(image);
					graphic.DrawImage(image, 0, 0, image.Width, image.Height);
					int num = 12;
					num = (image.Height <= image.Width ? image.Width / 10 : image.Height / 10);
					if (num > 16)
					{
						num = 16;
					}
					Font font = new Font("Arial", (float)num);
					Brush solidBrush = new SolidBrush(Color.Blue);
					int length = WaterText.Length * font.Height;
					int num1 = 15;
					int num2 = 15;
					StringFormat stringFormat = new StringFormat()
					{
						FormatFlags = StringFormatFlags.NoWrap
					};
					graphic.DrawString(WaterText, font, solidBrush, (float)(image.Width - num1 - length), (float)(image.Height - num2 - font.Height), stringFormat);
					graphic.Dispose();
					image.Save(HttpContext.Current.Server.MapPath(NewPath));
					image.Dispose();
				}
				catch
				{
					image.Save(HttpContext.Current.Server.MapPath(NewPath));
					image.Dispose();
				}
			}
			finally
			{
				if (isDelOld)
				{
					File.Delete(HttpContext.Current.Server.MapPath(Path));
				}
			}
		}

		public static bool MoveFile(string oldpath, string newpath)
		{
			bool flag;
			try
			{
				FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(oldpath));
				fileInfo.MoveTo(HttpContext.Current.Server.MapPath(newpath));
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public static bool MoveFiles(string oldpath, string newpath)
		{
			bool flag;
			DirectoryInfo directoryInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath(oldpath));
			FileInfo[] files = directoryInfo.GetFiles();
			try
			{
				FileInfo[] fileInfoArray = files;
				for (int i = 0; i < (int)fileInfoArray.Length; i++)
				{
					FileInfo fileInfo = fileInfoArray[i];
					fileInfo.MoveTo(HttpContext.Current.Server.MapPath(string.Concat(newpath, fileInfo.Name)));
				}
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		protected string OverrideFileName(string filePath, string fileName)
		{
			string str = fileName;
			if (File.Exists(string.Concat(filePath, fileName)))
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
				string extension = Path.GetExtension(fileName);
				string empty = string.Empty;
				int num = 1;
				while (true)
				{
					string[] strArrays = new string[] { fileNameWithoutExtension, "(", num.ToString(), ")", extension };
					empty = string.Concat(strArrays);
					if (!File.Exists(string.Concat(filePath, empty)))
					{
						break;
					}
					num++;
				}
				str = empty;
			}
			return str;
		}

		public static string ReadHtmlFile(string temp)
		{
			StreamReader streamReader = null;
			string end = "";
			try
			{
				try
				{
					streamReader = new StreamReader(temp);
					end = streamReader.ReadToEnd();
				}
				catch (Exception exception)
				{
					throw new Exception(exception.Message);
				}
			}
			finally
			{
				streamReader.Dispose();
				streamReader.Close();
			}
			return end;
		}

		public static ArrayList ReadKeys(string sectionName, string path)
		{
			byte[] numArray = new byte[5120];
			int privateProfileStringA = FileOperate.GetPrivateProfileStringA(sectionName, null, "", numArray, numArray.GetUpperBound(0), path);
			ArrayList arrayLists = new ArrayList();
			if (privateProfileStringA > 0)
			{
				int i = 0;
				int num = 0;
				for (i = 0; i < privateProfileStringA; i++)
				{
					if (numArray[i] == 0)
					{
						string str = Encoding.Default.GetString(numArray, num, i - num).Trim();
						num = i + 1;
						if (str != "")
						{
							arrayLists.Add(str);
						}
					}
				}
			}
			return arrayLists;
		}

		public static ArrayList ReadSections(string path)
		{
			byte[] numArray = new byte[65535];
			int privateProfileSectionNamesA = FileOperate.GetPrivateProfileSectionNamesA(numArray, numArray.GetUpperBound(0), path);
			ArrayList arrayLists = new ArrayList();
			if (privateProfileSectionNamesA > 0)
			{
				int i = 0;
				int num = 0;
				for (i = 0; i < privateProfileSectionNamesA; i++)
				{
					if (numArray[i] == 0)
					{
						string str = Encoding.Default.GetString(numArray, num, i - num).Trim();
						num = i + 1;
						if (str != "")
						{
							arrayLists.Add(str);
						}
					}
				}
			}
			return arrayLists;
		}

		private string SaveYuanFile(System.Text.RegularExpressions.Match m)
		{
			string str = "";
			string value = m.Value;
			string str1 = "";
			str1 = value.Substring(5);
			str1 = str1.Substring(0, str1.IndexOf("\""));
			if (!(new Regex("^http://*")).Match(str1).Success)
			{
				str = value;
			}
			else
			{
				value = value.Substring(5);
				value = value.Substring(0, value.IndexOf("\""));
				string str2 = ConfigurationManager.AppSettings["yuanimg"].ToString();
				string str3 = value;
				string str4 = str3.Substring(str3.LastIndexOf("."));
				string str5 = string.Concat(str2, FileOperate.GetFileName(), str4);
				if (File.Exists(HttpContext.Current.Request.MapPath(str5)))
				{
					File.Delete(HttpContext.Current.Request.MapPath(str5));
				}
				this.GetHttpFile(value, HttpContext.Current.Request.MapPath(str5));
				str = string.Concat("src=\"/", str5.Replace("~/", ""), "\"");
			}
			return str;
		}

		private static void SetXmlTargetValue(string Target, string attributes, string TargetValue, string xmlPath)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlPath);
			XmlElement documentElement = xmlDocument.DocumentElement;
			XmlNode targetValue = documentElement.SelectSingleNode(Target);
			if (targetValue == null)
			{
				targetValue = xmlDocument.CreateElement(Target);
				documentElement.AppendChild(targetValue);
				targetValue.InnerText = TargetValue;
			}
			else
			{
				((XmlElement)targetValue).SetAttribute(attributes, TargetValue);
			}
			xmlDocument.Save(xmlPath);
		}

		public static void SetXmlValue(string Target, string attributes, string TargetValue, string xmlPath)
		{
			FileOperate.SetXmlTargetValue(Target, attributes, TargetValue, xmlPath);
		}

		public static string UpLoadFile(FileUpload fileupload, string Folders)
		{
			string fileName = fileupload.PostedFile.FileName;
			if (fileName == null || fileName.Equals(""))
			{
				return "";
			}
			string str = fileName.Substring(fileName.LastIndexOf("."));
			string str1 = string.Concat(Folders, FileOperate.GetFileName(), str);
			string str2 = HttpContext.Current.Server.MapPath(str1);
			if (File.Exists(str2))
			{
				File.Delete(str2);
			}
			fileupload.PostedFile.SaveAs(str2);
			return str1;
		}

		public static string wipeScript(string html)
		{
			Regex regex = new Regex("<script[\\s\\S]+</script *>", RegexOptions.IgnoreCase);
			Regex regex1 = new Regex(" href *= *[\\s\\S]*script *:", RegexOptions.IgnoreCase);
			Regex regex2 = new Regex(" on[\\s\\S]*=", RegexOptions.IgnoreCase);
			Regex regex3 = new Regex("<iframe[\\s\\S]+</iframe *>", RegexOptions.IgnoreCase);
			Regex regex4 = new Regex("<frameset[\\s\\S]+</frameset *>", RegexOptions.IgnoreCase);
			html = regex.Replace(html, "");
			html = regex1.Replace(html, "");
			html = regex2.Replace(html, " _disibledevent=");
			html = regex3.Replace(html, "");
			html = regex4.Replace(html, "");
			return html;
		}

		public static bool WriteHtmlFile(string str, string htmlfilename)
		{
			StreamWriter streamWriter = null;
			try
			{
				try
				{
					streamWriter = new StreamWriter(htmlfilename, false, Encoding.UTF8);
					streamWriter.Write(str);
					streamWriter.Flush();
				}
				catch (Exception exception)
				{
					throw new Exception(exception.Message);
				}
			}
			finally
			{
				streamWriter.Dispose();
				streamWriter.Close();
			}
			return true;
		}

		[DllImport("kernel32", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
	}
}