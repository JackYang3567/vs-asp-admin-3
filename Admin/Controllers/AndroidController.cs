using Admin.Filters;
using Game.Entity.Accounts;
using Game.Facade;
using Game.Kernel;
using Game.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class AndroidController : BaseController
	{
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult AndroidConfigureList()
		{
			return View();
		}

		[CheckCustomer]
		public string RoomList()
		{
			int num = Convert.ToInt32(base.Request["kid"]);
			DataTable dataTable = new DataTable();
			Cache cache = HttpRuntime.Cache;
			if (cache["RoomList" + num] == null)
			{
				dataTable = FacadeManage.aidePlatformFacade.GetRoomList(num);
				CacheHelper.AddCache("RoomList" + num, dataTable);
			}
			else
			{
				dataTable = (cache["RoomList" + num] as DataTable);
			}
			return JsonHelper.SerializeObject(dataTable);
		}

		[CheckCustomer]
		public JsonResult GetAndroidConfigureList()
		{
			int @int = GameRequest.GetInt("pageIndex", 1);
			int int2 = GameRequest.GetInt("pageSize", 10);
			int int3 = GameRequest.GetInt("ServerID", 0);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (int3 > 0)
			{
				stringBuilder.AppendFormat(" AND ServerID ={0}", int3);
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("AndroidConfigure", @int, int2, stringBuilder.ToString(), "ORDER BY BatchID DESC");
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		public JsonResult DoConfigure(AndroidConfigure model)
		{
			int num = 0;
			num = ((model.LeaveTime < model.EnterTime) ? (model.LeaveTime + 86400 - model.EnterTime) : (model.LeaveTime - model.EnterTime));
			if (num < 1800)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "抱歉，机器人离开的时间最少要比进入的时间晚30分钟！"
				});
			}
			if (num <= 43200)
			{
				try
				{
					if (model.BatchID == 0)
					{
						FacadeManage.aideAccountsFacade.InsertAndroid(model);
					}
					else
					{
						FacadeManage.aideAccountsFacade.UpdateAndroid(model);
					}
					return Json(new
					{
						IsOk = true,
						Msg = "操作成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "操作失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "抱歉，机器人离开和进入的时间必须在12小时之内！"
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DelConfigure()
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
			string @string = GameRequest.GetString("cid");
			string[] array = @string.Trim().Split(',');
			StringBuilder stringBuilder = new StringBuilder();
			string[] array2 = array;
			foreach (string text in array2)
			{
				int result = 0;
				if (int.TryParse(text, out result))
				{
					stringBuilder.Append(text + ",");
				}
			}
			if (!string.IsNullOrEmpty(stringBuilder.ToString()))
			{
				try
				{
					FacadeManage.aideAccountsFacade.DeleteConfigure(stringBuilder.ToString().TrimEnd(','));
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
			return Json(new
			{
				IsOk = false,
				Msg = "参数不正确"
			});
		}

		[CheckCustomer]
		public ActionResult OnlineCountList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult OnlineCountTimeList()
		{
			base.ViewBag.ServerName = base.Request["serverName"];
			return View();
		}

		[CheckCustomer]
		public JsonResult GetOnlineCountList()
		{
			try
			{
				RequestMessage requestMessage = new RequestMessage(3);
				string text = requestMessage.Post();
				if (text.Contains("["))
				{
					List<OnlineCount> list = JsonHelper.DeserializeJsonToList<OnlineCount>(text);
					if (list.Count > 0)
					{
						int sid = GameRequest.GetInt("ServerID", 0);
						int kid = GameRequest.GetInt("KindID", 0);
						if (kid > 0)
						{
							list = (from q in list
							where q.KindID == kid
							select q).ToList();
						}
						if (sid > 0)
						{
							list = (from q in list
							where q.ServerID == sid
							select q).ToList();
						}
						return Json(new
						{
							IsOk = true,
							Msg = "",
							Total = list.Count,
							Data = JsonHelper.SerializeObject(list)
						});
					}
				}
				if (!(text == "") && !text.Contains("null"))
				{
					return Json(new
					{
						IsOk = false,
						Msg = text
					});
				}
				return Json(new
				{
					IsOk = true,
					Msg = "",
					Total = 0,
					Data = ""
				});
			}
			catch (Exception ex)
			{
				return Json(new
				{
					IsOk = false,
					Msg = ex.Message
				});
			}
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult UpdateCount()
		{
			int @int = GameRequest.GetInt("ID", 0);
			int int2 = GameRequest.GetInt("KindID", 0);
			int int3 = GameRequest.GetInt("ServerID", 0);
			int int4 = GameRequest.GetInt("AddCounts", 0);
			int int5 = GameRequest.GetInt("ChangeRate", 0);
			string formString = GameRequest.GetFormString("StartTime");
			string formString2 = GameRequest.GetFormString("EndTime");
			if (int3 > 0 && int4 > 0 && !(formString == "") && !(formString2 == "") && int5 >= 0)
			{
				try
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					dictionary["ID"] = @int;
					dictionary["KindID"] = int2;
					dictionary["ServerID"] = int3;
					dictionary["AddCounts"] = int4;
					dictionary["ChangeRate"] = int5;
					dictionary["StartTime"] = formString.Substring(0, 5);
					dictionary["EndTime"] = formString2.Substring(0, 5);
					dictionary["strErrorDescribe"] = "";
					Message message = FacadeManage.aideAccountsFacade.ExcuteByProce("RYPlatformDB..NET_PW_SaveOnlineCount", dictionary);
					if (!message.Success)
					{
						return Json(new
						{
							IsOk = false,
							Msg = message.Content
						});
					}
					RequestMessage requestMessage = new RequestMessage(5);
					string text = requestMessage.Post();
					if (!text.Contains("OK"))
					{
						return Json(new
						{
							IsOk = false,
							Msg = text
						});
					}
					return Json(new
					{
						IsOk = true,
						Msg = "操作成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "操作失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数错误"
			});
		}

		[CheckCustomer]
		public JsonResult GetOnlineCountTimeList()
		{
			int formInt = GameRequest.GetFormInt("ServerID", 0);
			if (formInt <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			DataTable onlineCount = FacadeManage.aidePlatformFacade.GetOnlineCount(formInt);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = onlineCount.Rows.Count,
				Data = JsonHelper.SerializeObject(onlineCount)
			});
		}

		[CheckCustomer]
		public JsonResult DelOnlineCount()
		{
			string formString = GameRequest.GetFormString("cid");
			if (formString != "")
			{
				string[] array = formString.Trim().Split(',');
				StringBuilder stringBuilder = new StringBuilder();
				string[] array2 = array;
				foreach (string text in array2)
				{
					int result = 0;
					if (int.TryParse(text, out result))
					{
						stringBuilder.Append(text + ",");
					}
				}
				if (!string.IsNullOrEmpty(stringBuilder.ToString()))
				{
					string sWhere = "WHERE ID in (" + stringBuilder.ToString().TrimEnd(',') + ")";
					try
					{
						FacadeManage.aidePlatformFacade.DeleteOnlineCount(sWhere);
						RequestMessage requestMessage = new RequestMessage(5);
						string text2 = requestMessage.Post();
						if (!text2.Contains("OK"))
						{
							return Json(new
							{
								IsOk = false,
								Msg = text2
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
				return Json(new
				{
					IsOk = false,
					Msg = "参数不正确"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "没有参数"
			});
		}
	}
}
