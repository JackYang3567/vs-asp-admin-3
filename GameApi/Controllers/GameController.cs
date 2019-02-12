using GameApi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GameApi.Controllers
{
	public class GameController : Controller
	{
		private string myKey = ConfigurationManager.AppSettings["myKey"];

		private string dzPath = ConfigurationManager.AppSettings["duizhan"];

		private string brPath = ConfigurationManager.AppSettings["bairen"];

		public GameController()
		{
		}

		public JsonResult GetBRControlList()
		{
			string item = base.Request.Form["key"];
			if (string.IsNullOrEmpty(item))
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			if (FormsAuthentication.HashPasswordForStoringInConfigFile(item, "MD5") != this.myKey)
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			string str = base.Request.Form["ids"];
			string[] strArrays = str.Split(new char[] { ',' });
			List<object> objs = new List<object>();
			string str1 = "0";
			string str2 = FileOperate.IniReadValue(str1, "IsOpen", this.brPath);
			if (string.IsNullOrEmpty(str2))
			{
				FileOperate.IniWriteValue(str1, "IsOpen", "1", this.brPath);
				str2 = "1";
			}
			string str3 = FileOperate.IniReadValue(str1, "StorageStart", this.brPath);
			if (string.IsNullOrEmpty(str3))
			{
				FileOperate.IniWriteValue(str1, "StorageStart", "0", this.brPath);
				str3 = "0";
			}
			string str4 = FileOperate.IniReadValue(str1, "StorageDeduct", this.brPath);
			if (string.IsNullOrEmpty(str4))
			{
				FileOperate.IniWriteValue(str1, "StorageDeduct", "2", this.brPath);
				str4 = "2";
			}
			objs.Add(new { ServerID = 0, IsOpen = str2, StorageStart = str3, StorageDeduct = str4, AttenuationScore = 0 });
			string[] strArrays1 = strArrays;
			for (int i = 0; i < (int)strArrays1.Length; i++)
			{
				string str5 = strArrays1[i];
				str2 = FileOperate.IniReadValue(str5, "IsOpen", this.brPath);
				if (string.IsNullOrEmpty(str2))
				{
					FileOperate.IniWriteValue(str5, "IsOpen", "1", this.brPath);
					str2 = "1";
				}
				str3 = FileOperate.IniReadValue(str5, "StorageStart", this.brPath);
				if (string.IsNullOrEmpty(str3))
				{
					FileOperate.IniWriteValue(str5, "StorageStart", "0", this.brPath);
					str3 = "0";
				}
				str4 = FileOperate.IniReadValue(str5, "StorageDeduct", this.brPath);
				if (string.IsNullOrEmpty(str4))
				{
					FileOperate.IniWriteValue(str5, "StorageDeduct", "2", this.brPath);
					str4 = "2";
				}
				string str6 = FileOperate.IniReadValue(str5, "AttenuationScore", this.brPath);
				if (string.IsNullOrEmpty(str6))
				{
					FileOperate.IniWriteValue(str5, "AttenuationScore", "0", this.brPath);
					str6 = "0";
				}
				objs.Add(new { ServerID = str5, IsOpen = str2, StorageStart = str3, StorageDeduct = str4, AttenuationScore = str6 });
			}
			return base.Json(new { Code = 0, Data = objs });
		}

		public JsonResult GetDZControlList()
		{
			string item = base.Request.Form["key"];
			if (string.IsNullOrEmpty(item))
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			if (FormsAuthentication.HashPasswordForStoringInConfigFile(item, "MD5") != this.myKey)
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			string str = base.Request.Form["ids"];
			string[] strArrays = str.Split(new char[] { ',' });
			List<object> objs = new List<object>();
			string[] strArrays1 = strArrays;
			for (int i = 0; i < (int)strArrays1.Length; i++)
			{
				string str1 = strArrays1[i];
				string str2 = FileOperate.IniReadValue(str1, "WinRate", this.dzPath);
				if (str2 == "")
				{
					FileOperate.IniWriteValue(str1, "WinRate", "50", this.dzPath);
					str2 = "50";
				}
				string str3 = FileOperate.IniReadValue(str1, "IsOpen", this.dzPath);
				if (str3 == "")
				{
					FileOperate.IniWriteValue(str1, "IsOpen", "1", this.dzPath);
					str3 = "1";
				}
				objs.Add(new { WinRate = str2, IsOpen = str3 });
			}
			return base.Json(new { Code = 0, Data = objs });
		}

		public ActionResult Index()
		{
			return base.View();
		}

		public JsonResult OpenBRControl()
		{
			JsonResult jsonResult;
			string item = base.Request.Form["key"];
			if (string.IsNullOrEmpty(item))
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			if (FormsAuthentication.HashPasswordForStoringInConfigFile(item, "MD5") != this.myKey)
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			int num = Convert.ToInt32(base.Request.Form["sid"]);
			int num1 = Convert.ToInt32(base.Request.Form["isOpen"]);
			try
			{
				FileOperate.IniWriteValue(num.ToString(), "IsOpen", num1.ToString(), this.brPath);
				jsonResult = base.Json(new { Code = 0, Msg = "操作成功" });
			}
			catch (Exception exception)
			{
				jsonResult = base.Json(new { Code = 1, Msg = string.Concat("请检查文件", this.brPath, "是否有写入权限") });
			}
			return jsonResult;
		}

		public JsonResult OpenDZControl()
		{
			JsonResult jsonResult;
			string item = base.Request.Form["key"];
			if (string.IsNullOrEmpty(item))
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			if (FormsAuthentication.HashPasswordForStoringInConfigFile(item, "MD5") != this.myKey)
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			int num = Convert.ToInt32(base.Request.Form["sid"]);
			int num1 = Convert.ToInt32(base.Request.Form["isOpen"]);
			try
			{
				FileOperate.IniWriteValue(num.ToString(), "IsOpen", num1.ToString(), this.dzPath);
				jsonResult = base.Json(new { Code = 0, Msg = "操作成功" });
			}
			catch (Exception exception)
			{
				jsonResult = base.Json(new { Code = 1, Msg = string.Concat("请检查文件", this.dzPath, "是否有写入权限") });
			}
			return jsonResult;
		}

		public JsonResult UpdateBRControl()
		{
			JsonResult jsonResult;
			string item = base.Request.Form["key"];
			if (string.IsNullOrEmpty(item))
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			if (FormsAuthentication.HashPasswordForStoringInConfigFile(item, "MD5") != this.myKey)
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			int num = Convert.ToInt32(base.Request.Form["sid"]);
			int num1 = Convert.ToInt32(base.Request.Form["StorageDeduct"]);
			decimal num2 = Convert.ToDecimal(base.Request.Form["StorageStart"]);
			try
			{
				FileOperate.IniWriteValue(num.ToString(), "StorageDeduct", num1.ToString(), this.brPath);
				if (num2 != new decimal(0))
				{
					decimal num3 = Convert.ToDecimal(FileOperate.IniReadValue(num.ToString(), "StorageStart", this.brPath));
					string str = num.ToString();
					decimal num4 = num3 + num2;
					FileOperate.IniWriteValue(str, "StorageStart", num4.ToString(), this.brPath);
				}
				jsonResult = base.Json(new { Code = 0, Msg = "操作成功" });
			}
			catch (Exception exception)
			{
				jsonResult = base.Json(new { Code = 1, Msg = string.Concat("请检查文件", this.brPath, "是否有写入权限") });
			}
			return jsonResult;
		}

		public JsonResult UpdateDZControl()
		{
			JsonResult jsonResult;
			string item = base.Request.Form["key"];
			if (string.IsNullOrEmpty(item))
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			if (FormsAuthentication.HashPasswordForStoringInConfigFile(item, "MD5") != this.myKey)
			{
				return base.Json(new { Code = 1, Msg = "签名错误" });
			}
			int num = Convert.ToInt32(base.Request.Form["sid"]);
			int num1 = Convert.ToInt32(base.Request.Form["winRate"]);
			try
			{
				FileOperate.IniWriteValue(num.ToString(), "WinRate", num1.ToString(), this.dzPath);
				jsonResult = base.Json(new { Code = 0, Msg = "操作成功" });
			}
			catch (Exception exception)
			{
				jsonResult = base.Json(new { Code = 1, Msg = string.Concat("请检查文件", this.dzPath, "是否有写入权限") });
			}
			return jsonResult;
		}
	}
}