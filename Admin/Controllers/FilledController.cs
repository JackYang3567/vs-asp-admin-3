using Admin.Filters;
using Admin.Models;
using Game.Entity.Accounts;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using Game.Utils.Cache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class FilledController : BaseController
	{
		[CheckCustomer]
		public ActionResult Index()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetLivcardBuildStreamList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY BuildDate DESC";
			int num = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			switch (num)
			{
			case 1:
			{
				string safeSQL = FiltUtil.GetSafeSQL(TypeUtil.ObjectToString(base.Request["SaleSperson"]));
				if (!string.IsNullOrEmpty(safeSQL))
				{
					stringBuilder.AppendFormat(" AND BuildID IN (SELECT BuildID FROM THTreasureDB.dbo.LivcardAssociator WHERE SalesPerson='{0}' GROUP BY BUILDID)", safeSQL);
				}
				else
				{
					stringBuilder.Append(" AND 1=3 ");
				}
				break;
			}
			case 3:
			{
				DateTime result = DateTime.Now;
				DateTime result2 = DateTime.Now;
				if (DateTime.TryParse(TypeUtil.ObjectToString(base.Request["StartDate"]), out result) && DateTime.TryParse(TypeUtil.ObjectToString(base.Request["EndDate"]), out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND BuildDate >= '{0}' AND BuildDate < '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			}
			}
			List<object> list = new List<object>();
			PagerSet livcardBuildStreamList = FacadeManage.aideTreasureFacade.GetLivcardBuildStreamList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			if (livcardBuildStreamList != null && livcardBuildStreamList.PageSet != null && livcardBuildStreamList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in livcardBuildStreamList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						BuildID = TypeUtil.ObjectToString(row["BuildID"]),
						BuildDate = TypeUtil.ObjectToDateTime(row["BuildDate"]).ToString("yyyy-MM-dd"),
						AdminName = TypeUtil.ObjectToString(row["AdminName"]),
						Salesperson = TypeUtil.GetSalesperson(TypeUtil.ObjectToInt(row["BuildID"])),
						CardTypeName = TypeUtil.GetCardTypeName(TypeUtil.ObjectToInt(row["CardTypeID"])),
						BuildCount = TypeUtil.ObjectToString(row["BuildCount"]),
						CardPrice = TypeUtil.ObjectToString(row["CardPrice"]),
						TotalPrice = (decimal)TypeUtil.ObjectToInt(row["BuildCount"]) * TypeUtil.ObjectToDecimal(row["CardPrice"]),
						Currency = TypeUtil.ObjectToString(row["Currency"]),
						BuildAddr = TypeUtil.ObjectToString(row["BuildAddr"]),
						DownLoadCount = TypeUtil.ObjectToString(row["DownLoadCount"]),
						NoteInfo = TypeUtil.ObjectToString(row["NoteInfo"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((livcardBuildStreamList.PageSet != null && livcardBuildStreamList.PageSet.Tables != null && livcardBuildStreamList.PageSet.Tables[0].Rows.Count != 0) ? livcardBuildStreamList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public JsonResult Export()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(4096L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			int num = TypeUtil.ObjectToInt(base.Request["BuildID"]);
			if (num > 0)
			{
				FacadeManage.aideTreasureFacade.UpdateLivcardBuildStream(num);
				LivcardBuildStream livcardBuildStreamInfo = FacadeManage.aideTreasureFacade.GetLivcardBuildStreamInfo(num);
				if (livcardBuildStreamInfo == null)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "实卡类型不存在"
					});
				}
				if (livcardBuildStreamInfo.BuildCardPacket.Length > 0)
				{
					byte[] buildCardPacket = livcardBuildStreamInfo.BuildCardPacket;
					string @string = Encoding.Default.GetString(buildCardPacket);
					if (@string.IndexOf("/") != -1)
					{
						@string = @string.Replace("/", "\r\n");
						@string = @string.Replace(",", ",\t");
						@string = "第" + livcardBuildStreamInfo.BuildID + "批次 (" + TypeUtil.GetCardTypeName(livcardBuildStreamInfo.CardTypeID) + ") " + livcardBuildStreamInfo.BuildCount + " 张\r\n卡号,\t卡密\r\n" + @string;
						buildCardPacket = Encoding.Default.GetBytes(@string);
					}
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
			return Json(new
			{
				IsOk = false,
				Msg = "参数有误"
			});
		}

		[CheckCustomer]
		public ActionResult LivcardCreate()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult ddlCardTypeSelected()
		{
			int num = TypeUtil.ObjectToInt(base.Request["cardTypeID"]);
			string cardPrice = "";
			string currency = "";
			if (num > 0)
			{
				GlobalLivcard globalLivcardInfo = FacadeManage.aideTreasureFacade.GetGlobalLivcardInfo(num);
				if (globalLivcardInfo != null)
				{
					cardPrice = globalLivcardInfo.CardPrice.ToString();
					currency = globalLivcardInfo.Currency.ToString();
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Data = JsonConvert.SerializeObject(new
				{
					CardPrice = cardPrice,
					Currency = currency
				})
			});
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult CreatCard()
		{
			if (user != null)
			{
				AdminPermission adminPermission = new AdminPermission(user, user.MoudleID);
				if (!adminPermission.GetPermission(2048L))
				{
					return Json(new
					{
						IsOk = false,
						Msg = "没有权限",
						Url = "/NoPower/Index"
					});
				}
			}
			int num = TypeUtil.ObjectToInt(base.Request["cardCount"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["cardTypeID"]);
			if (num2 <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请先添加实卡类型"
				});
			}
			if (num <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "生成实卡的数量必须大于零的整数"
				});
			}
			if (num > 10000)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "生成实卡的数量一次最多10000张"
				});
			}
			int num3 = TypeUtil.ObjectToInt(base.Request["cardLen"]);
			if (num3 < 15 || num3 > 32)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "卡号长度必须大于等于15小于32"
				});
			}
			int num4 = TypeUtil.ObjectToInt(base.Request["passwordLen"]);
			if (num4 < 8 || num4 > 32)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "密码长度必须大于等于8小于33"
				});
			}
			GlobalLivcard globalLivcardInfo = FacadeManage.aideTreasureFacade.GetGlobalLivcardInfo(num2);
			if (globalLivcardInfo == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "实卡类型不存在"
				});
			}
			byte[] bytes = Encoding.Default.GetBytes("");
			LivcardBuildStream livcardBuildStream = new LivcardBuildStream();
			livcardBuildStream.AdminName = user.Username;
			livcardBuildStream.BuildAddr = GameRequest.GetUserIP();
			livcardBuildStream.BuildCardPacket = bytes;
			livcardBuildStream.BuildCount = num;
			livcardBuildStream.BuildDate = DateTime.Now;
			livcardBuildStream.Currency = TypeUtil.StringToDecimal(base.Request["Currency"]);
			livcardBuildStream.CardPrice = globalLivcardInfo.CardPrice;
			livcardBuildStream.CardTypeID = num2;
			livcardBuildStream.DownLoadCount = 0;
			livcardBuildStream.NoteInfo = TypeUtil.ObjectToString(base.Request["Remark"]);
			livcardBuildStream.Gold = globalLivcardInfo.Gold;
			int num5 = FacadeManage.aideTreasureFacade.InsertLivcardBuildStream(livcardBuildStream);
			if (num5 > 0)
			{
				LivcardAssociator livcardAssociator = new LivcardAssociator();
				livcardAssociator.BuildID = num5;
				livcardAssociator.CardTypeID = globalLivcardInfo.CardTypeID;
				livcardAssociator.CardPrice = globalLivcardInfo.CardPrice;
				livcardAssociator.Currency = TypeUtil.StringToDecimal(base.Request["Currency"]);
				livcardAssociator.UseRange = TypeUtil.ObjectToInt(base.Request["UseRange"]);
				livcardAssociator.SalesPerson = TypeUtil.ObjectToString(base.Request["SalesPerson"]);
				livcardAssociator.ValidDate = TypeUtil.StringToDateTime(base.Request["ValidDate"]);
				livcardAssociator.Gold = globalLivcardInfo.Gold;
				StringBuilder stringBuilder = new StringBuilder();
				string[,] array = new string[num, 2];
				int num6 = 0;
				Random rand = new Random();
				while (num > 0)
				{
					string serialID = TypeUtil.GetSerialID(num3, globalLivcardInfo.CardTypeID, rand, TypeUtil.ObjectToString(base.Request["Initial"]));
					bool flag = false;
					for (int num7 = num6; num7 > 0; num7--)
					{
						if (array[num7, 0] == serialID)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						flag = false;
					}
					else
					{
						string password = TypeUtil.GetPassword(num4, rand, TypeUtil.ObjectToBool(base.Request["Digit"]), TypeUtil.ObjectToBool(base.Request["Lower"]), TypeUtil.ObjectToBool(base.Request["Upper"]));
						stringBuilder.AppendFormat("{0},{1}/", serialID, password);
						array[num6, 0] = serialID;
						string[,] array2 = array;
						int num8 = num6;
						string text = Utility.MD5(password);
						array2[num8, 1] = text;
						num--;
						num6++;
					}
				}
				FacadeManage.aideTreasureFacade.InsertLivcardAssociator(livcardAssociator, array);
				bytes = Encoding.Default.GetBytes(stringBuilder.ToString());
				livcardBuildStream.BuildID = num5;
				livcardBuildStream.BuildCardPacket = bytes;
				try
				{
					FacadeManage.aideTreasureFacade.UpdateLivcardBuildStream(livcardBuildStream);
					return Json(new
					{
						IsOk = true,
						Msg = "生成会员卡成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "生成会员卡失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "添加实卡批次失败"
			});
		}

		[CheckCustomer]
		public ActionResult LivcardStat()
		{
			DataSet livcardStat = FacadeManage.aideTreasureFacade.GetLivcardStat();
			base.ViewData["data"] = null;
			if (livcardStat != null && livcardStat.Tables.Count > 0)
			{
				base.ViewData["data"] = livcardStat.Tables[0];
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult GlobalLivcardList()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DelLivcard()
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
			string sql = "Delete GlobalLivcard WHERE CardTypeID in (" + str + ")";
			int num = FacadeManage.aideTreasureFacade.ExecuteSql(sql);
			if (num > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "删除成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "删除失败"
			});
		}

		[CheckCustomer]
		public JsonResult GetGlobalLivcardList()
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
				Msg = "",
				Total = ((globalLivcardList.PageSet != null && globalLivcardList.PageSet.Tables != null && globalLivcardList.PageSet.Tables[0].Rows.Count != 0) ? globalLivcardList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult GlobalLivcardInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.CardTypeID = num;
			GlobalLivcard globalLivcardInfo = FacadeManage.aideTreasureFacade.GetGlobalLivcardInfo(num);
			if (globalLivcardInfo != null)
			{
				base.ViewBag.CardName = globalLivcardInfo.CardName;
				base.ViewBag.CardPrice = globalLivcardInfo.CardPrice.ToString();
				base.ViewBag.Currency = globalLivcardInfo.Currency.ToString();
				base.ViewBag.Gold = globalLivcardInfo.Gold.ToString();
			}
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult AddGlobalLivcardInfo()
		{
			string cardName = TypeUtil.ObjectToString(base.Request["CardName"]);
			decimal cardPrice = TypeUtil.ObjectToDecimal(base.Request["CardPrice"]);
			decimal currency = TypeUtil.ObjectToDecimal(base.Request["Currency"]);
			int num = TypeUtil.ObjectToInt(base.Request["CardTypeID"]);
			int gold = TypeUtil.ObjectToInt(base.Request["Gold"]);
			GlobalLivcard globalLivcard = (num > 0) ? FacadeManage.aideTreasureFacade.GetGlobalLivcardInfo(num) : new GlobalLivcard();
			globalLivcard.CardName = cardName;
			globalLivcard.CardPrice = cardPrice;
			globalLivcard.Currency = currency;
			globalLivcard.Gold = gold;
			if (num <= 0)
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
				FacadeManage.aideTreasureFacade.InsertGlobalLivcard(globalLivcard);
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
			FacadeManage.aideTreasureFacade.UpdateGlobalLivcard(globalLivcard);
			return Json(new
			{
				IsOk = true,
				Msg = "更新成功"
			});
		}

		[CheckCustomer]
		public ActionResult LivcardAssociatorListByStat()
		{
			int num = TypeUtil.ObjectToInt(base.Request["cmd"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.cmd = num;
			base.ViewBag.param = num2;
			return View();
		}

		[CheckCustomer]
		public JsonResult GetLivcardAssociatorList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY CardID ASC";
			int num = TypeUtil.ObjectToInt(base.Request["BuildID"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["OpType"]);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num != 0)
			{
				stringBuilder.AppendFormat(" AND BuildID={0}", num);
			}
			switch (num2)
			{
			case 1:
				stringBuilder.Append(" AND ApplyDate IS NOT NULL ");
				break;
			case 2:
				stringBuilder.Append(" AND ApplyDate IS NULL ");
				break;
			case 3:
				stringBuilder.Append(" AND Nullity=1 ");
				break;
			case 4:
				stringBuilder.Append(" AND Nullity=0 ");
				break;
			}
			PagerSet livcardAssociatorList = FacadeManage.aideTreasureFacade.GetLivcardAssociatorList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (livcardAssociatorList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in livcardAssociatorList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CardID = TypeUtil.ObjectToString(row["CardID"]),
						SerialID = TypeUtil.ObjectToString(row["SerialID"]),
						CardTypeName = TypeUtil.GetCardTypeName(TypeUtil.ObjectToInt(row["CardTypeID"])),
						CardPrice = TypeUtil.ObjectToString(row["CardPrice"]),
						Currency = TypeUtil.ObjectToString(row["Currency"]),
						ValidDate = TypeUtil.ObjectToDateTime(row["ValidDate"]).ToString("yyyy-MM-dd hh:mm:ss"),
						ApplyDate = TypeUtil.ObjectToDateTime(row["ApplyDate"]).ToString("yyyy-MM-dd hh:mm:ss"),
						UserRange = TypeUtil.GetUserRange(TypeUtil.ObjectToInt(row["UseRange"])),
						SalesPerson = TypeUtil.ObjectToString(row["SalesPerson"]),
						NullityStatus = TypeUtil.GetNullityStatus((byte)row["Nullity"])
					});
				}
			}
			long num3 = 0L;
			long num4 = 0L;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			DataSet livcardStatByBuildID = FacadeManage.aideTreasureFacade.GetLivcardStatByBuildID(num);
			DataRow dataRow2 = livcardStatByBuildID.Tables[0].Rows[0];
			if (dataRow2["TotalCount"].ToString() != "0")
			{
				num3 = TypeUtil.ObjectToInt(dataRow2["TotalPrice"]);
				num4 = TypeUtil.ObjectToInt(dataRow2["TotalCurrency"]);
				num5 = TypeUtil.ObjectToInt(dataRow2["TotalUsed"]);
				num6 = TypeUtil.ObjectToInt(dataRow2["TotalNoUsed"]);
				num7 = TypeUtil.ObjectToInt(dataRow2["TotalOut"]);
				num8 = TypeUtil.ObjectToInt(dataRow2["TotalNullity"]);
				num9 = TypeUtil.ObjectToInt(dataRow2["TotalCount"]);
				string text = "总金额：￥" + TypeUtil.FormatMoney(num3.ToString()) + "，总游戏豆：" + TypeUtil.FormatMoney(num4.ToString()) + "，充值 " + num5.ToString() + " 张，未充值 " + num6.ToString() + " 张，已过期 " + num7.ToString() + " 张，禁用 " + num8.ToString() + " 张，激活 " + (num9 - num8).ToString() + " 张";
			}
			return Json(new
			{
				IsOk = true,
				Msg = JsonConvert.SerializeObject(new
				{
					amount = "总金额：￥" + TypeUtil.FormatMoney(num3.ToString()),
					gold = "总游戏豆：" + TypeUtil.FormatMoney(num4.ToString()),
					useNumber = "充值 " + num5.ToString() + " 张",
					notUserNumber = "未充值 " + num6.ToString() + " 张",
					pastdueNumber = "已过期 " + num7.ToString() + " 张",
					outNumber = "禁用 " + num8.ToString() + " 张",
					actNumber = "激活 " + (num9 - num8).ToString() + " 张"
				}),
				Total = ((livcardAssociatorList.PageSet != null && livcardAssociatorList.PageSet.Tables != null && livcardAssociatorList.PageSet.Tables[0].Rows.Count != 0) ? livcardAssociatorList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoLivcardAssociator()
		{
			int num = TypeUtil.ObjectToInt(base.Request["opt"]);
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				string text2 = "";
				if (num != 1)
				{
					text2 = "WHERE CardID in (" + text + ")";
					try
					{
						FacadeManage.aideTreasureFacade.SetCardDisbale(text2);
						return Json(new
						{
							IsOk = true,
							Msg = "禁用成功"
						});
					}
					catch
					{
						return Json(new
						{
							IsOk = false,
							Msg = "禁用失败"
						});
					}
				}
				text2 = "WHERE CardID in (" + text + ")";
				try
				{
					FacadeManage.aideTreasureFacade.SetCardEnbale(text2);
					return Json(new
					{
						IsOk = true,
						Msg = "启用成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "启用失败"
					});
				}
			}
			return Json(new
			{
				IsOk = false,
				Msg = "请选择操作项"
			});
		}

		[CheckCustomer]
		public ActionResult LivcardAssociatorInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.SerialID = text;
			LivcardAssociator livcardAssociatorInfo = FacadeManage.aideTreasureFacade.GetLivcardAssociatorInfo(text);
			if (livcardAssociatorInfo != null)
			{
				base.ViewBag.SerialID = livcardAssociatorInfo.SerialID;
				base.ViewBag.CardTypeName = TypeUtil.GetCardTypeName(livcardAssociatorInfo.CardTypeID);
				base.ViewBag.BuildID = livcardAssociatorInfo.BuildID.ToString();
				base.ViewBag.CardPrice = livcardAssociatorInfo.CardPrice.ToString();
				base.ViewBag.Currency = livcardAssociatorInfo.Currency.ToString();
				base.ViewBag.UserRange = TypeUtil.GetUserRange(livcardAssociatorInfo.UseRange);
				base.ViewBag.ValidDate = livcardAssociatorInfo.ValidDate.ToString("yyyy-MM-dd HH:mm:ss");
				base.ViewBag.BuildDate = livcardAssociatorInfo.BuildDate.ToString("yyyy-MM-dd HH:mm:ss");
				base.ViewBag.SalesPerson = livcardAssociatorInfo.SalesPerson;
				base.ViewBag.NullityStatus = TypeUtil.GetNullityStatus(livcardAssociatorInfo.Nullity);
				DateTime applyDate = livcardAssociatorInfo.ApplyDate;
				ShareDetailInfo shareDetailInfo = FacadeManage.aideTreasureFacade.GetShareDetailInfo(text.ToString());
				if (shareDetailInfo != null)
				{
					base.ViewBag.PyaCardVisible = true;
					base.ViewBag.PayCardMsg = "充值信息";
					base.ViewBag.ApplyDate = shareDetailInfo.ApplyDate.ToString();
					base.ViewBag.PayUser = TypeUtil.GetAccounts(shareDetailInfo.UserID) + "(" + TypeUtil.GetGameID(shareDetailInfo.UserID) + ")";
					base.ViewBag.PayOperUser = TypeUtil.GetAccounts(shareDetailInfo.OperUserID);
					base.ViewBag.PayBeforeCurrency = shareDetailInfo.BeforeCurrency.ToString();
					base.ViewBag.PayAddress = shareDetailInfo.IPAddress + "  " + IPQuery.GetAddressWithIP(shareDetailInfo.IPAddress);
				}
			}
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoLivcardAssociatorInfo()
		{
			string arg = TypeUtil.ObjectToString(base.Request["cid"]);
			int num = TypeUtil.ObjectToInt(base.Request["opt"]);
			if (num != 1)
			{
				string cardDisbale = string.Format("WHERE SerialID='{0}'", arg);
				try
				{
					FacadeManage.aideTreasureFacade.SetCardDisbale(cardDisbale);
					return Json(new
					{
						IsOk = true,
						Msg = "禁用成功"
					});
				}
				catch
				{
					return Json(new
					{
						IsOk = false,
						Msg = "禁用失败"
					});
				}
			}
			string cardEnbale = string.Format("WHERE SerialID='{0}'", arg);
			try
			{
				FacadeManage.aideTreasureFacade.SetCardEnbale(cardEnbale);
				return Json(new
				{
					IsOk = true,
					Msg = "启用成功"
				});
			}
			catch
			{
				return Json(new
				{
					IsOk = false,
					Msg = "启用失败"
				});
			}
		}

		[CheckCustomer]
		public ActionResult LivcardAssociatorList()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewBag.param = num;
			return View();
		}

		[CheckCustomer]
		public ActionResult LivcardEdit()
		{
			int num = TypeUtil.ObjectToInt(base.Request["buildid"]);
			string text = TypeUtil.ObjectToString(base.Request["param"]);
			base.ViewBag.BuildId = num;
			base.ViewBag.Param = text;
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DoLivcardEdit()
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
			int buildID = TypeUtil.ObjectToInt(base.Request["BuildId"]);
			string text = TypeUtil.ObjectToString(base.Request["StrParamsList"]);
			string text2 = TypeUtil.ObjectToString(base.Request["password"]);
			string text3 = TypeUtil.ObjectToString(base.Request["EnjoinOverDate"]);
			if (text3 == "" && text2 == "")
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请输入密码或者有效期"
				});
			}
			StringBuilder stringBuilder = new StringBuilder();
			DateTime result = DateTime.Now;
			bool flag = DateTime.TryParse(text3, out result);
			if (flag && text2 != "")
			{
				stringBuilder.AppendFormat("update {0} set ValidDate='{1}',PassWord='{2}' where CardID in ({3})", "LivcardAssociator", result, Utility.MD5(text2), text);
			}
			else if (flag)
			{
				stringBuilder.AppendFormat("update {0} set ValidDate='{1}' where CardID in ({2})", "LivcardAssociator", result, text);
			}
			else
			{
				stringBuilder.AppendFormat("update {0} set PassWord='{1}' where CardID in ({2})", "LivcardAssociator", Utility.MD5(text2), text);
			}
			int num = FacadeManage.aideTreasureFacade.ExecuteSql(stringBuilder.ToString());
			LivcardBuildStream livcardBuildStreamInfo = FacadeManage.aideTreasureFacade.GetLivcardBuildStreamInfo(buildID);
			string text4 = Encoding.Default.GetString(livcardBuildStreamInfo.BuildCardPacket);
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.AppendFormat("SELECT SerialID FROM {0} WHERE CardID in ({1})", "LivcardAssociator", text);
			DataSet dataSetBySql = FacadeManage.aideTreasureFacade.GetDataSetBySql(stringBuilder2.ToString());
			foreach (DataRow row in dataSetBySql.Tables[0].Rows)
			{
				string text5 = row["SerialID"].ToString();
				int num2 = text4.IndexOf(text5);
				string str = text4.Substring(0, num2 + text5.Length + 1);
				int startIndex = text4.IndexOf("/", num2 + text5.Length + 1);
				string str2 = text4.Substring(startIndex);
				text4 = str + text2 + str2;
			}
			livcardBuildStreamInfo.BuildCardPacket = Encoding.Default.GetBytes(text4);
			FacadeManage.aideTreasureFacade.UpdateLivcardBuildStream(livcardBuildStreamInfo);
			if (num > 0)
			{
				return Json(new
				{
					IsOk = true,
					Msg = "成功更新" + num + "条记录"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "没有符合的数据更新"
			});
		}

		[CheckCustomer]
		public ActionResult ShareInfoList()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetShareInfoList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["CardTypeID"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["ShareID"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num4 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "Order BY ApplyDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num3 > 0)
			{
				stringBuilder.AppendFormat(" AND ShareID={0} ", num3);
			}
			if (num2 > 0)
			{
				stringBuilder.AppendFormat(" AND CardTypeID={0} ", num2);
			}
			DateTime now = DateTime.Now;
			if (text3 != "")
			{
				switch (num4)
				{
				case 1:
					stringBuilder.AppendFormat(" AND Accounts='{0}' ", text3);
					break;
				case 2:
					if (TypeUtil.ObjectToInt(text3) <= 0)
					{
						return Json(new
						{
							IsOk = false,
							Msg = "游戏ID格式错误"
						});
					}
					stringBuilder.AppendFormat(" AND GameID='{0}'", text3);
					break;
				case 3:
					stringBuilder.AppendFormat(" AND OrderID like '{0}%' ", text3);
					break;
				case 4:
					stringBuilder.AppendFormat(" AND Spreader='{0}'", text3);
					break;
				case 5:
					stringBuilder.AppendFormat(" AND AgentAcc='{0}'", text3);
					break;
				}
			}
			switch (num)
			{
			case 1:
				if (text != "")
				{
					stringBuilder.AppendFormat(" AND ApplyDate > '{0}'", Convert.ToDateTime(text));
				}
				if (text2 != "")
				{
					stringBuilder.AppendFormat(" AND ApplyDate <= '{0}'", Convert.ToDateTime(text2));
				}
				break;
			case 2:
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 3:
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 4:
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 5:
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 6:
				text = now.ToString("yyyy-MM-01");
				text2 = now.ToString("yyyy-MM-dd 23:59:59");
				stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			case 7:
				text = now.AddMonths(-1).ToString("yyyy-MM-01");
				text2 = now.ToString("yyyy-MM-01");
				stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", text, text2);
				break;
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_FillOrders", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			string sql = "Select Sum(OrderAmount) AS OrderAmount, Sum(PayAmount) AS PayAmount, Sum(Currency) AS Currency From View_FillOrders" + stringBuilder.ToString();
			DataSet dataSetBySql = FacadeManage.aideTreasureFacade.GetDataSetBySql(sql);
			string msg = "";
			if (dataSetBySql.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = dataSetBySql.Tables[0].Rows[0];
				msg = JsonConvert.SerializeObject(new
				{
					Currency = TypeUtil.FormatMoney(dataRow["Currency"].ToString()),
					OrderAmount = dataRow["OrderAmount"].ToString(),
					PayAmount = dataRow["PayAmount"].ToString()
				});
			}
			return Json(new
			{
				IsOk = true,
				Msg = msg,
				Total = list.RecordCount,
				Data = JsonConvert.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public ActionResult OnLineOrderList()
		{
			return View();
		}

		[CheckCustomer]
		public ActionResult OnLineOrderInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["OrderID"]);
			if (text != "")
			{
				OnLineOrder onLineOrderInfo = FacadeManage.aideTreasureFacade.GetOnLineOrderInfo(text);
				if (onLineOrderInfo != null)
				{
					base.ViewBag.OrderID = onLineOrderInfo.OrderID.Trim();
					base.ViewBag.ApplyDate = onLineOrderInfo.ApplyDate.ToString("yyyy-MM-dd HH:mm:ss");
					base.ViewBag.Accounts = onLineOrderInfo.Accounts.Trim();
					base.ViewBag.OrderAmount = onLineOrderInfo.OrderAmount.ToString("N");
					base.ViewBag.PayAmount = onLineOrderInfo.PayAmount.ToString("N");
					base.ViewBag.PresentScore = onLineOrderInfo.CardGold.ToString();
					base.ViewBag.OrderStatus = ((onLineOrderInfo.OrderStatus == 0) ? "<span class='hong'>未付款</span>" : ((onLineOrderInfo.OrderStatus == 1) ? "<span class='lan'>已付款待处理</span>" : "<span class='lan'>成功</span>"));
					base.ViewBag.IPAddress = onLineOrderInfo.IPAddress.Trim() + "&nbsp;&nbsp;" + IPQuery.GetAddressWithIP(onLineOrderInfo.IPAddress.Trim());
					base.ViewBag.OperUserID = onLineOrderInfo.OperUserID.ToString();
				}
			}
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetOnLineOrderList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["CardTypeID"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["ShareID"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num4 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "ORDER BY ApplyDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			switch (num)
			{
			case 1:
				if (num3 > 0)
				{
					stringBuilder.AppendFormat(" AND ShareID={0} ", num3);
				}
				if (text != "")
				{
					stringBuilder.AppendFormat(" AND ApplyDate > '{0}'", Convert.ToDateTime(text));
				}
				if (text2 != "")
				{
					stringBuilder.AppendFormat(" AND ApplyDate <= '{0}'", Convert.ToDateTime(text2));
				}
				if (text3 != "")
				{
					switch (num4)
					{
					case 1:
						stringBuilder.AppendFormat(" AND Accounts='{0}' ", text3);
						break;
					case 2:
						if (TypeUtil.ObjectToInt(text3) <= 0)
						{
							return Json(new
							{
								IsOk = false,
								Msg = "用户格式错误"
							});
						}
						stringBuilder.AppendFormat(" AND GameID='{0}'", text3);
						break;
					case 3:
						stringBuilder.AppendFormat(" AND OrderID like '{0}%' ", text3);
						break;
					}
				}
				break;
			case 2:
				if (num3 > 0)
				{
					stringBuilder.AppendFormat(" AND ShareID={0} ", num3);
				}
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND CardTypeID={0} ", num2);
				}
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 3:
				if (num3 > 0)
				{
					stringBuilder.AppendFormat(" AND ShareID={0} ", num3);
				}
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND CardTypeID={0} ", num2);
				}
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 4:
				if (num3 > 0)
				{
					stringBuilder.AppendFormat(" AND ShareID={0} ", num3);
				}
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND CardTypeID={0} ", num2);
				}
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 5:
				if (num3 > 0)
				{
					stringBuilder.AppendFormat(" AND ShareID={0} ", num3);
				}
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND CardTypeID={0} ", num2);
				}
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND ApplyDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 6:
				if (!string.IsNullOrEmpty(text3))
				{
					switch (num4)
					{
					case 1:
						stringBuilder.AppendFormat(" AND Accounts='{0}' ", text3);
						break;
					case 2:
						if (TypeUtil.ObjectToInt(text3) > 0)
						{
							stringBuilder.AppendFormat(" AND GameID='{0}'", text3);
						}
						else
						{
							stringBuilder.Append(" AND 1=3 ");
						}
						break;
					case 3:
						stringBuilder.AppendFormat(" AND OrderID='{0}'", text3);
						break;
					}
				}
				else
				{
					stringBuilder.Append(" AND 1=3 ");
				}
				break;
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_OnLineOrder", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			DataSet dataSet = FacadeManage.aideTreasureFacade.SumPay(stringBuilder.ToString());
			decimal sumSuccess = Convert.ToDecimal(dataSet.Tables[0].Rows[0][0]);
			int successCount = Convert.ToInt32(dataSet.Tables[0].Rows[0][1]);
			decimal sumFail = Convert.ToDecimal(dataSet.Tables[1].Rows[0][0]);
			return Json(new
			{
				IsOk = true,
				Msg = new
				{
					SumSuccess = sumSuccess,
					SuccessCount = successCount,
					SumFail = sumFail
				},
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

        [AntiSqlInjection]
        [CheckCustomer]
        public JsonResult ExcuteOffLinePay()
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
            string orderID = TypeUtil.ObjectToString(base.Request["ID"]);
            int statu = TypeUtil.ObjectToInt(base.Request["statu"]);

            Message message = FacadeManage.aideTreasureFacade.OffLinePass(orderID, statu,user.UserID, GameRequest.GetUserIP());
            if (message.Success)
            {
                return Json(new
                {
                    IsOk = true,
                    Msg = "确认成功"
                });
            }
            string text2 = "";
            switch (message.MessageID)
            {
                case -4:
                    text2 = "选中的玩家正在游戏，不能扣除！";
                    break;
                case -3:
                    text2 = "未选中赠送对象！";
                    break;
                case -2:
                    text2 = "赠送金额不能为零！";
                    break;
                case -1:
                    text2 = "抱歉，未知服务器错误！";
                    break;
                default:
                    text2 = "抱歉，未知服务器错误！";
                    break;
            }
            return Json(new
            {
                IsOk = false,
                Msg = text2
            });
        }

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DeleteOnlineOrder()
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
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				string sqlQuery = "WHERE OnLineID in (" + text + ")";
				try
				{
					FacadeManage.aideTreasureFacade.DeleteOnlineOrder(sqlQuery);
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
				Msg = "没有删除的项"
			});
		}

		[CheckCustomer]
		public ActionResult AgentPayOrder()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetAgentPayOrder()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string s = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string s2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text = TypeUtil.ObjectToString(base.Request["Keyword"]);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			string orderby = "ORDER BY OperateDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE OrderStatus=1 ");
			if (DateTime.TryParse(s, out result))
			{
				stringBuilder.AppendFormat(" AND OperateDate>='{0}' ", result);
			}
			if (DateTime.TryParse(s2, out result2))
			{
				stringBuilder.AppendFormat(" AND OperateDate<'{0}' ", result2);
			}
			if (!string.IsNullOrEmpty(text))
			{
				switch (num)
				{
				case 1:
					stringBuilder.AppendFormat(" AND OrderId='{0}' ", text);
					break;
				case 2:
					if (TypeUtil.ObjectToInt(text) > 0)
					{
						stringBuilder.AppendFormat(" AND GameID={0} ", TypeUtil.ObjectToInt(text));
					}
					break;
				case 3:
					stringBuilder.AppendFormat(" AND Accounts='{0}' ", text);
					break;
				case 4:
					stringBuilder.AppendFormat(" AND AgentAcc='{0}' ", text);
					break;
				}
			}
			PagerSet agentPayOrder = FacadeManage.aideTreasureFacade.GetAgentPayOrder(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (agentPayOrder != null && agentPayOrder.PageSet != null && agentPayOrder.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in agentPayOrder.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						ID = TypeUtil.ObjectToInt(row["ID"]),
						OrderId = TypeUtil.ObjectToString(row["OrderId"]),
						Score = TypeUtil.ObjectToString(row["Score"]),
						GameID = TypeUtil.ObjectToString(row["GameID"]),
						Accounts = TypeUtil.ObjectToString(row["Accounts"]),
						AgentAcc = TypeUtil.ObjectToString(row["AgentAcc"]),
						OperateDate = TypeUtil.ObjectToString(row["OperateDate"])
					});
				}
			}
			string sql = "Select Sum(Score) AS Score From RYAgentDB..T_AgentFillOrder " + stringBuilder.ToString();
			DataTable dataTable = FacadeManage.aideTreasureFacade.GetDataSetBySql(sql).Tables[0];
			string msg = "";
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow2 = dataTable.Rows[0];
				msg = JsonConvert.SerializeObject(new
				{
					Score = dataRow2["Score"].ToString()
				});
			}
			return Json(new
			{
				IsOk = true,
				Total = agentPayOrder.RecordCount,
				Data = JsonConvert.SerializeObject(list),
				Msg = msg
			});
		}

		[CheckCustomer]
		public ActionResult OrderKQList()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetOrderKQList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["Status"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "ORDER BY PayDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			switch (num)
			{
			case 1:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND PayResult={0} ", num2);
				}
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND PayDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 2:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND PayResult={0} ", num2);
				}
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND PayDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 3:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND PayResult={0} ", num2);
				}
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND PayDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 4:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND PayResult={0} ", num2);
				}
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND PayDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 5:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND PayResult={0} ", num2);
				}
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND PayDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 6:
				if (!string.IsNullOrEmpty(text3))
				{
					switch (num3)
					{
					case 1:
						stringBuilder.AppendFormat(" AND OrderID='{0}' ", text3);
						break;
					case 2:
						stringBuilder.AppendFormat(" AND DealID='{0}'", text3);
						break;
					case 3:
						stringBuilder.AppendFormat(" AND Ext1='{0}'", text3);
						break;
					}
				}
				else
				{
					stringBuilder.Append(" AND 1=3 ");
				}
				break;
			}
			PagerSet kQDetailList = FacadeManage.aideTreasureFacade.GetKQDetailList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (kQDetailList != null && kQDetailList.PageSet != null && kQDetailList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in kQDetailList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						DetailIDStr = ((TypeUtil.ObjectToInt(row["PayResult"]) == 10) ? "" : TypeUtil.ObjectToString(row["OnLineID"])),
						DetailID = TypeUtil.ObjectToString(row["DetailID"]),
						OrderID = TypeUtil.ObjectToString(row["OrderID"]),
						DealID = TypeUtil.ObjectToString(row["DealID"]),
						OrderAmount = TypeUtil.ObjectToString(row["OrderAmount"]),
						PayAmount = TypeUtil.ObjectToString(row["PayAmount"]),
						Fee = TypeUtil.ObjectToString(row["Fee"]),
						OrderTime = TypeUtil.ObjectToString(row["OrderTime"]),
						DealTime = TypeUtil.ObjectToString(row["DealTime"]),
						PayResult = TypeUtil.ObjectToInt(row["PayResult"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((kQDetailList.PageSet != null && kQDetailList.PageSet.Tables != null && kQDetailList.PageSet.Tables[0].Rows.Count != 0) ? kQDetailList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DeleteOrderKQ()
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
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				string sqlQuery = "WHERE DetailID in (" + text + ")";
				try
				{
					FacadeManage.aideTreasureFacade.DeleteKQDetail(sqlQuery);
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
				Msg = "没有删除的项"
			});
		}

		[CheckCustomer]
		public ActionResult OrderKQInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["id"]);
			BillPayType billPayType = new BillPayType(TextUtility.GetRealPath("/App_Data/BillPayType.xml"));
			BillBanks billBanks = new BillBanks(TextUtility.GetRealPath("/App_Data/BillBanks.xml"));
			new BillErrorMsg(TextUtility.GetRealPath("/App_Data/BillErrorMsg.xml"));
			if (num > 0)
			{
				ReturnKQDetailInfo kQDetailInfo = FacadeManage.aideTreasureFacade.GetKQDetailInfo(num);
				if (kQDetailInfo != null)
				{
					base.ViewBag.OrderID = "<a class='l' href='javascript:void(0)' onclick=\"javascript:openWindowOwn('" + base.Server.MapPath("~/") + "Filled/OnLineOrderInfo?OrderID=" + kQDetailInfo.OrderID + "','online_" + kQDetailInfo.OrderID + "',600,465)\">" + kQDetailInfo.OrderID + "</a>";
					base.ViewBag.OrderTime = kQDetailInfo.OrderTime.ToString("yyyy-MM-dd HH:ss:mm");
					base.ViewBag.OrderAmount = kQDetailInfo.OrderAmount.ToString("N");
					base.ViewBag.PayAmount = kQDetailInfo.PayAmount.ToString("N");
					base.ViewBag.Fee = kQDetailInfo.Fee.ToString("f3");
					base.ViewBag.Revenue = (kQDetailInfo.PayAmount - kQDetailInfo.Fee).ToString("f3");
					base.ViewBag.PayResult = ((kQDetailInfo.PayResult == "10") ? "<span class='lan'>成功</span>" : "<span class='hong'>失败</span>");
					base.ViewBag.DealID = kQDetailInfo.DealID;
					base.ViewBag.DealTime = kQDetailInfo.DealTime.ToString("yyyy-MM-dd HH:mm:ss");
					base.ViewBag.BankDealID = kQDetailInfo.BankDealID;
					base.ViewBag.PayType = billPayType.GetBillPayType(kQDetailInfo.PayType.Trim());
					base.ViewBag.BankID = billBanks.GetBillBanksByCode(kQDetailInfo.BankID.Trim());
					base.ViewBag.ErrCode = kQDetailInfo.ErrCode;
					base.ViewBag.Version = kQDetailInfo.Version;
					base.ViewBag.Language = "中文";
					base.ViewBag.Ext1 = kQDetailInfo.Ext1 + "&nbsp;&nbsp;" + IPQuery.GetAddressWithIP(kQDetailInfo.Ext1);
					base.ViewBag.Ext2 = kQDetailInfo.Ext2;
					base.ViewBag.SignType = "与提交订单时的签名类型保持一致";
					base.ViewBag.SignMsg = kQDetailInfo.SignMsg;
				}
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult OrderYPList()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DeleteOrderYB()
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
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				string sqlQuery = "WHERE DetailID in (" + text + ")";
				try
				{
					FacadeManage.aideTreasureFacade.DeleteYPDetail(sqlQuery);
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
				Msg = "没有删除的项"
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetOrderYPList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["Status"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			switch (num)
			{
			case 1:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND r1_Code={0} ", num2);
				}
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND PayDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 2:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND r1_Code={0} ", num2);
				}
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND PayDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 3:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND r1_Code={0} ", num2);
				}
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND PayDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 4:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND r1_Code={0} ", num2);
				}
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND PayDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 5:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND r1_Code={0} ", num2);
				}
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND PayDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 6:
				if (!string.IsNullOrEmpty(text3))
				{
					switch (num3)
					{
					case 1:
						stringBuilder.AppendFormat(" AND r6_Order='{0}' ", text3);
						break;
					case 2:
						stringBuilder.AppendFormat(" AND r2_TrxId='{0}'", text3);
						break;
					}
				}
				else
				{
					stringBuilder.Append(" AND 1=3 ");
				}
				break;
			}
			PagerSet kQDetailList = FacadeManage.aideTreasureFacade.GetKQDetailList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (kQDetailList != null && kQDetailList.PageSet != null && kQDetailList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in kQDetailList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						DetailIDStr = ((TypeUtil.ObjectToInt(row["r1_Code"]) == 1) ? "" : TypeUtil.ObjectToString(row["DetailID"])),
						DetailID = TypeUtil.ObjectToString(row["DetailID"]),
						r6_Order = TypeUtil.ObjectToString(row["r6_Order"]),
						r2_TrxId = TypeUtil.ObjectToString(row["r2_TrxId"]),
						r3_Amt = TypeUtil.ObjectToString(row["r3_Amt"]),
						r5_Pid = TypeUtil.ObjectToString(row["r5_Pid"]),
						CollectDate = TypeUtil.ObjectToString(row["CollectDate"]),
						r1_Code = TypeUtil.ObjectToInt(row["r1_Code"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((kQDetailList.PageSet != null && kQDetailList.PageSet.Tables != null && kQDetailList.PageSet.Tables[0].Rows.Count != 0) ? kQDetailList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult OrderYPInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["id"]);
			if (num > 0)
			{
				ReturnYPDetailInfo yPDetailInfo = FacadeManage.aideTreasureFacade.GetYPDetailInfo(num);
				if (yPDetailInfo != null)
				{
					base.ViewBag.R6_Order = "<a class='l' href='javascript:void(0)' onclick=\"javascript:openWindowOwn('" + base.Server.MapPath("~/") + "Filled/OnLineOrderInfo?OrderID=" + yPDetailInfo.R6_Order + "','online_" + yPDetailInfo.R6_Order + "',600,465)\">" + yPDetailInfo.R6_Order + "</a>";
					base.ViewBag.R1_Code = ((yPDetailInfo.R1_Code == "1") ? "<span class='lan'>成功</span>" : "<span class='hong'>失败</span>");
					base.ViewBag.R2_TrxId = yPDetailInfo.R2_TrxId.Trim();
					base.ViewBag.R3_Amt = yPDetailInfo.R3_Amt.ToString("N");
					base.ViewBag.R5_Pid = yPDetailInfo.R5_Pid.Trim();
					base.ViewBag.R8_MP = yPDetailInfo.R8_MP.Trim();
					base.ViewBag.R9_BType = ((yPDetailInfo.R9_BType.Trim() == "1") ? "浏览器重定向" : "服务器点对点通讯");
					base.ViewBag.Rb_BankId = TypeUtil.GetBankName(yPDetailInfo.Rb_BankId.Trim());
					base.ViewBag.Ro_BankOrderId = yPDetailInfo.Ro_BankOrderId.Trim();
					base.ViewBag.Rp_PayDate = yPDetailInfo.Rp_PayDate.Trim().Substring(0, 4) + "-" + yPDetailInfo.Rp_PayDate.Trim().Substring(4, 2) + "-" + yPDetailInfo.Rp_PayDate.Trim().Substring(6, 2) + " " + yPDetailInfo.Rp_PayDate.Trim().Substring(8, 2) + ":" + yPDetailInfo.Rp_PayDate.Trim().Substring(10, 2) + ":" + yPDetailInfo.Rp_PayDate.Trim().Substring(12, 2);
					base.ViewBag.Rq_CardNo = yPDetailInfo.Rq_CardNo.Trim();
					base.ViewBag.Ru_Trxtime = yPDetailInfo.Ru_Trxtime.Trim().Substring(0, 4) + "-" + yPDetailInfo.Ru_Trxtime.Trim().Substring(4, 2) + "-" + yPDetailInfo.Ru_Trxtime.Trim().Substring(6, 2) + " " + yPDetailInfo.Ru_Trxtime.Trim().Substring(8, 2) + ":" + yPDetailInfo.Ru_Trxtime.Trim().Substring(10, 2) + ":" + yPDetailInfo.Ru_Trxtime.Trim().Substring(12, 2);
					base.ViewBag.Hmac = yPDetailInfo.Hmac.Trim();
					base.ViewBag.CollectDate = yPDetailInfo.CollectDate.ToString("yyyy-MM-dd HH:mm:ss");
				}
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult OrderVBList()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetOrderVBList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["Status"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			int num3 = TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			switch (num)
			{
			case 1:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND r1_Code={0} ", num2);
				}
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 2:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND r1_Code={0} ", num2);
				}
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 3:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND r1_Code={0} ", num2);
				}
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 4:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND r1_Code={0} ", num2);
				}
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 5:
				if (num2 > 0)
				{
					stringBuilder.AppendFormat(" AND r1_Code={0} ", num2);
				}
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 6:
				if (!string.IsNullOrEmpty(text3))
				{
					switch (num3)
					{
					case 1:
						stringBuilder.AppendFormat(" AND r6_Order='{0}' ", text3);
						break;
					case 2:
						stringBuilder.AppendFormat(" AND r2_TrxId='{0}'", text3);
						break;
					}
				}
				else
				{
					stringBuilder.Append(" AND 1=3 ");
				}
				break;
			}
			PagerSet vBDetailList = FacadeManage.aideTreasureFacade.GetVBDetailList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (vBDetailList != null && vBDetailList.PageSet != null && vBDetailList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in vBDetailList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						DetailID = TypeUtil.ObjectToString(row["DetailID"]),
						OrderID = TypeUtil.ObjectToString(row["OrderID"]),
						Rtmz = TypeUtil.ObjectToString(row["Rtmz"]),
						Rtlx = ((TypeUtil.ObjectToInt(row["Rtlx"]) == 1) ? "正式卡" : ((TypeUtil.ObjectToInt(row["Rtlx"]) == 2) ? "测试卡" : "促销卡")),
						Rtka = TypeUtil.ObjectToString(row["Rtka"]),
						Rtmi = TypeUtil.ObjectToString(row["Rtmi"]),
						CollectDate = TypeUtil.ObjectToString(row["CollectDate"]),
						Rtflag = TypeUtil.ObjectToInt(row["Rtflag"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((vBDetailList.PageSet != null && vBDetailList.PageSet.Tables != null && vBDetailList.PageSet.Tables[0].Rows.Count != 0) ? vBDetailList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DeleteOrderVB()
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
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				string sqlQuery = "WHERE DetailID in (" + text + ")";
				try
				{
					FacadeManage.aideTreasureFacade.DeleteVBDetail(sqlQuery);
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
				Msg = "没有删除的项"
			});
		}

		[CheckCustomer]
		public ActionResult OrderVBInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["id"]);
			if (num > 0)
			{
				ReturnVBDetailInfo vBDetailInfo = FacadeManage.aideTreasureFacade.GetVBDetailInfo(num);
				if (vBDetailInfo != null)
				{
					base.ViewBag.OrderID = "<a class='l' href='javascript:void(0)' onclick=\"javascript:openWindowOwn('OnLineOrderInfo.aspx?OrderID=" + vBDetailInfo.OrderID + "','online_" + vBDetailInfo.OrderID + "',600,465)\">" + vBDetailInfo.OrderID + "</a>";
					base.ViewBag.OrderTime = vBDetailInfo.CollectDate.ToString("yyyy-MM-dd HH:ss:mm");
					base.ViewBag.OperStationID = vBDetailInfo.OperStationID.ToString();
					base.ViewBag.Rtflag = ((vBDetailInfo.Rtflag == 1) ? "正常返回" : "补丁返回");
					base.ViewBag.Rtka = vBDetailInfo.Rtka.ToString();
					base.ViewBag.Rtlx = ((vBDetailInfo.Rtlx == 1) ? "正式卡" : ((vBDetailInfo.Rtlx == 2) ? "测试卡" : "促销卡"));
					base.ViewBag.Rtmd5 = vBDetailInfo.Rtmd5.ToString();
					base.ViewBag.Rtmi = vBDetailInfo.Rtmi;
					base.ViewBag.Rtmz = vBDetailInfo.Rtmz.ToString();
					base.ViewBag.RtoID = vBDetailInfo.Rtoid;
					base.ViewBag.SignMsg = vBDetailInfo.SignMsg.ToString();
				}
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult OrderAppList()
		{
			return View();
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult GetOrderAppList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			int num = TypeUtil.ObjectToInt(base.Request["Type"]);
			int num2 = TypeUtil.ObjectToInt(base.Request["Status"]);
			string text = TypeUtil.ObjectToString(base.Request["StartDate"]);
			string text2 = TypeUtil.ObjectToString(base.Request["EndDate"]);
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			string text3 = TypeUtil.ObjectToString(base.Request["KeyWord"]);
			TypeUtil.ObjectToInt(base.Request["SearchType"]);
			string orderby = "ORDER BY CollectDate DESC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			switch (num)
			{
			case 1:
				switch (num2)
				{
				case 0:
					stringBuilder.AppendFormat(" AND Status={0} ", num2);
					break;
				case 1:
					stringBuilder.Append(" AND Status<>0 ");
					break;
				}
				if (text != "")
				{
					stringBuilder.AppendFormat(" AND CollectDate > '{0}'", Convert.ToDateTime(text));
				}
				if (text2 != "")
				{
					stringBuilder.AppendFormat(" AND CollectDate <= '{0}'", Convert.ToDateTime(text2));
				}
				break;
			case 2:
				switch (num2)
				{
				case 0:
					stringBuilder.AppendFormat(" AND Status={0} ", num2);
					break;
				case 1:
					stringBuilder.Append(" AND Status<>0 ");
					break;
				}
				text = Fetch.GetTodayTime().Split('$')[0].ToString();
				text2 = Fetch.GetTodayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 3:
				switch (num2)
				{
				case 0:
					stringBuilder.AppendFormat(" AND Status={0} ", num2);
					break;
				case 1:
					stringBuilder.Append(" AND Status<>0 ");
					break;
				}
				text = Fetch.GetYesterdayTime().Split('$')[0].ToString();
				text2 = Fetch.GetYesterdayTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 4:
				switch (num2)
				{
				case 0:
					stringBuilder.AppendFormat(" AND Status={0} ", num2);
					break;
				case 1:
					stringBuilder.Append(" AND Status<>0 ");
					break;
				}
				text = Fetch.GetWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}'", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 5:
				switch (num2)
				{
				case 0:
					stringBuilder.AppendFormat(" AND Status={0} ", num2);
					break;
				case 1:
					stringBuilder.Append(" AND Status<>0 ");
					break;
				}
				text = Fetch.GetLastWeekTime().Split('$')[0].ToString();
				text2 = Fetch.GetLastWeekTime().Split('$')[1].ToString();
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && DateTime.TryParse(text, out result) && DateTime.TryParse(text2, out result2) && result <= result2)
				{
					stringBuilder.AppendFormat(" AND CollectDate BETWEEN '{0}' AND '{1}' ", result.ToString("yyyy-MM-dd"), result2.AddDays(1.0).ToString("yyyy-MM-dd"));
				}
				break;
			case 6:
				if (string.IsNullOrEmpty(text3))
				{
					stringBuilder.Append(" AND 1=3 ");
				}
				else
				{
					stringBuilder.AppendFormat(" AND OrderID='{0}' ", text3);
				}
				break;
			}
			PagerSet appDetailList = FacadeManage.aideTreasureFacade.GetAppDetailList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			List<object> list = new List<object>();
			if (appDetailList != null && appDetailList.PageSet != null && appDetailList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in appDetailList.PageSet.Tables[0].Rows)
				{
					list.Add(new
					{
						CollectDate = TypeUtil.ObjectToString(row["CollectDate"]),
						DetailID = TypeUtil.ObjectToString(row["DetailID"]),
						OrderID = TypeUtil.ObjectToString(row["OrderID"]),
						UserID = TypeUtil.ObjectToString(row["UserID"]),
						Accounts = TypeUtil.GetAccounts(TypeUtil.ObjectToInt(row["UserID"])),
						product_id = TypeUtil.ObjectToString(row["product_id"]),
						quantity = TypeUtil.ObjectToString(row["quantity"]),
						PayAmount = TypeUtil.ObjectToString(row["PayAmount"]),
						Status = TypeUtil.ObjectToInt(row["Status"])
					});
				}
			}
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = ((appDetailList.PageSet != null && appDetailList.PageSet.Tables != null && appDetailList.PageSet.Tables[0].Rows.Count != 0) ? appDetailList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult OrderAppInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["id"]);
			if (num > 0)
			{
				ReturnAppDetailInfo appDetailInfo = FacadeManage.aideTreasureFacade.GetAppDetailInfo(num);
				if (appDetailInfo != null)
				{
					base.ViewBag.OrderID = appDetailInfo.OrderID;
					base.ViewBag.PayAmount = appDetailInfo.PayAmount.ToString();
					base.ViewBag.product_id = appDetailInfo.Product_id;
					base.ViewBag.UserID = TypeUtil.GetAccounts(appDetailInfo.UserID);
					base.ViewBag.quantity = appDetailInfo.Quantity.ToString();
					if (appDetailInfo.Status == 0)
					{
						base.ViewBag.Status = "<span class='lan'>成功</span>";
					}
					else
					{
						base.ViewBag.Status = "<span class='hong'>失败</span>";
					}
					base.ViewBag.CollectDate = appDetailInfo.CollectDate.ToString("yyyy-MM-dd HH:mm:ss");
					base.ViewBag.transaction_id = appDetailInfo.Transaction_id;
					base.ViewBag.purchase_date = appDetailInfo.Purchase_date;
					base.ViewBag.original_transaction_id = appDetailInfo.Original_transaction_id;
					base.ViewBag.original_purchase_date = appDetailInfo.Original_purchase_date;
					base.ViewBag.app_item_id = appDetailInfo.App_item_id;
					base.ViewBag.version_external_identifier = appDetailInfo.Version_external_identifier;
					base.ViewBag.bid = appDetailInfo.Bid;
					base.ViewBag.bvrs = appDetailInfo.Bvrs;
				}
			}
			return View();
		}

		[CheckCustomer]
		public ActionResult MemberTypeList()
		{
			string orderby = "ORDER BY MemberOrder ASC";
			string condition = "WHERE 1=1";
			PagerSet list = FacadeManage.aideAccountsFacade.GetList("MemberProperty", 1, 100, condition, orderby);
			if (list.PageSet.Tables[0].Rows.Count > 0)
			{
				base.ViewData["DataTable"] = list.PageSet.Tables[0];
			}
			return View();
		}

		[CheckCustomer]
		public JsonResult GetMemberTypeList()
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
				Msg = "",
				Total = ((globalLivcardList.PageSet != null && globalLivcardList.PageSet.Tables != null && globalLivcardList.PageSet.Tables[0].Rows.Count != 0) ? globalLivcardList.RecordCount : 0),
				Data = JsonConvert.SerializeObject(list)
			});
		}

		[CheckCustomer]
		public ActionResult MemberTypeInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			MemberProperty memberProperty = FacadeManage.aideAccountsFacade.GetMemberProperty(num);
			base.ViewBag.UserID = 0;
			base.ViewBag.GiftID = 0;
			base.ViewBag.ID = num;
			if (memberProperty != null)
			{
				base.ViewBag.CardName = memberProperty.MemberName;
				base.ViewBag.TaskRate = memberProperty.TaskRate.ToString();
				base.ViewBag.ShopRate = memberProperty.ShopRate.ToString();
				base.ViewBag.InsureRate = memberProperty.InsureRate.ToString();
				base.ViewBag.PresentScore = memberProperty.DayPresent.ToString();
				base.ViewBag.GiftID = memberProperty.DayGiftID;
				base.ViewBag.UserID = memberProperty.UserRight;
			}
			return View();
		}

		[CheckCustomer]
		public JsonResult UpdateMemberTypeInfo()
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
			int memberOrder = TypeUtil.ObjectToInt(base.Request["ID"]);
			MemberProperty memberProperty = FacadeManage.aideAccountsFacade.GetMemberProperty(memberOrder);
			if (memberProperty != null)
			{
				memberProperty.TaskRate = TypeUtil.ObjectToInt(base.Request["TaskRate"]);
				memberProperty.ShopRate = TypeUtil.ObjectToInt(base.Request["ShopRate"]);
				memberProperty.InsureRate = TypeUtil.ObjectToInt(base.Request["InsureRate"]);
				memberProperty.DayPresent = TypeUtil.ObjectToInt(base.Request["DayPresent"]);
				memberProperty.DayGiftID = TypeUtil.ObjectToInt(base.Request["DayGiftID"]);
				memberProperty.UserRight = TypeUtil.ObjectToInt(base.Request["UserRight"]);
				FacadeManage.aideAccountsFacade.UpdateMemberType(memberProperty);
				return Json(new
				{
					IsOk = true,
					Msg = "更新成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = "没有找到相应的数据"
			});
		}

		[CheckCustomer]
		public ViewResult GlobalAppInfo()
		{
			int num = TypeUtil.ObjectToInt(base.Request["param"]);
			base.ViewData["data"] = null;
			base.ViewBag.OpStr = "新增";
			base.ViewBag.ID = num;
			if (num > 0)
			{
				Admin.Models.GlobalAppInfo globalAppInfo = new Admin.Models.GlobalAppInfo();
				DataTable globalAppInfo2 = FacadeManage.aideTreasureFacade.GetGlobalAppInfo(num);
				if (globalAppInfo2 != null && globalAppInfo2.Rows.Count > 0)
				{
					globalAppInfo.SortID = TypeUtil.ObjectToInt(globalAppInfo2.Rows[0]["SortID"]);
					globalAppInfo.AppID = TypeUtil.ObjectToInt(globalAppInfo2.Rows[0]["AppID"]);
					globalAppInfo.Price = TypeUtil.ObjectToDecimal(globalAppInfo2.Rows[0]["Price"]);
					globalAppInfo.PresentCurrency = TypeUtil.ObjectToDecimal(globalAppInfo2.Rows[0]["PresentCurrency"]);
				}
				base.ViewData["data"] = globalAppInfo;
				base.ViewBag.OpStr = "编辑";
			}
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult DoGlobalAppInfo(Admin.Models.GlobalAppInfo entity)
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
			if (entity == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有提交数据"
				});
			}
			if (FacadeManage.aideTreasureFacade.EditGlobalAppInfo(entity.AppID, entity.Price, entity.PresentCurrency, entity.SortID) > 0)
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
		public ViewResult GlobalAppInfoList()
		{
			return View();
		}

		[AntiSqlInjection]
		[CheckCustomer]
		public JsonResult GetGlobalAppInfoList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY SortID ASC";
			StringBuilder stringBuilder = new StringBuilder(" WHERE TagID=1");
			DateTime result = DateTime.Now;
			DateTime result2 = DateTime.Now;
			if (DateTime.TryParse(TypeUtil.ObjectToString(base.Request["StartDate"]), out result) && DateTime.TryParse(TypeUtil.ObjectToString(base.Request["EndDate"]), out result2) && result <= result2)
			{
				stringBuilder.AppendFormat(" and CollectDate>='{0}' and CollectDate<'{1}'", result, result2);
			}
			PagerSet globalAppInfoList = FacadeManage.aideTreasureFacade.GetGlobalAppInfoList(pageIndex, pageSize, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = globalAppInfoList.RecordCount,
				Data = JsonHelper.SerializeObject(globalAppInfoList.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		[AntiSqlInjection]
		public JsonResult DelGlobalAppInfo()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					string sqlQuery = "WHERE AppId in (" + text + ")";
					FacadeManage.aideTreasureFacade.DeleteGlobalAppInfo(sqlQuery);
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
		public ActionResult PayType()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetPayList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY SortID ASC";
			int num = TypeUtil.ObjectToInt(base.Request["Status"], -1);
			int num2 = TypeUtil.ObjectToInt(base.Request["PlatformID"], 0);
			int num3 = TypeUtil.ObjectToInt(base.Request["QudaoID"], 0);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num > -1)
			{
				stringBuilder.AppendFormat(" AND Nullity={0}", num);
			}
			if (num2 > 0)
			{
				stringBuilder.AppendFormat(" AND PlatformID={0}", num2);
			}
			if (num3 > 0)
			{
				stringBuilder.AppendFormat(" AND QudaoID={0}", num3);
			}
            PagerSet list = FacadeManage.aideTreasureFacade.GetList("T_PayPlatformInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

        [CheckCustomer]
        public JsonResult AddQudao()
        {
            try
            {
                int nullity = TypeUtil.ObjectToInt(base.Request["nullity"]);
                T_PayPlatformInfo payInfo = new T_PayPlatformInfo();
                payInfo.QudaoName = TypeUtil.ObjectToString(base.Request["qudaoName"]);
                payInfo.PlatformName = TypeUtil.ObjectToString(base.Request["qudaoName"]);
                payInfo.PlatformCode = TypeUtil.ObjectToString(base.Request["Accnum"]);
                payInfo.PriKey = TypeUtil.ObjectToString(base.Request["priKey"]);
                payInfo.PubKey = TypeUtil.ObjectToString(base.Request["pubKey"]);
                payInfo.Url = TypeUtil.ObjectToString(base.Request["payUrl"]);
                payInfo.FindUrl = TypeUtil.ObjectToString(base.Request["findUrl"]);
                payInfo.BackName = TypeUtil.ObjectToString(base.Request["bankName"]);
                payInfo.BackAcc = TypeUtil.ObjectToString(base.Request["bankAcc"]);
                payInfo.BackAdd = TypeUtil.ObjectToString(base.Request["bankAdd"]);
                payInfo.PryType = TypeUtil.ObjectToInt(base.Request["payType"]);
                FacadeManage.aideTreasureFacade.InsertPlantInfo(payInfo);
                return Json(new
                {
                    IsOk = true,
                    Msg = "操作成功，一分钟内生效"
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

        [CheckCustomer]
        public JsonResult EditQudao()
        {
            string text = TypeUtil.ObjectToString(base.Request["qudaoID"]);
            if (string.IsNullOrEmpty(text))
            {
                return Json(new
                {
                    IsOk = false,
                    Msg = "没有选择操作项"
                });
            }
            if (CheckHelper.CheckIds(text))
            {
                try
                {
                    T_PayPlatformInfo payInfo = new T_PayPlatformInfo();
                    payInfo.ID = TypeUtil.ObjectToInt(base.Request["qudaoID"], -1);
                    payInfo.QudaoName = TypeUtil.ObjectToString(base.Request["qudaoName"]);
                    payInfo.PlatformName = TypeUtil.ObjectToString(base.Request["qudaoName"]);
                    payInfo.PlatformCode = TypeUtil.ObjectToString(base.Request["Accnum"]);
                    payInfo.PriKey = TypeUtil.ObjectToString(base.Request["priKey"]);
                    payInfo.PubKey = TypeUtil.ObjectToString(base.Request["pubKey"]);
                    payInfo.Url = TypeUtil.ObjectToString(base.Request["payUrl"]);
                    payInfo.FindUrl = TypeUtil.ObjectToString(base.Request["findUrl"]);
                    payInfo.BackName = TypeUtil.ObjectToString(base.Request["bankName"]);
                    payInfo.BackAcc = TypeUtil.ObjectToString(base.Request["bankAcc"]);
                    payInfo.BackAdd = TypeUtil.ObjectToString(base.Request["bankAdd"]);
                    payInfo.PryType = TypeUtil.ObjectToInt(base.Request["payType"]);
                    payInfo.PlatformID = TypeUtil.ObjectToInt(base.Request["qudaoID"], -1);
                    payInfo.QudaoID = TypeUtil.ObjectToInt(base.Request["qudaoID"], -1);
                    FacadeManage.aideTreasureFacade.UpdatePlantInfo(payInfo);
                    return Json(new
                    {
                        IsOk = true,
                        Msg = "操作成功，一分钟内生效"
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
                Msg = "参数不正确"
            });
        }

		[CheckCustomer]
		public JsonResult Freeze()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有选择操作项"
				});
			}
			if (CheckHelper.CheckIds(text))
			{
				try
				{
					int nullity = TypeUtil.ObjectToInt(base.Request["nullity"]);
					FacadeManage.aideTreasureFacade.Freeze(text, nullity);
					return Json(new
					{
						IsOk = true,
						Msg = "操作成功，一分钟内生效"
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
				Msg = "参数不正确"
			});
		}

		[CheckCustomer]
		public JsonResult UpdateSort()
		{
			int num = TypeUtil.ObjectToInt(base.Request["id"]);
			int sort = TypeUtil.ObjectToInt(base.Request["sort"]);
			if (num <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数不正确"
				});
			}
			FacadeManage.aideTreasureFacade.UpdateSort(num, sort);
			return Json(new
			{
				IsOk = true,
				Msg = "操作成功，一分钟内生效"
			});
		}

		[CheckCustomer]
		public ActionResult PayQudaoList()
		{
			return View();
		}

		[CheckCustomer]
		public JsonResult GetPayQudaoList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 10);
			string orderby = "ORDER BY SortID ASC";
			int num = TypeUtil.ObjectToInt(base.Request["Status"], -1);
			StringBuilder stringBuilder = new StringBuilder(" WHERE 1=1");
			if (num > -1)
			{
				stringBuilder.AppendFormat(" AND IsShow={0}", num);
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("T_PayQudaoInfo", pageIndex, pageSize, stringBuilder.ToString(), orderby);
			return Json(new
			{
				IsOk = true,
				Msg = "",
				Total = list.RecordCount,
				Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
			});
		}

		[CheckCustomer]
		public JsonResult FreezeQudao()
		{
			string text = TypeUtil.ObjectToString(base.Request["cid"]);
			if (string.IsNullOrEmpty(text))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "没有选择操作项"
				});
			}
			if (CheckHelper.CheckIds(text))
			{
				try
				{
					int nullity = TypeUtil.ObjectToInt(base.Request["nullity"]);
					FacadeManage.aideTreasureFacade.FreezeQudao(text, nullity);
					return Json(new
					{
						IsOk = true,
						Msg = "操作成功，一分钟内生效"
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
				Msg = "参数不正确"
			});
		}

		[CheckCustomer]
		public JsonResult UpdateQudaoSort()
		{
			int num = TypeUtil.ObjectToInt(base.Request["id"]);
			int sort = TypeUtil.ObjectToInt(base.Request["sort"]);
			if (num <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数不正确"
				});
			}
			FacadeManage.aideTreasureFacade.UpdateQudaoSort(num, sort);
			return Json(new
			{
				IsOk = true,
				Msg = "操作成功，一分钟内生效"
			});
		}

		[CheckCustomer]
		public ActionResult PayAmountList()
		{
			DataTable payQudaoList = FacadeManage.aideTreasureFacade.GetPayQudaoList();
			return View(payQudaoList);
		}

		[CheckCustomer]
		public JsonResult GetPayAmountList()
		{
			int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
			int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 50);
			int num = TypeUtil.ObjectToInt(base.Request["QudaoId"], 0);
			string orderby = "ORDER BY Limit ASC";
			StringBuilder stringBuilder = new StringBuilder("WHERE 1=1");
			if (num > 0)
			{
				stringBuilder.AppendFormat(" AND QudaoID={0}", num);
			}
			PagerSet list = FacadeManage.aideTreasureFacade.GetList("T_QudaoLimit", pageIndex, pageSize, stringBuilder.ToString(), orderby);
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
		public JsonResult UpdateAmount()
		{
			int num = TypeUtil.ObjectToInt(base.Request["QudaoId"], 0);
			int num2 = TypeUtil.ObjectToInt(base.Request["Amount"], 0);
			int num3 = TypeUtil.ObjectToInt(base.Request["ID"], 0);
			if (num2 <= 0 || num3 < 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			if (num3 == 0 && num <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			int num4 = 0;
			num4 = ((num3 != 0) ? FacadeManage.aideTreasureFacade.UpdateAmount(num2, num3) : FacadeManage.aideTreasureFacade.AddAmount(num, num2));
			if (num4 > 0)
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
		public JsonResult DelAmount()
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
				string sqlWhere = " WHERE ID IN (" + text + ")";
				try
				{
					FacadeManage.aideTreasureFacade.DelAmount(sqlWhere);
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
        public ActionResult OffLinePayList()
        {
          
            DataTable payQudaoList = FacadeManage.aideTreasureFacade.GetPayQudaoOfOffLinePaymentList();
            return View(payQudaoList);
        }

        [CheckCustomer]
        public JsonResult GetOffLinePayList()
        {
            int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
            int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 50);
            int num = TypeUtil.ObjectToInt(base.Request["PaymentType"], 0);
            string orderby = "ORDER BY ApplyDate DESC";
            StringBuilder stringBuilder = new StringBuilder("WHERE 1=1");
            if (num > 0)
            {
                stringBuilder.AppendFormat(" AND PaymentType={0}", num);
            }
            PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_OffLinePayOrders", pageIndex, pageSize, stringBuilder.ToString(), orderby);
            return Json(new
            {
                IsOk = true,
                Msg = "",
                Total = list.RecordCount,
                Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
            });
        }

        [CheckCustomer]
        public JsonResult DelOffLinePay()
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
                string sqlWhere = " WHERE OffLinePayID IN (" + text + ")";
                try
                {
                    FacadeManage.aideTreasureFacade.DelOffLinePay(sqlWhere);
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
           public JsonResult UpdateOffLinePay()
		{
            int num = TypeUtil.ObjectToInt(base.Request["PaymentType"], 0);
            int num2 = TypeUtil.ObjectToInt(base.Request["IsAuded"], 0);
            int num3 = TypeUtil.ObjectToInt(base.Request["OffLinePayID"], 0);
            if (num2 > 0)
            {

                int paymentType = TypeUtil.ObjectToInt(base.Request["PaymentType"], 0);
               // int isAuded = TypeUtil.ObjectToInt(base.Request["IsAuded"], 0);
                int offLinePayID = TypeUtil.ObjectToInt(base.Request["OffLinePayID"], 0);
                int userID = TypeUtil.ObjectToInt(base.Request["UserID"], 0);
                string accounts = TypeUtil.ObjectToString(base.Request["Accounts"]);
                int gameID = TypeUtil.ObjectToInt(base.Request[" GameID"], 0);
                int payAmount = TypeUtil.ObjectToInt(base.Request["PayAmount"], 0);
                int orderID = TypeUtil.ObjectToInt(base.Request["OrderID"], 0);
                string applyDate = TypeUtil.ObjectToString(base.Request["ApplyDate"]);
                FacadeManage.aideTreasureFacade.UpdateGameScoreInfoInsertShareDetailInfo(userID,accounts,gameID,payAmount,applyDate) ;
            }
			if ( num3 < 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
			if (num3 == 0 && num <= 0)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "参数错误"
				});
			}
            int num4 = 0;
            num4 = ((num3 != 0) ? FacadeManage.aideTreasureFacade.UpdateOffLinePay(num2, num3) : 0);
			if (num4 > 0)
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
        public ActionResult OffLineQrCodeList()
        {
          
            DataTable payQudaoList = FacadeManage.aideTreasureFacade.GetPayQudaoOfOffLinePaymentList();
            return View(payQudaoList);
        }

        [CheckCustomer]
        public JsonResult GetOffLineQrCodeList()
        {
            int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
            int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 50);
            int num = TypeUtil.ObjectToInt(base.Request["ID"], 0);
            string orderby = "ORDER BY ID ASC";
            StringBuilder stringBuilder = new StringBuilder("WHERE 1=1");
            if (num > 0)
            {
                stringBuilder.AppendFormat(" AND ID={0}", num);
            }
            PagerSet list = FacadeManage.aideTreasureFacade.GetList("OffLinePayQrCode", pageIndex, pageSize, stringBuilder.ToString(), orderby);
            return Json(new
            {
                IsOk = true,
                Msg = "",
                Total = list.RecordCount,
                Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
            });
        }

         [CheckCustomer]
        public JsonResult GetOffLinePayQrCodeIconPathLimit()
        {
            int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
            int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 50);
            int num = TypeUtil.ObjectToInt(base.Request["ID"], 0);
            string orderby = "ORDER BY ID ASC";
            StringBuilder stringBuilder = new StringBuilder("WHERE 1=1");
            if (num > 0)
            {
                stringBuilder.AppendFormat(" AND ID={0}", num);
            }
            PagerSet list = FacadeManage.aideTreasureFacade.GetList("View_OffLinePayQrCode  ", pageIndex, pageSize, stringBuilder.ToString(), orderby);
            return Json(new
            {
                IsOk = true,
                Msg = "",
                Total = list.RecordCount,
                Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
            });
        }


        [CheckCustomer]
        public JsonResult DelOffLineQrCodeImg()
        {

            string _iconPath = TypeUtil.ObjectToString(base.Request["path"]);
            string iconPath = TypeUtil.GetMapPath("~/Content/Upload/OffLinePayQrCode/" + base.Request["path"]);
            try
            {
                if (_iconPath != "")
                {
                    System.IO.File.Delete(iconPath);
                }

                return Json(new
                {
                    IsOk = true,
                    Msg = "操作成功" + iconPath
                   
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
         public JsonResult DelOffLineQrCode()
        {
            string text = TypeUtil.ObjectToString(base.Request["cid"]);
            string _iconPath = TypeUtil.ObjectToString(base.Request["path"]);
            string iconPath = TypeUtil.GetMapPath("~/Content/Upload/OffLinePayQrCode/" + base.Request["path"]);
         

            // string FileInfo; "~/"
          
          // FileInfo file = new FileInfo(Server.MapPath(iconPath));//指定文件路径
           /* if (file.Exists)//判断文件是否存在
           {
               file.Attributes = FileAttributes.Normal;//将文件属性设置为普通,比方说只读文件设置为普通
               file.Delete();//删除文件
           }
          */
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
                string sqlWhere = " WHERE ID IN (" + text + ")";
                try
                {
                    FacadeManage.aideTreasureFacade.DelOffLineQrCode(sqlWhere);
                    if (_iconPath != "")
                    {
                        System.IO.File.Delete(iconPath);
                    }
                    
                    return Json(new
                    {
                        IsOk = true,
                        Msg = "操作成功"
                       // Path = iconPath
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
         public ActionResult OffLineQrCodeInfo()
         {
             int ID = TypeUtil.ObjectToInt(base.Request["param"]);
             base.ViewBag.ID = ID;
             base.ViewData["data"] = null;
             if (ID > 0)
             {
                 OffLineQrCode offLineQrCode = FacadeManage.aideTreasureFacade.GetOffLineQrCode(ID);
                 base.ViewData["data"] = offLineQrCode;
             }
             return View();
         }

         [CheckCustomer]
         [ValidateInput(false)]
         [AntiSqlInjection]
         public JsonResult DoOffLineQrCodeInfo(OffLineQrCode entity)
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
             if (!string.IsNullOrEmpty(entity.IconPath))
             {
                 if (entity.ID >= 1)
                 {
                    
                     try
                     {
                         FacadeManage.aideTreasureFacade.UpdateOffLineQrCode(entity);
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
                     FacadeManage.aideTreasureFacade.AddOffLineQrCode(entity);
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

         //[CheckCustomer]
         //public JsonResult AddOffLineQrCode()
         //{
           
         //    int PaymentTypeID = TypeUtil.ObjectToInt(base.Request["PaymentTypeID"], 0);
         //    int OwnerID = TypeUtil.ObjectToInt(base.Request["OwnerID"], 0);
         //    string PaymentName = TypeUtil.ObjectToString(base.Request["PaymentName"]);

         //    if (PaymentTypeID <= 0 || OwnerID < 0)
         //    {
         //        return Json(new
         //        {
         //            IsOk = false,
         //            Msg = "参数错误"
         //        });
         //    }
         //    if (PaymentName == "")
         //    {
         //        return Json(new
         //        {
         //            IsOk = false,
         //            Msg = "参数错误"
         //        });
         //    }
         //    int num4 = 0;
         //    num4 = ((PaymentName != "") ? FacadeManage.aideTreasureFacade.AddOffLineQrCode(PaymentTypeID, OwnerID, PaymentName) : 0);
         //    if (num4 > 0)
         //    {
         //        return Json(new
         //        {
         //            IsOk = true,
         //            Msg = "保存成功"
         //        });
         //    }
         //    return Json(new
         //    {
         //        IsOk = false,
         //        Msg ="保存失败"
         //    });
         //}



        //[CheckCustomer]
        // public JsonResult UpdateOffLineQrCode()
        //{
        //    int ID = TypeUtil.ObjectToInt(base.Request["ID"], 0);
        //    int PaymentTypeID = TypeUtil.ObjectToInt(base.Request["PaymentTypeID"], 0);
        //    int OwnerID = TypeUtil.ObjectToInt(base.Request["OwnerID"], 0);
        //    string PaymentName = TypeUtil.ObjectToString(base.Request["PaymentName"]);

        //    string IconPath = TypeUtil.ObjectToString(base.Request["IconPath"]);
        //    if (PaymentTypeID <= 0 || OwnerID < 0)
        //    {
        //        return Json(new
        //        {
        //            IsOk = false,
        //            Msg = "参数错误"
        //        });
        //    }
        //    if (PaymentName == "")
        //    {
        //        return Json(new
        //        {
        //            IsOk = false,
        //            Msg = "参数错误"
        //        });
        //    }
        //    int num4 = 0;
        //    num4 = ((PaymentName != "") ? FacadeManage.aideTreasureFacade.UpdateOffLineQrCode(PaymentTypeID, OwnerID, PaymentName, IconPath, ID) : 0);
        //    if (num4 > 0)
        //    {
        //        return Json(new
        //        {
        //            IsOk = true,
        //            Msg = "保存成功"
        //        });
        //    }
        //    return Json(new
        //    {
        //        IsOk = false,
        //        Msg = "保存失败"
        //    });
        //}
        /*
        [CheckCustomer]
        public ActionResult PayAmountList()
        {
            DataTable payQudaoList = FacadeManage.aideTreasureFacade.GetPayQudaoList();
            return View(payQudaoList);
        }

        [CheckCustomer]
        public JsonResult GetPayAmountList()
        {
            int pageIndex = TypeUtil.ObjectToInt(base.Request["pageIndex"], 1);
            int pageSize = TypeUtil.ObjectToInt(base.Request["pageSize"], 50);
            int num = TypeUtil.ObjectToInt(base.Request["QudaoId"], 0);
            string orderby = "ORDER BY Limit ASC";
            StringBuilder stringBuilder = new StringBuilder("WHERE 1=1");
            if (num > 0)
            {
                stringBuilder.AppendFormat(" AND QudaoID={0}", num);
            }
            PagerSet list = FacadeManage.aideTreasureFacade.GetList("T_QudaoLimit", pageIndex, pageSize, stringBuilder.ToString(), orderby);
            return Json(new
            {
                IsOk = true,
                Msg = "",
                Total = list.RecordCount,
                Data = JsonHelper.SerializeObject(list.PageSet.Tables[0])
            });
        }
        */
	}
}
