using Admin.Filters;
using Game.Entity.Platform;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class TaskController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetTaskList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY TaskID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet list = FacadeManage.aidePlatformFacade.GetList("TaskInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						TaskID = TypeUtil.ObjectToString(row["TaskID"]),
						TaskName = TypeUtil.ObjectToString(row["TaskName"]),
						Name = Enum.GetName(typeof(EnumerationList.TaskType), row["TaskType"]),
						GameKindName = TypeUtil.GetGameKindName(TypeUtil.ObjectToInt(row["KindID"])),
						TimeLimit = TypeUtil.ObjectToString(row["TimeLimit"]),
						InputDate = TypeUtil.ObjectToString(row["InputDate"]),
						Nullity = row["Nullity"]
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
		public JsonResult JinyongTask()
		{
			int formInt = GameRequest.GetFormInt("id", 0);
			int formInt2 = GameRequest.GetFormInt("value", -1);
			if (formInt > 0 && formInt2 >= 0)
			{
				try
				{
					FacadeManage.aidePlatformFacade.JinyongTask(formInt, formInt2);
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

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DelTask()
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
			string sqlQuery = "WHERE TaskID IN (" + str + ")";
			try
			{
				FacadeManage.aidePlatformFacade.DeleteTaskInfo(sqlQuery);
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
		public ActionResult TaskInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.ID = num;
			base.ViewData["data"] = null;
			TaskInfo taskInfoByID = FacadeManage.aidePlatformFacade.GetTaskInfoByID(num);
			if (taskInfoByID != null)
			{
				base.ViewData["data"] = taskInfoByID;
			}
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoTaskInfo(TaskInfo entity)
		{
			if (entity != null)
			{
				if (entity.TaskType < 1)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "请选择任务类型"
					});
				}
				if (entity.UserType < 1)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "请选择可领取任务玩家类型"
					});
				}
				if (entity.KindID < 0)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "请选择任务所属游戏"
					});
				}
				if (entity.Innings < 1)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "请输入比赛局数"
					});
				}
				if (string.IsNullOrEmpty(entity.TaskDescription))
				{
					entity.TaskDescription = "";
				}
				bool flag;
				if (entity.TaskID > 0)
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
					flag = FacadeManage.aidePlatformFacade.UpdateTaskInfo(entity);
				}
				else
				{
					if (user != null)
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
					}
					entity.InputDate = DateTime.Now;
					flag = FacadeManage.aidePlatformFacade.InsertTaskInfo(entity);
				}
				if (flag)
				{
					return Json(new
					{
						IsOk = true,
						Msg = "保存任务信息成功"
					});
				}
				return Json(new
				{
					IsOk = false,
					Msg = "保存任务信息失败"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "没有提交数据"
			});
		}

		[CheckCustomer]
		public ActionResult TaskRecordList()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetTaskRecordList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			string text = TypeUtil.ObjectToString(base.Request["User"]);
			string s = TypeUtil.ObjectToString(base.Request["Date"]);
			string orderby = "ORDER BY InputDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (!string.IsNullOrEmpty(text))
			{
				switch (num)
				{
				case 0:
					stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByAccount(text));
					break;
				case 1:
					if (TypeUtil.ObjectToInt(text) > 0)
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.GetUserIDByGameID(TypeUtil.ObjectToInt(text)));
					}
					break;
				case 2:
					if (TypeUtil.ObjectToInt(text) > 0)
					{
						stringBuilder.AppendFormat(" AND UserID={0}", TypeUtil.ObjectToInt(text));
					}
					break;
				}
			}
			DateTime result = DateTime.Now;
			if (DateTime.TryParse(s, out result))
			{
				string dateID = Fetch.GetDateID(result);
				stringBuilder.AppendFormat(" AND DateID={0}", dateID);
			}
			PagerSet list = FacadeManage.aideRecordFacade.GetList("RecordTask", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						RecordID = TypeUtil.ObjectToString(row["RecordID"]),
						UserID = TypeUtil.ObjectToString(row["UserID"]),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["UserID"])),
						TaskName = TypeUtil.GetTaskName(TypeUtil.ObjectToInt(row["TaskID"])),
						AwardGold = TypeUtil.ObjectToDecimal(row["AwardGold"]).ToString("N"),
						AwardMedal = TypeUtil.ObjectToString(row["AwardMedal"]),
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
		public JsonResult ClearRecord()
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
			string sqlQuery = "WHERE InputDate <= '" + Fetch.GetEndTime(DateTime.Now.AddMonths(-1)) + "'";
			try
			{
				FacadeManage.aideRecordFacade.DeleteTaskRecord(sqlQuery);
				return Json(new
				{
					IsOk = true,
					Msg = "清除成功"
				});
			}
			catch
			{
				return Json(new
				{
					IsOk = false,
					Msg = "清除失败"
				});
			}
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DelRecord()
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
			string sqlQuery = "WHERE RecordID IN (" + str + ")";
			try
			{
				FacadeManage.aideRecordFacade.DeleteTaskRecord(sqlQuery);
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
	}
}
