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
	public class MallController : BaseController
	{
		public ActionResult Index()
		{
			return View();
		}

		public JsonResult GetMallTypeList()
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

		public ActionResult MallTypeInfo()
		{
			return View();
		}

		public ActionResult MallInfo()
		{
			return View();
		}

		public ActionResult MallInfoList()
		{
			return View();
		}

		public JsonResult GetMallInfoList()
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

		public ActionResult MallOrderList()
		{
			return View();
		}

		public JsonResult GetMallOrderList()
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

		public ActionResult MallOrder()
		{
			return View();
		}
	}
}
