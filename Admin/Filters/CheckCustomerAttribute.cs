using Game.Entity.PlatformManager;
using Game.Facade;
using Game.Facade.Tools;
using System.Linq;
using System.Web.Mvc;

namespace Admin.Filters
{
	public class CheckCustomerAttribute : FilterAttribute, IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Base_Users userInfoFromCache = FacadeManage.aidePlatformManagerFacade.GetUserInfoFromCache();
			if (userInfoFromCache == null)
			{
				filterContext.HttpContext.Response.Redirect("/Login/index");
			}
			else
			{
				string text = filterContext.HttpContext.Request.Url.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					string text2 = "";
					string[] array = text.Split('/');
					if (array != null && array.Length > 0)
					{
						if (array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("AddAccount");
							}
							return false;
						}) > 0)
						{
							text2 = "添加";
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("ConfineContenInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("ConfineAddressInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("AccountsInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("ConfineMachineInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Filled"))
							{
								return q.Equals("MemberTypeInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("DataBaseInfoInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("GameGameItemInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("SystemMessageInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("GamePropertyInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("GlobalPlayPresentInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Web"))
							{
								return q.Equals("NewsInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Web"))
							{
								return q.Equals("RulesInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Web"))
							{
								return q.Equals("ActivityInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Web"))
							{
								return q.Equals("MobileNoticeInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Back"))
							{
								return q.Equals("BaseRoleInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Task"))
							{
								return q.Equals("TaskInfo");
							}
							return false;
						}) > 0)
						{
							int num = TypeUtil.ObjectToInt(filterContext.HttpContext.Request["param"]);
							text2 = ((num <= 0) ? "添加" : "编辑");
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("SystemSet");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("SpreadSet");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("SigninConfig");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("LevelConfig");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("LotteryConfigSet");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("LotteryConfigSet");
							}
							return false;
						}) > 0)
						{
							text2 = "编辑";
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("DelConfineContent");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("DelConfineAddrContent");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("DelConfineMachine");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Filled"))
							{
								return q.Equals("DeleteOnlineOrder");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Filled"))
							{
								return q.Equals("DeleteOrderKQ");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Filled"))
							{
								return q.Equals("DeleteOrderYB");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("DelDataBaseInfoInfo");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("DelGameGameItem");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("DelGlobalPlayPresent");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Web"))
							{
								return q.Equals("DoNewsList");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Web"))
							{
								return q.Equals("DoRulesList");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Back"))
							{
								return q.Equals("DoBaseRoleList");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Task"))
							{
								return q.Equals("DelTask");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Task"))
							{
								return q.Equals("DelRecord");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Task"))
							{
								return q.Equals("ClearRecord");
							}
							return false;
						}) > 0)
						{
							text2 = "删除";
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("DoSystemMessageList");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("App"))
							{
								return q.Equals("DoSystemMessageList");
							}
							return false;
						}) > 0)
						{
							int num2 = TypeUtil.ObjectToInt(filterContext.HttpContext.Request["OP"]);
							if (num2 == 1)
							{
								text2 = "删除";
							}
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("GrantMember");
							}
							return false;
						}) > 0)
						{
							text2 = "赠送会员";
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("GrantTreasure");
							}
							return false;
						}) > 0)
						{
							text2 = "赠送金币";
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("GrantExperience");
							}
							return false;
						}) > 0)
						{
							text2 = "赠送经验";
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("GrantScore");
							}
							return false;
						}) > 0)
						{
							text2 = "赠送积分";
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("GrantGameID");
							}
							return false;
						}) > 0)
						{
							text2 = "赠送靓号";
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("GrantClearScore");
							}
							return false;
						}) > 0)
						{
							text2 = "清零积分";
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("GrantFlee");
							}
							return false;
						}) > 0)
						{
							text2 = "清零逃率";
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("FreezeAccount");
							}
							return false;
						}) > 0 || array.Count(delegate(string q)
						{
							if (q.Equals("Account"))
							{
								return q.Equals("UnfreezeAccount");
							}
							return false;
						}) > 0)
						{
							text2 = "冻/解";
						}
						else if (array.Count(delegate(string q)
						{
							if (q.Equals("DataAnalysis"))
							{
								return q.Equals("ClearTableData");
							}
							return false;
						}) > 0)
						{
							text2 = "清除数据";
						}
					}
					if (text2 != "" && !TypeUtil.IsPower(userInfoFromCache.MoudleID, userInfoFromCache.RoleID, text2))
					{
						filterContext.HttpContext.Response.Redirect("/NoPower/Index");
					}
				}
			}
		}
	}
}
