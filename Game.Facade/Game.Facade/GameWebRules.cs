using Game.Entity.PlatformManager;
using Game.Kernel;
using Game.Utils;
using System;

namespace Game.Facade
{
	public class GameWebRules
	{
		internal static int MIN_LOGONPASSWD_LENGTH = 6;

		public static Message CheckedUserLogon(Base_Users user)
		{
			Message message = CheckedAccounts(user.Username);
			if (!message.Success)
			{
				return message;
			}
			message = CheckedPassword(user.Password);
			if (!message.Success)
			{
				return message;
			}
			user.Username = TextUtility.SqlEncode(user.Username);
			user.Username = Utility.HtmlEncode(TextFilter.FilterScript(user.Username));
			return new Message(true);
		}

		public static Message CheckedUserToRegister(ref Base_Users user)
		{
			Message message = CheckedAccounts(user.Username);
			if (!message.Success)
			{
				return message;
			}
			message = CheckedPassword(user.Password);
			if (!message.Success)
			{
				return message;
			}
			user.Username = Utility.HtmlEncode(TextFilter.FilterScript(user.Username));
			user.PreLogintime = DateTime.Now;
			user.PreLoginIP = GameRequest.GetUserIP();
			user.LastLogintime = DateTime.Now;
			user.LastLoginIP = user.LastLoginIP;
			user.Username = user.Username.Replace("&", "").Replace("#", "");
			return new Message(true);
		}

		public static Message CheckedUserToModify(ref Base_Users user)
		{
			new Message(false);
			return new Message(true);
		}

		public static Message CheckUserPasswordForModify(ref string oldPasswd, ref string newPasswd)
		{
			if (TextUtility.EmptyTrimOrNull(oldPasswd))
			{
				return new Message(false, "原密码不能为空。");
			}
			Message message = CheckedPassword(newPasswd);
			if (!message.Success)
			{
				return message;
			}
			return new Message(true);
		}

		public static Message CheckedAccounts(string accounts)
		{
			if (TextUtility.EmptyTrimOrNull(accounts))
			{
				return new Message(false, "用户帐号为空。");
			}
			return new Message(true);
		}

		public static Message CheckedPassword(string password)
		{
			if (TextUtility.EmptyTrimOrNull(password))
			{
				return new Message(false, "密码为空。");
			}
			if (password.Length < MIN_LOGONPASSWD_LENGTH)
			{
				return new Message(false, "密码长度最少为6位。");
			}
			return new Message(true);
		}

		public static Message CheckedRealname(string realname)
		{
			if (TextUtility.EmptyTrimOrNull(realname))
			{
				return new Message(false, "用户姓名为空。");
			}
			return new Message(true);
		}

		public static Message CheckedEmail(string email)
		{
			if (TextUtility.EmptyTrimOrNull(email))
			{
				return new Message(false, "邮件地址为空。");
			}
			if (!Validate.IsEmail(email))
			{
				return new Message(false, "邮件地址非法。");
			}
			return new Message(true);
		}
	}
}
