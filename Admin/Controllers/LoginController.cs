using Game.Entity.PlatformManager;
using Game.Facade;
using Game.Facade.Tools;
using Game.Kernel;
using Game.Utils;
using System;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class LoginController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public JsonResult ValidateLogin()
		{
            //string a = TypeUtil.ObjectToString(base.Request["verifyCode"]);
			string text = TypeUtil.ObjectToString(base.Request["userName"]);
			string password = Utility.MD5(TypeUtil.ObjectToString(base.Request["password"]));
			Base_Users userByAccounts = FacadeManage.aidePlatformManagerFacade.GetUserByAccounts(text);
			if (userByAccounts == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "账号不存在"
				});
			}
			if (userByAccounts.IsMobileNeed)
			{
                //if (a == "")
                //{
                //    return Json(new
                //    {
                //        IsOk = false,
                //        Msg = "请输入手机验证码"
                //    });
                //}
				if (base.Session["code"] == null)
				{
					return Json(new
					{
						IsOk = false,
						Msg = "验证码不存在或已过期，请重新获取验证码"
					});
				}
                //if (a != base.Session["code"].ToString())
                //{
                //    if (base.Session["error"] == null)
                //    {
                //        base.Session["error"] = 1;
                //    }
                //    else
                //    {
                //        base.Session["error"] = Convert.ToInt32(base.Session["error"]) + 1;
                //    }
                //    if (Convert.ToInt32(base.Session["error"]) > 3)
                //    {
                //        base.Session["code"] = null;
                //        return Json(new
                //        {
                //            IsOk = false,
                //            Msg = "验证码错误多次，请重新获取！"
                //        });
                //    }
                //    return Json(new
                //    {
                //        IsOk = false,
                //        Msg = "验证码错误！"
                //    });
                //}
			}
			userByAccounts = new Base_Users();
			userByAccounts.Username = text;
			userByAccounts.Password = password;
			userByAccounts.LastLoginIP = GameRequest.GetUserIP();
			Message message = FacadeManage.aidePlatformManagerFacade.UserLogon(userByAccounts);
			string text2 = "";
			if (!message.Success)
			{
				switch (message.MessageID)
				{
				case 100:
					text2 = "用户名或者密码错误";
					break;
				case 101:
					text2 = "IP错误";
					break;
				case 102:
					text2 = "没有此用户";
					break;
				default:
					text2 = "登录错误";
					break;
				}
				return Json(new
				{
					IsOk = false,
					Msg = text2
				});
			}
			userByAccounts = (message.EntityList[0] as Base_Users);
			if (userByAccounts == null || (userByAccounts.UserID != 1 && userByAccounts.RoleID < 0))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "登录失败"
				});
			}
			base.Session["code"] = null;
			base.Session["error"] = null;
			return Json(new
			{
				IsOk = true,
				Msg = "登录成功"
			});
		}

		public JsonResult SendCode()
		{
			string accounts = TypeUtil.ObjectToString(base.Request["userName"]);
			Base_Users userByAccounts = FacadeManage.aidePlatformManagerFacade.GetUserByAccounts(accounts);
			if (userByAccounts == null)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "账号不存在"
				});
			}
			if (!userByAccounts.IsMobileNeed)
			{
				return Json(new
				{
					IsOk = false,
					Msg = "该账号不需要手机验证"
				});
			}
			if (string.IsNullOrEmpty(userByAccounts.MobilePhone))
			{
				return Json(new
				{
					IsOk = false,
					Msg = "请先设置管理员手机号码"
				});
			}
			string content = ApplicationSettings.Get("phoneContent");
			string text = TextUtility.CreateAuthStr(6, true);
			string text2 = CodeHelper.SendCode(userByAccounts.MobilePhone, text, content);
			if (text2 == "发送成功")
			{
				base.Session["code"] = text;
				return Json(new
				{
					IsOk = true,
					Msg = "发送成功"
				});
			}
			return Json(new
			{
				IsOk = false,
				Msg = text2
			});
		}
	}
}
