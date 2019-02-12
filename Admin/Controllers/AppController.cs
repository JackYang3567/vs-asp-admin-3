using Admin.Filters;
using Codeplex.Data;
using Game.Entity.Accounts;
using Game.Entity.Platform;
using Game.Entity.Record;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using Jiguang.JPush;
using Jiguang.JPush.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class AppController : BaseController
	{
		private string api_url = ConfigurationManager.AppSettings["game_url"];

		private string api_key = ConfigurationManager.AppSettings["game_key"];

		[CheckCustomer]
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetDataBaseInfoList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY DBInfoID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string text = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.AppendFormat(" AND DBAddr='{0}'", text);
			}
			PagerSet dataBaseList = FacadeManage.aidePlatformFacade.GetDataBaseList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (dataBaseList != null && dataBaseList.PageSet != null && dataBaseList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in dataBaseList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						DBInfoID = TypeUtil.ObjectToString(row["DBInfoID"]),
						Information = TypeUtil.ObjectToString(row["Information"]),
						DBAddr = TypeUtil.ObjectToString(row["DBAddr"]),
						DBPort = TypeUtil.ObjectToString(row["DBPort"]),
						MachineID = TypeUtil.ObjectToString(row["MachineID"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((dataBaseList.PageSet != null && dataBaseList.PageSet.Tables != null && dataBaseList.PageSet.Tables[0].Rows.Count != 0) ? dataBaseList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult DataBaseInfoInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["cmd"]);
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.OP = text;
			base.ViewBag.ID = num;
			DataBaseInfo model = new DataBaseInfo();
			if (num > 0)
			{
				model = FacadeManage.aidePlatformFacade.GetDataBaseInfo(num);
			}
			return View(model);
		}

		[CheckCustomer]
		public JsonResult DelDataBaseInfoInfo()
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
			string str = TypeUtil.ObjectToString(base.Request["cid"]);
			string sqlQuery = "WHERE DBInfoID in (" + str + ")";
			try
			{
				FacadeManage.aidePlatformFacade.DeleteDataBase(sqlQuery);
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
		public JsonResult UpdateDataBaseInfoInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["OP"]);
			int num = TypeUtil.ObjectToInt(base.Request["ID"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["DBPort"]);
			if (num2 <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "端口不规范，端口只能为正整数"
				});
			}
			string text = TypeUtil.ObjectToString(base.Request["DBAddr"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "地址不能为空"
				});
			}
			string text2 = TypeUtil.ObjectToString(base.Request["DBUser"]);
			if (string.IsNullOrEmpty(text2))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "账号不能为空"
				});
			}
			string text3 = TypeUtil.ObjectToString(base.Request["DBPassword"]);
			if (num == 0 && text3 == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "密码不能为空"
				});
			}
			string machineID = TypeUtil.ObjectToString(base.Request["MachineID"]);
			string information = TypeUtil.ObjectToString(base.Request["Information"]);
			DataBaseInfo dataBaseInfo = new DataBaseInfo();
			dataBaseInfo.DBAddr = text;
			dataBaseInfo.DBPort = num2;
			dataBaseInfo.DBUser = CWHEncryptNet.XorEncrypt(text2);
			if (text3 != "")
			{
				dataBaseInfo.DBPassword = CWHEncryptNet.XorEncrypt(text3);
			}
			dataBaseInfo.MachineID = machineID;
			dataBaseInfo.Information = information;
			Game.Kernel.Message message = new Game.Kernel.Message();
			if (a == "add")
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
					if (!adminPermission.GetPermission(2L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				if (FacadeManage.aidePlatformFacade.IsExistsDBAddr(dataBaseInfo.DBAddr))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "地址已经存在"
					});
				}
				message = FacadeManage.aidePlatformFacade.InsertDataBase(dataBaseInfo);
			}
			else
			{
				if (user != null)
				{
					AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
					if (!adminPermission2.GetPermission(4L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				dataBaseInfo.DBInfoID = num;
				message = FacadeManage.aidePlatformFacade.UpdateDataBase(dataBaseInfo);
			}
			if (message.Success)
			{
				if (a == "add")
				{
					return Json(new
					{
						IsOk = true,
						Msg = "机器信息增加成功"
					});
				}
				return Json(new
				{
					IsOk = true,
					Msg = "机器信息修改成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		public ActionResult GameGameItemList()
		{
			base.ViewBag.Title = "游戏系统 - " + TypeUtil.ObjectToString(base.Request["title"]);
			return View();
		}

		[CheckCustomer]
		public JsonResult GetGameGameItemList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY GameID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet gameGameItemList = FacadeManage.aidePlatformFacade.GetGameGameItemList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (gameGameItemList != null && gameGameItemList.PageSet != null && gameGameItemList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in gameGameItemList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						GameID = TypeUtil.ObjectToInt(row["GameID"]),
						GameName = TypeUtil.ObjectToString(row["GameName"]),
						ServerVersion = TypeUtil.CalVersion(TypeUtil.ObjectToInt(row["ServerVersion"])),
						ClientVersion = TypeUtil.CalVersion(TypeUtil.ObjectToInt(row["ClientVersion"])),
						DataBaseName = TypeUtil.ObjectToString(row["DataBaseName"]),
						DataBaseAddr = TypeUtil.ObjectToString(row["DataBaseAddr"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((gameGameItemList.PageSet != null && gameGameItemList.PageSet.Tables != null && gameGameItemList.PageSet.Tables[0].Rows.Count != 0) ? gameGameItemList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DelGameGameItem()
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
			string str = TypeUtil.ObjectToString(base.Request["cid"]);
			string sqlQuery = "WHERE GameID in (" + str + ")";
			try
			{
				FacadeManage.aidePlatformFacade.DeleteGameGameItem(sqlQuery);
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
		public ActionResult GameGameItemInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["cmd"]);
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			if (a == "add")
			{
				base.ViewBag.OpStr = "新增";
				base.ViewBag.GameID = (FacadeManage.aidePlatformFacade.GetMaxGameKindID() + 1).ToString();
			}
			else
			{
				base.ViewBag.OpStr = "更新";
				GameGameItem gameGameItemInfo = FacadeManage.aidePlatformFacade.GetGameGameItemInfo(num);
				if (gameGameItemInfo != null)
				{
					base.ViewBag.ServerVersion = TypeUtil.CalVersion(gameGameItemInfo.ServerVersion);
					base.ViewBag.ClientVersion = TypeUtil.CalVersion(gameGameItemInfo.ClientVersion);
				}
				base.ViewData["GameGameItem"] = gameGameItemInfo;
			}
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult AddGameGameItemInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["IntParam"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["GameID"]);
			if (num2 < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "模块标识不能为空"
				});
			}
			string text = TypeUtil.ObjectToString(base.Request["GameName"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "模块名称不能为空"
				});
			}
			string text2 = TypeUtil.ObjectToString(base.Request["DataBaseName"]);
			if (string.IsNullOrEmpty(text2))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "数据库名不能为空"
				});
			}
			string text3 = TypeUtil.ObjectToString(base.Request["DataBaseAddr"]);
			if (string.IsNullOrEmpty(text3))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "数据库地址不能为空"
				});
			}
			string text4 = TypeUtil.ObjectToString(base.Request["ServerVersion"]);
			if (string.IsNullOrEmpty(text4))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "服务端版本不能为空"
				});
			}
			string version = TypeUtil.ObjectToString(base.Request["ClientVersion"]);
			if (string.IsNullOrEmpty(text4))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "客户端版本不能为空"
				});
			}
			string text5 = TypeUtil.ObjectToString(base.Request["ServerDLLName"]);
			if (string.IsNullOrEmpty(text5))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "服务端名不能为空"
				});
			}
			string text6 = TypeUtil.ObjectToString(base.Request["ClientExeName"]);
			if (string.IsNullOrEmpty(text6))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "客户端名不能为空"
				});
			}
			int num3 = TypeUtil.ObjectToInt(base.Request["SuportType"]);
			if (num3 < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请选择支持类型"
				});
			}
			GameGameItem gameGameItem = new GameGameItem();
			gameGameItem.GameID = num2;
			gameGameItem.GameName = text;
			gameGameItem.DataBaseName = text2;
			gameGameItem.DataBaseAddr = text3;
			gameGameItem.ServerVersion = TypeUtil.CalVersion2(text4);
			gameGameItem.ClientVersion = TypeUtil.CalVersion2(version);
			gameGameItem.ServerDLLName = text5;
			gameGameItem.ClientExeName = text6;
			gameGameItem.SuportType = num3;
			Game.Kernel.Message message = new Game.Kernel.Message();
			if (num > 0)
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
					if (!adminPermission.GetPermission(4L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				message = FacadeManage.aidePlatformFacade.UpdateGameGameItem(gameGameItem);
			}
			else if (user != null)
			{
				AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
				if (!adminPermission2.GetPermission(2L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
				if (FacadeManage.aidePlatformFacade.IsExistsGameID(gameGameItem.GameID))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "模块标识已经存在"
					});
				}
				message = FacadeManage.aidePlatformFacade.InsertGameGameItem(gameGameItem);
			}
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		public ActionResult GameKindItemList()
		{
			base.ViewBag.Title = "游戏系统 - " + TypeUtil.ObjectToString(base.Request["title"]);
			return View();
		}

		[CheckCustomer]
		public JsonResult GetGameKindItemList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY KindID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet gameKindItemList = FacadeManage.aidePlatformFacade.GetGameKindItemList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (gameKindItemList != null && gameKindItemList.PageSet != null && gameKindItemList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in gameKindItemList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						KindID = TypeUtil.ObjectToInt(row["KindID"]),
						KindName = TypeUtil.ObjectToString(row["KindName"]),
						GameTypeName = TypeUtil.GetGameTypeName(TypeUtil.ObjectToInt(row["TypeID"])),
						SortID = TypeUtil.ObjectToInt(row["SortID"]),
						ProcessName = TypeUtil.ObjectToString(row["ProcessName"]),
						GameFlagName = TypeUtil.GetGameFlagName(TypeUtil.ObjectToInt(row["GameFlag"])),
						GameRuleUrl = TypeUtil.ObjectToString(row["GameRuleUrl"]),
						DownLoadUrl = TypeUtil.ObjectToString(row["DownLoadUrl"]),
						RecommendName = ((TypeUtil.ObjectToInt(row["Recommend"]) == 0) ? "<span>否</span>" : ((TypeUtil.ObjectToInt(row["Recommend"]) == 1) ? "<span style='color:red;'>是</span>" : "<span>否</span>")),
						NullityStatus = TypeUtil.GetNullityStatus((byte)row["Nullity"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((gameKindItemList.PageSet != null && gameKindItemList.PageSet.Tables != null && gameKindItemList.PageSet.Tables[0].Rows.Count != 0) ? gameKindItemList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DelGameKindItem()
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
			string str = TypeUtil.ObjectToString(base.Request["cid"]);
			string sqlQuery = "WHERE KindID in (" + str + ")";
			try
			{
				FacadeManage.aidePlatformFacade.DeleteGameKindItem(sqlQuery);
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
		public ActionResult GameKindItemInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["cmd"]);
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.WinExperience = 0;
			base.ViewBag.ID = num;
			if (a == "add")
			{
				base.ViewBag.OpStr = "新增";
				base.ViewBag.KindID = (FacadeManage.aidePlatformFacade.GetMaxGameKindID() + 1).ToString();
			}
			else
			{
				base.ViewBag.OpStr = "更新";
				GameKindItem gameKindItemInfo = FacadeManage.aidePlatformFacade.GetGameKindItemInfo(num);
				base.ViewData["GameKindItem"] = gameKindItemInfo;
			}
			GameConfig gameConfig = FacadeManage.aidePlatformFacade.GetGameConfig(num);
			if (gameConfig != null)
			{
				base.ViewBag.WinExperience = gameConfig.WinExperience;
			}
			else
			{
				SystemStatusInfo systemStatusInfo = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(EnumerationList.SystemStatusKey.WinExperience.ToString());
				if (systemStatusInfo != null)
				{
					base.ViewBag.WinExperience = systemStatusInfo.StatusValue;
				}
				else
				{
					base.ViewBag.WinExperience = 10;
				}
			}
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult AddGameKindItemInfo(GameKindItem entity)
		{
			int num = TypeUtil.ObjectToInt(base.Request["IntParam"]);
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			int num2 = TypeUtil.ObjectToInt(base.Request["WinExperience"]);
			if (num2 < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "赢一局经验为大于0的数字"
				});
			}
			if (string.IsNullOrEmpty(entity.GameRuleUrl))
			{
				entity.GameRuleUrl = "";
			}
			if (string.IsNullOrEmpty(entity.DownLoadUrl))
			{
				entity.DownLoadUrl = "";
			}
			Game.Kernel.Message message = new Game.Kernel.Message();
			if (num > 0)
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
					if (!adminPermission.GetPermission(4L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				message = FacadeManage.aidePlatformFacade.UpdateGameKindItem(entity);
			}
			else if (user != null)
			{
				AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
				if (!adminPermission2.GetPermission(2L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
				if (FacadeManage.aidePlatformFacade.IsExistsKindID(entity.KindID))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "游戏标识已经存在"
					});
				}
				message = FacadeManage.aidePlatformFacade.InsertGameKindItem(entity);
			}
			if (message.Success)
			{
				GameConfig gameConfig = new GameConfig();
				gameConfig.KindID = entity.KindID;
				gameConfig.NoticeChangeGolds = 0L;
				gameConfig.WinExperience = num2;
				FacadeManage.aidePlatformFacade.UpdateGameConfig(gameConfig);
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		public ActionResult GameTypeItemList()
		{
			base.ViewBag.Title = "游戏系统 - " + TypeUtil.ObjectToString(base.Request["title"]);
			return View();
		}

		[CheckCustomer]
		public JsonResult GetGameTypeItemList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY TypeID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet gameTypeItemList = FacadeManage.aidePlatformFacade.GetGameTypeItemList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (gameTypeItemList != null && gameTypeItemList.PageSet != null && gameTypeItemList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in gameTypeItemList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						TypeID = TypeUtil.ObjectToInt(row["TypeID"]),
						TypeName = TypeUtil.ObjectToString(row["TypeName"]),
						GameTypeName = TypeUtil.GetGameTypeName(TypeUtil.ObjectToInt(row["JoinID"])),
						SortID = TypeUtil.ObjectToInt(row["SortID"]),
						NullityStatus = TypeUtil.GetNullityStatus((byte)row["Nullity"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((gameTypeItemList.PageSet != null && gameTypeItemList.PageSet.Tables != null && gameTypeItemList.PageSet.Tables[0].Rows.Count != 0) ? gameTypeItemList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DelGameTypeItemInfo()
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
			string str = TypeUtil.ObjectToString(base.Request["cid"]);
			string sqlQuery = "WHERE TypeID in (" + str + ")";
			try
			{
				FacadeManage.aidePlatformFacade.DeleteGameTypeItem(sqlQuery);
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
		public ActionResult GameTypeItemInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["cmd"]);
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			if (a == "add")
			{
				base.ViewBag.OpStr = "新增";
				base.ViewBag.TypeID = (FacadeManage.aidePlatformFacade.GetMaxGameKindID() + 1).ToString();
			}
			else
			{
				base.ViewBag.OpStr = "更新";
				GameTypeItem gameTypeItemInfo = FacadeManage.aidePlatformFacade.GetGameTypeItemInfo(num);
				base.ViewData["GameTypeItem"] = gameTypeItemInfo;
			}
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult AddGameTypeItemInfo(GameTypeItem entity)
		{
			int num = TypeUtil.ObjectToInt(base.Request["IntParam"]);
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			Game.Kernel.Message message = new Game.Kernel.Message();
			if (num > 0)
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
					if (!adminPermission.GetPermission(4L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				message = FacadeManage.aidePlatformFacade.UpdateGameTypeItem(entity);
			}
			else if (user != null)
			{
				AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
				if (!adminPermission2.GetPermission(2L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
				if (FacadeManage.aidePlatformFacade.IsExistsTypeID(entity.TypeID))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "游戏标识已经存在"
					});
				}
				message = FacadeManage.aidePlatformFacade.InsertGameTypeItem(entity);
			}
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		public ActionResult MobileKindList()
		{
			base.ViewBag.Title = "游戏系统 - " + TypeUtil.ObjectToString(base.Request["title"]);
			return View();
		}

		[CheckCustomer]
		public JsonResult GetMobileKindList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY SortID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet mobileKindItemList = FacadeManage.aidePlatformFacade.GetMobileKindItemList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (mobileKindItemList != null && mobileKindItemList.PageSet != null && mobileKindItemList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in mobileKindItemList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						KindID = TypeUtil.ObjectToInt(row["KindID"]),
						KindName = TypeUtil.ObjectToString(row["KindName"]),
						ModuleName = TypeUtil.ObjectToString(row["ModuleName"]),
						CalVersion = TypeUtil.CalVersion(TypeUtil.ObjectToInt(row["ClientVersion"])),
						ResVersion = TypeUtil.ObjectToString(row["ResVersion"]),
						SortID = TypeUtil.ObjectToInt(row["SortID"]),
						MarkName = TypeUtil.GetMarkName((int)row["KindMark"]),
						NullityStatus = TypeUtil.GetNullityStatus((byte)row["Nullity"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((mobileKindItemList.PageSet != null && mobileKindItemList.PageSet.Tables != null && mobileKindItemList.PageSet.Tables[0].Rows.Count != 0) ? mobileKindItemList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DelMobileKindInfo()
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
			string str = TypeUtil.ObjectToString(base.Request["cid"]);
			string sqlQuery = "WHERE KindID in (" + str + ")";
			try
			{
				FacadeManage.aidePlatformFacade.DeleteMobileKindItem(sqlQuery);
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
		public ActionResult MobileKindInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["cmd"]);
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			if (a == "add")
			{
				base.ViewBag.OpStr = "新增";
				base.ViewBag.TypeID = (FacadeManage.aidePlatformFacade.GetMaxMobileKindID() + 1).ToString();
			}
			else
			{
				base.ViewBag.OpStr = "更新";
				MobileKindItem mobileKindItemInfo = FacadeManage.aidePlatformFacade.GetMobileKindItemInfo(num);
				base.ViewData["MobileKindItem"] = mobileKindItemInfo;
			}
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult AddMobileKindInfo(MobileKindItem entity)
		{
			int num = TypeUtil.ObjectToInt(base.Request["IntParam"]);
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			if (string.IsNullOrEmpty(entity.ModuleName))
			{
				entity.ModuleName = "";
			}
			Game.Kernel.Message message = new Game.Kernel.Message();
			if (num > 0)
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
					if (!adminPermission.GetPermission(4L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				message = FacadeManage.aidePlatformFacade.UpdateMobileKindItem(entity);
			}
			else if (user != null)
			{
				AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
				if (!adminPermission2.GetPermission(2L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
				if (FacadeManage.aidePlatformFacade.GetMobileKindItemInfo(entity.KindID) != null)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "游戏标识:" + entity.KindID + "已经存在"
					});
				}
				message = FacadeManage.aidePlatformFacade.InsertMobileKindItem(entity);
			}
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		public ActionResult SystemMessageList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetSystemMessageList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY StartTime DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet systemMessageList = FacadeManage.aidePlatformFacade.GetSystemMessageList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (systemMessageList != null && systemMessageList.PageSet != null && systemMessageList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in systemMessageList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						ID = TypeUtil.ObjectToString(row["ID"]),
						StartTime = TypeUtil.ObjectToString(row["StartTime"]),
						ConcludeTime = TypeUtil.ObjectToString(row["ConcludeTime"]),
						MessageTypeName = TypeUtil.GetMessageTypeName(TypeUtil.ObjectToInt(row["MessageType"])),
						MessageString = TypeUtil.ObjectToString(row["MessageString"]),
						TimeRate = TypeUtil.ObjectToString(row["TimeRate"]),
						NullityStatus = TypeUtil.GetNullityStatus((byte)row["Nullity"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((systemMessageList.PageSet != null && systemMessageList.PageSet.Tables != null && systemMessageList.PageSet.Tables[0].Rows.Count != 0) ? systemMessageList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult DoSystemMessageList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["OP"]);
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				switch (num)
				{
				case 1:
				{
					string sqlQuery = "WHERE ID in (" + text + ")";
					try
					{
						FacadeManage.aidePlatformFacade.DeleteSystemMessage(sqlQuery);
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
				case 2:
				{
					string sql2 = "Update SystemMessage Set Nullity = 1 WHERE ID in (" + text + ") and Nullity=0";
					int num3 = FacadeManage.aidePlatformFacade.ExecuteSql(sql2);
					if (num3 > 0)
					{
						return Json(new
						{
							IsOk = true,
							Msg = "冻结成功"
						});
					}
					return Json(new
					{
						IsOk = false,
						Msg = "冻结失败，没有需要冻结的消息！"
					});
				}
				case 3:
				{
					string sql = "Update SystemMessage Set Nullity = 0 WHERE ID in (" + text + ") and Nullity = 1";
					int num2 = FacadeManage.aidePlatformFacade.ExecuteSql(sql);
					if (num2 > 0)
					{
						return Json(new
						{
							IsOk = true,
							Msg = "解冻成功"
						});
					}
					return Json(new
					{
						IsOk = false,
						Msg = "解冻失败，没有需要解冻的消息！"
					});
				}
				default:
					return Json(new
					{
						IsOk = false,
						Msg = "没有操作！"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "没有选中任何项"
			});
		}

		[CheckCustomer]
		public ActionResult SystemMessageInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["cmd"]);
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.OpStr = ((a == "add") ? "新增" : "更新");
			base.ViewBag.ID = num;
			SystemMessage systemMessageInfo = FacadeManage.aidePlatformFacade.GetSystemMessageInfo(num);
			if (systemMessageInfo != null)
			{
				base.ViewData["SystemMessage"] = systemMessageInfo;
				base.ViewBag.CreateMaster = TypeUtil.GetMasterName(systemMessageInfo.CreateMasterID);
				base.ViewBag.UpdateMaster = TypeUtil.GetMasterName(systemMessageInfo.UpdateMasterID);
				base.ViewBag.Servers = systemMessageInfo.ServerRange;
			}
			else
			{
				base.ViewData["SystemMessage"] = null;
				base.ViewBag.Servers = "";
			}
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult AddSystemMessageInfo(SystemMessage entity)
		{
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			if (string.IsNullOrEmpty(entity.ServerRange))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请选择要控制的房间"
				});
			}
			if (entity.MessageType < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "消息类型！"
				});
			}
			if (string.IsNullOrEmpty(entity.MessageString))
			{
				entity.MessageString = "";
			}
			DateTime now = DateTime.Now;
			DateTime now2 = DateTime.Now;
			if (entity.ID == 0)
			{
				entity.CreateDate = DateTime.Now;
				entity.CreateMasterID = user.UserID;
			}
			else
			{
				entity.UpdateDate = DateTime.Now;
				entity.UpdateMasterID = user.UserID;
			}
			if (string.IsNullOrEmpty(entity.CollectNote))
			{
				entity.CollectNote = "";
			}
			Game.Kernel.Message message = new Game.Kernel.Message();
			message = ((entity.ID <= 0) ? FacadeManage.aidePlatformFacade.InsertSystemMessage(entity) : FacadeManage.aidePlatformFacade.UpdateSystemMessage(entity));
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功！"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "操作失败！"
			});
		}

		[CheckCustomer]
		public ActionResult SystemSet()
		{
			string orderby = "ORDER BY SortID ASC";
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.StrParam = text;
			StringBuilder stringBuilder = new StringBuilder();
			PagerSet systemStatusInfoList = FacadeManage.aideAccountsFacade.GetSystemStatusInfoList(1, 10000, " WHERE 1=1 and IsShow=0 ", orderby);
			if (systemStatusInfoList.PageSet.Tables[0].Rows.Count > 0)
			{
				for (int i = 0; i < systemStatusInfoList.PageSet.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = systemStatusInfoList.PageSet.Tables[0].Rows[i];
					stringBuilder.AppendFormat("<li {0} style=\"margin-bottom: 1px;width:132px; text-align:center;\"><a href=\"/App/SystemSet?param={1}\">{2}</a></li> ", (TypeUtil.ObjectToString(dataRow["StatusName"]) == text || (string.IsNullOrEmpty(text) && i == 0)) ? "class=\"active\"" : "", TypeUtil.ObjectToString(dataRow["StatusName"]), TypeUtil.ObjectToString(dataRow["StatusTip"]));
				}
			}
			base.ViewBag.LI = stringBuilder.ToString();
			SystemStatusInfo systemStatusInfo = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(string.IsNullOrEmpty(text) ? "EnjoinRegister" : text);
			if (systemStatusInfo != null)
			{
				base.ViewBag.StatusName = systemStatusInfo.StatusName;
				base.ViewBag.StatusValue = systemStatusInfo.StatusValue.ToString();
				base.ViewBag.StatusTip = systemStatusInfo.StatusTip;
				base.ViewBag.StatusString = systemStatusInfo.StatusString;
				base.ViewBag.StatusDescription = systemStatusInfo.StatusDescription;
			}
			return View();
		}

		[CheckCustomer]
		public JsonResult DoSystemSet()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(4L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			TypeUtil.ObjectToString(base.Request["StrParam"]);
			string text = TypeUtil.ObjectToString(base.Request["StatusName"]);
			decimal statusValue = TypeUtil.ObjectToDecimal(base.Request["StatusValue"]);
			string statusString = TypeUtil.ObjectToString(base.Request["StatusString"]);
			string statusTip = TypeUtil.ObjectToString(base.Request["StatusTip"]);
			string statusDescription = TypeUtil.ObjectToString(base.Request["StatusDescription"]);
			if (string.IsNullOrEmpty(text.Trim()))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "名称不能为空！"
				});
			}
			SystemStatusInfo systemStatusInfo = new SystemStatusInfo();
			systemStatusInfo.StatusName = text;
			systemStatusInfo.StatusValue = statusValue;
			systemStatusInfo.StatusString = statusString;
			systemStatusInfo.StatusTip = statusTip;
			systemStatusInfo.StatusDescription = statusDescription;
			Game.Kernel.Message message = new Game.Kernel.Message();
			message = FacadeManage.aideAccountsFacade.UpdateSystemStatusInfo(systemStatusInfo);
			if (message.Success)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "修改成功！"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		public ActionResult SigninConfig()
		{
			DataSet signinConfig = FacadeManage.aidePlatformFacade.GetSigninConfig();
			if (signinConfig.Tables[0].Rows.Count > 0)
			{
				try
				{
					base.ViewBag.Gold1 = TypeUtil.ObjectToDecimal(signinConfig.Tables[0].Rows[0]["RewardGold"]);
					base.ViewBag.Gold2 = TypeUtil.ObjectToDecimal(signinConfig.Tables[0].Rows[1]["RewardGold"]);
					base.ViewBag.Gold3 = TypeUtil.ObjectToDecimal(signinConfig.Tables[0].Rows[2]["RewardGold"]);
					base.ViewBag.Gold4 = TypeUtil.ObjectToDecimal(signinConfig.Tables[0].Rows[3]["RewardGold"]);
					base.ViewBag.Gold5 = TypeUtil.ObjectToDecimal(signinConfig.Tables[0].Rows[4]["RewardGold"]);
					base.ViewBag.Gold6 = TypeUtil.ObjectToDecimal(signinConfig.Tables[0].Rows[5]["RewardGold"]);
					base.ViewBag.Gold7 = TypeUtil.ObjectToDecimal(signinConfig.Tables[0].Rows[6]["RewardGold"]);
				}
				catch
				{
				}
			}
			return View();
		}

		[CheckCustomer]
		public JsonResult DoSigninConfig()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(4L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			DataSet dataSet = new DataSet();
			DataTable dataTable = new DataTable();
			dataTable.TableName = "SigninConfig";
			dataTable.Columns.Add("DayID");
			dataTable.Columns.Add("RewardGold");
			decimal num = TypeUtil.ObjectToDecimal(base.Request["Gold1"]);
			decimal num2 = TypeUtil.ObjectToDecimal(base.Request["Gold2"]);
			decimal num3 = TypeUtil.ObjectToDecimal(base.Request["Gold3"]);
			decimal num4 = TypeUtil.ObjectToDecimal(base.Request["Gold4"]);
			decimal num5 = TypeUtil.ObjectToDecimal(base.Request["Gold5"]);
			decimal num6 = TypeUtil.ObjectToDecimal(base.Request["Gold6"]);
			decimal num7 = TypeUtil.ObjectToDecimal(base.Request["Gold7"]);
			for (int i = 1; i <= 7; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["DayID"] = i;
				switch (i)
				{
				case 1:
					dataRow["RewardGold"] = num;
					break;
				case 2:
					dataRow["RewardGold"] = num2;
					break;
				case 3:
					dataRow["RewardGold"] = num3;
					break;
				case 4:
					dataRow["RewardGold"] = num4;
					break;
				case 5:
					dataRow["RewardGold"] = num5;
					break;
				case 6:
					dataRow["RewardGold"] = num6;
					break;
				case 7:
					dataRow["RewardGold"] = num7;
					break;
				}
				dataTable.Rows.Add(dataRow);
			}
			dataSet.Tables.Add(dataTable);
			FacadeManage.aidePlatformFacade.UpdateSigninConfig(dataSet);
			return Json(new
			{
				IsOk = true,
				Msg = "操作成功"
			});
		}

		[CheckCustomer]
		public ActionResult LevelConfig()
		{
			PagerSet list = FacadeManage.aidePlatformFacade.GetList("GrowLevelConfig", 1, 10000, "", "ORDER BY LevelID ASC");
			base.ViewData["data"] = null;
			if (list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				base.ViewData["data"] = list.PageSet.Tables[0];
			}
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult UpdateLevelConfig()
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
			int num = TypeUtil.ObjectToInt(base.Request["LevelID"]);
			string text = TypeUtil.ObjectToString(base.Request["Experience"]);
			string text2 = TypeUtil.ObjectToString(base.Request["RewardGold"]);
			string text3 = TypeUtil.ObjectToString(base.Request["RewardMedal"]);
			string text4 = TypeUtil.ObjectToString(base.Request["LevelRemark"]);
			if (num == 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "非法操作，无效的等级标识"
				});
			}
			if (!Validate.IsNumeric(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入正确的经验值"
				});
			}
			if (!Validate.IsNumeric(text2))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入正确的金币"
				});
			}
			if (!Validate.IsNumeric(text3))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入正确的元宝"
				});
			}
			if (text4.Length > 64)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "备注的最大长度不能超过64"
				});
			}
			GrowLevelConfig growLevelConfig = new GrowLevelConfig();
			growLevelConfig.LevelID = num;
			growLevelConfig.Experience = TypeUtil.ObjectToInt(text);
			growLevelConfig.RewardGold = TypeUtil.ObjectToInt(text2);
			growLevelConfig.RewardMedal = TypeUtil.ObjectToInt(text3);
			growLevelConfig.LevelRemark = text4;
			int num2 = FacadeManage.aidePlatformFacade.UpdateGrowLevelConfig(growLevelConfig);
			if (num2 > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "修改成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "修改失败"
			});
		}

		[CheckCustomer]
		public ActionResult SpreadSet()
		{
			GlobalSpreadInfo globalSpreadInfo = FacadeManage.aideTreasureFacade.GetGlobalSpreadInfo(1);
			base.ViewData["data"] = null;
			base.ViewData["data"] = globalSpreadInfo;
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoSpreadSet()
		{
			int registerGrantScore = TypeUtil.ObjectToInt(base.Request["RegisterGrantScore"]);
			int playTimeCount = TypeUtil.ObjectToInt(base.Request["PlayTimeCount"]);
			int playTimeGrantScore = TypeUtil.ObjectToInt(base.Request["PlayTimeGrantScore"]);
			int num = TypeUtil.ObjectToInt(base.Request["FillGrantRate"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["BalanceRate"]);
			int minBalanceScore = TypeUtil.ObjectToInt(base.Request["MinBalanceScore"]);
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(4L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			GlobalSpreadInfo globalSpreadInfo = FacadeManage.aideTreasureFacade.GetGlobalSpreadInfo(1);
			globalSpreadInfo.RegisterGrantScore = registerGrantScore;
			globalSpreadInfo.PlayTimeCount = playTimeCount;
			globalSpreadInfo.PlayTimeGrantScore = playTimeGrantScore;
			globalSpreadInfo.FillGrantRate = num / 100;
			globalSpreadInfo.BalanceRate = num2 / 100;
			globalSpreadInfo.MinBalanceScore = minBalanceScore;
			try
			{
				FacadeManage.aideTreasureFacade.UpdateGlobalSpreadInfo(globalSpreadInfo);
				return Json(new
				{
					IsOk = true,
					Msg = "更新成功"
				});
			}
			catch
			{
				return Json(new
				{
					IsOk = false,
					Msg = "更新失败"
				});
			}
		}

		[CheckCustomer]
		public ActionResult SpreadManager()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetSpreadList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY RegisterDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string text = TypeUtil.ObjectToString(base.Request["keyWord"]);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			AccountsInfo accountsInfo = new AccountsInfo();
			int num2 = -1;
			if (!string.IsNullOrEmpty(text))
			{
				if (num == 1)
				{
					if (TypeUtil.ObjectToInt(text) > 0)
					{
						accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByGameID(TypeUtil.ObjectToInt(text));
						if (accountsInfo.UserID != 0 && accountsInfo.AgentID == 0)
						{
							num2 = accountsInfo.UserID;
						}
						stringBuilder.AppendFormat(" SpreaderID={0} ", num2);
					}
					else
					{
						stringBuilder.Append(" AND 1=3 ");
					}
				}
				else
				{
					accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByAccount(text);
					if (accountsInfo.UserID != 0 && accountsInfo.AgentID == 0)
					{
						num2 = accountsInfo.UserID;
					}
					stringBuilder.AppendFormat(" WHERE SpreaderID={0}", num2);
				}
			}
			PagerSet accountsList = FacadeManage.aideAccountsFacade.GetAccountsList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (accountsList != null && accountsList.PageSet != null && accountsList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in accountsList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						Accounts = TypeUtil.ObjectToString(row["Accounts"]),
						GameID = TypeUtil.ObjectToString(row["GameID"]),
						SpreadScore = TypeUtil.FormatMoney(FacadeManage.aideTreasureFacade.GetSpreadScore(TypeUtil.ObjectToInt(row["SpreaderID"])).ToString()),
						RegisterDate = TypeUtil.ObjectToDateTime(row["RegisterDate"]).ToString("yyyy-MM-dd"),
						UserID = TypeUtil.ObjectToInt(row["UserID"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((accountsList.PageSet != null && accountsList.PageSet.Tables != null && accountsList.PageSet.Tables[0].Rows.Count != 0) ? accountsList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult SpreadInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public JsonResult GetSpreadInfoList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["ID"]);
			string orderby = "Order By CollectDate Desc";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			TypeUtil.ObjectToString(base.Request["keyWord"]);
			TypeUtil.ObjectToInt(base.Request["Type"]);
			new AccountsInfo();
			stringBuilder.AppendFormat(" AND ChildrenID ={0}", num);
			PagerSet recordSpreadInfoList = FacadeManage.aideTreasureFacade.GetRecordSpreadInfoList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (recordSpreadInfoList != null && recordSpreadInfoList.PageSet != null && recordSpreadInfoList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordSpreadInfoList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CollectDate = TypeUtil.ObjectToString(row["CollectDate"]),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["UserID"])),
						CollectNote = TypeUtil.ObjectToString(row["CollectNote"]),
						Score = TypeUtil.FormatMoney(TypeUtil.ObjectToString(row["Score"])),
						Name = Enum.GetName(typeof(EnumerationList.SpreadTypes), row["TypeID"]),
						UserID = TypeUtil.ObjectToInt(row["UserID"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((recordSpreadInfoList.PageSet != null && recordSpreadInfoList.PageSet.Tables != null && recordSpreadInfoList.PageSet.Tables[0].Rows.Count != 0) ? recordSpreadInfoList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult FinanceInfo()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetFinanceInfoList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string text = TypeUtil.ObjectToString(base.Request["keyWord"]);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (!string.IsNullOrEmpty(text))
			{
				if (num == 1)
				{
					if (TypeUtil.ObjectToInt(text) > 0)
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByGameID(TypeUtil.ObjectToInt(text)));
					}
					else
					{
						stringBuilder.Append(" AND 1=3 ");
					}
				}
				else
				{
					stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByAccount(text));
				}
			}
			PagerSet recordSpreadInfoList = FacadeManage.aideTreasureFacade.GetRecordSpreadInfoList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (recordSpreadInfoList != null && recordSpreadInfoList.PageSet != null && recordSpreadInfoList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in recordSpreadInfoList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["UserID"])),
						Score1 = ((TypeUtil.ObjectToInt(row["Score"]) < 0) ? "" : TypeUtil.FormatMoney(row["Score"].ToString())),
						Score2 = ((TypeUtil.ObjectToInt(row["Score"]) > 0) ? "" : TypeUtil.FormatMoney((TypeUtil.ObjectToInt(row["Score"]) * -1).ToString())),
						Name = Enum.GetName(typeof(EnumerationList.SpreadTypes), row["TypeID"]),
						ChildrenID = ((TypeUtil.ObjectToInt(row["ChildrenID"]) > 0) ? TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["ChildrenID"])) : ""),
						CollectNote = TypeUtil.ObjectToString(row["CollectNote"])
					});
				}
			}
			string gainGold = "";
			string expendGold = "";
			string str = stringBuilder.ToString() + " and Score > 0";
			string sql = "Select Sum(Score) AS GainGold From RecordSpreadInfo " + str;
			DataSet dataSetBySql = FacadeManage.aideTreasureFacade.GetDataSetBySql(sql);
			if (dataSetBySql.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow2 = dataSetBySql.Tables[0].Rows[0];
				if (!string.IsNullOrEmpty(dataRow2["GainGold"].ToString()))
				{
					gainGold = TypeUtil.FormatMoney(dataRow2["GainGold"].ToString());
				}
			}
			string str2 = stringBuilder.ToString() + " and Score < 0";
			sql = "Select Sum(Score) AS CardGold From RecordSpreadInfo " + str2;
			dataSetBySql = FacadeManage.aideTreasureFacade.GetDataSetBySql(sql);
			if (dataSetBySql.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow3 = dataSetBySql.Tables[0].Rows[0];
				if (!string.IsNullOrEmpty(dataRow3["CardGold"].ToString()))
				{
					object obj = dataRow3["CardGold"];
					expendGold = TypeUtil.FormatMoney((TypeUtil.ObjectToInt(dataRow3["CardGold"]) * -1).ToString());
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					GainGold = gainGold,
					ExpendGold = expendGold
				},
				Total = ((recordSpreadInfoList.PageSet != null && recordSpreadInfoList.PageSet.Tables != null && recordSpreadInfoList.PageSet.Tables[0].Rows.Count != 0) ? recordSpreadInfoList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult GlobalPlayPresentList()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DelGlobalPlayPresent()
		{
			string str = TypeUtil.ObjectToString(base.Request["cid"]);
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
			string sqlQuery = "WHERE ServerID in (" + str + ")";
			try
			{
				FacadeManage.aidePlatformFacade.DeleteGlobalPlayPresent(sqlQuery);
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
		public JsonResult GetGlobalPlayPresentList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE ServerID <> -3");
			PagerSet globalPlayPresentList = FacadeManage.aidePlatformFacade.GetGlobalPlayPresentList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (globalPlayPresentList != null && globalPlayPresentList.PageSet != null && globalPlayPresentList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in globalPlayPresentList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						ServerID = TypeUtil.ObjectToString(row["ServerID"]),
						RoomName = ((TypeUtil.ObjectToInt(row["ServerID"]) == -1) ? "游戏币房间" : ((TypeUtil.ObjectToInt(row["ServerID"]) == -2) ? "积分房间" : TypeUtil.GetGameRoomName(TypeUtil.ObjectToInt(row["ServerID"])))),
						MemberList = TypeUtil.GetMemberList(TypeUtil.ObjectToString(row["PresentMember"])),
						CellPlayPresnet = TypeUtil.ObjectToString(row["CellPlayPresnet"]),
						CellPlayTime = TypeUtil.ObjectToString(row["CellPlayTime"]),
						StartPlayTime = TypeUtil.ObjectToString(row["StartPlayTime"]),
						CellOnlinePresent = TypeUtil.ObjectToString(row["CellOnlinePresent"]),
						CellOnlineTime = TypeUtil.ObjectToString(row["CellOnlineTime"]),
						StartOnlineTime = TypeUtil.ObjectToString(row["StartOnlineTime"]),
						IsPlayPresent = ((TypeUtil.ObjectToInt(row["IsPlayPresent"]) == 0) ? "关闭" : "启用"),
						IsOnlinePresent = ((TypeUtil.ObjectToInt(row["IsOnlinePresent"]) == 0) ? "关闭" : "启用"),
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss")
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((globalPlayPresentList.PageSet != null && globalPlayPresentList.PageSet.Tables != null && globalPlayPresentList.PageSet.Tables[0].Rows.Count != 0) ? globalPlayPresentList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult GlobalPlayPresentInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			if (num == 0)
			{
				base.ViewBag.First = false;
				base.ViewBag.Second = true;
			}
			base.ViewData["data"] = null;
			base.ViewBag.ArrayMemberOrder = null;
			base.ViewBag.ServerID = 0;
			GlobalPlayPresent globalPlayPresentInfo = FacadeManage.aidePlatformFacade.GetGlobalPlayPresentInfo(num);
			if (globalPlayPresentInfo != null)
			{
				if (num == -3)
				{
					base.ViewBag.First = true;
					base.ViewBag.Second = false;
				}
				else
				{
					string[] array = globalPlayPresentInfo.PresentMember.Split(new char[1]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries);
					base.ViewBag.ArrayMemberOrder = array;
					base.ViewBag.First = false;
					base.ViewBag.Second = true;
				}
				base.ViewData["data"] = globalPlayPresentInfo;
				base.ViewBag.ServerID = globalPlayPresentInfo.ServerID;
			}
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoGlobalPlayPresentInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["ID"]);
			GlobalPlayPresent globalPlayPresent;
			if (num == 0)
			{
				globalPlayPresent = new GlobalPlayPresent();
			}
			else
			{
				globalPlayPresent = FacadeManage.aidePlatformFacade.GetGlobalPlayPresentInfo(num);
				if (globalPlayPresent == null)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没找到编辑的信息"
					});
				}
			}
			if (num == -3)
			{
				globalPlayPresent.ServerID = num;
				globalPlayPresent.PresentMember = "";
			}
			else
			{
				int num3 = globalPlayPresent.ServerID = TypeUtil.ObjectToInt(base.Request["ServerID"]);
				string text = TypeUtil.ObjectToString(base.Request["PresentMember"]);
				if (string.IsNullOrEmpty(text))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "请选择会员类型"
					});
				}
				globalPlayPresent.PresentMember = text.ToString().TrimEnd(',');
			}
			globalPlayPresent.MaxPresent = TypeUtil.ObjectToInt(base.Request["MaxPresent"]);
			globalPlayPresent.MaxDatePresent = TypeUtil.ObjectToInt(base.Request["MaxDatePresent"]);
			globalPlayPresent.CellPlayPresnet = TypeUtil.ObjectToInt(base.Request["CellPlayPresnet"]);
			globalPlayPresent.CellPlayTime = TypeUtil.ObjectToInt(base.Request["CellPlayTime"]);
			globalPlayPresent.StartPlayTime = TypeUtil.ObjectToInt(base.Request["StartPlayTime"]);
			globalPlayPresent.CellOnlinePresent = TypeUtil.ObjectToInt(base.Request["CellOnlinePresent"]);
			globalPlayPresent.CellOnlineTime = TypeUtil.ObjectToInt(base.Request["CellOnlineTime"]);
			globalPlayPresent.StartOnlineTime = TypeUtil.ObjectToInt(base.Request["StartOnlineTime"]);
			globalPlayPresent.IsPlayPresent = (byte)TypeUtil.ObjectToInt(base.Request["IsPlayPresent"]);
			globalPlayPresent.IsOnlinePresent = (byte)TypeUtil.ObjectToInt(base.Request["IsOnlinePresent"]);
			globalPlayPresent.CollectDate = DateTime.Now;
			if (num == 0)
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
					if (!adminPermission.GetPermission(2L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				if (FacadeManage.aidePlatformFacade.GetGlobalPlayPresentInfo(globalPlayPresent.ServerID) != null)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "该房间的泡点设置已存在"
					});
				}
				FacadeManage.aidePlatformFacade.InsertGlobalPlayPresent(globalPlayPresent);
				return Json(new
				{
					IsOk = true,
					Msg = "新增成功"
				});
			}
			if (user != null)
			{
				AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
				if (!adminPermission2.GetPermission(4L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			if (FacadeManage.aidePlatformFacade.GetGlobalPlayPresentInfo(globalPlayPresent.ServerID) != null && num != globalPlayPresent.ServerID)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "该房间的泡点设置已存在"
				});
			}
			FacadeManage.aidePlatformFacade.UpdateGlobalPlayPresent(globalPlayPresent);
			return Json(new
			{
				IsOk = true,
				Msg = "新增成功"
			});
		}

		[CheckCustomer]
		public ActionResult GamePropertyManager()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult DoGameProperty()
		{
			int num = TypeUtil.ObjectToInt(base.Request["OP"]);
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有选择操作项"
				});
			}
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(4L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			string text2 = "";
			string msg = "";
			string text3 = "WHERE ID in (" + text + ")";
			switch (num)
			{
			case 1:
				try
				{
					FacadeManage.aidePlatformFacade.SetPropertyRecommend(1, text3);
					text2 = "推荐成功";
					msg = "";
				}
				catch
				{
					text2 = "";
					msg = "推荐失败";
				}
				break;
			case 2:
				try
				{
					FacadeManage.aidePlatformFacade.SetPropertyRecommend(0, text3);
					text2 = "取消推荐成功";
					msg = "";
				}
				catch
				{
					text2 = "";
					msg = "取消推荐失败";
				}
				break;
			case 3:
				try
				{
					FacadeManage.aidePlatformFacade.SetPropertyDisbale(text3);
					text2 = "禁用成功";
					msg = "";
				}
				catch
				{
					text2 = "";
					msg = "禁用失败";
				}
				break;
			case 4:
				try
				{
					FacadeManage.aidePlatformFacade.SetPropertyEnbale(text3);
					text2 = "启用成功";
					msg = "";
				}
				catch
				{
					text2 = "";
					msg = "启用失败";
				}
				break;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				return Json(new
				{
					IsOk = true,
					Msg = text2
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = msg
			});
		}

		[CheckCustomer]
		public JsonResult DoGamePropertyInfo()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(4L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			int num = TypeUtil.ObjectToInt(base.Request["ID"]);
			if (num > 0)
			{
				GameProperty gamePropertyInfo = FacadeManage.aidePlatformFacade.GetGamePropertyInfo(num);
				if (gamePropertyInfo != null)
				{
					gamePropertyInfo.Name = TypeUtil.ObjectToString(base.Request["Name"]);
					gamePropertyInfo.PTypeID = TypeUtil.ObjectToInt(base.Request["PTypeID"]);
					gamePropertyInfo.MTypeID = TypeUtil.ObjectToInt(base.Request["MTypeID"]);
					gamePropertyInfo.Cash = TypeUtil.ObjectToDecimal(base.Request["Cash"]);
					gamePropertyInfo.Gold = TypeUtil.ObjectToInt(base.Request["Gold"]);
					gamePropertyInfo.UserMedal = TypeUtil.ObjectToInt(base.Request["UserMedal"]);
					gamePropertyInfo.LoveLiness = TypeUtil.ObjectToInt(base.Request["LoveLiness"]);
					gamePropertyInfo.UseArea = (short)TypeUtil.ObjectToInt(base.Request["IssueArea"]);
					gamePropertyInfo.ServiceArea = (short)TypeUtil.ObjectToInt(base.Request["ServiceArea"]);
					gamePropertyInfo.SendLoveLiness = TypeUtil.ObjectToInt(base.Request["SendLoveLiness"]);
					gamePropertyInfo.RecvLoveLiness = TypeUtil.ObjectToInt(base.Request["RecvLoveLiness"]);
					gamePropertyInfo.RegulationsInfo = TypeUtil.ObjectToString(base.Request["UseResultsGold"]);
					gamePropertyInfo.UseResultsGold = TypeUtil.ObjectToInt(base.Request["UseResultsGold"]);
					gamePropertyInfo.UseResultsValidTime = TypeUtil.ObjectToInt(base.Request["UseResultsValidTime"]);
					gamePropertyInfo.UseResultsValidTimeScoreMultiple = TypeUtil.ObjectToInt(base.Request["UseResultsValidTimeScoreMultiple"]);
					gamePropertyInfo.Nullity = (byte)TypeUtil.ObjectToInt(base.Request["Nullity"]);
					gamePropertyInfo.Recommend = (byte)TypeUtil.ObjectToInt(base.Request["Recommend"]);
					gamePropertyInfo.SuportMobile = (byte)TypeUtil.ObjectToInt(base.Request["SuportMobile"]);
					try
					{
						FacadeManage.aidePlatformFacade.UpdateGameProperty(gamePropertyInfo);
						return Json(new
						{
							IsOk = true,
							Msg = "更新成功"
						});
					}
					catch
					{
						return Json(new
						{
							IsOk = false,
							Msg = "更新失败"
						});
					}
				}
				return Json(new
				{
					IsOk = false,
					Msg = "参数出错"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "参数出错"
			});
		}

		[CheckCustomer]
		public JsonResult GetGamePropertyList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY ID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.Append(string.Format(" AND Kind<>{0} ", 11));
			PagerSet gamePropertyList = FacadeManage.aidePlatformFacade.GetGamePropertyList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (gamePropertyList != null && gamePropertyList.PageSet != null && gamePropertyList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in gamePropertyList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						ID = TypeUtil.ObjectToString(row["ID"]),
						Name = TypeUtil.ObjectToString(row["Name"]),
						Cash = TypeUtil.ObjectToString(row["Cash"]),
						Gold = TypeUtil.ObjectToString(row["Gold"]),
						UserMedal = TypeUtil.ObjectToString(row["UserMedal"]),
						LoveLiness = TypeUtil.ObjectToString(row["LoveLiness"]),
						IssueArea = TypeUtil.GetIssueArea(TypeUtil.ObjectToInt(row["UseArea"])),
						ServiceArea = TypeUtil.GetServiceArea(TypeUtil.ObjectToInt(row["ServiceArea"])),
						SuportMobile = "<span>否</span>",
						SendLoveLiness = TypeUtil.ObjectToString(row["SendLoveLiness"]),
						RecvLoveLiness = TypeUtil.ObjectToString(row["RecvLoveLiness"]),
						UseResultsGold = TypeUtil.ObjectToString(row["UseResultsGold"]),
						UseResultsValidTime = TypeUtil.ObjectToString(row["UseResultsValidTime"]),
						UseResultsValidTimeScoreMultiple = TypeUtil.ObjectToString(row["UseResultsValidTimeScoreMultiple"]),
						RecommendName = ((TypeUtil.ObjectToInt(row["Recommend"]) == 0) ? "<span>否</span>" : ((TypeUtil.ObjectToInt(row["Recommend"]) == 1) ? "<span class='hong'>是</span>" : "<span>否</span>")),
						NullityStatus = TypeUtil.GetNullityStatus((byte)row["Nullity"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((gamePropertyList.PageSet != null && gamePropertyList.PageSet.Tables != null && gamePropertyList.PageSet.Tables[0].Rows.Count != 0) ? gamePropertyList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult GamePropertyInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			base.ViewBag.PTypeID = 0;
			base.ViewBag.MTypeID = 0;
			base.ViewBag.intIssueArea = 0;
			base.ViewBag.intServiceArea = 0;
			if (num > 0)
			{
				GameProperty gamePropertyInfo = FacadeManage.aidePlatformFacade.GetGamePropertyInfo(num);
				if (gamePropertyInfo != null)
				{
					base.ViewBag.intIssueArea = gamePropertyInfo.UseArea;
					base.ViewBag.intServiceArea = gamePropertyInfo.ServiceArea;
					base.ViewBag.PTypeID = gamePropertyInfo.PTypeID;
					base.ViewBag.MTypeID = gamePropertyInfo.MTypeID;
					base.ViewBag.Name = gamePropertyInfo.Name.ToString();
					base.ViewBag.Cash = gamePropertyInfo.Cash.ToString();
					base.ViewBag.Gold = gamePropertyInfo.Gold.ToString();
					base.ViewBag.UserMedal = gamePropertyInfo.UserMedal.ToString();
					base.ViewBag.LoveLiness = gamePropertyInfo.LoveLiness.ToString();
					base.ViewBag.SendLoveLiness = gamePropertyInfo.SendLoveLiness.ToString();
					base.ViewBag.RecvLoveLiness = gamePropertyInfo.RecvLoveLiness.ToString();
					base.ViewBag.RegulationsInfo = gamePropertyInfo.RegulationsInfo.ToString();
					base.ViewBag.UseResultsGold = gamePropertyInfo.UseResultsGold.ToString();
					base.ViewBag.UseResultsValidTime = gamePropertyInfo.UseResultsValidTime.ToString();
					base.ViewBag.UseResultsValidTimeScoreMultiple = gamePropertyInfo.UseResultsValidTimeScoreMultiple.ToString();
					base.ViewBag.Nullity = ((gamePropertyInfo.Nullity == 0) ? true : false);
					base.ViewBag.Recommend = ((gamePropertyInfo.Recommend == 1) ? true : false);
					base.ViewBag.SuportMobile = ((gamePropertyInfo.SuportMobile == 1) ? true : false);
				}
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult GameGiftList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetGameGiftList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY ID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.Append(string.Format(" AND Kind={0} ", 11));
			PagerSet gamePropertyList = FacadeManage.aidePlatformFacade.GetGamePropertyList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (gamePropertyList != null && gamePropertyList.PageSet != null && gamePropertyList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in gamePropertyList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						ID = TypeUtil.ObjectToString(row["ID"]),
						Name = TypeUtil.ObjectToString(row["Name"]),
						Cash = TypeUtil.ObjectToString(row["Cash"]),
						Gold = TypeUtil.ObjectToString(row["Gold"]),
						UserMedal = TypeUtil.ObjectToString(row["UserMedal"]),
						LoveLiness = TypeUtil.ObjectToString(row["LoveLiness"]),
						IssueArea = TypeUtil.GetIssueArea(TypeUtil.ObjectToInt(row["UseArea"])),
						ServiceArea = TypeUtil.GetServiceArea(TypeUtil.ObjectToInt(row["ServiceArea"])),
						SendLoveLiness = TypeUtil.ObjectToString(row["SendLoveLiness"]),
						RecvLoveLiness = TypeUtil.ObjectToString(row["RecvLoveLiness"]),
						UseResultsGold = TypeUtil.ObjectToString(row["UseResultsGold"]),
						UseResultsValidTime = TypeUtil.ObjectToString(row["UseResultsValidTime"]),
						UseResultsValidTimeScoreMultiple = TypeUtil.ObjectToString(row["UseResultsValidTimeScoreMultiple"]),
						RecommendName = ((TypeUtil.ObjectToInt(row["Recommend"]) == 0) ? "<span>否</span>" : ((TypeUtil.ObjectToInt(row["Recommend"]) == 1) ? "<span class='hong'>是</span>" : "<span>否</span>")),
						NullityStatus = TypeUtil.GetNullityStatus((byte)row["Nullity"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((gamePropertyList.PageSet != null && gamePropertyList.PageSet.Tables != null && gamePropertyList.PageSet.Tables[0].Rows.Count != 0) ? gamePropertyList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult DoGameGiftList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["OP"]);
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有选择操作项"
				});
			}
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(4L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			string text2 = "";
			string msg = "";
			string text3 = "WHERE ID in (" + text + ")";
			switch (num)
			{
			case 1:
				try
				{
					FacadeManage.aidePlatformFacade.SetPropertyRecommend(1, text3);
					text2 = "推荐成功";
					msg = "";
				}
				catch
				{
					text2 = "";
					msg = "推荐失败";
				}
				break;
			case 2:
				try
				{
					FacadeManage.aidePlatformFacade.SetPropertyRecommend(0, text3);
					text2 = "取消推荐成功";
					msg = "";
				}
				catch
				{
					text2 = "";
					msg = "取消推荐失败";
				}
				break;
			case 3:
				try
				{
					FacadeManage.aidePlatformFacade.SetPropertyDisbale(text3);
					text2 = "禁用成功";
					msg = "";
				}
				catch
				{
					text2 = "";
					msg = "禁用失败";
				}
				break;
			case 4:
				try
				{
					FacadeManage.aidePlatformFacade.SetPropertyEnbale(text3);
					text2 = "启用成功";
					msg = "";
				}
				catch
				{
					text2 = "";
					msg = "启用失败";
				}
				break;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				return Json(new
				{
					IsOk = true,
					Msg = text2
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = msg
			});
		}

		[CheckCustomer]
		public ActionResult GameGiftInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["cmd"]);
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.OP = text;
			base.ViewBag.ID = num;
			if (text == "add")
			{
				base.ViewBag.Info = "新增";
			}
			else
			{
				base.ViewBag.Info = "更新";
			}
			if (num > 0)
			{
				GameProperty gamePropertyInfo = FacadeManage.aidePlatformFacade.GetGamePropertyInfo(num);
				if (gamePropertyInfo != null)
				{
					base.ViewBag.IssueArea = gamePropertyInfo.UseArea;
					base.ViewBag.ServiceArea = gamePropertyInfo.UseArea;
					base.ViewBag.Name = gamePropertyInfo.Name.ToString();
					base.ViewBag.Cash = gamePropertyInfo.Cash.ToString();
					base.ViewBag.Gold = gamePropertyInfo.Gold.ToString();
					base.ViewBag.UserMedal = gamePropertyInfo.UserMedal.ToString();
					base.ViewBag.LoveLiness = gamePropertyInfo.LoveLiness.ToString();
					base.ViewBag.SendLoveLiness = gamePropertyInfo.SendLoveLiness.ToString();
					base.ViewBag.RecvLoveLiness = gamePropertyInfo.RecvLoveLiness.ToString();
					base.ViewBag.RegulationsInfo = gamePropertyInfo.RegulationsInfo;
					base.ViewBag.UseResultsGold = gamePropertyInfo.UseResultsGold.ToString();
					base.ViewBag.UseResultsValidTime = gamePropertyInfo.UseResultsValidTime.ToString();
					base.ViewBag.UseResultsValidTimeScoreMultiple = gamePropertyInfo.UseResultsValidTimeScoreMultiple.ToString();
					base.ViewBag.Nullity = (gamePropertyInfo.Nullity == 0);
					base.ViewBag.Recommend = (gamePropertyInfo.Recommend == 1);
				}
			}
			return View();
		}

		[CheckCustomer]
		public JsonResult DoGameGiftInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["OP"]);
			int id = TypeUtil.ObjectToInt(base.Request["ID"]);
			string name = TypeUtil.ObjectToString(base.Request["Name"]);
			int num = TypeUtil.ObjectToInt(base.Request["Cash"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["Gold"]);
			int userMedal = TypeUtil.ObjectToInt(base.Request["UserMedal"]);
			int loveLiness = TypeUtil.ObjectToInt(base.Request["LoveLiness"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["ServiceArea"]);
			int num4 = TypeUtil.ObjectToInt(base.Request["IssueArea"]);
			int num5 = TypeUtil.ObjectToInt(base.Request["SendLoveLiness"]);
			int num6 = TypeUtil.ObjectToInt(base.Request["RecvLoveLiness"]);
			int num7 = TypeUtil.ObjectToInt(base.Request["UseResultsGold"]);
			int num8 = TypeUtil.ObjectToInt(base.Request["UseResultsValidTime"]);
			int useResultsValidTimeScoreMultiple = TypeUtil.ObjectToInt(base.Request["UseResultsValidTimeScoreMultiple"]);
			string regulationsInfo = TypeUtil.ObjectToString(base.Request["RegulationsInfo"]);
			int num9 = TypeUtil.ObjectToInt(base.Request["Nullity"]);
			int recommend = TypeUtil.ObjectToInt(base.Request["Recommend"]);
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(4L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			GameProperty gameProperty = new GameProperty();
			if (a != "add")
			{
				gameProperty = FacadeManage.aidePlatformFacade.GetGamePropertyInfo(id);
			}
			gameProperty.Name = name;
			gameProperty.Cash = TypeUtil.ObjectToDecimal(num);
			gameProperty.Gold = TypeUtil.ObjectToLong(num2);
			gameProperty.UserMedal = userMedal;
			gameProperty.LoveLiness = loveLiness;
			gameProperty.UseArea = (short)num4;
			gameProperty.ServiceArea = (short)num3;
			gameProperty.SendLoveLiness = num5;
			gameProperty.RecvLoveLiness = num6;
			gameProperty.RegulationsInfo = regulationsInfo;
			gameProperty.UseResultsGold = TypeUtil.ObjectToLong(num7);
			gameProperty.UseResultsValidTime = TypeUtil.ObjectToLong(num8);
			gameProperty.UseResultsValidTimeScoreMultiple = useResultsValidTimeScoreMultiple;
			gameProperty.Nullity = (byte)num9;
			gameProperty.Recommend = recommend;
			if (!(a == "add"))
			{
				try
				{
					FacadeManage.aidePlatformFacade.UpdateGameProperty(gameProperty);
					return Json(new
					{
						IsOk = true,
						Msg = "更新成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "更新失败"
					});
				}
			}
			int maxPropertyID = FacadeManage.aidePlatformFacade.GetMaxPropertyID();
			gameProperty.ID = maxPropertyID + 1;
			gameProperty.Kind = 11;
			try
			{
				FacadeManage.aidePlatformFacade.InsertGameProperty(gameProperty);
				return Json(new
				{
					IsOk = true,
					Msg = "新增成功"
				});
			}
			catch
			{
				return Json(new
				{
					IsOk = false,
					Msg = "新增失败"
				});
			}
		}

		[CheckCustomer]
		public ActionResult GameGiftSubList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			return View();
		}

		[CheckCustomer]
		public JsonResult GetGameGiftSubList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["ID"]);
			string orderby = "ORDER BY SortID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num > 0)
			{
				stringBuilder.Append(string.Format(" AND OwnerID={0} ", num));
			}
			PagerSet list = FacadeManage.aidePlatformFacade.GetList("GamePropertySub", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						ID = TypeUtil.ObjectToString(row["ID"]),
						OwnerID = TypeUtil.ObjectToString(row["OwnerID"]),
						Count = TypeUtil.ObjectToString(row["Count"]),
						SortID = TypeUtil.ObjectToString(row["SortID"]),
						GamePropertyName = TypeUtil.GetGamePropertyName(TypeUtil.ObjectToInt(row["ID"]))
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((list.PageSet != null && list.PageSet.Tables != null && list.PageSet.Tables[0].Rows.Count != 0) ? list.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list2)
			});
		}

		[CheckCustomer]
		public JsonResult DoGameGiftSubList()
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
			string str = TypeUtil.ObjectToString(base.Request["cid"]);
			string sqlQuery = "WHERE ID in (" + str + ")";
			try
			{
				FacadeManage.aidePlatformFacade.DeleteGamePropertySub(sqlQuery);
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
		public ActionResult GameGiftSubInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["OwnerID"]);
			string text = TypeUtil.ObjectToString(base.Request["cmd"]);
			base.ViewBag.ID = num;
			base.ViewBag.OwnerID = num2;
			base.ViewBag.Op = text;
			base.ViewBag.SelectedID = 0;
			if (text == "add")
			{
				base.ViewBag.Info = "新增";
			}
			else
			{
				base.ViewBag.Info = "更新";
			}
			if (num > 0 && num2 > 0)
			{
				GamePropertySub gamePropertySubInfo = FacadeManage.aidePlatformFacade.GetGamePropertySubInfo(num, num2);
				if (gamePropertySubInfo != null)
				{
					base.ViewBag.SelectedID = gamePropertySubInfo.ID;
					base.ViewBag.Count = gamePropertySubInfo.Count.ToString();
					base.ViewBag.SortID = gamePropertySubInfo.SortID.ToString();
				}
			}
			return View();
		}

		[CheckCustomer]
		public JsonResult DoGameGiftSubInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["OP"]);
			int iD = TypeUtil.ObjectToInt(base.Request["GameProperty"]);
			int ownerID = TypeUtil.ObjectToInt(base.Request["OwnerID"]);
			int count = TypeUtil.ObjectToInt(base.Request["Count"]);
			int sortID = TypeUtil.ObjectToInt(base.Request["SortID"]);
			GamePropertySub gamePropertySub = new GamePropertySub();
			gamePropertySub.ID = iD;
			gamePropertySub.OwnerID = ownerID;
			gamePropertySub.Count = count;
			gamePropertySub.SortID = sortID;
			if (!(a == "add"))
			{
				try
				{
					FacadeManage.aidePlatformFacade.UpdateGamePropertySub(gamePropertySub);
					return Json(new
					{
						IsOk = true,
						Msg = "更新成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "更新失败"
					});
				}
			}
			try
			{
				FacadeManage.aidePlatformFacade.InsertGamePropertySub(gamePropertySub);
				return Json(new
				{
					IsOk = true,
					Msg = "新增成功"
				});
			}
			catch
			{
				return Json(new
				{
					IsOk = false,
					Msg = "新增失败"
				});
			}
		}

		[CheckCustomer]
		public ActionResult GamePropertyTypeList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetGamePropertyTypeList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY TypeID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet gamePropertyTypeList = FacadeManage.aidePlatformFacade.GetGamePropertyTypeList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (gamePropertyTypeList != null && gamePropertyTypeList.PageSet != null && gamePropertyTypeList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in gamePropertyTypeList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						TypeID = TypeUtil.ObjectToString(row["TypeID"]),
						TypeName = TypeUtil.ObjectToString(row["TypeName"]),
						TagID = ((TypeUtil.ObjectToInt(row["TagID"]) == 0) ? "大厅" : "手机"),
						SortID = TypeUtil.ObjectToString(row["SortID"]),
						NullityStatus = TypeUtil.GetNullityStatus((byte)row["Nullity"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((gamePropertyTypeList.PageSet != null && gamePropertyTypeList.PageSet.Tables != null && gamePropertyTypeList.PageSet.Tables[0].Rows.Count != 0) ? gamePropertyTypeList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult GamePropertyTypeInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["cmd"]);
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.OP = text;
			base.ViewBag.ID = num;
			if (num <= 0)
			{
				return RedirectToRoute(new
				{
					controller = "App",
					action = "GamePropertyTypeList"
				});
			}
			GamePropertyType gamePropertyTypeInfo = FacadeManage.aidePlatformFacade.GetGamePropertyTypeInfo(num);
			if (gamePropertyTypeInfo == null)
			{
				return RedirectToRoute(new
				{
					controller = "App",
					action = "GamePropertyTypeList"
				});
			}
			base.ViewBag.TypeID = gamePropertyTypeInfo.TypeID.ToString().Trim();
			base.ViewBag.TypeName = gamePropertyTypeInfo.TypeName.Trim();
			base.ViewBag.SortID = gamePropertyTypeInfo.SortID.ToString().Trim();
			base.ViewBag.Nullity = gamePropertyTypeInfo.Nullity.ToString().Trim();
			base.ViewBag.TagID = gamePropertyTypeInfo.TagID.ToString();
			base.ViewBag.TagName = ((gamePropertyTypeInfo.TagID == 0) ? "大厅" : "手机");
			return View();
		}

		[CheckCustomer]
		public JsonResult DoGamePropertyTypeInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["OP"]);
			TypeUtil.ObjectToInt(base.Request["ID"]);
			int num = TypeUtil.ObjectToInt(base.Request["typeID"]);
			if (num < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "类型标识为整数"
				});
			}
			int num2 = TypeUtil.ObjectToInt(base.Request["SortID"]);
			if (num2 < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "排序为整数"
				});
			}
			string text = TypeUtil.ObjectToString(base.Request["TypeName"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "类型名称不能为空"
				});
			}
			int num3 = TypeUtil.ObjectToInt(base.Request["Nullity"]);
			int tagID = TypeUtil.ObjectToInt(base.Request["TagID"]);
			GamePropertyType gamePropertyType = new GamePropertyType();
			gamePropertyType.TypeID = num;
			gamePropertyType.TypeName = text;
			gamePropertyType.SortID = num2;
			gamePropertyType.Nullity = (byte)num3;
			gamePropertyType.TagID = tagID;
			Game.Kernel.Message message = new Game.Kernel.Message();
			if (a == "add")
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
					if (!adminPermission.GetPermission(2L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				if (FacadeManage.aidePlatformFacade.IsExistsTypeID(gamePropertyType.TypeID))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "游戏类型标识已经存在"
					});
				}
				message = FacadeManage.aidePlatformFacade.InsertGamePropertyType(gamePropertyType);
			}
			else
			{
				if (user != null)
				{
					AdminPermission adminPermission2 = new AdminPermission(user, user.MoudleID);
					if (!adminPermission2.GetPermission(4L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				message = FacadeManage.aidePlatformFacade.UpdateGamePropertyType(gamePropertyType);
			}
			if (message.Success)
			{
				if (a == "add")
				{
					return Json(new
					{
						IsOk = true,
						Msg = "类型信息增加成功"
					});
				}
				return Json(new
				{
					IsOk = true,
					Msg = "类型信息修改成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = message.Content
			});
		}

		[CheckCustomer]
		public ActionResult LotteryConfigSet()
		{
			base.ViewData["data"] = null;
			LotteryConfig lotteryConfig = FacadeManage.aideTreasureFacade.GetLotteryConfig(1);
			base.ViewData["data"] = lotteryConfig;
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoLotteryConfigSet()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(4L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			int freeCount = TypeUtil.ObjectToInt(base.Request["FreeCount"]);
			int chargeFee = TypeUtil.ObjectToInt(base.Request["ChargeFee"]);
			byte isCharge = (byte)TypeUtil.ObjectToInt(base.Request["IsCharge"]);
			LotteryConfig lotteryConfig = FacadeManage.aideTreasureFacade.GetLotteryConfig(1);
			lotteryConfig.FreeCount = freeCount;
			lotteryConfig.ChargeFee = chargeFee;
			lotteryConfig.IsCharge = isCharge;
			try
			{
				FacadeManage.aideTreasureFacade.UpdateLotteryConfig(lotteryConfig);
				return Json(new
				{
					IsOk = true,
					Msg = "更新成功"
				});
			}
			catch
			{
				return Json(new
				{
					IsOk = false,
					Msg = "更新失败"
				});
			}
		}

		[CheckCustomer]
		public ActionResult LotteryItemSet()
		{
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("LotteryItem", 1, 10000, "", "ORDER BY ItemIndex ASC");
			base.ViewData["data"] = null;
			if (list != null && list.PageSet != null)
			{
				base.ViewData["data"] = list.PageSet.Tables[0];
			}
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoLotteryItemSet()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(4L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			int num = TypeUtil.ObjectToInt(base.Request["ItemIndex"]);
			if (num < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "非法操作，无效的奖品索引"
				});
			}
			int itemType = TypeUtil.ObjectToInt(base.Request["ItemType"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["ItemQuota"]);
			if (num2 < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入正确的奖品数量"
				});
			}
			int num3 = TypeUtil.ObjectToInt(base.Request["ItemRate"]);
			if (num3 < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入正确的中奖几率"
				});
			}
			LotteryItem lotteryItem = new LotteryItem();
			lotteryItem.ItemIndex = num;
			lotteryItem.ItemType = itemType;
			lotteryItem.ItemQuota = num2;
			lotteryItem.ItemRate = num3;
			int num4 = FacadeManage.aideTreasureFacade.UpdateLotteryItem(lotteryItem);
			if (num4 > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "修改成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "修改失败"
			});
		}

		[CheckCustomer]
		public ActionResult LotteryRecord()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetLotteryRecordList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY CollectDate DESC";
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			string text = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (!string.IsNullOrEmpty(text))
			{
				if (num == 1)
				{
					if (TypeUtil.ObjectToInt(text) > 0)
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByGameID(TypeUtil.ObjectToInt(text)));
					}
					else
					{
						stringBuilder.Append(" AND 1=3 ");
					}
				}
				else
				{
					stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByAccount(text));
				}
			}
			PagerSet list = FacadeManage.aideRecordFacade.GetList("RecordLottery", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						CollectDate = TypeUtil.ObjectToDateTime(row["CollectDate"]).ToString("yyyy-MM-dd HH:mm:ss"),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["UserID"])),
						ChargeFee = TypeUtil.ObjectToString(row["ChargeFee"]),
						ItemType = ((TypeUtil.ObjectToInt(row["ItemType"]) == 0) ? "游戏币" : "游戏豆"),
						ItemQuota = TypeUtil.ObjectToString(row["ItemQuota"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((list.PageSet != null && list.PageSet.Tables != null && list.PageSet.Tables[0].Rows.Count != 0) ? list.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list2)
			});
		}

		[CheckCustomer]
		public ActionResult HundredGames()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetHundredGames()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			PagerSet list = FacadeManage.aidePlatformFacade.GetList("GameRoomInfo", pageIndex, pageSize, "WHERE GameID in(SELECT KindID FROM GameKindItem where WebTypeID=2)", " ORDER BY GameID asc");
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				DataTable dataTable = list.PageSet.Tables[0];
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
					string url = api_url + "Game/GetBRControlList";
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
					int num = 0;
					foreach (dynamic item in val.Data)
					{
						list2.Add(new
						{
							ServerID = (object)item.ServerID,
							ServerName = ((num == 0) ? "默认配置" : dataTable.Rows[num - 1]["ServerName"]),
							IsOpen = (object)item.IsOpen,
							StorageStart = (object)item.StorageStart,
							StorageDeduct = (object)item.StorageDeduct,
							AttenuationScore = (object)item.AttenuationScore
						});
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
				Data = JsonConvert.SerializeObject(list2)
			});
		}

		[CheckCustomer]
		public ActionResult HundredGamesLog()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetHundredGamesLog()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			PagerSet list = FacadeManage.aideRecordFacade.GetList("BarrenGameLog", pageIndex, pageSize, "", " ORDER BY AddTime DESC");
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						ServerID = TypeUtil.ObjectToInt(row["ServerID"]),
						ServerName = TypeUtil.ObjectToString(row["ServerName"]),
						StorageStart = TypeUtil.ObjectToLong(row["StorageStart"]),
						StorageDeduct = TypeUtil.ObjectToInt(row["StorageDeduct"]),
						AttenuationScore = TypeUtil.ObjectToDecimal(row["AttenuationScore"]),
						Operator = TypeUtil.ObjectToString(row["Operator"]),
						AddTime = TypeUtil.ObjectToDateTime(row["AddTime"]).ToString("yyyy-MM-dd HH:mm:ss"),
						ClientIP = TypeUtil.ObjectToString(row["ClientIP"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((list.PageSet != null && list.PageSet.Tables != null && list.PageSet.Tables[0].Rows.Count != 0) ? list.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list2)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult AddHundredGames()
		{
			int num = TypeUtil.ObjectToInt(base.Request["sid"]);
			string serverName = TypeUtil.ObjectToString(base.Request["name"]);
			decimal num2 = TypeUtil.ObjectToDecimal(base.Request["StorageStart"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["StorageDeduct"]);
			decimal num4 = TypeUtil.ObjectToDecimal(base.Request["AttenuationScore"]);
			if (!(num4 > 0m))
			{
				try
				{
					string url = api_url + "Game/UpdateBRControl";
					string param = "key=" + api_key + "&sid=" + num + "&StorageStart=" + num2 + "&StorageDeduct=" + num3;
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
					BarrenGameLog barrenGameLog = new BarrenGameLog();
					barrenGameLog.ServerID = num;
					barrenGameLog.ServerName = serverName;
					barrenGameLog.StorageStart = num2;
					barrenGameLog.AttenuationScore = num4;
					barrenGameLog.StorageDeduct = num3;
					barrenGameLog.Operator = user.Username;
					barrenGameLog.ClientIP = GameRequest.GetUserIP();
					FacadeManage.aideRecordFacade.InsertBaiRenGameLog(barrenGameLog);
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
				Msg = "提取衰减只能为负数"
			});
		}

		[CheckCustomer]
		public JsonResult UpdateHundredGamesStute()
		{
			int num = TypeUtil.ObjectToInt(base.Request["sid"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["IsOpen"]);
			try
			{
				string url = api_url + "Game/OpenBRControl";
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

		[CheckCustomer]
		public ActionResult PushMessageList()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetPutMessageList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY ID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string text = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			if (text != "")
			{
				stringBuilder.AppendFormat(" AND Accounts='{0}'", text);
			}
			PagerSet list = FacadeManage.aideNativeWebFacade.GetList("T_PushedNews", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public ActionResult PushMessageInfo()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult PushMessage()
		{
			string accounts = TypeUtil.ObjectToString(base.Request["Accounts"]);
			string device = TypeUtil.ObjectToString(base.Request["Device"]);
			string text = TypeUtil.ObjectToString(base.Request["Body"]);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			string text2 = TypeUtil.ObjectToString(base.Request["SendTime"]);
			if (num != 1 && text2 == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请设置定时推送的时间"
				});
			}
			DateTime result = DateTime.Now;
			if (num != 1 && !DateTime.TryParse(text2, out result))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请设置定时推送的时间格式错误"
				});
			}
			if (!(text == ""))
			{
				if (user != null)
				{
					AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
					if (!adminPermission.GetPermission(2L))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "没有权限",
							Url = "/NoPower/Index"
						});
					}
				}
				try
				{
					Push(AppConfig.AppKey, AppConfig.MasterSecret, accounts, device, text, num, result.ToString("yyyy-MM-dd HH:mm:ss"));
					DataTable pushConfig = FacadeManage.aideAccountsFacade.GetPushConfig();
					if (pushConfig.Rows.Count > 0)
					{
						foreach (DataRow row in pushConfig.Rows)
						{
							Push(row["AppKey"].ToString(), row["AppSecret"].ToString(), accounts, device, text, num, result.ToString("yyyy-MM-dd HH:mm:ss"));
						}
					}
					FacadeManage.aideNativeWebFacade.InsertPushMessage(accounts, device, text, user.Username, GameRequest.GetUserIP());
					return Json(new
					{
						IsOk = true,
						Msg = "推送成功"
					});
				}
				catch (Exception ex)
				{
					return Json(new
					{
						IsOk = true,
						Msg = ex.Message
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "内容不能为空"
			});
		}

		public bool Push(string key, string secret, string Accounts, string Device, string Body, int Type, string sendTime)
		{
			JPushClient jPushClient = new JPushClient(key, secret);
			object audience;
			if (Accounts == "")
			{
				audience = "all";
			}
			else
			{
				Audience audience2 = new Audience();
				audience2.Alias = new List<string>
				{
					Accounts
				};
				audience = audience2;
			}
			string isApnsProduction = AppConfig.IsApnsProduction;
			bool isApnsProduction2 = false;
			if (isApnsProduction == "true")
			{
				isApnsProduction2 = true;
			}
			PushPayload pushPayload = new PushPayload();
			pushPayload.Platform = ((Device == "all") ? "all" : ("[\"" + Device + "\"]"));
			pushPayload.Audience = audience;
			pushPayload.Notification = new Notification
			{
				Alert = Body,
				Android = new Android
				{
					Alert = Body,
					Title = "title",
					Extras = new Dictionary<string, object>
					{
						{
							"key",
							""
						}
					}
				},
				IOS = new IOS
				{
					Alert = Body,
					Badge = "+1",
					Extras = new Dictionary<string, object>
					{
						{
							"key",
							""
						}
					}
				}
			};
			pushPayload.Message = new Jiguang.JPush.Model.Message
			{
				Title = "message title",
				Content = "message content"
			};
			pushPayload.Options = new Options
			{
				IsApnsProduction = isApnsProduction2
			};
			PushPayload pushPayload2 = pushPayload;
			HttpResponse httpResponse = (Type == 1) ? jPushClient.SendPush(pushPayload2) : jPushClient.Schedule.CreateSingleScheduleTask("task1", pushPayload2, sendTime);
			Dictionary<string, object> dictionary = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(httpResponse.Content);
			if (dictionary.ContainsKey("error"))
			{
				return false;
			}
			return true;
		}

		[CheckCustomer]
		public ActionResult HighScoreAnnounceList()
		{
			DataTable payQudaoList = FacadeManage.aideTreasureFacade.GetPayQudaoList();
			return View(payQudaoList);
		}

		[CheckCustomer]
		public JsonResult GetHighScoreAnnounceList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 50);
			string orderby = "ORDER BY KindID ASC";
			StringBuilder stringBuilder = new StringBuilder("WHERE 1=1");
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("T_HighScoreAnnounce", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public JsonResult UpdateHighScoreAnnounce()
		{
			int num = TypeUtil.ObjectToInt(base.Request["score"], 0);
			int num2 = TypeUtil.ObjectToInt(base.Request["kid"], 0);
			if (num <= 0 || num2 < 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			int num3 = FacadeManage.aideTreasureFacade.UpdateHighScoreAnnounce(num, num2);
			if (num3 > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "保存成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "保存失败"
			});
		}

		[CheckCustomer]
		public JsonResult DelHighScoreAnnounce()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (text == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有选择操作项"
				});
			}
			if (CheckHelper.CheckIds(text))
			{
				string sqlWhere = " WHERE KindID IN (" + text + ")";
				try
				{
					FacadeManage.aideTreasureFacade.DelHighScoreAnnounce(sqlWhere);
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
	}
}
