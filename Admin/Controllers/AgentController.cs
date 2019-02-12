using Admin.Filters;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class AgentController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetAgentList()
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
		public ActionResult AgentInfo()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult AgentManager()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult AgentChildList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetAgentChildList()
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
		public ActionResult ChildRevenueInfo()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetChildRevenueInfoList()
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
		public ActionResult ChildPayInfo()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetChildPayInfoList()
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
		public ActionResult AgentPayList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetAgentPayList()
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
		public ActionResult AgentRevenueList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetAgentRevenueList()
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
		public ActionResult AgentPayBackList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetAgentPayBackList()
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
		public ActionResult StatAgentInfo()
		{
			return View();
		}
	}
}
