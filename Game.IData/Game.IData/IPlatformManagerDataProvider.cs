using Game.Entity.PlatformManager;
using Game.Kernel;
using System.Collections.Generic;
using System.Data;

namespace Game.IData
{
	public interface IPlatformManagerDataProvider
	{
		Message UserLogon(Base_Users user);

		void Register(Base_Users user);

		void DeleteUser(string userIDList);

		void ModifyUserLogonPass(Base_Users user, string newLogonPass);

		void ModifyUserNullity(string userIDList, bool nullity);

		void ModifyUserInfo(Base_Users user);

		void BindIP(Base_Users user);

		Base_Users GetUserByAccounts(string userName);

		Base_Users GetUserByUserID(int userID);

		DataSet GetUserListByRoleID(int roleID);

		DataSet GetUserList();

		PagerSet GetUserList(int pageIndex, int pageSize, string condition, string orderby);

		PagerSet GetRoleList(int pageIndex, int pageSize, string condition, string orderby);

		Base_Roles GetRoleInfo(int roleID);

		string GetRolenameByRoleID(int roleID);

		void InsertRole(Base_Roles role);

		void UpdateRole(Base_Roles role);

		void DeleteRole(string sqlQuery);

		DataSet GetMenuByRoleID(int roleID);

		DataSet GetPermissionByModuleID(int moduleID, int RoleID);

		DataSet GetMenuByUserID(int userID);

		DataSet GetPermissionByUserID(int userID);

		DataSet GetModuleList();

		DataSet GetModuleParentList();

		DataSet GetModuleListByModuleID(int moduleID);

		DataSet GetModulePermissionList();

		DataSet GetModulePermissionList(int moduleID);

		DataSet GetRolePermissionList(int roleID);

		void InsertRolePermission(Base_RolePermission rolePermission);

		void DeleteRolePermission(int roleID);

		PagerSet GetSystemSecurityList(int pageIndex, int pageSize, string condition, string orderby);

		SystemSecurity GetSystemSecurityById(int Id);

		QPAdminSiteInfo GetQPAdminSiteInfo(int siteID);

		void UpdateQPAdminSiteInfo(QPAdminSiteInfo site);

		Message UserLogonApp(Base_Users user, string machineID);

		void AddSystemSecurity(Dictionary<string, object> dic);

		PagerSet GetOperatingLog(int pageIndex, int pageSize, string condition, string orderby);

		PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby);

		PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby, string[] fields);

		int ExecuteSql(string sql);

		DataSet GetDataSetBySql(string sql);

		string GetScalarBySql(string sql);

		Message ExcuteByProce(string proceName, Dictionary<string, object> dir);
	}
}
