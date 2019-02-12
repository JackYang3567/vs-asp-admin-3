using Game.Entity.Enum;
using Game.Entity.Platform;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
	public static class HtmlCreater
	{
		public static MvcHtmlString HtmlSelectYearBuilder(this HtmlHelper helper, string y = "")
		{
			string text = "";
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlSelectYear");
			tagBuilder.Attributes.Add("class", "form-control input-sm");
			int num = (TypeUtil.ObjectToInt(y) <= 1000) ? DateTime.Now.Year : TypeUtil.ObjectToInt(y);
			for (int i = 2007; i < num + 1; i++)
			{
				TagBuilder tagBuilder2 = new TagBuilder("option");
				tagBuilder2.Attributes.Add("value", i.ToString().Trim());
				tagBuilder2.InnerHtml = i.ToString() + "年";
				if (i == num)
				{
					tagBuilder2.Attributes.Add("selected", "selected");
				}
				TagBuilder tagBuilder3 = tagBuilder;
				tagBuilder3.InnerHtml += tagBuilder2.ToString();
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlSelectMonthBuilder(this HtmlHelper helper, string m = "")
		{
			string text = "";
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlSelectMonth");
			tagBuilder.Attributes.Add("class", "form-control input-sm");
			int num = -1;
			switch (m)
			{
			case "01":
				num = 1;
				break;
			case "02":
				num = 2;
				break;
			case "03":
				num = 3;
				break;
			case "04":
				num = 4;
				break;
			case "05":
				num = 5;
				break;
			case "06":
				num = 6;
				break;
			case "07":
				num = 7;
				break;
			case "08":
				num = 8;
				break;
			case "09":
				num = 9;
				break;
			case "10":
				num = 10;
				break;
			case "11":
				num = 11;
				break;
			case "12":
				num = 12;
				break;
			}
			int num2 = (num == -1) ? DateTime.Now.Month : num;
			string text2 = "";
			for (int i = 1; i < 13; i++)
			{
				text2 = i.ToString().PadLeft(2, '0');
				TagBuilder tagBuilder2 = new TagBuilder("option");
				tagBuilder2.Attributes.Add("value", text2);
				tagBuilder2.InnerHtml = text2 + "月";
				if (i == num2)
				{
					tagBuilder2.Attributes.Add("selected", "selected");
				}
				TagBuilder tagBuilder3 = tagBuilder;
				tagBuilder3.InnerHtml += tagBuilder2.ToString();
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlUserRoleSelectBuilder(this HtmlHelper helper, int selectValue = 0)
		{
			string text = "";
			PagerSet roleList = FacadeManage.aidePlatformManagerFacade.GetRoleList(1, 2147483647, "", "ORDER BY RoleID ASC");
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlRole");
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-150");
			if (roleList != null && roleList.PageSet != null && roleList.PageSet.Tables.Count > 0)
			{
				foreach (DataRow row in roleList.PageSet.Tables[0].Rows)
				{
					TagBuilder tagBuilder2 = new TagBuilder("option");
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["RoleID"]));
					if (TypeUtil.ObjectToInt(row["RoleID"]) == selectValue)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["RoleName"]);
					TagBuilder tagBuilder3 = tagBuilder;
					tagBuilder3.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlTaskTypeSelectBuilder(this HtmlHelper helper, int selectValue = 0)
		{
			string text = "";
			Dictionary<string, string> dictionary = TypeUtil.EnumToDictionary(typeof(EnumerationList.TaskType));
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlTaskType");
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-150");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "0");
			tagBuilder2.InnerHtml = "==请选择任务类型==";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			if (dictionary != null && dictionary.Count > 0)
			{
				foreach (KeyValuePair<string, string> item in dictionary)
				{
					tagBuilder2 = new TagBuilder("option");
					tagBuilder2.Attributes.Add("value", item.Value);
					if (TypeUtil.ObjectToInt(item.Value) == selectValue)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.InnerHtml = item.Key;
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlGameKindSelectBuilder(this HtmlHelper helper, int KindID = -1)
		{
			string text = "";
			PagerSet gameKindItemList = FacadeManage.aidePlatformFacade.GetGameKindItemList(1, 2147483647, "", "ORDER BY KindID ASC");
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlGameKind");
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-150");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "-1");
			tagBuilder2.InnerHtml = "==请选择所属游戏==";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			if (gameKindItemList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in gameKindItemList.PageSet.Tables[0].Rows)
				{
					tagBuilder2 = new TagBuilder("option");
					if (TypeUtil.ObjectToInt(row["KindID"]) == KindID)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["KindID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["KindName"]);
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlGameRulesSelectList(this HtmlHelper helper, int KindID = -1)
		{
			PagerSet gameKindItemList = FacadeManage.aidePlatformFacade.GetGameKindItemList(1, 2147483647, "WHERE KindID NOT IN(SELECT KindID FROM RYNativeWebDB.dbo.GameRulesInfo)", "ORDER BY KindID ASC");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<select id='ddlGameKind' class='form-control input-sm input-width-150'>");
			stringBuilder.Append("<option value='-1'>==请选择所属游戏==</option>");
			if (gameKindItemList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in gameKindItemList.PageSet.Tables[0].Rows)
				{
					if (TypeUtil.ObjectToInt(row["KindID"]) == KindID)
					{
						stringBuilder.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", row["KindID"], row["KindName"]);
					}
					else
					{
						stringBuilder.AppendFormat("<option value='{0}'>{1}</option>", row["KindID"], row["KindName"]);
					}
				}
			}
			stringBuilder.Append("</select>");
			return new MvcHtmlString(stringBuilder.ToString());
		}

		public static MvcHtmlString HtmlRecordTypeSelectBuilder(this HtmlHelper helper, string table, int Id = -1)
		{
			string text = "";
			DataSet recordType = FacadeManage.aideTreasureFacade.GetRecordType(table);
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlRecordType");
			tagBuilder.Attributes.Add("class", "form-control input-sm");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "0");
			tagBuilder2.InnerHtml = "==请选择金币变化类型==";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			if (recordType.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in recordType.Tables[0].Rows)
				{
					tagBuilder2 = new TagBuilder("option");
					if (TypeUtil.ObjectToInt(row["ID"]) == Id)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["ID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["TypeName"]);
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlGameRoomSelectBuilder(this HtmlHelper helper, int ServerID = -1)
		{
			string text = "";
			PagerSet gameRoomInfoList = FacadeManage.aidePlatformFacade.GetGameRoomInfoList(1, 2147483647, "", "ORDER BY ServerID ASC");
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlGameRoom");
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-150");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "-2");
			tagBuilder2.InnerHtml = "积分房间";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "-1");
			tagBuilder2.InnerHtml = "游戏币房间";
			TagBuilder tagBuilder4 = tagBuilder;
			tagBuilder4.InnerHtml += tagBuilder2.ToString();
			if (gameRoomInfoList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in gameRoomInfoList.PageSet.Tables[0].Rows)
				{
					tagBuilder2 = new TagBuilder("option");
					if (TypeUtil.ObjectToInt(row["ServerID"]) == ServerID)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["ServerID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["ServerName"]);
					TagBuilder tagBuilder5 = tagBuilder;
					tagBuilder5.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlServerSelectBuilder(this HtmlHelper helper, int ServerID = -1)
		{
			string text = "";
			PagerSet gameRoomInfoList = FacadeManage.aidePlatformFacade.GetGameRoomInfoList(1, 2147483647, "WHERE ServerType=1", "ORDER BY GameID ASC");
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlServerID");
			tagBuilder.Attributes.Add("class", "form-control input-sm");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "0");
			tagBuilder2.InnerHtml = "全部金币房间";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			if (gameRoomInfoList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in gameRoomInfoList.PageSet.Tables[0].Rows)
				{
					tagBuilder2 = new TagBuilder("option");
					if (TypeUtil.ObjectToInt(row["ServerID"]) == ServerID)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["ServerID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["ServerName"]);
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlGlobalShareInfoSelectBuilder(this HtmlHelper helper)
		{
			string text = "";
			PagerSet globalShareList = FacadeManage.aideTreasureFacade.GetGlobalShareList(1, 1000, "", "ORDER BY ShareID ASC");
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlGlobalShareInfo");
			tagBuilder.Attributes.Add("class", "form-control input-sm");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "0");
			tagBuilder2.InnerHtml = "全部服务";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			if (globalShareList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in globalShareList.PageSet.Tables[0].Rows)
				{
					tagBuilder2 = new TagBuilder("option");
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["ShareID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["ShareName"]);
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlPayPlatform(this HtmlHelper helper)
		{
			string text = "";
			DataTable dataTable = new DataTable();
			if (HttpRuntime.Cache["PayPlatform"] == null)
			{
				dataTable = FacadeManage.aideTreasureFacade.GetDataSetBySql("SELECT ID,PlatformName FROM T_PayPlatformInfo").Tables[0];
				CacheHelper.AddCache("PayPlatform", dataTable);
			}
			else
			{
				dataTable = (HttpRuntime.Cache["PayPlatform"] as DataTable);
			}
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlPayPlatform");
			tagBuilder.Attributes.Add("class", "form-control input-sm");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "0");
			tagBuilder2.InnerHtml = "请选择";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					tagBuilder2 = new TagBuilder("option");
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["ID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["PlatformName"]);
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlPayQudao(this HtmlHelper helper)
		{
			string text = "";
			DataTable dataTable = new DataTable();
			if (HttpRuntime.Cache["PayQudao"] == null)
			{
				dataTable = FacadeManage.aideTreasureFacade.GetDataSetBySql("SELECT ID,QudaoName FROM T_PayQudaoInfo").Tables[0];
				CacheHelper.AddCache("PayQudao", dataTable);
			}
			else
			{
				dataTable = (HttpRuntime.Cache["PayQudao"] as DataTable);
			}
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlPayQudao");
			tagBuilder.Attributes.Add("class", "form-control input-sm");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "0");
			tagBuilder2.InnerHtml = "请选择";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow row in dataTable.Rows)
				{
					tagBuilder2 = new TagBuilder("option");
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["ID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["QudaoName"]);
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlKindAndServerBuilder(this HtmlHelper helper, string Servers)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<ul><li>");
			if (Servers != "0")
			{
				stringBuilder.Append("<label class=\"one-checkbox\" for=\"all\"><input type=\"checkbox\" onclick=\"allFn(this,this.checked)\" value=\"-1\"/>游戏列表</label>");
				string[] array = Servers.Split(',');
				DataSet pageSet = FacadeManage.aidePlatformFacade.GetGameKindItemList(1, 2147483647, "WHERE Nullity=0", "ORDER BY KindID ASC").PageSet;
				if (pageSet.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row in pageSet.Tables[0].Rows)
					{
						int num = Convert.ToInt32(row["GameID"]);
						DataSet pageSet2 = FacadeManage.aidePlatformFacade.GetGameRoomInfoList(1, 2147483647, string.Format("WHERE GameID={0} AND Nullity=0", num), "ORDER BY ServerID ASC").PageSet;
						if (pageSet2.Tables[0].Rows.Count > 0)
						{
							stringBuilder.Append("<ul class=\"two-checkbox\"><li>");
							int num2 = 0;
							StringBuilder stringBuilder2 = new StringBuilder();
							stringBuilder2.Append("<ul class=\"three-checkbox\"><li>");
							foreach (DataRow row2 in pageSet2.Tables[0].Rows)
							{
								bool flag = false;
								for (int i = 0; i < array.Length; i++)
								{
									if (!(array[i].Trim() == "") && array[i].Trim() == row2["ServerID"].ToString().Trim())
									{
										flag = true;
										num2++;
										break;
									}
								}
								stringBuilder2.AppendFormat("<label class=\"three-checkbox\"><input type=\"checkbox\" name=\"threecheckbox\" value=\"{1}\" onclick=\"allFn(this,this.checked)\" {2}>{0}</label>", row2["ServerName"].ToString(), row2["ServerID"].ToString().Trim(), flag ? "checked=\"checked\"" : "");
							}
							stringBuilder2.Append("</li></ul>");
							stringBuilder.AppendFormat("<label class=\"two-checkbox-list\" for=\"twoCheck\"><input type=\"checkbox\" value=\"\" onclick=\"allFn(this,this.checked)\" {1}>{0}</label>", row["KindName"].ToString(), (num2 == pageSet2.Tables[0].Rows.Count && num2 != 0) ? "checked=\"checked\"" : "");
							if (!string.IsNullOrEmpty(stringBuilder2.ToString()))
							{
								stringBuilder.Append(stringBuilder2.ToString());
							}
							stringBuilder.Append("</li></ul>");
						}
					}
				}
			}
			else
			{
				stringBuilder.Append("<label class=\"one-checkbox\" for=\"all\"><input type=\"checkbox\" onclick=\"allFn(this,this.checked)\" checked=\"checked\" value=\"-1\"/>游戏列表</label>");
				DataSet pageSet3 = FacadeManage.aidePlatformFacade.GetGameKindItemList(1, 2147483647, "WHERE Nullity=0", "ORDER BY KindID ASC").PageSet;
				if (pageSet3.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row3 in pageSet3.Tables[0].Rows)
					{
						stringBuilder.Append("<ul class=\"two-checkbox\"><li>");
						stringBuilder.AppendFormat("<label class=\"two-checkbox-list\" for=\"twoCheck\"><input type=\"checkbox\" value=\"\" onclick=\"allFn(this,this.checked)\" checked=\"checked\">{0}</label>", row3["KindName"].ToString());
						int num3 = Convert.ToInt32(row3["KindID"]);
						DataSet pageSet4 = FacadeManage.aidePlatformFacade.GetGameRoomInfoList(1, 2147483647, string.Format("WHERE KindID={0} AND Nullity=0", num3), "ORDER BY ServerID ASC").PageSet;
						if (pageSet4.Tables[0].Rows.Count != 0)
						{
							foreach (DataRow row4 in pageSet4.Tables[0].Rows)
							{
								stringBuilder.Append("<ul class=\"three-checkbox\"><li>");
								stringBuilder.AppendFormat("<label class=\"three-checkbox\"><input type=\"checkbox\" value=\"{1}\" onclick=\"allFn(this,this.checked)\" checked=\"checked\">{0}</label>", row4["ServerName"].ToString(), row4["ServerID"].ToString().Trim());
								stringBuilder.Append("</li></ul>");
							}
							stringBuilder.Append("</li></ul>");
						}
					}
				}
			}
			stringBuilder.Append("</li></ul>");
			return new MvcHtmlString(stringBuilder.ToString());
		}

		public static MvcHtmlString HtmlDataBaseAddrSelectBuilder(this HtmlHelper helper, string addr = "")
		{
			string text = "";
			PagerSet dataBaseList = FacadeManage.aidePlatformFacade.GetDataBaseList(1, 2147483647, "", "ORDER BY DBInfoID ASC");
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlDataBaseAddr");
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-200");
			if (dataBaseList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in dataBaseList.PageSet.Tables[0].Rows)
				{
					TagBuilder tagBuilder2 = new TagBuilder("option");
					if (TypeUtil.ObjectToString(row["DBAddr"]) == addr)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["DBAddr"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["Information"]);
					TagBuilder tagBuilder3 = tagBuilder;
					tagBuilder3.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlGameSelectBuilder(this HtmlHelper helper, int ID = 0)
		{
			string text = "";
			DataSet gameList = FacadeManage.aidePlatformFacade.GetGameList();
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlGame");
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-150");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "0");
			tagBuilder2.InnerHtml = "请选择";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			if (gameList.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in gameList.Tables[0].Rows)
				{
					tagBuilder2 = new TagBuilder("option");
					if (TypeUtil.ObjectToInt(row["KindID"]) == ID)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["KindID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["KindName"]);
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlChangeTypeSelectBuilder(this HtmlHelper html, int selectedValue = 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<select name='changeType' id='changeType'>");
			stringBuilder.Append("<option value='0'>全部</option>");
			DataTable dataTable = FacadeManage.aideAccountsFacade.AgentLogType();
			foreach (DataRow row in dataTable.Rows)
			{
				if (Convert.ToInt32(row["ID"]) == selectedValue && selectedValue != 0)
				{
					stringBuilder.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", row["ID"], row["Name"]);
				}
				else
				{
					stringBuilder.AppendFormat("<option value='{0}'>{1}</option>", row["ID"], row["Name"]);
				}
			}
			stringBuilder.Append("</select>");
			return MvcHtmlString.Create(stringBuilder.ToString());
		}

		public static MvcHtmlString HtmlPhoneTypeSelectBuilder(this HtmlHelper helper, string tagID, int ID = 0)
		{
			string text = "";
			PagerSet gamePropertyTypeList = FacadeManage.aidePlatformFacade.GetGamePropertyTypeList(1, 2147483647, "Where Nullity=0", "ORDER BY TypeID ASC");
			DataSet pageSet = gamePropertyTypeList.PageSet;
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", tagID);
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-150");
			if (pageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in pageSet.Tables[0].Rows)
				{
					TagBuilder tagBuilder2 = new TagBuilder("option");
					if (TypeUtil.ObjectToInt(row["TypeID"]) == ID)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["TypeID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["TypeName"]);
					TagBuilder tagBuilder3 = tagBuilder;
					tagBuilder3.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlGamePropertySelectBuilder(this HtmlHelper helper, string tagID, int ID = 0)
		{
			string text = "";
			PagerSet gamePropertyList = FacadeManage.aidePlatformFacade.GetGamePropertyList(1, 2147483647, string.Format("WHERE Kind<>{0}", 11), "ORDER BY ID ASC");
			DataSet pageSet = gamePropertyList.PageSet;
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", tagID);
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-150");
			if (pageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in pageSet.Tables[0].Rows)
				{
					TagBuilder tagBuilder2 = new TagBuilder("option");
					if (TypeUtil.ObjectToInt(row["ID"]) == ID)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["ID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["Name"]);
					TagBuilder tagBuilder3 = tagBuilder;
					tagBuilder3.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlCardTypeSelectBuilder(this HtmlHelper helper)
		{
			string text = "";
			PagerSet globalLivcardList = FacadeManage.aideTreasureFacade.GetGlobalLivcardList(1, 1000, "", "ORDER BY CardTypeID ASC");
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlCardType");
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-150");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "0");
			tagBuilder2.InnerHtml = "全部卡片";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			if (globalLivcardList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in globalLivcardList.PageSet.Tables[0].Rows)
				{
					tagBuilder2 = new TagBuilder("option");
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["CardTypeID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["CardName"]);
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlShareIDSelectBuilder(this HtmlHelper helper, int ID = 0)
		{
			string text = "";
			PagerSet globalShareList = FacadeManage.aideTreasureFacade.GetGlobalShareList(1, 2147483647, "", "ORDER BY ShareID ASC");
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlShareID");
			tagBuilder.Attributes.Add("class", "form-control input-sm");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "0");
			tagBuilder2.InnerHtml = "全部服务";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			if (globalShareList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in globalShareList.PageSet.Tables[0].Rows)
				{
					tagBuilder2 = new TagBuilder("option");
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["ShareID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["ShareName"]);
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlGameIDSelectBuilder(this HtmlHelper helper)
		{
			string text = "";
			DataSet reserveIdentifierList = FacadeManage.aideAccountsFacade.GetReserveIdentifierList();
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlGameID");
			tagBuilder.Attributes.Add("class", "form-control input-sm");
			if (reserveIdentifierList.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in reserveIdentifierList.Tables[0].Rows)
				{
					TagBuilder tagBuilder2 = new TagBuilder("option");
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["GameID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["GameID"]);
					TagBuilder tagBuilder3 = tagBuilder;
					tagBuilder3.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlMemberTypeSelectBuilder(this HtmlHelper helper)
		{
			string text = "";
			DataSet memberPropertyList = FacadeManage.aideAccountsFacade.GetMemberPropertyList();
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlMemberType");
			if (memberPropertyList.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in memberPropertyList.Tables[0].Rows)
				{
					TagBuilder tagBuilder2 = new TagBuilder("option");
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["MemberOrder"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["MemberName"]);
					TagBuilder tagBuilder3 = tagBuilder;
					tagBuilder3.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlDayGiftSelectBuilder(this HtmlHelper helper, int Id = 0)
		{
			string text = "";
			IList<GameProperty> gamePropertyGift = FacadeManage.aidePlatformFacade.GetGamePropertyGift(11);
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlDayGiftID");
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-200");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "0");
			tagBuilder2.InnerHtml = "无礼包";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			if (gamePropertyGift.Count > 0)
			{
				foreach (GameProperty item in gamePropertyGift)
				{
					tagBuilder2 = new TagBuilder("option");
					if (item.ID == Id)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(item.ID));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(item.Name);
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlMemberOrderSelectBuilder(this HtmlHelper helper)
		{
			string text = "";
			IList<EnumDescription> memberOrderStatusList = MemberOrderHelper.GetMemberOrderStatusList(typeof(MemberOrderStatus));
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlMemberOrder");
			tagBuilder.Attributes.Add("class", "form-control input-sm");
			if (memberOrderStatusList != null && memberOrderStatusList.Count > 0)
			{
				foreach (EnumDescription item in memberOrderStatusList)
				{
					TagBuilder tagBuilder2 = new TagBuilder("option");
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(item.EnumValue));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(item.Description);
					TagBuilder tagBuilder3 = tagBuilder;
					tagBuilder3.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlGameTypeSelectBuilder(this HtmlHelper helper, int typeID = 0)
		{
			string text = "";
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlTypeID");
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-150");
			PagerSet gameTypeItemList = FacadeManage.aidePlatformFacade.GetGameTypeItemList(1, 2147483647, "Where Nullity=0", "ORDER BY TypeID ASC");
			if (gameTypeItemList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in gameTypeItemList.PageSet.Tables[0].Rows)
				{
					TagBuilder tagBuilder2 = new TagBuilder("option");
					if (TypeUtil.ObjectToInt(row["TypeID"]) == typeID)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["TypeID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["TypeName"]);
					TagBuilder tagBuilder3 = tagBuilder;
					tagBuilder3.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlGameMoudelSelectBuilder(this HtmlHelper helper, int gameID = 0)
		{
			string text = "";
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlGameID");
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-150");
			PagerSet gameGameItemList = FacadeManage.aidePlatformFacade.GetGameGameItemList(1, 2147483647, "", "ORDER BY GameID ASC");
			if (gameGameItemList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in gameGameItemList.PageSet.Tables[0].Rows)
				{
					TagBuilder tagBuilder2 = new TagBuilder("option");
					if (TypeUtil.ObjectToInt(row["GameID"]) == gameID)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["GameID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["GameName"]);
					TagBuilder tagBuilder3 = tagBuilder;
					tagBuilder3.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlGameJoinSelectBuilder(this HtmlHelper helper, int kindID = 0)
		{
			string text = "";
			TagBuilder tagBuilder = new TagBuilder("select");
			tagBuilder.Attributes.Add("id", "ddlJoin");
			tagBuilder.Attributes.Add("class", "form-control input-sm input-width-150");
			TagBuilder tagBuilder2 = new TagBuilder("option");
			tagBuilder2.Attributes.Add("value", "0");
			tagBuilder2.InnerHtml = "无挂接";
			TagBuilder tagBuilder3 = tagBuilder;
			tagBuilder3.InnerHtml += tagBuilder2.ToString();
			PagerSet gameKindItemList = FacadeManage.aidePlatformFacade.GetGameKindItemList(1, 2147483647, "", "ORDER BY KindID ASC");
			if (gameKindItemList.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in gameKindItemList.PageSet.Tables[0].Rows)
				{
					tagBuilder2 = new TagBuilder("option");
					if (TypeUtil.ObjectToInt(row["KindID"]) == kindID)
					{
						tagBuilder2.Attributes.Add("selected", "selected");
					}
					tagBuilder2.Attributes.Add("value", TypeUtil.ObjectToString(row["KindID"]));
					tagBuilder2.InnerHtml = TypeUtil.ObjectToString(row["KindName"]);
					TagBuilder tagBuilder4 = tagBuilder;
					tagBuilder4.InnerHtml += tagBuilder2.ToString();
				}
			}
			text = tagBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlMemberOrderCHKListBuilder(this HtmlHelper helper, string[] array = null)
		{
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			IList<EnumDescription> memberOrderStatusList = MemberOrderHelper.GetMemberOrderStatusList(typeof(MemberOrderStatus));
			if (memberOrderStatusList != null && memberOrderStatusList.Count > 0)
			{
				stringBuilder.Append("<p class=\"checkbox-radio\">");
				for (int i = 0; i < memberOrderStatusList.Count; i++)
				{
					EnumDescription enumDescription = memberOrderStatusList[i];
					bool flag = array != null && array.Contains(enumDescription.EnumValue.ToString());
					stringBuilder.AppendFormat("<span class=\"break-word\"><input id=\"ckbMemberOrder{0}\" type=\"checkbox\" name=\"ckbMemberOrder\" value=\"{0}\" {2}/> <label for=\"ckbMemberOrder{0}\">{1}</label></span>", enumDescription.EnumValue, enumDescription.Description, flag ? "checked=\"checked\"" : "");
				}
				stringBuilder.Append("</p>");
			}
			text = stringBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlUserRightCHKListBuilder(this HtmlHelper helper, int intUserRight = -1)
		{
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			IList<EnumDescription> userRightList = UserRightHelper.GetUserRightList(typeof(UserRightStatus));
			if (userRightList != null && userRightList.Count > 0)
			{
				stringBuilder.Append("<p class=\"checkbox-radio\">");
				for (int i = 0; i < userRightList.Count; i++)
				{
					EnumDescription enumDescription = userRightList[i];
					bool flag = intUserRight != -1 && enumDescription.EnumValue == (enumDescription.EnumValue & intUserRight);
					stringBuilder.AppendFormat("<span class=\"break-word\"><input id=\"ckbUserRight{0}\" type=\"checkbox\" name=\"ckbUserRight\" value=\"{0}\" {2}/> <label for=\"ckbUserRight{0}\">{1}</label></span>", enumDescription.EnumValue, enumDescription.Description, flag ? "checked=\"checked\"" : "");
				}
				stringBuilder.Append("</p>");
			}
			text = stringBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlPayTypeCHKListBuilder(this HtmlHelper helper, int intPayType = -1)
		{
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			IList<EnumDescription> userRightList = UserRightHelper.GetUserRightList(typeof(PayType));
			if (userRightList != null && userRightList.Count > 0)
			{
				stringBuilder.Append("<ul>");
				for (int i = 0; i < userRightList.Count; i++)
				{
					EnumDescription enumDescription = userRightList[i];
					bool flag = intPayType != -1 && enumDescription.EnumValue == (enumDescription.EnumValue & intPayType);
					stringBuilder.AppendFormat("<li><input type=\"checkbox\" name=\"ckbPayType\" value=\"{0}\" {2}/> <label>{1}</label></li>", enumDescription.EnumValue, enumDescription.Description, flag ? "checked=\"checked\"" : "");
				}
				stringBuilder.Append("</ul>");
			}
			text = stringBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlKindsDataTreeBuilder(this HtmlHelper helper, string Server)
		{
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			IList<EnumDescription> userRightList = UserRightHelper.GetUserRightList(typeof(UserRightStatus));
			if (userRightList != null && userRightList.Count > 0)
			{
				stringBuilder.Append("<p class=\"checkbox-radio\">");
				for (int i = 0; i < userRightList.Count; i++)
				{
					EnumDescription enumDescription = userRightList[i];
					bool flag = true;
					stringBuilder.AppendFormat("<span class=\"break-word\"><input id=\"ckbUserRight{0}\" type=\"checkbox\" name=\"ckbUserRight\" value=\"{0}\" {2}/> <label for=\"ckbUserRight{0}\">{1}</label></span>", enumDescription.EnumValue, enumDescription.Description, flag ? "checked=\"checked\"" : "");
				}
				stringBuilder.Append("</p>");
			}
			text = stringBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlSupporTypeCHKListBuilder(this HtmlHelper helper, int intType = -1)
		{
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			IList<EnumDescription> supporTypeList = SupporTypeHelper.GetSupporTypeList(typeof(SupporTypeStatus));
			if (supporTypeList != null && supporTypeList.Count > 0)
			{
				stringBuilder.Append("<p class=\"checkbox-radio\">");
				for (int i = 0; i < supporTypeList.Count; i++)
				{
					EnumDescription enumDescription = supporTypeList[i];
					bool flag = intType != -1 && enumDescription.EnumValue == (enumDescription.EnumValue & intType);
					stringBuilder.AppendFormat("<span class=\"break-word\"><input id=\"ckbSupporType{0}\" type=\"checkbox\" name=\"ckbSupporType\" value=\"{0}\" {2}/> <label for=\"ckbSupporType{0}\">{1}</label></span>", enumDescription.EnumValue, enumDescription.Description, flag ? "checked=\"checked\"" : "");
				}
				stringBuilder.Append("</p>");
			}
			text = stringBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlMasterRightCHKListBuilder(this HtmlHelper helper, int intMasterRight = -1)
		{
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			IList<EnumDescription> masterRightList = MasterRightHelper.GetMasterRightList(typeof(MasterRightStatus));
			if (masterRightList != null && masterRightList.Count > 0)
			{
				stringBuilder.Append("<p class=\"checkbox-radio\">");
				for (int i = 0; i < masterRightList.Count; i++)
				{
					EnumDescription enumDescription = masterRightList[i];
					bool flag = intMasterRight != -1 && enumDescription.EnumValue == (enumDescription.EnumValue & intMasterRight);
					stringBuilder.AppendFormat("<span class=\"break-word\"><input id=\"ckbMasterRight{0}\" type=\"checkbox\" name=\"ckbMasterRight\" value=\"{0}\" {2}/><label for=\"ckbMasterRight{0}\">{1}</label></span>", enumDescription.EnumValue, enumDescription.Description, flag ? "checked=\"checked\"" : "");
				}
				stringBuilder.Append("</p>");
			}
			text = stringBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlIssueAreaCHKListBuilder(this HtmlHelper helper, string tagName, int ID = -1)
		{
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			IList<EnumDescription> issueAreaList = IssueAreaHelper.GetIssueAreaList(typeof(IssueArea));
			if (issueAreaList != null && issueAreaList.Count > 0)
			{
				stringBuilder.Append("<p class=\"checkbox-radio\">");
				for (int i = 0; i < issueAreaList.Count; i++)
				{
					EnumDescription enumDescription = issueAreaList[i];
					bool flag = ID != -1 && enumDescription.EnumValue == (enumDescription.EnumValue & ID);
					stringBuilder.AppendFormat("<span class=\"break-word\"><input type=\"checkbox\" name=\"{3}\" value=\"{0}\" {2}/><label>{1}</label></span>", enumDescription.EnumValue, enumDescription.Description, flag ? "checked=\"checked\"" : "", tagName);
				}
				stringBuilder.Append("</p>");
			}
			text = stringBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlWebConfigMuneBuilder(this HtmlHelper helper, int IntParam = 0)
		{
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			NativeWebFacade nativeWebFacade = new NativeWebFacade();
			PagerSet list = nativeWebFacade.GetList("ConfigInfo", 1, 10000, "WHERE Nullity=0", " ORDER BY ConfigID ASC");
			stringBuilder.Append("<ul class=\"media nav nav-tabs margin-b-10\">");
			if (list.PageSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in list.PageSet.Tables[0].Rows)
				{
					stringBuilder.AppendFormat("<li {0}><a href=\"/Web/SiteConfig?param={1}&title={2}\" title=\"{2}\">{2}</a></li>", (IntParam == TypeUtil.ObjectToInt(row["ConfigID"])) ? "class=\"active\"" : "", TypeUtil.ObjectToInt(row["ConfigID"]), TypeUtil.ObjectToString(row["ConfigName"]));
				}
			}
			stringBuilder.AppendFormat("<li {0}><a href=\"/Web/IOSConfig?param={1}&title={2}\" title=\"{2}\">{2}</a></li>", (IntParam == -1) ? "class=\"active\"" : "", -1, "IOS内购配置");
			stringBuilder.Append("</ul>");
			text = stringBuilder.ToString();
			return new MvcHtmlString(text);
		}

		public static MvcHtmlString HtmlGameConfigMuneBuilder(this HtmlHelper helper, string tag = "a")
		{
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<ul class=\"media nav nav-tabs margin-b-10\">");
			stringBuilder.AppendFormat("<li {0}><a href=\"/App/GameGameItemList?tag={1}&title={2}\" title=\"模块\">模块</a></li>", (tag == "a") ? "class=\"active\"" : "", "a", "游戏管理");
			stringBuilder.AppendFormat("<li {0}><a href=\"/App/GameKindItemList?tag={1}&title={2}\" title=\"游戏\">游戏</a></li>", (tag == "c") ? "class=\"active\"" : "", "c", "游戏管理");
			stringBuilder.AppendFormat("<li {0}><a href=\"/App/GameTypeItemList?tag={1}&title={2}\" title=\"类型\">类型</a></li>", (tag == "b") ? "class=\"active\"" : "", "c", "游戏类型管理");
			stringBuilder.AppendFormat("<li {0}><a href=\"/App/MobileKindList?tag={1}&title={2}\" title=\"手游\">手游</a></li>", (tag == "d") ? "class=\"active\"" : "", "c", "手游管理");
			stringBuilder.Append("</ul>");
			text = stringBuilder.ToString();
			return new MvcHtmlString(text);
		}
	}
}
