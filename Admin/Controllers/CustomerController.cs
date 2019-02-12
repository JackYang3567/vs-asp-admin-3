using Admin.Filters;
using Game.Entity.NativeWeb;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using System;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class CustomerController : BaseController
	{
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult QuestionList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetQuestionList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY Hot DESC,IssueID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet list = FacadeManage.aideNativeWebFacade.GetList("View_GameIssueInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public ActionResult QuestionInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			GameIssueInfo gameIssueInfo = new GameIssueInfo();
			if (num > 0)
			{
				gameIssueInfo = FacadeManage.aideNativeWebFacade.GetGameIssueInfo(num);
				if (gameIssueInfo == null)
				{
					return Redirect("/Customer/QuestionList");
				}
			}
			return View(gameIssueInfo);
		}

		[CheckCustomer]
		[AntiSqlInjection]
		[ValidateInput(false)]
		public JsonResult DoQuestionInfo(GameIssueInfo entity)
		{
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			if (string.IsNullOrEmpty(entity.IssueTitle))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入问题标题"
				});
			}
			if (string.IsNullOrEmpty(entity.IssueContent))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入问题内容"
				});
			}
			Message message = new Message();
			if (entity.IssueID < 1)
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
				message = (message = FacadeManage.aideNativeWebFacade.InsertGameIssue(entity));
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
				message = FacadeManage.aideNativeWebFacade.UpdateGameIssue(entity);
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

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DelQuestion()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					string sqlQuery = "WHERE IssueID in (" + text + ")";
					FacadeManage.aideNativeWebFacade.DeleteGameIssue(sqlQuery);
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
				Msg = "没有选择要操作的项"
			});
		}

		[CheckCustomer]
		public string GetQuestionType()
		{
			int num = TypeUtil.ObjectToInt(base.Request["type"]);
			string tabName = "GameIssueInfo";
			if (num == 1)
			{
				tabName = "GameIssueInfo";
			}
			if (num == 2)
			{
				tabName = "GameFeedbackInfo";
			}
			if (num == 3)
			{
				tabName = "GameAccuseInfo";
			}
			DataTable type = FacadeManage.aideTreasureFacade.GetType(tabName);
			return JsonHelper.SerializeObject(type);
		}

		[CheckCustomer]
		public ActionResult QuestionType()
		{
			int num = TypeUtil.ObjectToInt(base.Request["type"]);
			string tabName = "GameIssueInfo";
			if (num == 1)
			{
				tabName = "GameIssueInfo";
			}
			if (num == 2)
			{
				tabName = "GameFeedbackInfo";
			}
			if (num == 3)
			{
				tabName = "GameAccuseInfo";
			}
			return View(FacadeManage.aideTreasureFacade.GetType(tabName));
		}

		[CheckCustomer]
		public ActionResult QuestionTypeInfo()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoQuestionTypeInfo(RecordType entity)
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
			if (entity == null || entity.ID <= 0 || entity.TypeName == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交数据"
				});
			}
			entity.TabName = "GameIssueInfo";
			if (entity.TypeID == 1)
			{
				entity.TabName = "GameIssueInfo";
			}
			if (entity.TypeID == 2)
			{
				entity.TabName = "GameFeedbackInfo";
			}
			if (entity.TypeID == 3)
			{
				entity.TabName = "GameAccuseInfo";
			}
			entity.Memo = "RYNativeWebDB";
			if (FacadeManage.aideTreasureFacade.EditRecordType(entity) > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "操作成功"
				});
			}
			if (entity.Mode == "add")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "添加失败，请检查ID是否存在"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "操作失败"
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DelQuestionType()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					int num = TypeUtil.ObjectToInt(base.Request["TypeID"]);
					string text2 = "GameIssueInfo";
					if (num == 1)
					{
						text2 = "GameIssueInfo";
					}
					if (num == 2)
					{
						text2 = "GameFeedbackInfo";
					}
					if (num == 3)
					{
						text2 = "GameAccuseInfo";
					}
					string sWhere = "WHERE TabName='" + text2 + "' AND ID in (" + text + ")";
					FacadeManage.aideTreasureFacade.DelRecordType(sWhere);
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
				Msg = "没有选择要操作的项"
			});
		}

		[CheckCustomer]
		public ActionResult ReportList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult ReportInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			GameAccuseInfo gameAccuseInfo = new GameAccuseInfo();
			if (num > 0)
			{
				gameAccuseInfo = FacadeManage.aideNativeWebFacade.GetGameAccuseInfo(num);
				if (gameAccuseInfo == null)
				{
					return Redirect("/Customer/ReportList");
				}
			}
			return View(gameAccuseInfo);
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetReportList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["Status"]);
			string text = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			string orderby = "ORDER BY AccuseID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (text != "")
			{
				switch (num)
				{
				case 1:
					stringBuilder.AppendFormat(" AND Accounts='{0}'", text);
					break;
				case 2:
					if (!TypeUtil.IsInteger(text))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "举报人ID错误"
						});
					}
					stringBuilder.AppendFormat(" AND GameID={0}", text);
					break;
				case 3:
					if (!TypeUtil.IsInteger(text))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "被举报ID错误"
						});
					}
					stringBuilder.AppendFormat(" AND TarGameID={0}", text);
					break;
				}
			}
			if (num2 == 1)
			{
				stringBuilder.Append(" AND Dealer<>''");
			}
			if (num2 == 2)
			{
				stringBuilder.Append(" AND Dealer=''");
			}
			PagerSet list = FacadeManage.aideNativeWebFacade.GetList("View_GameAccuseInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
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
		public JsonResult DelReport()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					string sWhere = "WHERE  AccuseID in (" + text + ")";
					FacadeManage.aideNativeWebFacade.DelReport(sWhere);
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
				Msg = "没有选择要操作的项"
			});
		}

		[ValidateInput(false)]
		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoReport()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			string text = TypeUtil.ObjectToString(base.Request["body"]);
			if (num == 0 || text == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
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
			int num2 = FacadeManage.aideNativeWebFacade.UpdateReport(num, text, user.Username);
			if (num2 > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "回复成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "操作失败"
			});
		}

		[CheckCustomer]
		public ActionResult MessageList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult MessageInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			GameFeedbackInfo gameFeedbackInfo = new GameFeedbackInfo();
			if (num > 0)
			{
				gameFeedbackInfo = FacadeManage.aideNativeWebFacade.GetGameFeedbackInfo(num);
				if (gameFeedbackInfo == null)
				{
					return Redirect("/Customer/MessageList");
				}
			}
			return View(gameFeedbackInfo);
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetMessageList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["Status"]);
			string text = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			string orderby = "ORDER BY FeedbackID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (text != "")
			{
				switch (num)
				{
				case 1:
					stringBuilder.AppendFormat(" AND Accounts='{0}'", text);
					break;
				case 2:
					if (!TypeUtil.IsInteger(text))
					{
						return Json(new
						{
							IsOk = false,
							Msg = "留言ID错误"
						});
					}
					stringBuilder.AppendFormat(" AND GameID={0}", text);
					break;
				case 3:
					stringBuilder.AppendFormat(" AND FeedbackTitle='{0}'", text);
					break;
				}
			}
			if (num2 == 1)
			{
				stringBuilder.Append(" AND Username<>''");
			}
			if (num2 == 2)
			{
				stringBuilder.Append(" AND Username=''");
			}
			PagerSet list = FacadeManage.aideNativeWebFacade.GetList("View_GameFeedbackInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
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
		public JsonResult DelMessage()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					string sqlQuery = "WHERE  FeedbackID in (" + text + ")";
					FacadeManage.aideNativeWebFacade.DeleteGameFeedback(sqlQuery);
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
				Msg = "没有选择要操作的项"
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		[ValidateInput(false)]
		public JsonResult DoMessage()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			string text = TypeUtil.ObjectToString(base.Request["body"]);
			if (num == 0 || text == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
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
			GameFeedbackInfo gameFeedbackInfo = new GameFeedbackInfo();
			gameFeedbackInfo.RevertContent = text;
			gameFeedbackInfo.RevertUserID = user.UserID;
			gameFeedbackInfo.RevertDate = DateTime.Now;
			gameFeedbackInfo.Nullity = 0;
			gameFeedbackInfo.IsProcessed = 1;
			gameFeedbackInfo.FeedbackID = num;
			Message message = FacadeManage.aideNativeWebFacade.RevertGameFeedback(gameFeedbackInfo);
			if (message.Success)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "回复成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "操作失败"
			});
		}
	}
}
