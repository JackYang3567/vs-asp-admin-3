using Admin.Filters;
using Game.Entity.GameMatch;
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
	public class MatchController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetMatchConfigList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY MatchID DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			PagerSet list = FacadeManage.aideGameMatchFacade.GetList("MatchInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list2 = new List<object>();
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					list2.Add(new
					{
						MatchID = TypeUtil.ObjectToString(row["MatchID"]),
						MatchName = TypeUtil.ObjectToString(row["MatchName"]),
						MatchDate = TypeUtil.ObjectToString(row["MatchDate"]),
						MatchTypeName = TypeUtil.GetMatchTypeName(TypeUtil.ObjectToInt(row["MatchID"])),
						MatchStatusName = TypeUtil.GetMatchStatusName(TypeUtil.ObjectToInt(row["MatchID"])),
						SortID = TypeUtil.ObjectToString(row["SortID"]),
						NullityStatus = TypeUtil.GetNullityStatus((byte)TypeUtil.ObjectToInt(row["Nullity"])),
						CollectDate = TypeUtil.ObjectToString(row["CollectDate"])
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
		[AntiSqlInjection]
		public JsonResult UnAbleMatchConfig()
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
			string str = TypeUtil.ObjectToString(base.Request["cid"]);
			string sql = "Update MatchInfo Set Nullity = 1 WHERE MatchID in (" + str + ") and Nullity=0";
			int num = FacadeManage.aideGameMatchFacade.ExecuteSql(sql);
			if (num > 0)
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

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult AbleMatchConfig()
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
			string str = TypeUtil.ObjectToString(base.Request["cid"]);
			string sql = "Update MatchInfo Set Nullity = 0 WHERE MatchID in (" + str + ") and Nullity = 1";
			int num = FacadeManage.aideGameMatchFacade.ExecuteSql(sql);
			if (num > 0)
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

		[CheckCustomer]
		public ActionResult MatchConfigInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["cmd"]);
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.OP = text;
			base.ViewBag.ID = num;
			string text2 = "";
			string empty = string.Empty;
			empty += "<div class=\"ui-reward-item\">";
			empty += "第<span class=\"ui-item-serial\">{0}</span>名：";
			empty += "金币：<input type=\"text\" class=\"text wd2\" name=\"gold\" value=\"{1}\" readonly> ";
			empty += "</div>";
			DataSet matchRewardList = FacadeManage.aideGameMatchFacade.GetMatchRewardList(num);
			if (matchRewardList.Tables[0].Rows.Count > 0)
			{
				text2 = string.Empty;
				for (int i = 0; i < matchRewardList.Tables[0].Rows.Count; i++)
				{
					DataRow dataRow = matchRewardList.Tables[0].Rows[i];
					text2 += string.Format(empty, i + 1, dataRow["RewardGold"], dataRow["RewardIngot"], dataRow["RewardExperience"]);
				}
			}
			base.ViewBag.strReward = text2;
			base.ViewBag.MatchName = "";
			base.ViewBag.MatchTypeName = "";
			base.ViewBag.MatchStatusName = "";
			base.ViewBag.KindName = "";
			base.ViewBag.MatchSummary = "";
			base.ViewBag.MatchDate = "";
			base.ViewBag.SortID = "0";
			base.ViewBag.MatchImage = "";
			base.ViewBag.Content = "";
			MatchInfo matchInfo = FacadeManage.aideGameMatchFacade.GetMatchInfo(num);
			if (matchInfo != null)
			{
				base.ViewBag.MatchSummary = matchInfo.MatchSummary;
				base.ViewBag.MatchDate = matchInfo.MatchDate;
				base.ViewBag.SortID = matchInfo.SortID.ToString();
				base.ViewBag.MatchImage = matchInfo.MatchImage;
				base.ViewBag.Content = matchInfo.MatchContent;
			}
			MatchPublic matchPublicInfo = FacadeManage.aideGameMatchFacade.GetMatchPublicInfo(num);
			if (matchPublicInfo != null)
			{
				base.ViewBag.MatchName = matchPublicInfo.MatchName;
				base.ViewBag.MatchTypeName = ((matchPublicInfo.MatchType == 0) ? "定时赛" : ((matchPublicInfo.MatchType == 1) ? "即时赛" : "未知"));
				base.ViewBag.MatchStatusName = ((matchPublicInfo.MatchStatus == 0) ? "<span style='color:red;'>未开始</span>" : ((matchPublicInfo.MatchStatus == 2) ? "<span style='color:blue;'>进行中</span>" : ((matchPublicInfo.MatchStatus == 8) ? "<span style='color:green;'>已结束</span>" : "未知")));
				base.ViewBag.KindName = TypeUtil.GetGameKindName(matchPublicInfo.KindID);
			}
			base.ViewData["MatchInfo"] = matchInfo;
			base.ViewData["MatchPublic"] = matchPublicInfo;
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoMatchConfigInfo(MatchInfo model)
		{
			if (model == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交任何数据"
				});
			}
			if (string.IsNullOrEmpty(model.MatchImage))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "操作失败，请上传展示小图"
				});
			}
			if (string.IsNullOrEmpty(model.MatchContent))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "操作失败，请输入比赛描述"
				});
			}
			Message message = FacadeManage.aideGameMatchFacade.UpdateMatchInfo(model);
			if (message.Success)
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
		public ActionResult MatchRank()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetMatchRankList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["MatchType"]);
			string text = TypeUtil.ObjectToString(base.Request["MatchName"]);
			string orderby = "ORDER BY MatchStartTime DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (string.IsNullOrEmpty(text) && text != "0")
			{
				stringBuilder.AppendFormat(" AND MatchName='{0}'", text);
			}
			PagerSet timingMatchHistoryGroup = FacadeManage.aideGameMatchFacade.GetTimingMatchHistoryGroup((byte)num, pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (timingMatchHistoryGroup != null && timingMatchHistoryGroup.PageSet != null && timingMatchHistoryGroup.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in timingMatchHistoryGroup.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						MatchID = TypeUtil.ObjectToString(row["MatchID"]),
						ServerID = TypeUtil.ObjectToString(row["ServerID"]),
						MatchNo = TypeUtil.ObjectToString(row["MatchNo"]),
						MatchName = TypeUtil.ObjectToString(row["MatchName"]),
						ServerName = TypeUtil.ObjectToString(row["ServerName"]),
						Date = TypeUtil.ObjectToDateTime(row["MatchStartTime"]).ToString("yyyy-MM-dd hh:mm:ss") + "至" + TypeUtil.ObjectToDateTime(row["MatchEndTime"]).ToString("yyyy-MM-dd hh:mm:ss"),
						MatchType = num
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((timingMatchHistoryGroup.PageSet != null && timingMatchHistoryGroup.PageSet.Tables != null && timingMatchHistoryGroup.PageSet.Tables[0].Rows.Count != 0) ? timingMatchHistoryGroup.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult GetMatchNameData()
		{
			int num = TypeUtil.ObjectToInt(base.Request["MatchType"]);
			DataSet dataSet = null;
			if (num > -1)
			{
				dataSet = FacadeManage.aideGameMatchFacade.GetMatchListByMatchType((byte)num);
			}
			List<object> list = new List<object>();
			list.Add(new
			{
				Text = "全部",
				Value = "0"
			});
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					list.Add(new
					{
						Text = TypeUtil.ObjectToString(row["MatchName"]),
						Value = TypeUtil.ObjectToString(row["MatchName"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = 0,
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult MatchRankInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["matchtype"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["serverid"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["matchid"]);
			long num4 = TypeUtil.ObjectToLong(base.Request["matchno"]);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(" WHERE MatchType={0} AND ServerID={1} AND MatchID={2} AND MatchNo={3}", num, num2, num3, num4);
			PagerSet list = FacadeManage.aideGameMatchFacade.GetList("StreamMatchHistory", 1, 1000, stringBuilder.ToString(), "ORDER BY RankID ASC");
			base.ViewData["data"] = null;
			if (list != null && list.PageSet != null && list.PageSet.Tables.Count > 0)
			{
				base.ViewData["data"] = list.PageSet.Tables[0];
			}
			return View();
		}
	}
}
