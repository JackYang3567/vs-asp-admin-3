using Admin.Filters;
using Codeplex.Data;
using Game.Entity.Platform;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class KongZhiController : BaseController
	{
		private string api_url = ConfigurationManager.AppSettings["game_url"];

		private string api_key = ConfigurationManager.AppSettings["game_key"];

		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult PlayerControlList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult PlayerControlInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["op"]);
			base.ViewBag.OP = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult PlayerControlLog()
		{
			return View();
		}

		[CheckCustomer]
		public string GetPlayerControl()
		{
			int num = TypeUtil.ObjectToInt(base.Request["userId"], 0);
			if (num <= 0)
			{
				return "[]";
			}
			DataTable playerControl = FacadeManage.aideTreasureFacade.GetPlayerControl(num);
			return JsonHelper.SerializeObject(playerControl);
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetPlayerControlList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string text = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			if (text != "")
			{
				stringBuilder.AppendFormat(" AND Accounts='{0}'", text);
			}
			int num = TypeUtil.ObjectToInt(base.Request["ServerID"], -1);
			if (num >= 0)
			{
				stringBuilder.AppendFormat(" AND ServerID={0}", num);
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_PlayerControl", pageIndex, pageSize, stringBuilder.ToString(), "ORDER BY AddTime DESC");
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetPlayerControlLog()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (text3 != "")
			{
				stringBuilder.AppendFormat(" AND Accounts='{0}'", text3);
			}
			if (text != "")
			{
				stringBuilder.AppendFormat(" AND AddTime>='{0}'", text);
			}
			if (text2 != "")
			{
				stringBuilder.AppendFormat(" AND AddTime<='{0}'", text2);
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_PlayerControlLog", pageIndex, pageSize, stringBuilder.ToString(), "ORDER BY AddTime DESC");
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoPlayerControlInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["Accounts"]);
			if (text == "")
			{
				return Json(new
				{
					IsOk = true,
					Msg = "账号不能为空"
				});
			}
			string text2 = TypeUtil.ObjectToString(base.Request["paramJson"]);
			if (text2 == "[]")
			{
				return Json(new
				{
					IsOk = true,
					Msg = "输赢至少需要填一个"
				});
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["Accounts"] = text;
			dictionary["ServerID"] = 0;
			dictionary["WinScore"] = 0;
			dictionary["WinRate"] = 0;
			dictionary["Operator"] = user.Username;
			dictionary["ClientIP"] = GameRequest.GetUserIP();
			dictionary["strErr"] = "";
			List<PlayerControl> list = JsonHelper.DeserializeJsonToList<PlayerControl>(text2);
			if (list.Count > 0)
			{
				foreach (PlayerControl item in list)
				{
					dictionary["ServerID"] = item.ServerID;
					dictionary["WinScore"] = item.WinScore;
					dictionary["WinRate"] = item.WinRate;
					FacadeManage.aideAccountsFacade.ExcuteByProce("RYTreasureDB.dbo.P_EditPlayerControl", dictionary);
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "操作成功"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DelPlayerControl()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(8L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			string value = TypeUtil.ObjectToString(base.Request["cid"]);
			try
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["dwIDs"] = value;
				dictionary["Operator"] = user.Username;
				dictionary["ClientIP"] = GameRequest.GetUserIP();
				dictionary["strErr"] = "";
				Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYTreasureDB.dbo.P_DelPlayerControl", dictionary);
				if (!message.Success)
				{
					return Json(new
					{
						IsOk = false,
						Msg = message.Content
					});
				}
				return Json(new
				{
					IsOk = true,
					Msg = "删除成功"
				});
			}
			catch
			{
				return Json(new
				{
					IsOk = false,
					Msg = "删除失败"
				});
			}
		}

		[CheckCustomer]
		public ActionResult GameSettingList()
		{
			int queryInt = GameRequest.GetQueryInt("type", 1);
			IList<GameKindItem> gameList = FacadeManage.aidePlatformFacade.GetGameList(queryInt);
			return View(gameList);
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetGameSettingList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int @int = GameRequest.GetInt("KindID", 1);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("WHERE KindID = {0} ", @int);
			PagerSet list = FacadeManage.aideRecordFacade.GetList("View_RoomWaste", pageIndex, pageSize, stringBuilder.ToString(), " Order by ServerID ");
			DataTable dataTable = list.PageSet.Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				string text = "";
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					if (i > 0)
					{
						text += ",";
					}
					text += dataTable.Rows[i]["ServerID"];
				}
				try
				{
					string url = api_url + "Game/GetDZControlList";
					string param = "key=" + api_key + "&ids=" + text;
					string json = HttpHelper.HttpRequest(url, param);
					dynamic val = DynamicJson.Parse(json);
					if (!((val.Code == 0) ? true : false))
					{
						return Json(new
						{
							IsOk = false,
							Msg = (object)val.Msg
						});
					}
					dataTable.Columns.Add("WinRate", typeof(string));
					dataTable.Columns.Add("IsOpen", typeof(string));
					int num = 0;
					foreach (dynamic item in val.Data)
					{
						dataTable.Rows[num]["WinRate"] = (object)item.WinRate;
						dataTable.Rows[num]["IsOpen"] = (object)item.IsOpen;
						num++;
					}
				}
				catch (Exception ex)
				{
					LogUtil.WriteError(ex.Message);
					return Json(new
					{
						IsOk = false,
						Msg = "连接服务器失败"
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(dataTable)
			});
		}

		[CheckCustomer]
		public JsonResult DoSetting()
		{
			int num = TypeUtil.ObjectToInt(base.Request["ServerID"], 0);
			int num2 = TypeUtil.ObjectToInt(base.Request["WinRate"], 0);
			if (num >= 0)
			{
				if (num2 >= 0 && num2 <= 100)
				{
					try
					{
						string url = api_url + "Game/UpdateDZControl";
						string param = "key=" + api_key + "&sid=" + num + "&winRate=" + num2;
						string json = HttpHelper.HttpRequest(url, param);
						dynamic val = DynamicJson.Parse(json);
						if (!((val.Code == 0) ? true : false))
						{
							return Json(new
							{
								IsOk = false,
								Msg = (object)val.Msg
							});
						}
						return Json(new
						{
							IsOk = true,
							Msg = (object)val.Msg
						});
					}
					catch (Exception ex)
					{
						LogUtil.WriteError(ex.Message);
						return Json(new
						{
							IsOk = false,
							Msg = "连接服务器失败"
						});
					}
				}
				return Json(new
				{
					IsOk = false,
					Msg = "机器人胜率为0-100%！"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数非法"
			});
		}

		[CheckCustomer]
		public JsonResult IsOpen()
		{
			int num = TypeUtil.ObjectToInt(base.Request["ServerID"], 0);
			int num2 = TypeUtil.ObjectToInt(base.Request["IsOpen"], 0);
			if (num >= 0)
			{
				try
				{
					string url = api_url + "Game/OpenDZControl";
					string param = "key=" + api_key + "&sid=" + num + "&isOpen=" + num2;
					string json = HttpHelper.HttpRequest(url, param);
					dynamic val = DynamicJson.Parse(json);
					if (!((val.Code == 0) ? true : false))
					{
						return Json(new
						{
							IsOk = false,
							Msg = (object)val.Msg
						});
					}
					return Json(new
					{
						IsOk = true,
						Msg = (object)val.Msg
					});
				}
				catch (Exception ex)
				{
					LogUtil.WriteError(ex.Message);
					return Json(new
					{
						IsOk = false,
						Msg = "连接服务器失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数非法"
			});
		}
	}
}
