using Game.Entity.PlatformManager;
using System.Collections.Generic;

namespace Game.Facade
{
	public class AdminPermission
	{
		private Base_Users _user;

		private int _moduleID;

		private Dictionary<string, long> _userPriv;

		public Base_Users User
		{
			get
			{
				return _user;
			}
			set
			{
				_user = value;
			}
		}

		public int ModuleID
		{
			get
			{
				return _moduleID;
			}
			set
			{
				_moduleID = value;
			}
		}

		public Dictionary<string, long> UserPermission
		{
			get
			{
				return _userPriv;
			}
			set
			{
				_userPriv = value;
			}
		}

		public AdminPermission(Base_Users user, int moduleID)
		{
			User = user;
			ModuleID = moduleID;
			UserPermission = ((user != null) ? new PlatformManagerFacade().GetPermissionByUserID(user.UserID) : null);
		}

		public bool GetPermission(long authValue)
		{
			bool result = true;
			if (User == null)
			{
				return false;
			}
			if (User.RoleID == 1 || User.UserID == 1)
			{
				return result;
			}
			long value = 0L;
			if (!UserPermission.TryGetValue(ModuleID.ToString().Trim(), out value))
			{
				return false;
			}
			if ((value & authValue) > 0)
			{
				return result;
			}
			return false;
		}

		public bool GetUserPagePermission()
		{
			bool result = true;
			if (User == null)
			{
				return false;
			}
			if (User.RoleID == 1 || User.UserID == 1)
			{
				return result;
			}
			long value = 0L;
			if (!UserPermission.TryGetValue(ModuleID.ToString().Trim(), out value))
			{
				return false;
			}
			return true;
		}
	}
}
