using Game.Entity.PlatformManager;
using Game.IData;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Game.Data
{
	public class PlatformManagerDataProvider : BaseDataProvider, IPlatformManagerDataProvider
	{
		private ITableProvider aideUserProvider;

		private ITableProvider aideRoleProvider;

		private ITableProvider aideRolePermissionProvider;

		private ITableProvider aideQPAdminSiteInfoProvider;

		public PlatformManagerDataProvider(string connString)
			: base(connString)
		{
			aideUserProvider = GetTableProvider("Base_Users");
			aideRoleProvider = GetTableProvider("Base_Roles");
			aideRolePermissionProvider = GetTableProvider("Base_RolePermission");
			aideQPAdminSiteInfoProvider = GetTableProvider("QPAdminSiteInfo");
		}

		public Message UserLogon(Base_Users user)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strUserName", user.Username));
			list.Add(base.Database.MakeInParam("strPassword", user.Password));
			list.Add(base.Database.MakeInParam("strClientIP", user.LastLoginIP));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForObject<Base_Users>(base.Database, "NET_PM_UserLogon", list);
		}

		public void Register(Base_Users user)
		{
			DataRow dataRow = aideUserProvider.NewRow();
			dataRow["Username"] = user.Username;
			dataRow["Password"] = user.Password;
			dataRow["RoleID"] = user.RoleID;
			dataRow["Nullity"] = user.Nullity;
			dataRow["PreLoginIP"] = user.PreLoginIP;
			dataRow["PreLogintime"] = user.PreLogintime;
			dataRow["LastLoginIP"] = user.LastLoginIP;
			dataRow["LastLogintime"] = user.LastLogintime;
			dataRow["LoginTimes"] = user.LoginTimes;
			dataRow["IsBand"] = user.IsBand;
			dataRow["BandIP"] = user.BandIP;
			dataRow["IsAssist"] = user.IsAssist;
			dataRow["MobilePhone"] = user.MobilePhone;
			dataRow["IsMobileNeed"] = 1;
			aideUserProvider.Insert(dataRow);
		}

		public void DeleteUser(string userIDList)
		{
			aideUserProvider.Delete(string.Format("{0}", userIDList));
		}

		public void ModifyUserLogonPass(Base_Users user, string newLogonPass)
		{
			string commandText = "UPDATE Base_Users SET Password = @Password WHERE UserID= @UserID";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserID", user.UserID));
			list.Add(base.Database.MakeInParam("Password", newLogonPass));
			base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		public void ModifyUserNullity(string userIDList, bool nullity)
		{
			string commandText = string.Format("UPDATE Base_Users SET Nullity = @Nullity WHERE UserID IN ({0})", userIDList);
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("Nullity", nullity));
			base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		public void ModifyUserInfo(Base_Users user)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE Base_Users SET ").Append("Password=@Password, ").Append("RoleID=@RoleID, ")
				.Append("Nullity=@Nullity, ")
				.Append("IsAssist=@IsAssist, ")
				.Append("MobilePhone=@MobilePhone ")
				.Append("WHERE UserID=@UserID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("Password", user.Password));
			list.Add(base.Database.MakeInParam("RoleID", user.RoleID));
			list.Add(base.Database.MakeInParam("Nullity", user.Nullity));
			list.Add(base.Database.MakeInParam("UserID", user.UserID));
			list.Add(base.Database.MakeInParam("IsAssist", user.IsAssist));
			list.Add(base.Database.MakeInParam("MobilePhone", user.MobilePhone));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void BindIP(Base_Users user)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE Base_Users SET ").Append("IsBand=@IsBand, ").Append("BandIP=@BandIP ")
				.Append("WHERE UserID=@UserID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("IsBand", user.IsBand));
			list.Add(base.Database.MakeInParam("BandIP", user.BandIP));
			list.Add(base.Database.MakeInParam("UserID", user.UserID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public Base_Users GetUserByAccounts(string userName)
		{
			string where = string.Format("(NOLOCK) WHERE UserName= N'{0}'", userName);
			return aideUserProvider.GetObject<Base_Users>(where);
		}

		public Base_Users GetUserByUserID(int userID)
		{
			string where = string.Format("(NOLOCK) WHERE UserID={0}", userID);
			return aideUserProvider.GetObject<Base_Users>(where);
		}

		public DataSet GetUserListByRoleID(int roleID)
		{
			return aideUserProvider.Get(string.Format("(NOLOCK) WHERE RoleID={0}", roleID));
		}

		public DataSet GetUserList()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT UserID, RoleID").AppendFormat("      ,Rolename=").AppendFormat("         CASE UserID")
				.AppendFormat("             WHEN 1 THEN N'超级管理员'")
				.AppendFormat("             ELSE (SELECT RoleName FROM Base_Roles(NOLOCK) WHERE RoleID=u.RoleID)")
				.AppendFormat("         END")
				.AppendFormat("      ,UserName,PreLogintime,PreLoginIP,LastLogintime,LastLoginIP")
				.AppendFormat("      ,LoginTimes,IsBand,BandIP")
				.AppendFormat("  FROM Base_Users(NOLOCK) AS u")
				.Append(" WHERE UserID>1");
			return base.Database.ExecuteDataset(stringBuilder.ToString());
		}

		public PagerSet GetUserList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("Base_Users", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public PagerSet GetRoleList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("Base_Roles", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public Base_Roles GetRoleInfo(int roleID)
		{
			string where = string.Format("(NOLOCK) WHERE RoleID= {0}", roleID);
			return aideRoleProvider.GetObject<Base_Roles>(where);
		}

		public string GetRolenameByRoleID(int roleID)
		{
			string where = string.Format("(NOLOCK) WHERE RoleID={0}", roleID);
			Base_Roles @object = aideRoleProvider.GetObject<Base_Roles>(where);
			if (@object == null)
			{
				return "";
			}
			return @object.RoleName;
		}

		public void InsertRole(Base_Roles role)
		{
			DataRow dataRow = aideRoleProvider.NewRow();
			dataRow["RoleID"] = role.RoleID;
			dataRow["RoleName"] = role.RoleName;
			dataRow["Description"] = role.Description;
			aideRoleProvider.Insert(dataRow);
		}

		public void UpdateRole(Base_Roles role)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE Base_Roles SET ").Append("RoleName=@RoleName ,").Append("Description=@Description ")
				.Append("WHERE RoleID= @RoleID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("RoleName", role.RoleName));
			list.Add(base.Database.MakeInParam("Description", role.Description));
			list.Add(base.Database.MakeInParam("RoleID", role.RoleID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void DeleteRole(string sqlQuery)
		{
			aideRoleProvider.Delete(sqlQuery);
		}

		public DataSet GetMenuByRoleID(int roleID)
		{
			string text = "";
			if (roleID <= 1)
			{
				text = "SELECT ModuleID,Title,Link,OrderNo,ParentID FROM Base_Module WHERE Nullity=0";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("SELECT b.ModuleID,b.Title,b.Link,b.OrderNo,b.ParentID,b.Nullity,a.RoleID FROM Base_Module b LEFT JOIN Base_RolePermission a ON a.ModuleID = b.ModuleID ");
				stringBuilder.AppendFormat("WHERE b.Nullity=0 AND a.RoleID={0} ", roleID);
				stringBuilder.AppendFormat("UNION ALL SELECT ModuleID,Title,Link,OrderNo,ParentID,Nullity,{0} as RoleID from Base_Module WHERE ModuleID  ", roleID);
				stringBuilder.AppendFormat("IN(SELECT distinct b.ParentID FROM Base_Module b LEFT JOIN Base_RolePermission a ON a.ModuleID = b.ModuleID WHERE b.Nullity=0 AND a.RoleID={0})", roleID);
				text = stringBuilder.ToString();
			}
			return base.Database.ExecuteDataset(CommandType.Text, text);
		}

		public DataSet GetPermissionByModuleID(int moduleID, int RoleID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT t.PermissionTitle,t.ModuleID,m.Title,m.Link FROM (SELECT a.ModuleID,a.RoleID,c.PermissionTitle,c.PermissionValue FROM Base_ModulePermission c LEFT JOIN Base_RolePermission a ON c.ModuleID=a.ModuleID WHERE c.Nullity=0 {0} AND c.PermissionValue & a.OperationPermission =c.PermissionValue) AS t INNER JOIN Base_Module m ON t.ModuleID = m.ModuleID WHERE m.Nullity=0 AND m.ModuleID={1}", (RoleID <= 1) ? "" : ("AND a.RoleID=" + RoleID), moduleID);
			return base.Database.ExecuteDataset(CommandType.Text, stringBuilder.ToString());
		}

		public DataSet GetMenuByUserID(int userID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwUserID", userID));
			DataSet ds;
			base.Database.RunProc("NET_PM_GetMenuByUserID", list, out ds);
			return ds;
		}

		public DataSet GetPermissionByUserID(int userID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwUserID", userID));
			DataSet ds;
			base.Database.RunProc("NET_PM_GetPermissionByUserID", list, out ds);
			return ds;
		}

		public DataSet GetModuleList()
		{
			string commandText = "SELECT * FROM Base_Module WHERE  Nullity=0 ORDER BY OrderNo";
			return base.Database.ExecuteDataset(CommandType.Text, commandText);
		}

		public DataSet GetModuleParentList()
		{
			string commandText = "SELECT * FROM Base_Module WHERE ParentID=0 AND Nullity=0 ORDER BY OrderNo";
			return base.Database.ExecuteDataset(CommandType.Text, commandText);
		}

		public DataSet GetModuleListByModuleID(int moduleID)
		{
			string commandText = string.Format("SELECT * FROM Base_Module WHERE ParentID={0} ORDER BY OrderNo", moduleID);
			return base.Database.ExecuteDataset(CommandType.Text, commandText);
		}

		public DataSet GetModulePermissionList()
		{
			string commandText = string.Format("SELECT * FROM Base_ModulePermission WHERE Nullity=0");
			return base.Database.ExecuteDataset(CommandType.Text, commandText);
		}

		public DataSet GetModulePermissionList(int moduleID)
		{
			string commandText = string.Format("SELECT * FROM Base_ModulePermission WHERE ModuleID={0} AND Nullity=0", moduleID);
			return base.Database.ExecuteDataset(CommandType.Text, commandText);
		}

		public DataSet GetRolePermissionList(int roleID)
		{
			string commandText = string.Format("SELECT * FROM Base_RolePermission WHERE RoleID={0}", roleID);
			return base.Database.ExecuteDataset(CommandType.Text, commandText);
		}

		public void InsertRolePermission(Base_RolePermission rolePermission)
		{
			DataRow dataRow = aideRolePermissionProvider.NewRow();
			dataRow["RoleID"] = rolePermission.RoleID;
			dataRow["ModuleID"] = rolePermission.ModuleID;
			dataRow["ManagerPermission"] = rolePermission.ManagerPermission;
			dataRow["OperationPermission"] = rolePermission.OperationPermission;
			dataRow["StateFlag"] = rolePermission.StateFlag;
			aideRolePermissionProvider.Insert(dataRow);
		}

		public void DeleteRolePermission(int roleID)
		{
			aideRolePermissionProvider.Delete(string.Format("WHERE RoleID = ({0})", roleID));
		}

		public PagerSet GetSystemSecurityList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("SystemSecurity", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public SystemSecurity GetSystemSecurityById(int Id)
		{
			SystemSecurity systemSecurity = new SystemSecurity();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("select RecordID,OperatingTime,OperatingName,OperatingIP,OperatingAccounts,Remark from SystemSecurity where RecordID=" + Id);
			DataSet dataSet = base.Database.ExecuteDataset(CommandType.Text, stringBuilder.ToString());
			int result = 0;
			DateTime result2 = DateTime.Now;
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				systemSecurity.RecordID = (int.TryParse(dataSet.Tables[0].Rows[0]["RecordID"].ToString(), out result) ? Convert.ToInt32(dataSet.Tables[0].Rows[0]["RecordID"]) : 0);
				systemSecurity.OperatingTime = (DateTime.TryParse(dataSet.Tables[0].Rows[0]["OperatingTime"].ToString(), out result2) ? Convert.ToDateTime(dataSet.Tables[0].Rows[0]["OperatingTime"]) : result2);
				systemSecurity.OperatingName = ((dataSet.Tables[0].Rows[0]["OperatingName"] == null) ? "" : dataSet.Tables[0].Rows[0]["OperatingName"].ToString());
				systemSecurity.OperatingIP = ((dataSet.Tables[0].Rows[0]["OperatingIP"] == null) ? "" : dataSet.Tables[0].Rows[0]["OperatingIP"].ToString());
				systemSecurity.OperatingAccounts = ((dataSet.Tables[0].Rows[0]["OperatingAccounts"] == null) ? "" : dataSet.Tables[0].Rows[0]["OperatingAccounts"].ToString());
				systemSecurity.Remark = ((dataSet.Tables[0].Rows[0]["Remark"] == null) ? "" : dataSet.Tables[0].Rows[0]["Remark"].ToString());
			}
			return systemSecurity;
		}

		public QPAdminSiteInfo GetQPAdminSiteInfo(int siteID)
		{
			string where = string.Format("(NOLOCK) WHERE SiteID= {0}", siteID);
			return aideQPAdminSiteInfoProvider.GetObject<QPAdminSiteInfo>(where);
		}

		public void UpdateQPAdminSiteInfo(QPAdminSiteInfo site)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE QPAdminSiteInfo SET ").Append("SiteName=@SiteName ,").Append("PageSize=@PageSize ,")
				.Append("CopyRight=@CopyRight ")
				.Append("WHERE SiteID= @SiteID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("SiteName", site.SiteName));
			list.Add(base.Database.MakeInParam("PageSize", site.PageSize));
			list.Add(base.Database.MakeInParam("CopyRight", site.CopyRight));
			list.Add(base.Database.MakeInParam("SiteID", site.SiteID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public Message UserLogonApp(Base_Users user, string machineID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", user.Username));
			list.Add(base.Database.MakeInParam("strPassword", user.Password));
			list.Add(base.Database.MakeInParam("strClientIP", user.LastLoginIP));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForObject<Base_Users>(base.Database, "APP_PM_UserLogon", list);
		}

		public void AddSystemSecurity(Dictionary<string, object> dic)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("INSERT SystemSecurity ( OperatingTime ,OperatingName ,OperatingIP ,OperatingAccounts, Remark,ObjectAccounts)VALUES(GETDATE()").Append(",@OperatingName").Append(",@OperatingIP ")
				.Append(",@OperatingAccounts")
				.Append(",@Remark")
				.Append(",@Accounts")
				.Append(")");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("OperatingName", dic["OperatingName"]));
			list.Add(base.Database.MakeInParam("OperatingIP", dic["OperatingIP"]));
			list.Add(base.Database.MakeInParam("OperatingAccounts", dic["OperatingAccounts"]));
			list.Add(base.Database.MakeInParam("Remark", dic["Remark"]));
			list.Add(base.Database.MakeInParam("Accounts", dic["Accounts"]));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public PagerSet GetOperatingLog(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("OperatingLog", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters(tableName, orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby, string[] fields)
		{
			PagerParameters prams = new PagerParameters(tableName, orderby, condition, pageIndex, pageSize, fields);
			return GetPagerSet2(prams);
		}

		public int ExecuteSql(string sql)
		{
			return base.Database.ExecuteNonQuery(sql);
		}

		public DataSet GetDataSetBySql(string sql)
		{
			return base.Database.ExecuteDataset(sql);
		}

		public string GetScalarBySql(string sql)
		{
			return base.Database.ExecuteScalarToStr(CommandType.Text, sql);
		}

		public Message ExcuteByProce(string proceName, Dictionary<string, object> dir)
		{
			List<DbParameter> list = new List<DbParameter>();
			if (dir != null)
			{
				int num = 0;
				foreach (KeyValuePair<string, object> item in dir)
				{
					if (num == dir.Count - 1)
					{
						list.Add(base.Database.MakeOutParam(item.Key, typeof(string), 127));
					}
					else
					{
						list.Add(base.Database.MakeInParam(item.Key, item.Value));
					}
					num++;
				}
			}
			return MessageHelper.GetMessage(base.Database, proceName, list);
		}
	}
}
