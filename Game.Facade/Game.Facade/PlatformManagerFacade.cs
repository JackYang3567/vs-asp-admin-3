using Game.Data.Factory;
using Game.Entity.PlatformManager;
using Game.IData;
using Game.Kernel;
using Game.Utils;
using Game.Utils.Cache;
using System;
using System.Collections.Generic;
using System.Data;

namespace Game.Facade
{
	public class PlatformManagerFacade
	{
		private IPlatformManagerDataProvider aidePlatformManagerData;

		public PlatformManagerFacade()
		{
			aidePlatformManagerData = ClassFactory.GetIPlatformManagerDataProvider();
		}

		public Message UserLogon(Base_Users user)
		{
			Message message = GameWebRules.CheckedUserLogon(user);
			if (!message.Success)
			{
				return message;
			}
			message = aidePlatformManagerData.UserLogon(user);
			if (message.Success)
			{
				Base_Users base_Users = message.EntityList[0] as Base_Users;
				WHCache.Default.Save<SessionCache>(AppConfig.UserCacheKey, base_Users, AppConfig.UserCacheTimeOut);
				WHCache.Default.Save<CookiesCache>(AppConfig.UserCacheKey, base_Users.UserID, AppConfig.UserCacheTimeOut);
			}
			return message;
		}

		public void SaveUserCache(Base_Users user)
		{
			WHCache.Default.Save<SessionCache>(AppConfig.UserCacheKey, user, AppConfig.UserCacheTimeOut);
		}

		public void UserLogout()
		{
			Base_Users base_Users = WHCache.Default.Get<SessionCache>(AppConfig.UserCacheKey) as Base_Users;
			if (base_Users != null)
			{
				WHCache.Default.Delete<SessionCache>(AppConfig.UserCacheKey);
				WHCache.Default.Delete<CookiesCache>(AppConfig.UserCacheKey);
			}
		}

		public Base_Users GetUserInfoFromCache()
		{
			object obj = WHCache.Default.Get<SessionCache>(AppConfig.UserCacheKey);
			if (obj == null)
			{
				obj = WHCache.Default.Get<CookiesCache>(AppConfig.UserCacheKey);
				if (obj != null && Validate.IsNumeric(obj.ToString()))
				{
					Base_Users userByUserID = GetUserByUserID(Convert.ToInt32(obj));
					if (userByUserID != null)
					{
						return userByUserID;
					}
				}
				return null;
			}
			return obj as Base_Users;
		}

		public bool CheckedUserLogon()
		{
			Base_Users userInfoFromCache = GetUserInfoFromCache();
			if (userInfoFromCache == null || userInfoFromCache.UserID <= 0 || userInfoFromCache.RoleID <= 0)
			{
				return false;
			}
			return true;
		}

		public Message Register(Base_Users user)
		{
			Message message = GameWebRules.CheckedUserToRegister(ref user);
			if (!message.Success)
			{
				return message;
			}
			message = ExistUserAccounts(user.Username);
			if (message.Success)
			{
				message.Success = false;
				return message;
			}
			aidePlatformManagerData.Register(user);
			return new Message(true);
		}

		public Message ModifyUserInfo(Base_Users user)
		{
			Message message = GameWebRules.CheckedUserToModify(ref user);
			if (!message.Success)
			{
				return message;
			}
			Base_Users userByUserID = GetUserByUserID(user.UserID);
			if (userByUserID.Username != user.Username)
			{
				message = ExistUserAccounts(user.Username);
				if (message.Success)
				{
					message.Success = false;
					return message;
				}
			}
			aidePlatformManagerData.ModifyUserInfo(user);
			return new Message(true);
		}

		public void BindIP(Base_Users user)
		{
			aidePlatformManagerData.BindIP(user);
		}

		public void ModifyUserNullity(string userIDList, bool nullity)
		{
			aidePlatformManagerData.ModifyUserNullity(userIDList, nullity);
		}

		public DataSet GetUserList()
		{
			return aidePlatformManagerData.GetUserList();
		}

		public PagerSet GetUserList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformManagerData.GetUserList(pageIndex, pageSize, condition, orderby);
		}

		public DataSet GetUserListByRoleID(int roleID)
		{
			return aidePlatformManagerData.GetUserListByRoleID(roleID);
		}

		public Base_Users GetUserByUserID(int userID)
		{
			return aidePlatformManagerData.GetUserByUserID(userID);
		}

		public Base_Users GetUserByAccounts(string accounts)
		{
			return aidePlatformManagerData.GetUserByAccounts(accounts);
		}

		public string GetAccountsByUserID(int userID)
		{
			Base_Users userByUserID = GetUserByUserID(userID);
			if (userByUserID != null)
			{
				return userByUserID.Username;
			}
			return "";
		}

		public void DeleteUser(string userIDList)
		{
			aidePlatformManagerData.DeleteUser(userIDList);
		}

		public Message ExistUserAccounts(string accounts)
		{
			Base_Users userByAccounts = aidePlatformManagerData.GetUserByAccounts(accounts);
			if (userByAccounts != null && userByAccounts.UserID > 0)
			{
				return new Message(true, ResMessage.Error_ExistsUser);
			}
			return new Message(false);
		}

		public Message ValidUserLogonPass(int userID, string logonPass)
		{
			Base_Users userByUserID = aidePlatformManagerData.GetUserByUserID(userID);
			if (userByUserID == null || userByUserID.UserID <= 0 || !userByUserID.Password.Equals(logonPass, StringComparison.InvariantCultureIgnoreCase))
			{
				return new Message(false, "帐号不存在或密码输入错误。");
			}
			return new Message(true);
		}

		public Message ModifyUserLogonPass(Base_Users userExt, string oldPasswd, string newPasswd)
		{
			Message message = GameWebRules.CheckUserPasswordForModify(ref oldPasswd, ref newPasswd);
			if (!message.Success)
			{
				return message;
			}
			message = ValidUserLogonPass(userExt.UserID, oldPasswd);
			if (!message.Success)
			{
				return message;
			}
			aidePlatformManagerData.ModifyUserLogonPass(userExt, Utility.MD5(newPasswd));
			return new Message(true);
		}

		public Message ModifyPowerUserLogonPass(Base_Users admin, Base_Users powerUser, string newPasswd)
		{
			if (admin.UserID != 1 || admin.RoleID != 1)
			{
				return new Message(false, "您没有修改用户密码的权限。");
			}
			Message message = GameWebRules.CheckedPassword(newPasswd);
			if (!message.Success)
			{
				return message;
			}
			newPasswd = TextEncrypt.EncryptPassword(newPasswd);
			aidePlatformManagerData.ModifyUserLogonPass(powerUser, newPasswd);
			return new Message(true);
		}

		public PagerSet GetRoleList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformManagerData.GetRoleList(pageIndex, pageSize, condition, orderby);
		}

		public Base_Roles GetRoleInfo(int roleID)
		{
			return aidePlatformManagerData.GetRoleInfo(roleID);
		}

		public string GetRolenameByRoleID(int roleID)
		{
			return aidePlatformManagerData.GetRolenameByRoleID(roleID);
		}

		public Message InsertRole(Base_Roles role)
		{
			aidePlatformManagerData.InsertRole(role);
			return new Message(true);
		}

		public Message UpdateRole(Base_Roles role)
		{
			aidePlatformManagerData.UpdateRole(role);
			return new Message(true);
		}

		public void DeleteRole(string sqlQuery)
		{
			aidePlatformManagerData.DeleteRole(sqlQuery);
		}

		public DataSet GetMenuByRoleID(int roleID)
		{
			return aidePlatformManagerData.GetMenuByRoleID(roleID);
		}

		public DataSet GetPermissionByModuleID(int moduleID, int RoleID)
		{
			return aidePlatformManagerData.GetPermissionByModuleID(moduleID, RoleID);
		}

		public DataSet GetMenuByUserID(int userID)
		{
			return aidePlatformManagerData.GetMenuByUserID(userID);
		}

		public Dictionary<string, long> GetPermissionByUserID(int userID)
		{
			Dictionary<string, long> dictionary = new Dictionary<string, long>();
			DataSet permissionByUserID = aidePlatformManagerData.GetPermissionByUserID(userID);
			if (permissionByUserID.Tables.Count != 0 && permissionByUserID.Tables[0].Rows.Count != 0)
			{
				for (int i = 0; i < permissionByUserID.Tables[0].Rows.Count; i++)
				{
					dictionary.Add(permissionByUserID.Tables[0].Rows[i]["ModuleID"].ToString().Trim(), Utility.StrToInt(permissionByUserID.Tables[0].Rows[i]["OperationPermission"].ToString().Trim(), 0));
				}
			}
			return dictionary;
		}

		public DataSet GetModuleList()
		{
			return aidePlatformManagerData.GetModuleList();
		}

		public DataSet GetModuleParentList()
		{
			return aidePlatformManagerData.GetModuleParentList();
		}

		public DataSet GetModuleListByModuleID(int moduleID)
		{
			return aidePlatformManagerData.GetModuleListByModuleID(moduleID);
		}

		public DataSet GetModulePermissionList()
		{
			return aidePlatformManagerData.GetModulePermissionList();
		}

		public DataSet GetModulePermissionList(int moduleID)
		{
			return aidePlatformManagerData.GetModulePermissionList(moduleID);
		}

		public DataSet GetRolePermissionList(int roleID)
		{
			return aidePlatformManagerData.GetRolePermissionList(roleID);
		}

		public Message InsertRolePermission(Base_RolePermission rolePermission)
		{
			aidePlatformManagerData.InsertRolePermission(rolePermission);
			return new Message(true);
		}

		public void DeleteRolePermission(int roleID)
		{
			aidePlatformManagerData.DeleteRolePermission(roleID);
		}

		public PagerSet GetSystemSecurityList(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformManagerData.GetSystemSecurityList(pageIndex, pageSize, condition, orderby);
		}

		public SystemSecurity GetSystemSecurityById(int Id)
		{
			return aidePlatformManagerData.GetSystemSecurityById(Id);
		}

		public DataTable GetLogType()
		{
			string sql = "SELECT * FROM View_LogType";
			return aidePlatformManagerData.GetDataSetBySql(sql).Tables[0];
		}

		public QPAdminSiteInfo GetQPAdminSiteInfo(int siteID)
		{
			return aidePlatformManagerData.GetQPAdminSiteInfo(siteID);
		}

		public void UpdateQPAdminSiteInfo(QPAdminSiteInfo site)
		{
			aidePlatformManagerData.UpdateQPAdminSiteInfo(site);
		}

		public Message UserLogonApp(Base_Users user, string machineID)
		{
			return aidePlatformManagerData.UserLogonApp(user, machineID);
		}

		public void AddSystemSecurity(Dictionary<string, object> dic)
		{
			aidePlatformManagerData.AddSystemSecurity(dic);
		}

		public PagerSet GetOperatingLog(int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformManagerData.GetOperatingLog(pageIndex, pageSize, condition, orderby);
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
		{
			return aidePlatformManagerData.GetList(tableName, pageIndex, pageSize, condition, orderby);
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby, string[] fields)
		{
			return aidePlatformManagerData.GetList(tableName, pageIndex, pageSize, condition, orderby, fields);
		}

		public int ExecuteSql(string sql)
		{
			return aidePlatformManagerData.ExecuteSql(sql);
		}

		public DataSet GetDataSetBySql(string sql)
		{
			return aidePlatformManagerData.GetDataSetBySql(sql);
		}

		public string GetScalarBySql(string sql)
		{
			return aidePlatformManagerData.GetScalarBySql(sql);
		}

		public Message ExcuteByProce(string proceName, Dictionary<string, object> dir)
		{
			return aidePlatformManagerData.ExcuteByProce(proceName, dir);
		}
	}
}
