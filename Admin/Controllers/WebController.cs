using Admin.Filters;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class WebController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetNewsList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY IssueDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			stringBuilder.Append(" AND ClassID IN (1,2) ");
			PagerSet newsList = FacadeManage.aideNativeWebFacade.GetNewsList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (newsList != null && newsList.PageSet != null && newsList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in newsList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						NewsID = TypeUtil.ObjectToString(row["NewsID"]),
						Subject = TypeUtil.ObjectToString(row["Subject"]),
						ClassName = ((TypeUtil.ObjectToInt(row["ClassID"]) == 1) ? "新闻" : ((TypeUtil.ObjectToInt(row["ClassID"]) == 2) ? "公告" : "")),
						IsLinks = ((TypeUtil.ObjectToInt(row["IsLinks"]) == 0) ? "否" : ((TypeUtil.ObjectToInt(row["IsLinks"]) == 1) ? "<span class='hong'>是</span>" : "")),
						IsLock = ((TypeUtil.ObjectToInt(row["IsLock"]) == 0) ? "否" : ((TypeUtil.ObjectToInt(row["IsLock"]) == 1) ? "<span class='hong'>是</span>" : "")),
						OnTopAll = ((TypeUtil.ObjectToInt(row["OnTopAll"]) == 0) ? "否" : ((TypeUtil.ObjectToInt(row["OnTopAll"]) == 1) ? "<span class='hong'>是</span>" : "")),
						OnTop = ((TypeUtil.ObjectToInt(row["OnTop"]) == 0) ? "否" : ((TypeUtil.ObjectToInt(row["OnTop"]) == 1) ? "<span class='hong'>是</span>" : "")),
						IsElite = ((TypeUtil.ObjectToInt(row["IsElite"]) == 0) ? "否" : ((TypeUtil.ObjectToInt(row["IsElite"]) == 1) ? "<span class='hong'>是</span>" : "")),
						IsHot = ((TypeUtil.ObjectToInt(row["IsHot"]) == 0) ? "否" : ((TypeUtil.ObjectToInt(row["IsHot"]) == 1) ? "<span class='hong'>是</span>" : "")),
						MasterName = TypeUtil.GetMasterName(TypeUtil.ObjectToInt(row["UserID"])),
						IssueDate = TypeUtil.ObjectToString(row["IssueDate"]),
						IsDelete = row["IsDelete"]
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((newsList.PageSet != null && newsList.PageSet.Tables != null && newsList.PageSet.Tables[0].Rows.Count != 0) ? newsList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoNewsList()
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
			string sqlQuery = "WHERE NewsID in (" + str + ")";
			try
			{
				FacadeManage.aideNativeWebFacade.DeleteNews(sqlQuery);
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
		public JsonResult JinyongNews()
		{
			int formInt = GameRequest.GetFormInt("id", 0);
			int formInt2 = GameRequest.GetFormInt("value", -1);
			if (formInt > 0 && formInt2 >= 0)
			{
				try
				{
					FacadeManage.aideNativeWebFacade.JinyongNews(formInt, formInt2);
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
		public ActionResult NewsInfo()
		{
			AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
			if (!adminPermission.GetPermission(4L))
			{
				return Redirect("/NoPower/Index");
			}
			string a = TypeUtil.ObjectToString(base.Request["cmd"]);
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			if (a == "add")
			{
				base.ViewBag.Info = "新增";
			}
			else
			{
				base.ViewBag.Info = "更新";
			}
			base.ViewBag.ID = num;
			base.ViewBag.ClassID = "";
			base.ViewBag.Subject = "";
			base.ViewBag.OnTop = false;
			base.ViewBag.OnTopAll = false;
			base.ViewBag.IsElite = false;
			base.ViewBag.IsHot = false;
			base.ViewBag.IsLock = false;
			base.ViewBag.LinkUrl = "";
			base.ViewBag.IsLinks = false;
			base.ViewBag.Body = "";
			if (num > 0)
			{
				News newsInfo = FacadeManage.aideNativeWebFacade.GetNewsInfo(num);
				if (newsInfo != null)
				{
					base.ViewBag.ClassID = newsInfo.ClassID.ToString().Trim();
					base.ViewBag.Subject = newsInfo.Subject;
					base.ViewBag.OnTop = (newsInfo.OnTop == 1);
					base.ViewBag.OnTopAll = (newsInfo.OnTopAll == 1);
					base.ViewBag.IsElite = (newsInfo.IsElite == 1);
					base.ViewBag.IsHot = (newsInfo.IsHot == 1);
					base.ViewBag.IsLock = (newsInfo.IsLock == 1);
					base.ViewBag.LinkUrl = ((newsInfo.LinkUrl == "") ? "http://" : newsInfo.LinkUrl);
					base.ViewBag.IsLinks = (newsInfo.IsLinks == 1);
					base.ViewBag.Body = newsInfo.Body;
				}
			}
			return View();
		}

		[ValidateInput(false)]
		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoNewsInfo(News entity)
		{
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			if (string.IsNullOrEmpty(entity.Body))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入新闻内容"
				});
			}
			Message message = new Message();
			if (entity.NewsID < 1)
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
				entity.UserID = user.UserID;
				entity.IssueIP = Utility.UserIP;
				message = FacadeManage.aideNativeWebFacade.InsertNews(entity);
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
				entity.UserID = user.UserID;
				entity.LastModifyIP = Utility.UserIP;
				message = FacadeManage.aideNativeWebFacade.UpdateNews(entity);
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
		public ActionResult MobileNoticeList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetMobileNoticeList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY IssueDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1 AND ClassID=3 ");
			PagerSet newsList = FacadeManage.aideNativeWebFacade.GetNewsList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (newsList != null && newsList.PageSet != null && newsList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in newsList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						NewsID = TypeUtil.ObjectToString(row["NewsID"]),
						Subject = TypeUtil.ObjectToString(row["Subject"]),
						StatusName = ((TypeUtil.ObjectToInt(row["OnTop"]) == 0) ? "否" : ((TypeUtil.ObjectToInt(row["OnTop"]) == 1) ? "<span style='color:red;'>是</span>" : "")),
						IssueDate = TypeUtil.ObjectToString(row["IssueDate"]),
						IsDelete = row["IsDelete"]
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((newsList.PageSet != null && newsList.PageSet.Tables != null && newsList.PageSet.Tables[0].Rows.Count != 0) ? newsList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoMobileNoticeList()
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
			string sqlQuery = "WHERE NewsID in (" + str + ")";
			try
			{
				FacadeManage.aideNativeWebFacade.DeleteNews(sqlQuery);
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
		public ActionResult MobileNoticeInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			string a = TypeUtil.ObjectToString(base.Request["cmd"]);
			base.ViewBag.ID = num;
			if (a == "add")
			{
				base.ViewBag.Info = "新增";
			}
			else
			{
				base.ViewBag.Info = "更新";
			}
			base.ViewData["Data"] = null;
			News newsInfo = FacadeManage.aideNativeWebFacade.GetNewsInfo(num);
			base.ViewData["Data"] = newsInfo;
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoMobileNoticeInfo(News entity)
		{
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			if (string.IsNullOrEmpty(entity.Body))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入新闻内容"
				});
			}
			Message message = new Message();
			if (entity.NewsID < 1)
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
				entity.UserID = user.UserID;
				entity.IssueIP = Utility.UserIP;
				entity.ClassID = 3;
				message = FacadeManage.aideNativeWebFacade.InsertNews(entity);
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
				entity.NewsID = entity.NewsID;
				entity.LastModifyIP = Utility.UserIP;
				entity.ClassID = 3;
				message = FacadeManage.aideNativeWebFacade.UpdateNews(entity);
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
		public ActionResult RulesList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetRulesList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY KindID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet gameRulesList = FacadeManage.aideNativeWebFacade.GetGameRulesList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (gameRulesList != null && gameRulesList.PageSet != null && gameRulesList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in gameRulesList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						KindID = TypeUtil.ObjectToString(row["KindID"]),
						KindName = TypeUtil.ObjectToString(row["KindName"]),
						CollectDate = TypeUtil.ObjectToString(row["CollectDate"]),
						ModifyDate = TypeUtil.ObjectToString(row["ModifyDate"]),
						NullityStatus = TypeUtil.GetNullityStatus((byte)row["Nullity"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((gameRulesList.PageSet != null && gameRulesList.PageSet.Tables != null && gameRulesList.PageSet.Tables[0].Rows.Count != 0) ? gameRulesList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoRulesList()
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
			string sqlQuery = "WHERE KindID in (" + str + ")";
			try
			{
				FacadeManage.aideNativeWebFacade.DeleteGameRules(sqlQuery);
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
		public ActionResult RulesInfo()
		{
			string a = TypeUtil.ObjectToString(base.Request["cmd"]);
			if (a == "add")
			{
				base.ViewBag.Info = "新增";
			}
			else
			{
				base.ViewBag.Info = "更新";
			}
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			GameRulesInfo gameRulesInfo = FacadeManage.aideNativeWebFacade.GetGameRulesInfo(num);
			base.ViewData["data"] = gameRulesInfo;
			return View();
		}

		[ValidateInput(false)]
		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoRulesInfo(GameRulesInfo entity)
		{
			string a = TypeUtil.ObjectToString(base.Request["OP"]);
			int num = TypeUtil.ObjectToInt(base.Request["ID"]);
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有数据"
				});
			}
			if (string.IsNullOrEmpty(entity.ImgRuleUrl))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请选择一张PC网站效果图！"
				});
			}
			if (string.IsNullOrEmpty(entity.HelpIntro))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入游戏介绍"
				});
			}
			if (string.IsNullOrEmpty(entity.HelpRule))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入游戏规则介绍"
				});
			}
			if (string.IsNullOrEmpty(entity.HelpGrade))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入游戏等级介绍"
				});
			}
			Message message = new Message();
			if (a == "新增")
			{
				if (entity.KindID < 1)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "请选择游戏"
					});
				}
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
				if (FacadeManage.aideNativeWebFacade.JudgeRulesIsExistence(entity.KindID))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "该游戏规则已存在"
					});
				}
				message = FacadeManage.aideNativeWebFacade.InsertGameRules(entity);
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
				if (FacadeManage.aideNativeWebFacade.JudgeRulesIsExistence(entity.KindID) && entity.KindID != num)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "该游戏规则已存在"
					});
				}
				message = FacadeManage.aideNativeWebFacade.UpdateGameRules(entity, num);
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
		public ActionResult SinglePageList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult IssueList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetIssueList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY CardTypeID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet globalLivcardList = FacadeManage.aideTreasureFacade.GetGlobalLivcardList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (globalLivcardList != null && globalLivcardList.PageSet != null && globalLivcardList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in globalLivcardList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CardTypeID = TypeUtil.ObjectToString(row["CardTypeID"]),
						CardName = TypeUtil.ObjectToString(row["CardName"]),
						CardPrice = TypeUtil.ObjectToString(row["CardPrice"]),
						Currency = TypeUtil.ObjectToString(row["Currency"]),
						InputDate = TypeUtil.ObjectToString(row["InputDate"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "成功",
				Total = ((globalLivcardList.PageSet != null && globalLivcardList.PageSet.Tables != null && globalLivcardList.PageSet.Tables[0].Rows.Count != 0) ? globalLivcardList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult IssueInfo()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult FeedbackList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetFeedbackList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY CardTypeID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet globalLivcardList = FacadeManage.aideTreasureFacade.GetGlobalLivcardList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (globalLivcardList != null && globalLivcardList.PageSet != null && globalLivcardList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in globalLivcardList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CardTypeID = TypeUtil.ObjectToString(row["CardTypeID"]),
						CardName = TypeUtil.ObjectToString(row["CardName"]),
						CardPrice = TypeUtil.ObjectToString(row["CardPrice"]),
						Currency = TypeUtil.ObjectToString(row["Currency"]),
						InputDate = TypeUtil.ObjectToString(row["InputDate"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "成功",
				Total = ((globalLivcardList.PageSet != null && globalLivcardList.PageSet.Tables != null && globalLivcardList.PageSet.Tables[0].Rows.Count != 0) ? globalLivcardList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult FeedbackInfo()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult LogoSet()
		{
			base.ViewBag.IntParam = TypeUtil.ObjectToInt(base.Request["param"]);
			return View();
		}

		[CheckCustomer]
		public ActionResult SiteConfig()
		{
			base.ViewData["data"] = null;
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.IntParam = ((num == 0) ? 27 : num);
			base.ViewBag.Title = "网站系统 - " + ((TypeUtil.ObjectToString(base.Request["title"]) == "") ? "站点配置" : TypeUtil.ObjectToString(base.Request["title"]));
			if (base.ViewBag.IntParam > 0)
			{
				ConfigInfo value = (ConfigInfo)FacadeManage.aideNativeWebFacade.GetConfigInfo(base.ViewBag.IntParam);
				base.ViewData["data"] = value;
			}
			return View();
		}

		[CheckCustomer]
		public JsonResult DoLogoSet()
		{
			string value = TypeUtil.ObjectToString(base.Request["fuLogoUrl"]);
			if (string.IsNullOrEmpty(value))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请上传网站前台LOGO"
				});
			}
			string value2 = TypeUtil.ObjectToString(base.Request["fuAdminLogoUrl"]);
			if (string.IsNullOrEmpty(value2))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请上传网站后台LOGO"
				});
			}
			string value3 = TypeUtil.ObjectToString(base.Request["fuMobileLogoUrl"]);
			if (string.IsNullOrEmpty(value3))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请上传移动版网站LOGO"
				});
			}
			string value4 = TypeUtil.ObjectToString(base.Request["fuMobileRegLogoUrl"]);
			if (string.IsNullOrEmpty(value4))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请上传移动版注册网站LOGO"
				});
			}
			return Json(new
			{
				IsOk = true,
				Msg = "操作成功"
			});
		}

		[CheckCustomer]
		public JsonResult DoSiteConfig(ConfigInfo config)
		{
			if (config == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交数据"
				});
			}
			config.ConfigKey = TypeUtil.ObjectToString(config.ConfigKey);
			config.ConfigName = TypeUtil.ObjectToString(config.ConfigName);
			config.ConfigString = TypeUtil.ObjectToString(config.ConfigString);
			config.Field1 = TypeUtil.ObjectToString(config.Field1);
			config.Field2 = TypeUtil.ObjectToString(config.Field2);
			config.Field3 = TypeUtil.ObjectToString(config.Field3);
			config.Field4 = TypeUtil.ObjectToString(config.Field4);
			config.Field5 = TypeUtil.ObjectToString(config.Field5);
			config.Field6 = TypeUtil.ObjectToString(config.Field6);
			config.Field7 = TypeUtil.ObjectToString(config.Field7);
			config.Field8 = TypeUtil.ObjectToString(config.Field8);
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
			if (config.ConfigID > 0)
			{
				FacadeManage.aideNativeWebFacade.UpdateConfigInfo(config);
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "操作失败"
			});
		}

		[CheckCustomer]
		public ActionResult AdsWebHomeList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetAdsWebHomeList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY CardTypeID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet globalLivcardList = FacadeManage.aideTreasureFacade.GetGlobalLivcardList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (globalLivcardList != null && globalLivcardList.PageSet != null && globalLivcardList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in globalLivcardList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CardTypeID = TypeUtil.ObjectToString(row["CardTypeID"]),
						CardName = TypeUtil.ObjectToString(row["CardName"]),
						CardPrice = TypeUtil.ObjectToString(row["CardPrice"]),
						Currency = TypeUtil.ObjectToString(row["Currency"]),
						InputDate = TypeUtil.ObjectToString(row["InputDate"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "成功",
				Total = ((globalLivcardList.PageSet != null && globalLivcardList.PageSet.Tables != null && globalLivcardList.PageSet.Tables[0].Rows.Count != 0) ? globalLivcardList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult AdsWebHomeListInfo()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult AdsGameList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetAdsGameList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY CardTypeID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet globalLivcardList = FacadeManage.aideTreasureFacade.GetGlobalLivcardList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (globalLivcardList != null && globalLivcardList.PageSet != null && globalLivcardList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in globalLivcardList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CardTypeID = TypeUtil.ObjectToString(row["CardTypeID"]),
						CardName = TypeUtil.ObjectToString(row["CardName"]),
						CardPrice = TypeUtil.ObjectToString(row["CardPrice"]),
						Currency = TypeUtil.ObjectToString(row["Currency"]),
						InputDate = TypeUtil.ObjectToString(row["InputDate"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "成功",
				Total = ((globalLivcardList.PageSet != null && globalLivcardList.PageSet.Tables != null && globalLivcardList.PageSet.Tables[0].Rows.Count != 0) ? globalLivcardList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult AdsGameInfo()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult ActivityList()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoActivityList()
		{
			string idList = TypeUtil.ObjectToString(base.Request["cid"]);
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
			try
			{
				FacadeManage.aideNativeWebFacade.DeleteActivity(idList);
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
		public JsonResult GetActivityList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY ActivityID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet list = FacadeManage.aideNativeWebFacade.GetList("Activity", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						ActivityID = TypeUtil.ObjectToString(row["ActivityID"]),
						Title = TypeUtil.ObjectToString(row["Title"]),
						SortID = TypeUtil.ObjectToString(row["SortID"]),
						InputDate = TypeUtil.ObjectToString(row["InputDate"])
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
		public ActionResult ActivityInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			base.ViewData["data"] = null;
			if (num > 0)
			{
				Activity activity = FacadeManage.aideNativeWebFacade.GetActivity(num);
				base.ViewData["data"] = activity;
			}
			return View();
		}

		[CheckCustomer]
		[ValidateInput(false)]
		[AntiSqlInjection]
		public JsonResult DoActivityInfo(Activity entity)
		{
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交数据"
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
			if (!string.IsNullOrEmpty(entity.ImageUrl))
			{
				if (entity.ActivityID >= 1)
				{
					try
					{
						FacadeManage.aideNativeWebFacade.UpdateActivity(entity);
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
					FacadeManage.aideNativeWebFacade.AddActivity(entity);
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
			return Json(new
			{
				IsOk = false,
				Msg = "请上传图片"
			});
		}

		[CheckCustomer]
		public ActionResult IOSConfig()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetIOSConfigs()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY AgentID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			string text = TypeUtil.ObjectToString(base.Request["keyword"]);
			if (!string.IsNullOrEmpty(text))
			{
				if (TypeUtil.ObjectToInt(text) > 0)
				{
					stringBuilder.AppendFormat(" and AgentID={0}", text);
				}
				else
				{
					stringBuilder.AppendFormat(" and AgentAcc='{0}'", text);
				}
			}
			int num = TypeUtil.ObjectToInt(base.Request["stute"]);
			switch (num)
			{
			case 0:
				stringBuilder.AppendFormat(" and IsIOSShop={0}", num);
				break;
			case 1:
				stringBuilder.AppendFormat(" and IsIOSShop={0}", num);
				break;
			}
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("RYAgentDB..T_AgentIsIOS", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult AddIOSConfig()
		{
			string text = TypeUtil.ObjectToString(base.Request["AgentAcc"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "账号不能为空"
				});
			}
			int agentIDByAccounts = FacadeManage.aideAccountsFacade.GetAgentIDByAccounts(text);
			if (agentIDByAccounts < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "该代理账号不存在"
				});
			}
			string text2 = TypeUtil.ObjectToString(base.Request["AgentName"]);
			if (string.IsNullOrEmpty(text2))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "名称不能为空"
				});
			}
			int isIOSShop = TypeUtil.ObjectToInt(base.Request["IsIOSShop"]);
			int versionNo = TypeUtil.ObjectToInt(base.Request["VersionNo"]);
			int srcVersionNo = TypeUtil.ObjectToInt(base.Request["SrcVersionNo"]);
			string appKey = TypeUtil.ObjectToString(base.Request["AppKey"]);
			string appSecret = TypeUtil.ObjectToString(base.Request["AppSecret"]);
			string appUrl = TypeUtil.ObjectToString(base.Request["AppUrl"]);
			if (FacadeManage.aideAccountsFacade.AddIOSConfig(isIOSShop, agentIDByAccounts, text, text2, versionNo, srcVersionNo, appKey, appSecret, appUrl) > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "添加成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "添加失败"
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DelIOSConfig(string AgentID)
		{
			if (string.IsNullOrEmpty(AgentID))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			if (AgentID.Contains(","))
			{
				int result = 0;
				string[] array = AgentID.Split(',');
				bool flag = true;
				string[] array2 = array;
				foreach (string s in array2)
				{
					if (!int.TryParse(s, out result))
					{
						flag = false;
					}
				}
				if (!flag)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "参数错误"
					});
				}
			}
			else
			{
				int result2 = 0;
				if (!int.TryParse(AgentID, out result2))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "参数错误"
					});
				}
			}
			if (FacadeManage.aideAccountsFacade.DelIOSConfig(AgentID))
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
				Msg = "操作失败"
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult UpdateIOSConfig()
		{
			int num = TypeUtil.ObjectToInt(base.Request["AgentID"]);
			int isIOSShop = TypeUtil.ObjectToInt(base.Request["IsIOSShop"]);
			int versionNo = TypeUtil.ObjectToInt(base.Request["VersionNo"]);
			int srcVersionNo = TypeUtil.ObjectToInt(base.Request["SrcVersionNo"]);
			string text = TypeUtil.ObjectToString(base.Request["AgentName"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "名称不能为空"
				});
			}
			if (num < 1)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			string appKey = TypeUtil.ObjectToString(base.Request["AppKey"]);
			string appSecret = TypeUtil.ObjectToString(base.Request["AppSecret"]);
			string appUrl = TypeUtil.ObjectToString(base.Request["AppUrl"]);
			if (FacadeManage.aideAccountsFacade.UpdateIOSConfig(isIOSShop, num, versionNo, srcVersionNo, text, appKey, appSecret, appUrl) > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "更新成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "更新失败"
			});
		}
	}
}
