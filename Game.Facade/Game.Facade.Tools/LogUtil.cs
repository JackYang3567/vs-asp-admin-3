using System;
using System.Globalization;
using System.IO;
using System.Web;

namespace Game.Facade.Tools
{
	public class LogUtil
	{
		public static void WriteError(string errorMessage)
		{
			try
			{
				string text = HttpContext.Current.Server.MapPath("~/Log/Error/");
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				text = text + DateTime.Today.ToString("yyMMdd") + ".txt";
				if (!File.Exists(text))
				{
					File.Create(text).Close();
				}
				using (StreamWriter streamWriter = File.AppendText(text))
				{
					streamWriter.WriteLine("\r\nLog Entry : ");
					streamWriter.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
					streamWriter.WriteLine(errorMessage);
					streamWriter.WriteLine("________________________________________________________");
					streamWriter.Flush();
					streamWriter.Close();
				}
			}
			catch (Exception ex)
			{
				WriteError(ex.Message);
			}
		}

		public static void WritePostLog(string dicname, string errorMessage)
		{
			try
			{
				string path = "~/Log/" + dicname + "/" + DateTime.Today.ToString("yyMMdd") + ".txt";
				if (!File.Exists(HttpContext.Current.Server.MapPath(path)))
				{
					File.Create(HttpContext.Current.Server.MapPath(path)).Close();
				}
				using (StreamWriter streamWriter = File.AppendText(HttpContext.Current.Server.MapPath(path)))
				{
					streamWriter.WriteLine("\r\nLog Entry : ");
					streamWriter.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
					streamWriter.WriteLine(errorMessage);
					streamWriter.WriteLine("________________________________________________________");
					streamWriter.Flush();
					streamWriter.Close();
				}
			}
			catch (Exception ex)
			{
				WriteError(ex.Message);
			}
		}

		public static void WriteAPIError(string errorMessage)
		{
			try
			{
				string path = "~/Log/Error/" + DateTime.Today.ToString("yyMMdd") + ".txt";
				if (!File.Exists(HttpContext.Current.Server.MapPath(path)))
				{
					File.Create(HttpContext.Current.Server.MapPath(path)).Close();
				}
				using (StreamWriter streamWriter = File.AppendText(HttpContext.Current.Server.MapPath(path)))
				{
					streamWriter.WriteLine("\r\nLog Entry : ");
					streamWriter.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
					streamWriter.WriteLine(errorMessage);
					streamWriter.WriteLine("________________________________________________________");
					streamWriter.Flush();
					streamWriter.Close();
				}
			}
			catch (Exception ex)
			{
				WriteError(ex.Message);
			}
		}

		public static void WriteBengBengReturnData(string data)
		{
			try
			{
				string path = "~/Log/BengBeng/" + DateTime.Today.ToString("yyMMdd") + ".txt";
				if (!File.Exists(HttpContext.Current.Server.MapPath(path)))
				{
					File.Create(HttpContext.Current.Server.MapPath(path)).Close();
				}
				using (StreamWriter streamWriter = File.AppendText(HttpContext.Current.Server.MapPath(path)))
				{
					streamWriter.WriteLine("\r\nLog Entry : ");
					streamWriter.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
					streamWriter.WriteLine(data);
					streamWriter.WriteLine("________________________________________________________");
					streamWriter.Flush();
					streamWriter.Close();
				}
			}
			catch (Exception ex)
			{
				WriteError(ex.Message);
			}
		}

		public static void WriteDangDangReturnData(string data)
		{
			try
			{
				string path = "~/Log/DangDang/" + DateTime.Today.ToString("yyMMdd") + ".txt";
				if (!File.Exists(HttpContext.Current.Server.MapPath(path)))
				{
					File.Create(HttpContext.Current.Server.MapPath(path)).Close();
				}
				using (StreamWriter streamWriter = File.AppendText(HttpContext.Current.Server.MapPath(path)))
				{
					streamWriter.WriteLine("\r\nLog Entry : ");
					streamWriter.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
					streamWriter.WriteLine(data);
					streamWriter.WriteLine("________________________________________________________");
					streamWriter.Flush();
					streamWriter.Close();
				}
			}
			catch (Exception ex)
			{
				WriteError(ex.Message);
			}
		}

		public static void WriteJuJuReturnData(string data)
		{
			try
			{
				string path = "~/Log/JuJu/" + DateTime.Today.ToString("yyMMdd") + ".txt";
				if (!File.Exists(HttpContext.Current.Server.MapPath(path)))
				{
					File.Create(HttpContext.Current.Server.MapPath(path)).Close();
				}
				using (StreamWriter streamWriter = File.AppendText(HttpContext.Current.Server.MapPath(path)))
				{
					streamWriter.WriteLine("\r\nLog Entry : ");
					streamWriter.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
					streamWriter.WriteLine(data);
					streamWriter.WriteLine("________________________________________________________");
					streamWriter.Flush();
					streamWriter.Close();
				}
			}
			catch (Exception ex)
			{
				WriteError(ex.Message);
			}
		}
	}
}
