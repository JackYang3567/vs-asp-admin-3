using Game.Entity.Accounts;
using Game.IData;
using Game.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Game.Data
{
	public class AccountsDataProvider : BaseDataProvider, IAccountsDataProvider
	{
		private ITableProvider aideAccountsProvider;

		private ITableProvider aideIndividualDatumProvider;

		private ITableProvider aideAccountsProtectProvider;

		private ITableProvider aideConfineContentProvider;

		private ITableProvider aideConfineAddressProvider;

		private ITableProvider aideConfineMachineProvider;

		private ITableProvider aideSystemStatusInfoProvider;

		private ITableProvider aideAccountsControlProvider;

		private ITableProvider aideAccountsAgent;

		private ITableProvider aideMemberProperty;

		private ITableProvider aideAccountsAgentGame;

		public AccountsDataProvider(string connString)
			: base(connString)
		{
			aideAccountsProvider = GetTableProvider("AccountsInfo");
			aideIndividualDatumProvider = GetTableProvider("IndividualDatum");
			aideAccountsProtectProvider = GetTableProvider("AccountsProtect");
			aideConfineContentProvider = GetTableProvider("ConfineContent");
			aideConfineAddressProvider = GetTableProvider("ConfineAddress");
			aideConfineMachineProvider = GetTableProvider("ConfineMachine");
			aideSystemStatusInfoProvider = GetTableProvider("SystemStatusInfo");
			aideAccountsControlProvider = GetTableProvider("AccountsControl");
			aideAccountsAgent = GetTableProvider("AccountsAgent");
			aideMemberProperty = GetTableProvider("MemberProperty");
			aideAccountsAgentGame = GetTableProvider("AccountsAgentGame");
		}

		public PagerSet GetAccountsList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("AccountsInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public string GetAccountByUserID(int userID)
		{
			AccountsInfo accountInfoByUserID = GetAccountInfoByUserID(userID);
			if (accountInfoByUserID != null)
			{
				return accountInfoByUserID.Accounts;
			}
			return "";
		}

		public int GetExperienceByUserID(int userID)
		{
			AccountsInfo accountInfoByUserID = GetAccountInfoByUserID(userID);
			if (accountInfoByUserID != null)
			{
				return accountInfoByUserID.Experience;
			}
			return 0;
		}

		public AccountsInfo GetAccountInfoByUserID(int userID)
		{
			string where = string.Format("(NOLOCK) WHERE UserID= N'{0}'", userID);
			AccountsInfo @object = aideAccountsProvider.GetObject<AccountsInfo>(where);
			if (@object != null)
			{
				return @object;
			}
			return new AccountsInfo();
		}

		public AccountsInfo GetAccountInfoByAccount(string account)
		{
			string commandText = string.Format("SELECT * FROM AccountsInfo(NOLOCK) WHERE Accounts=@Accounts");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("Accounts", account));
			AccountsInfo accountsInfo = base.Database.ExecuteObject<AccountsInfo>(commandText, list);
			if (accountsInfo != null)
			{
				return accountsInfo;
			}
			return new AccountsInfo();
		}

		public AccountsInfo GetAccountInfoByNickname(string nickname)
		{
			string commandText = string.Format("SELECT * FROM AccountsInfo(NOLOCK) WHERE NickName=@NickName");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("NickName", nickname));
			AccountsInfo accountsInfo = base.Database.ExecuteObject<AccountsInfo>(commandText, list);
			if (accountsInfo != null)
			{
				return accountsInfo;
			}
			return new AccountsInfo();
		}

		public AccountsInfo GetAccountInfoByGameID(int gameID)
		{
			string commandText = string.Format("SELECT * FROM AccountsInfo(NOLOCK) WHERE GameID=@GameID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("GameID", gameID));
			AccountsInfo accountsInfo = base.Database.ExecuteObject<AccountsInfo>(commandText, list);
			if (accountsInfo != null)
			{
				return accountsInfo;
			}
			return new AccountsInfo();
		}

		public string GetAccountsByGameID(int gameId)
		{
			string commandText = string.Format("SELECT Accounts FROM AccountsInfo(NOLOCK) WHERE GameID=@gameId");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("gameId", gameId));
			object obj = base.Database.ExecuteScalar(CommandType.Text, commandText, list.ToArray());
			if (obj != null)
			{
				return obj.ToString();
			}
			return "";
		}

		public void DongjieAccount(string sqlQuery)
		{
			string commandText = string.Format("UPDATE AccountsInfo SET Nullity=1 {0}", sqlQuery);
			base.Database.ExecuteNonQuery(commandText);
		}

		public void JieDongAccount(string sqlQuery)
		{
			string commandText = string.Format("UPDATE AccountsInfo SET Nullity=0 {0}", sqlQuery);
			base.Database.ExecuteNonQuery(commandText);
		}

		public void SettingAndroid(string sqlQuery)
		{
			string str = string.Format("UPDATE AccountsInfo SET IsAndroid=1 {0};", sqlQuery);
			str += string.Format("INSERT INTO AndroidLockInfo(UserID) SELECT UserID FROM AccountsInfo {0} AND UserID NOT IN(SELECT UserID FROM AndroidLockInfo {0})", sqlQuery);
			base.Database.ExecuteNonQuery(str);
		}

		public void CancleAndroid(string sqlQuery)
		{
			string str = string.Format("UPDATE AccountsInfo SET IsAndroid=0 {0};", sqlQuery);
			str += string.Format("DELETE AndroidLockInfo {0}", sqlQuery);
			base.Database.ExecuteNonQuery(str);
		}

		public Message AddAccount(AccountsInfo account, IndividualDatum datum)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", account.Accounts));
			list.Add(base.Database.MakeInParam("strNickName", account.NickName));
			list.Add(base.Database.MakeInParam("strLogonPass", account.LogonPass));
			list.Add(base.Database.MakeInParam("strInsurePass", account.InsurePass));
			list.Add(base.Database.MakeInParam("strDynamicPass", account.DynamicPass));
			list.Add(base.Database.MakeInParam("dwFaceID", account.FaceID));
			list.Add(base.Database.MakeInParam("strUnderWrite", account.UnderWrite));
			list.Add(base.Database.MakeInParam("strPassPortID", account.PassPortID));
			list.Add(base.Database.MakeInParam("strCompellation", account.Compellation));
			list.Add(base.Database.MakeInParam("dwExperience", account.Experience));
			list.Add(base.Database.MakeInParam("dwPresent", account.Present));
			list.Add(base.Database.MakeInParam("dwLoveLiness", account.LoveLiness));
			list.Add(base.Database.MakeInParam("dwUserRight", account.UserRight));
			list.Add(base.Database.MakeInParam("dwMasterRight", account.MasterRight));
			list.Add(base.Database.MakeInParam("dwServiceRight", account.ServiceRight));
			list.Add(base.Database.MakeInParam("dwMasterOrder", account.MasterOrder));
			list.Add(base.Database.MakeInParam("dwMemberOrder", account.MemberOrder));
			list.Add(base.Database.MakeInParam("dtMemberOverDate", account.MemberOverDate));
			list.Add(base.Database.MakeInParam("dtMemberSwitchDate", account.MemberSwitchDate));
			list.Add(base.Database.MakeInParam("dwGender", account.Gender));
			list.Add(base.Database.MakeInParam("dwNullity", account.Nullity));
			list.Add(base.Database.MakeInParam("dwStunDown", account.StunDown));
			list.Add(base.Database.MakeInParam("dwMoorMachine", account.MoorMachine));
			list.Add(base.Database.MakeInParam("strRegisterIP", account.RegisterIP));
			list.Add(base.Database.MakeInParam("strRegisterMachine", account.RegisterMachine));
			list.Add(base.Database.MakeInParam("IsAndroid", account.IsAndroid));
			list.Add(base.Database.MakeInParam("strQQ", datum.QQ));
			list.Add(base.Database.MakeInParam("strEMail", datum.EMail));
			list.Add(base.Database.MakeInParam("strSeatPhone", datum.SeatPhone));
			list.Add(base.Database.MakeInParam("strMobilePhone", datum.MobilePhone));
			list.Add(base.Database.MakeInParam("strDwellingPlace", datum.DwellingPlace));
			list.Add(base.Database.MakeInParam("strPostalCode", datum.PostalCode));
			list.Add(base.Database.MakeInParam("strUserNote", datum.UserNote));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessage(base.Database, "NET_PM_AddAccount", list);
		}

		public Message UpdateAccount(AccountsInfo account, int masterID, string clientIP)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwUserID", account.UserID));
			list.Add(base.Database.MakeInParam("strAccounts", account.Accounts));
			list.Add(base.Database.MakeInParam("strNickName", account.NickName));
			list.Add(base.Database.MakeInParam("strLogonPass", account.LogonPass));
			list.Add(base.Database.MakeInParam("strInsurePass", account.InsurePass));
			list.Add(base.Database.MakeInParam("strUnderWrite", account.UnderWrite));
			list.Add(base.Database.MakeInParam("dwExperience", account.Experience));
			list.Add(base.Database.MakeInParam("dwPresent", account.Present));
			list.Add(base.Database.MakeInParam("dwLoveLiness", account.LoveLiness));
			list.Add(base.Database.MakeInParam("dwGender", account.Gender));
			list.Add(base.Database.MakeInParam("dwFaceID", account.FaceID));
			list.Add(base.Database.MakeInParam("dwCustomID", account.CustomID));
			list.Add(base.Database.MakeInParam("dwStunDown", account.StunDown));
			list.Add(base.Database.MakeInParam("dwNullity", account.Nullity));
			list.Add(base.Database.MakeInParam("dwMoorMachine", account.MoorMachine));
			list.Add(base.Database.MakeInParam("dwIsAndroid", account.IsAndroid));
			list.Add(base.Database.MakeInParam("dwUserRight", account.UserRight));
			list.Add(base.Database.MakeInParam("dwMasterOrder", account.MasterOrder));
			list.Add(base.Database.MakeInParam("dwMasterRight", account.MasterRight));
			list.Add(base.Database.MakeInParam("dwMasterID", masterID));
			list.Add(base.Database.MakeInParam("strClientIP", clientIP));
			list.Add(base.Database.MakeInParam("dwUserType", account.UserType));
			list.Add(base.Database.MakeInParam("strMobile", account.RegisterMobile));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessage(base.Database, "NET_PM_UpdateAccountInfo", list);
		}

		public bool AddUserMedal(int userId, int medal)
		{
			string commandText = "UPDATE AccountsInfo SET UserMedal=UserMedal+@UserMedal WHERE UserID=@UserID";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserMedal", medal));
			list.Add(base.Database.MakeInParam("UserID", userId));
			if (base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray()) > 0)
			{
				return true;
			}
			return false;
		}

		public bool UpdateUserPassword(AccountsInfo accountsInfo)
		{
			StringBuilder stringBuilder = new StringBuilder("UPDATE AccountsInfo SET ");
			List<DbParameter> list = new List<DbParameter>();
			if (!string.IsNullOrEmpty(accountsInfo.LogonPass))
			{
				stringBuilder.Append(" LogonPass=@LogonPass");
				list.Add(base.Database.MakeInParam("LogonPass", accountsInfo.LogonPass));
			}
			if (!string.IsNullOrEmpty(accountsInfo.InsurePass))
			{
				stringBuilder.Append(",InsurePass=@InsurePass ");
				list.Add(base.Database.MakeInParam("InsurePass", accountsInfo.InsurePass));
			}
			stringBuilder.Append(" WHERE UserID=@UserID");
			list.Add(base.Database.MakeInParam("UserID", accountsInfo.UserID));
			if (base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray()) > 0)
			{
				return true;
			}
			return false;
		}

		public IndividualDatum GetAccountDetailByUserID(int userID)
		{
			string where = string.Format("(NOLOCK) WHERE UserID= N'{0}'", userID);
			return aideIndividualDatumProvider.GetObject<IndividualDatum>(where);
		}

		public void InsertAccountDetail(IndividualDatum accountDetail)
		{
			DataRow dataRow = aideIndividualDatumProvider.NewRow();
			dataRow["UserID"] = accountDetail.UserID;
			dataRow["QQ"] = accountDetail.QQ;
			dataRow["EMail"] = accountDetail.EMail;
			dataRow["SeatPhone"] = accountDetail.SeatPhone;
			dataRow["MobilePhone"] = accountDetail.MobilePhone;
			dataRow["DwellingPlace"] = accountDetail.DwellingPlace;
			dataRow["PostalCode"] = accountDetail.PostalCode;
			dataRow["CollectDate"] = accountDetail.CollectDate;
			dataRow["UserNote"] = accountDetail.UserNote;
			dataRow["Compellation"] = accountDetail.Compellation;
			dataRow["BankNO"] = accountDetail.BankNO;
			dataRow["BankName"] = accountDetail.BankName;
			dataRow["BankAddress"] = accountDetail.BankAddress;
			aideIndividualDatumProvider.Insert(dataRow);
		}

		public void UpdateAccountDetail(IndividualDatum accountDetail)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE IndividualDatum SET ").Append("QQ=@QQ, ").Append("EMail=@EMail, ")
				.Append("MobilePhone=@MobilePhone, ")
				.Append("PostalCode=@PostalCode, ")
				.Append("DwellingPlace=@DwellingPlace, ")
				.Append("UserNote=@UserNote, ")
				.Append("Compellation=@Compellation, ")
				.Append("BankNO=@BankNO, ")
				.Append("BankName=@BankName, ")
				.Append("BankAddress=@BankAddress ")
				.Append("WHERE UserID=@UserID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("QQ", accountDetail.QQ));
			list.Add(base.Database.MakeInParam("EMail", accountDetail.EMail));
			list.Add(base.Database.MakeInParam("SeatPhone", accountDetail.SeatPhone));
			list.Add(base.Database.MakeInParam("MobilePhone", accountDetail.MobilePhone));
			list.Add(base.Database.MakeInParam("PostalCode", accountDetail.PostalCode));
			list.Add(base.Database.MakeInParam("DwellingPlace", accountDetail.DwellingPlace));
			list.Add(base.Database.MakeInParam("UserNote", accountDetail.UserNote));
			list.Add(base.Database.MakeInParam("Compellation", accountDetail.Compellation));
			list.Add(base.Database.MakeInParam("BankNO", accountDetail.BankNO));
			list.Add(base.Database.MakeInParam("BankName", accountDetail.BankName));
			list.Add(base.Database.MakeInParam("BankAddress", accountDetail.BankAddress));
			list.Add(base.Database.MakeInParam("UserID", accountDetail.UserID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public AccountsProtect GetAccountsProtectByPID(int pid)
		{
			string where = string.Format("(NOLOCK) WHERE ProtectID= N'{0}'", pid);
			return aideAccountsProtectProvider.GetObject<AccountsProtect>(where);
		}

		public AccountsProtect GetAccountsProtectByUserID(int uid)
		{
			string where = string.Format("(NOLOCK) WHERE UserID= N'{0}'", uid);
			AccountsProtect @object = aideAccountsProtectProvider.GetObject<AccountsProtect>(where);
			if (@object != null)
			{
				return @object;
			}
			return new AccountsProtect();
		}

		public void UpdateAccountsProtect(AccountsProtect accountProtect)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE AccountsProtect SET ").Append("Question1=@Question1, ").Append("Response1=@Response1, ")
				.Append("Question2=@Question2, ")
				.Append("Response2=@Response2, ")
				.Append("Question3=@Question3, ")
				.Append("Response3=@Response3, ")
				.Append("SafeEmail=@SafeEmail, ")
				.Append("ModifyDate=getdate()  ")
				.Append("WHERE ProtectID=@ProtectID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("Question1", accountProtect.Question1));
			list.Add(base.Database.MakeInParam("Response1", accountProtect.Response1));
			list.Add(base.Database.MakeInParam("Question2", accountProtect.Question2));
			list.Add(base.Database.MakeInParam("Response2", accountProtect.Response2));
			list.Add(base.Database.MakeInParam("Question3", accountProtect.Question3));
			list.Add(base.Database.MakeInParam("Response3", accountProtect.Response3));
			list.Add(base.Database.MakeInParam("SafeEmail", accountProtect.SafeEmail));
			list.Add(base.Database.MakeInParam("ProtectID", accountProtect.ProtectID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void DeleteAccountsProtect(int pid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("DELETE AccountsProtect WHERE ProtectID={0}; ", pid);
			stringBuilder.AppendFormat("UPDATE AccountsInfo SET ProtectID=0 WHERE ProtectID={0}", pid);
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString());
		}

		public void DeleteAccountsProtect(string sqlQuery)
		{
			aideAccountsProtectProvider.Delete(sqlQuery);
		}

		public DataSet GetAccountsFaceList(int userId)
		{
			string commandText = "SELECT * FROM AccountsFace WHERE UserID=@UserID";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserID", userId));
			return base.Database.ExecuteDataset(CommandType.Text, commandText, list.ToArray());
		}

		public AccountsFace GetAccountsFace(int customId)
		{
			string commandText = "SELECT * FROM AccountsFace WHERE ID=@ID";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ID", customId));
			return base.Database.ExecuteObject<AccountsFace>(commandText, list);
		}

		public DataSet GetReserveIdentifierList()
		{
			string commandText = "SELECT TOP 10 GameID FROM ReserveIdentifier\r\n                                        WHERE Distribute=0 ORDER BY NEWID()";
			return base.Database.ExecuteDataset(CommandType.Text, commandText);
		}

		public PagerSet GetConfineContentList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("ConfineContent", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public ConfineContent GetConfineContentByContentID(int contentID)
		{
			string where = string.Format("(NOLOCK) WHERE ContentID= N'{0}'", contentID);
			ConfineContent @object = aideConfineContentProvider.GetObject<ConfineContent>(where);
			if (@object != null)
			{
				return @object;
			}
			return null;
		}

		public void InsertConfineContent(ConfineContent content)
		{
			if (content.String.IndexOf(",") > 0)
			{
				StringBuilder stringBuilder = new StringBuilder("");
				string[] array = content.String.Split(',');
				foreach (string text in array)
				{
					if (!string.IsNullOrEmpty(text))
					{
						if ("".Equals(stringBuilder.ToString()))
						{
							stringBuilder.AppendFormat("Insert into ConfineContent(String,EnjoinOverDate) values('{0}','{1}')", text, (content.EnjoinOverDate <= (DateTime?)Convert.ToDateTime("1900-01-01")) ? null : content.EnjoinOverDate.ToString());
						}
						else
						{
							stringBuilder.AppendFormat(",('{0}','{1}')", text, (content.EnjoinOverDate <= (DateTime?)Convert.ToDateTime("1900-01-01")) ? null : content.EnjoinOverDate.ToString());
						}
					}
				}
				base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString());
			}
			else
			{
				DataRow dataRow = aideConfineContentProvider.NewRow();
				dataRow["String"] = content.String;
				if (content.EnjoinOverDate > (DateTime?)Convert.ToDateTime("1900-01-01"))
				{
					dataRow["EnjoinOverDate"] = content.EnjoinOverDate;
				}
				dataRow["CollectDate"] = DateTime.Now;
				aideConfineContentProvider.Insert(dataRow);
			}
		}

		public void UpdateConfineContent(ConfineContent content)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE ConfineContent SET ").Append("EnjoinOverDate=@EnjoinOverDate ").Append("WHERE String=@String");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("EnjoinOverDate", (content.EnjoinOverDate <= (DateTime?)Convert.ToDateTime("1900-01-01")) ? null : content.EnjoinOverDate.ToString()));
			list.Add(base.Database.MakeInParam("String", content.String));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void DeleteConfineContentByContentID(int contentID)
		{
			string where = string.Format("WHERE ContentID={0}", contentID);
			aideConfineContentProvider.Delete(where);
		}

		public void DeleteConfineContent(string sqlQuery)
		{
			aideConfineContentProvider.Delete(sqlQuery);
		}

		public bool IsExitStringInConfineContent(string str)
		{
			int result = 0;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select top 1 ContentID from ConfineContent ").Append("WHERE String=@String");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("String", str));
			object obj = base.Database.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), list.ToArray());
			if (obj == null)
			{
				return true;
			}
			if (int.TryParse(obj.ToString(), out result))
			{
				if (result > 0)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public PagerSet GetConfineAddressList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("ConfineAddress", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public ConfineAddress GetConfineAddressByAddress(string strAddress)
		{
			string commandText = string.Format("SELECT * FROM ConfineAddress(NOLOCK) WHERE AddrString=@AddrString");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("AddrString", strAddress));
			return base.Database.ExecuteObject<ConfineAddress>(commandText, list);
		}

		public void InsertConfineAddress(ConfineAddress address)
		{
			DataRow dataRow = aideConfineAddressProvider.NewRow();
			dataRow["AddrString"] = address.AddrString;
			dataRow["EnjoinLogon"] = address.EnjoinLogon;
			dataRow["EnjoinRegister"] = address.EnjoinRegister;
			if (address.EnjoinOverDate > (DateTime?)Convert.ToDateTime("1900-01-01"))
			{
				dataRow["EnjoinOverDate"] = address.EnjoinOverDate;
			}
			dataRow["CollectNote"] = address.CollectNote;
			dataRow["CollectDate"] = DateTime.Now;
			aideConfineAddressProvider.Insert(dataRow);
		}

		public void UpdateConfineAddress(ConfineAddress address)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE ConfineAddress SET ").Append("EnjoinLogon=@EnjoinLogon, ").Append("EnjoinRegister=@EnjoinRegister, ")
				.Append("EnjoinOverDate=@EnjoinOverDate, ")
				.Append("CollectNote=@CollectNote ")
				.Append("WHERE AddrString=@AddrString");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("EnjoinLogon", address.EnjoinLogon));
			list.Add(base.Database.MakeInParam("EnjoinRegister", address.EnjoinRegister));
			list.Add(base.Database.MakeInParam("EnjoinOverDate", (address.EnjoinOverDate <= (DateTime?)Convert.ToDateTime("1900-01-01")) ? null : address.EnjoinOverDate.ToString()));
			list.Add(base.Database.MakeInParam("CollectNote", address.CollectNote));
			list.Add(base.Database.MakeInParam("AddrString", address.AddrString));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void DeleteConfineAddressByAddress(string strAddress)
		{
			string where = string.Format("WHERE AddrString='{0}'", strAddress);
			aideConfineAddressProvider.Delete(where);
		}

		public void DeleteConfineAddress(string sqlQuery)
		{
			aideConfineAddressProvider.Delete(sqlQuery);
		}

		public DataSet GetIPRegisterTop100()
		{
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetIPRegisterTop100");
		}

		public void BatchInsertConfineAddress(string ipList)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strIpList", ipList));
			base.Database.ExecuteNonQuery(CommandType.StoredProcedure, "NET_PM_BatchInsertConfineAddress", list.ToArray());
		}

		public PagerSet GetConfineMachineList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("ConfineMachine", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public ConfineMachine GetConfineMachineBySerial(string strSerial)
		{
			string commandText = string.Format("SELECT * FROM ConfineMachine(NOLOCK) WHERE MachineSerial=@MachineSerial");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("MachineSerial", strSerial));
			return base.Database.ExecuteObject<ConfineMachine>(commandText, list);
		}

		public void InsertConfineMachine(ConfineMachine machine)
		{
			DataRow dataRow = aideConfineMachineProvider.NewRow();
			dataRow["MachineSerial"] = machine.MachineSerial;
			dataRow["EnjoinLogon"] = machine.EnjoinLogon;
			dataRow["EnjoinRegister"] = machine.EnjoinRegister;
			if (machine.EnjoinOverDate > (DateTime?)Convert.ToDateTime("1900-01-01"))
			{
				dataRow["EnjoinOverDate"] = machine.EnjoinOverDate;
			}
			dataRow["CollectNote"] = machine.CollectNote;
			dataRow["CollectDate"] = DateTime.Now;
			aideConfineMachineProvider.Insert(dataRow);
		}

		public void UpdateConfineMachine(ConfineMachine machine)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE ConfineMachine SET ").Append("EnjoinLogon=@EnjoinLogon, ").Append("EnjoinRegister=@EnjoinRegister, ")
				.Append("EnjoinOverDate=@EnjoinOverDate, ")
				.Append("CollectNote=@CollectNote ")
				.Append("WHERE MachineSerial=@MachineSerial");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("EnjoinLogon", machine.EnjoinLogon));
			list.Add(base.Database.MakeInParam("EnjoinRegister", machine.EnjoinRegister));
			list.Add(base.Database.MakeInParam("EnjoinOverDate", (machine.EnjoinOverDate <= (DateTime?)Convert.ToDateTime("1900-01-01")) ? null : machine.EnjoinOverDate.ToString()));
			list.Add(base.Database.MakeInParam("CollectNote", machine.CollectNote));
			list.Add(base.Database.MakeInParam("MachineSerial", machine.MachineSerial));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void DeleteConfineMachineBySerial(string strSerial)
		{
			string where = string.Format("WHERE MachineSerial='{0}'", strSerial);
			aideConfineMachineProvider.Delete(where);
		}

		public void DeleteConfineMachine(string sqlQuery)
		{
			aideConfineMachineProvider.Delete(sqlQuery);
		}

		public DataSet GetMachineRegisterTop100()
		{
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetMachineRegisterTop100");
		}

		public void BatchInsertConfineMachine(string machineList)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strMachineList", machineList));
			base.Database.ExecuteNonQuery(CommandType.StoredProcedure, "NET_PM_BatchInsertConfineMachine", list.ToArray());
		}

		public DataTable SystemCount()
		{
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "P_Acc_SystemCount").Tables[0];
		}

		public PagerSet GetSystemStatusInfoList(int pageIndex, int pageSize, string condition, string orderby)
		{
			PagerParameters prams = new PagerParameters("SystemStatusInfo", orderby, condition, pageIndex, pageSize);
			return GetPagerSet2(prams);
		}

		public SystemStatusInfo GetSystemStatusInfo(string statusName)
		{
			string where = string.Format("(NOLOCK) WHERE StatusName= '{0}'", statusName);
			return aideSystemStatusInfoProvider.GetObject<SystemStatusInfo>(where);
		}

		public void UpdateSystemStatusInfo(SystemStatusInfo systemStatusInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE SystemStatusInfo SET ").Append("StatusValue=@StatusValue, ").Append("StatusString=@StatusString, ")
				.Append("StatusTip=@StatusTip, ")
				.Append("StatusDescription=@StatusDescription ")
				.Append("WHERE StatusName=@StatusName");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("StatusValue", systemStatusInfo.StatusValue));
			list.Add(base.Database.MakeInParam("StatusString", systemStatusInfo.StatusString));
			list.Add(base.Database.MakeInParam("StatusTip", systemStatusInfo.StatusTip));
			list.Add(base.Database.MakeInParam("StatusDescription", systemStatusInfo.StatusDescription));
			list.Add(base.Database.MakeInParam("StatusName", systemStatusInfo.StatusName));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public DataSet GetRegUserByDays(string startDate, string endDate)
		{
			string empty = string.Empty;
			empty = "SELECT DateID,WebRegisterSuccess+GameRegisterSuccess AS RegisterCount FROM SystemStreamInfo";
			empty += " WHERE CollectDate>=@StartDate AND CollectDate<=@EndDate";
			empty += " ORDER BY DateID ASC";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("StartDate", startDate));
			list.Add(base.Database.MakeInParam("EndDate", endDate + " 23:59:59"));
			return base.Database.ExecuteDataset(CommandType.Text, empty, list.ToArray());
		}

		public DataSet GetRegUserByHours(string startDate, string endDate)
		{
			string str = "SELECT COUNT(RegisterDate) AS RegisterCount,DATEPART(hh,RegisterDate) AS StatDate FROM AccountsInfo(NOLOCK)";
			str += " WHERE RegisterDate>=@StartDate AND RegisterDate<=@EndDate";
			str += " GROUP BY DATEPART(hh,RegisterDate) ORDER BY StatDate ASC";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("StartDate", startDate));
			list.Add(base.Database.MakeInParam("EndDate", endDate));
			return base.Database.ExecuteDataset(CommandType.Text, str, list.ToArray());
		}

		public DataSet GetRegUserByMonth()
		{
			string str = "SELECT SUM(WebRegisterSuccess+GameRegisterSuccess) AS RegisterCount,CONVERT(char(7),CollectDate,120) AS StatDate FROM SystemStreamInfo";
			str += " GROUP BY CONVERT(char(7),CollectDate,120) ORDER BY StatDate ASC";
			return base.Database.ExecuteDataset(str);
		}

		public DataSet GetUsersStat()
		{
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PM_AnalUserStat", (DbParameter[])null);
		}

		public DataSet GetUsersNumber()
		{
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PM_UsersNumberStat", (DbParameter[])null);
		}

		public Message AppStatLogon(string accounts, string logonPass, string machineID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_StatLogin", list);
		}

		public Message AppStatUserActive(string accounts, string logonPass, string machineID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_StatUserActive", list);
		}

		public Message AppStatUserMember(string accounts, string logonPass, string machineID)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_StatUserMember", list);
		}

		public Message AppGetLogonData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeInParam("dwTypeID", typeID));
			list.Add(base.Database.MakeInParam("dwDateType", dateType));
			list.Add(base.Database.MakeInParam("strStartDate", startDate));
			list.Add(base.Database.MakeInParam("strEndDate", endDate));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_GetLogonData", list);
		}

		public Message AppGetRegData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("strAccounts", accounts));
			list.Add(base.Database.MakeInParam("strPassword", logonPass));
			list.Add(base.Database.MakeInParam("strMachineSerial", machineID));
			list.Add(base.Database.MakeInParam("dwTypeID", typeID));
			list.Add(base.Database.MakeInParam("dwDateType", dateType));
			list.Add(base.Database.MakeInParam("strStartDate", startDate));
			list.Add(base.Database.MakeInParam("strEndDate", endDate));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessageForDataSet(base.Database, "APP_PM_GetRegData", list);
		}

		public ControlConfig GetControlConfig()
		{
			string commandText = "SELECT * FROM ControlConfig";
			return base.Database.ExecuteObject<ControlConfig>(commandText);
		}

		public void UpdateControlConfig(ControlConfig model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE ControlConfig SET AutoControlEnable=@AutoControlEnable,");
			stringBuilder.Append("JoinBlackWinScore=@JoinBlackWinScore,");
			stringBuilder.Append("JoinWhiteLoseScore=@JoinWhiteLoseScore,");
			stringBuilder.Append("BlackControlType=@BlackControlType,");
			stringBuilder.Append("BSustainedTimeCount=@BSustainedTimeCount,");
			stringBuilder.Append("QuitBlackLoseScore=@QuitBlackLoseScore,");
			stringBuilder.Append("WhiteControlType=@WhiteControlType,");
			stringBuilder.Append("WSustainedTimeCount=@WSustainedTimeCount,");
			stringBuilder.Append("QuitWhiteWinScore=@QuitWhiteWinScore,");
			stringBuilder.Append("BlackWinRate=@BlackWinRate,");
			stringBuilder.Append("WhiteWinRate=@WhiteWinRate ");
			stringBuilder.Append("IF @@ROWCOUNT=0 BEGIN ");
			stringBuilder.Append("INSERT INTO ControlConfig ");
			stringBuilder.Append("VALUES(@AutoControlEnable,@JoinBlackWinScore,@JoinWhiteLoseScore,@BlackControlType,@BSustainedTimeCount,");
			stringBuilder.Append("@QuitBlackLoseScore,@WhiteControlType,@WSustainedTimeCount,@QuitWhiteWinScore,@BlackWinRate,@WhiteWinRate) ");
			stringBuilder.Append("END");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("AutoControlEnable", model.AutoControlEnable));
			list.Add(base.Database.MakeInParam("JoinBlackWinScore", model.JoinBlackWinScore));
			list.Add(base.Database.MakeInParam("JoinWhiteLoseScore", model.JoinWhiteLoseScore));
			list.Add(base.Database.MakeInParam("BlackControlType", model.BlackControlType));
			list.Add(base.Database.MakeInParam("BSustainedTimeCount", model.BSustainedTimeCount));
			list.Add(base.Database.MakeInParam("QuitBlackLoseScore", model.QuitBlackLoseScore));
			list.Add(base.Database.MakeInParam("WhiteControlType", model.WhiteControlType));
			list.Add(base.Database.MakeInParam("WSustainedTimeCount", model.WSustainedTimeCount));
			list.Add(base.Database.MakeInParam("QuitWhiteWinScore", model.QuitWhiteWinScore));
			list.Add(base.Database.MakeInParam("BlackWinRate", model.BlackWinRate));
			list.Add(base.Database.MakeInParam("WhiteWinRate", model.WhiteWinRate));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void DeleteAccountsControl(string where)
		{
			aideAccountsControlProvider.Delete(where);
		}

		public void AddAccountsControl(AccountsControl model)
		{
			DataRow dataRow = aideAccountsControlProvider.NewRow();
			dataRow["UserID"] = model.UserID;
			dataRow["Accounts"] = model.Accounts;
			dataRow["ActiveDateTime"] = model.ActiveDateTime;
			dataRow["ControlStatus"] = model.ControlStatus;
			dataRow["ControlType"] = model.ControlType;
			dataRow["ChangeScore"] = model.ChangeScore;
			dataRow["SustainedTimeCount"] = model.SustainedTimeCount;
			dataRow["WinRate"] = model.WinRate;
			dataRow["ConsumeTimeCount"] = 0;
			dataRow["WinScore"] = 0;
			dataRow["LoseScore"] = 0;
			aideAccountsControlProvider.Insert(dataRow);
		}

		public void UpdateAccountsControl(AccountsControl model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE AccountsControl SET ");
			stringBuilder.Append("ActiveDateTime=@ActiveDateTime,");
			stringBuilder.Append("ControlStatus=@ControlStatus,");
			stringBuilder.Append("ControlType=@ControlType,");
			stringBuilder.Append("ChangeScore=@ChangeScore,");
			stringBuilder.Append("SustainedTimeCount=@SustainedTimeCount,");
			stringBuilder.Append("WinRate=@WinRate ");
			stringBuilder.Append("WHERE UserID=@UserID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ActiveDateTime", model.ActiveDateTime));
			list.Add(base.Database.MakeInParam("ControlStatus", model.ControlStatus));
			list.Add(base.Database.MakeInParam("ControlType", model.ControlType));
			list.Add(base.Database.MakeInParam("ChangeScore", model.ChangeScore));
			list.Add(base.Database.MakeInParam("SustainedTimeCount", model.SustainedTimeCount));
			list.Add(base.Database.MakeInParam("WinRate", model.WinRate));
			list.Add(base.Database.MakeInParam("UserID", model.UserID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public AccountsControl GetAccountsControl(int id)
		{
			string where = string.Format(" WHERE UserID={0}", id);
			return aideAccountsControlProvider.GetObject<AccountsControl>(where);
		}

		public void DongjieAgent(string sqlQuery)
		{
			string commandText = string.Format("UPDATE AccountsAgent SET Nullity=1 {0}", sqlQuery);
			base.Database.ExecuteNonQuery(commandText);
		}

		public void JieDongAgent(string sqlQuery)
		{
			string commandText = string.Format("UPDATE AccountsAgent SET Nullity=0 {0}", sqlQuery);
			base.Database.ExecuteNonQuery(commandText);
		}

		public AccountsAgent GetAccountAgentByUserID(int userID)
		{
			string where = string.Format("(NOLOCK) WHERE UserID= N'{0}'", userID);
			AccountsAgent @object = aideAccountsAgent.GetObject<AccountsAgent>(where);
			if (@object != null)
			{
				return @object;
			}
			return new AccountsAgent();
		}

		public AccountsAgent GetAccountAgentByDomain(string domain)
		{
			string where = string.Format("(NOLOCK) WHERE Domain= N'{0}'", domain);
			return aideAccountsAgent.GetObject<AccountsAgent>(where);
		}

		public Message AddAgent(AccountsAgent agent)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwUserID", agent.UserID));
			list.Add(base.Database.MakeInParam("strCompellation", agent.Compellation));
			list.Add(base.Database.MakeInParam("strDomain", agent.Domain));
			list.Add(base.Database.MakeInParam("dwAgentType", agent.AgentType));
			list.Add(base.Database.MakeInParam("dcAgentScale", agent.AgentScale));
			list.Add(base.Database.MakeInParam("dwPayBackScore", agent.PayBackScore));
			list.Add(base.Database.MakeInParam("dcPayBackScale", agent.PayBackScale));
			list.Add(base.Database.MakeInParam("strMobilePhone", agent.MobilePhone));
			list.Add(base.Database.MakeInParam("strEMail", agent.EMail));
			list.Add(base.Database.MakeInParam("strDwellingPlace", agent.DwellingPlace));
			list.Add(base.Database.MakeInParam("dwNullity", agent.Nullity));
			list.Add(base.Database.MakeInParam("strAgentNote", agent.AgentNote));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			return MessageHelper.GetMessage(base.Database, "NET_PM_AddAgent", list);
		}

		public bool UpdateAgent(AccountsAgent agent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE AccountsAgent SET ").Append("Compellation=@Compellation, ").Append("Domain=@Domain, ")
				.Append("AgentType=@AgentType, ")
				.Append("AgentScale=@AgentScale, ")
				.Append("PayBackScore=@PayBackScore, ")
				.Append("PayBackScale=@PayBackScale, ")
				.Append("MobilePhone=@MobilePhone, ")
				.Append("EMail=@EMail, ")
				.Append("DwellingPlace=@DwellingPlace, ")
				.Append("AgentNote=@AgentNote ")
				.Append("WHERE UserID=@UserID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserID", agent.UserID));
			list.Add(base.Database.MakeInParam("Compellation", agent.Compellation));
			list.Add(base.Database.MakeInParam("Domain", agent.Domain));
			list.Add(base.Database.MakeInParam("AgentType", agent.AgentType));
			list.Add(base.Database.MakeInParam("AgentScale", agent.AgentScale));
			list.Add(base.Database.MakeInParam("PayBackScore", agent.PayBackScore));
			list.Add(base.Database.MakeInParam("PayBackScale", agent.PayBackScale));
			list.Add(base.Database.MakeInParam("MobilePhone", agent.MobilePhone));
			list.Add(base.Database.MakeInParam("EMail", agent.EMail));
			list.Add(base.Database.MakeInParam("DwellingPlace", agent.DwellingPlace));
			list.Add(base.Database.MakeInParam("AgentNote", agent.AgentNote));
			if (base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray()) > 0)
			{
				return true;
			}
			return false;
		}

		public decimal GetDecSum(string sql)
		{
			object value = base.Database.ExecuteScalar(CommandType.Text, sql);
			return Convert.ToDecimal(value);
		}

		public int GetIntSum(string sql)
		{
			object value = base.Database.ExecuteScalar(CommandType.Text, sql);
			return Convert.ToInt32(value);
		}

		public long GetLongSum(string sql)
		{
			object value = base.Database.ExecuteScalar(CommandType.Text, sql);
			return Convert.ToInt64(value);
		}

		public int GetAgentChildCount(int userID)
		{
			string commandText = string.Format("SELECT COUNT(UserID) FROM AccountsInfo WHERE SpreaderID= {0}", userID);
			object obj = base.Database.ExecuteScalar(CommandType.Text, commandText);
			if (obj == null || obj.ToString().Length <= 0)
			{
				return 0;
			}
			return Convert.ToInt32(obj);
		}

		public AccountsAgentGame GetAccountsAgentGameInfo(int id)
		{
			string where = string.Format("(NOLOCK) WHERE ID= {0}", id);
			return aideAccountsAgentGame.GetObject<AccountsAgentGame>(where);
		}

		public void InsertAccountsAgentGame(AccountsAgentGame model)
		{
			DataRow dataRow = aideAccountsAgentGame.NewRow();
			dataRow["AgentID"] = model.AgentID;
			dataRow["KindID"] = model.KindID;
			dataRow["DeviceID"] = model.DeviceID;
			dataRow["SortID"] = model.SortID;
			dataRow["CollectDate"] = model.CollectDate;
			aideAccountsAgentGame.Insert(dataRow);
		}

		public void UpdateAccountsAgentGame(AccountsAgentGame model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE AccountsAgentGame SET ").Append("SortID=@SortID ").Append("WHERE ID=@ID");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("SortID", model.SortID));
			list.Add(base.Database.MakeInParam("ID", model.ID));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		public void DeleteAccountsAgentGame(string sqlQuery)
		{
			aideAccountsAgentGame.Delete(sqlQuery);
		}

		public DataSet GetMemberPropertyList()
		{
			string commandText = "SELECT * FROM MemberProperty ORDER BY MemberOrder ASC";
			return base.Database.ExecuteDataset(commandText);
		}

		public MemberProperty GetMemberProperty(int memberOrder)
		{
			string where = " WHERE MemberOrder=" + memberOrder;
			return aideMemberProperty.GetObject<MemberProperty>(where);
		}

		public void UpdateMemberType(MemberProperty memberType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("UPDATE MemberProperty SET ").Append("UserRight=@UserRight,").Append("TaskRate=@TaskRate,")
				.Append("ShopRate=@ShopRate,")
				.Append("InsureRate=@InsureRate,")
				.Append("DayPresent=@DayPresent,")
				.Append("DayGiftID=@DayGiftID")
				.Append(" WHERE MemberOrder= @MemberOrder");
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserRight", memberType.UserRight));
			list.Add(base.Database.MakeInParam("TaskRate", memberType.TaskRate));
			list.Add(base.Database.MakeInParam("ShopRate", memberType.ShopRate));
			list.Add(base.Database.MakeInParam("InsureRate", memberType.InsureRate));
			list.Add(base.Database.MakeInParam("DayPresent", memberType.DayPresent));
			list.Add(base.Database.MakeInParam("DayGiftID", memberType.DayGiftID));
			list.Add(base.Database.MakeInParam("MemberOrder", memberType.MemberOrder));
			base.Database.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
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

		public CashSetting PlayerCashInfo(int id)
		{
			string commandText = "SELECT IsOpen,IsSale,GameCnt,WeekOpenDay,Shour,Ehour,DailyApplyTimes,BalancePrice,MinBalance,CounterFee,MinCounterFee,MinPlayerScore,DrawMultiple,IsMobileSale FROM T_Acc_DealSet WHERE ID=" + id;
			return base.Database.ExecuteObject<CashSetting>(commandText);
		}

		public void UpdatePlayerSetting(CashSetting model)
		{
			string commandText = "UPDATE T_Acc_DealSet SET IsOpen=@IsOpen,IsSale=@IsSale,GameCnt=@GameCnt,WeekOpenDay=@WeekOpenDay,Shour=@Shour,Ehour=@Ehour, DailyApplyTimes=@DailyApplyTimes, BalancePrice=@BalancePrice,MinBalance=@MinBalance,CounterFee=@CounterFee,MinCounterFee=@MinCounterFee,MinPlayerScore=@MinPlayerScore,DrawMultiple=@DrawMultiple,IsMobileSale=@IsMobileSale WHERE ID=@ID";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ID", model.ID));
			list.Add(base.Database.MakeInParam("IsOpen", model.IsOpen));
			list.Add(base.Database.MakeInParam("IsSale", model.IsSale));
			list.Add(base.Database.MakeInParam("GameCnt", model.GameCnt));
			list.Add(base.Database.MakeInParam("WeekOpenDay", model.WeekOpenDay));
			list.Add(base.Database.MakeInParam("Shour", model.Shour));
			list.Add(base.Database.MakeInParam("Ehour", model.Ehour));
			list.Add(base.Database.MakeInParam("DailyApplyTimes", model.DailyApplyTimes));
			list.Add(base.Database.MakeInParam("BalancePrice", model.BalancePrice));
			list.Add(base.Database.MakeInParam("MinBalance", model.MinBalance));
			list.Add(base.Database.MakeInParam("CounterFee", model.CounterFee / 100.0));
			list.Add(base.Database.MakeInParam("MinCounterFee", model.MinCounterFee));
			list.Add(base.Database.MakeInParam("MinPlayerScore", model.MinPlayerScore));
			list.Add(base.Database.MakeInParam("DrawMultiple", model.DrawMultiple));
			list.Add(base.Database.MakeInParam("IsMobileSale", model.IsMobileSale));
			base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		public int TradeOper(int type, int id)
		{
			int result = 0;
			try
			{
				string commandText = "";
				switch (type)
				{
				case 1:
					commandText = "UPDATE ApplyOrder SET Status=@type where ID=@id and Status=4";
					break;
				case 4:
					commandText = "UPDATE ApplyOrder SET Status=@type where ID=@id and Status=1";
					break;
				}
				List<DbParameter> list = new List<DbParameter>();
				list.Add(base.Database.MakeInParam("id", id));
				list.Add(base.Database.MakeInParam("type", type));
				result = base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
				return result;
			}
			catch (Exception)
			{
				return result;
			}
		}

		public int TradePay_DaiFu(string operateUser, int tradeId, string orderId, string bankAccount, string bankAccountCode, string bankAmount, string selectBankCode, string bankName)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("TradeId", tradeId));
			list.Add(base.Database.MakeInParam("OrderID", orderId));
			list.Add(base.Database.MakeInParam("BankAccounts", bankAccount));
			list.Add(base.Database.MakeInParam("BankName", bankName));
			list.Add(base.Database.MakeInParam("BankCardNo", bankAccountCode));
			list.Add(base.Database.MakeInParam("Amount", bankAmount));
			list.Add(base.Database.MakeInParam("BankCode", selectBankCode));
			list.Add(base.Database.MakeInParam("OperateUser", operateUser));
			list.Add(base.Database.MakeInParam("Amount", bankAmount));
			base.Database.RunProc("NET_PW_Trade_DaiFu", list);
			return Convert.ToInt32(list[list.Count - 1].Value);
		}

		public int Trade_DaiFu_ApiStatus(string operateUser, int tradeId, string orderId, string apiErrorMessage, int status)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("TradeId", tradeId));
			list.Add(base.Database.MakeInParam("OrderID", orderId));
			list.Add(base.Database.MakeInParam("Ostatus", status));
			list.Add(base.Database.MakeInParam("ApiErrorMessage", apiErrorMessage));
			list.Add(base.Database.MakeInParam("OperateUser", operateUser));
			base.Database.RunProc("NET_PM_Trade_DaiFu_ApiStatus", list);
			return Convert.ToInt32(list[list.Count - 1].Value);
		}

		public decimal SumPlayerDraw(string sWhere)
		{
			string commandText = "select sum(SellScore) from View_ApplyOrder " + sWhere;
			object obj = base.Database.ExecuteScalar(CommandType.Text, commandText);
			if (obj is DBNull)
			{
				return 0m;
			}
			return Convert.ToDecimal(obj);
		}

		public int GetUndoApplyOrderCount()
		{
			string commandText = "select count(ID) from View_ApplyOrder where Status=1 ";
			object obj = base.Database.ExecuteScalar(CommandType.Text, commandText);
			if (obj is DBNull)
			{
				return 0;
			}
			return Convert.ToInt32(obj);
		}

		public int GetUndoAgentDrawCount()
		{
			string commandText = "select count(ID) from RYAgentDB..View_AgentDraw where DealStatus=0 ";
			object obj = base.Database.ExecuteScalar(CommandType.Text, commandText);
			if (obj is DBNull)
			{
				return 0;
			}
			return Convert.ToInt32(obj);
		}

		public void UpdateAgentBaseURL(AgentStatusInfo model)
		{
			string commandText = "UPDATE RYAgentDB..T_Acc_AgentStatusInfo SET regTgAddr1=@regTgAddr1,regTgAddr2=@regTgAddr2,regTgAddr3=@regTgAddr3,mainTgAddr1=@mainTgAddr1, mainTgAddr2=@mainTgAddr2, mainTgAddr3=@mainTgAddr3";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("regTgAddr1", model.regTgAddr1));
			list.Add(base.Database.MakeInParam("regTgAddr2", model.regTgAddr2));
			list.Add(base.Database.MakeInParam("regTgAddr3", model.regTgAddr3));
			list.Add(base.Database.MakeInParam("mainTgAddr1", model.mainTgAddr1));
			list.Add(base.Database.MakeInParam("mainTgAddr2", model.mainTgAddr2));
			list.Add(base.Database.MakeInParam("mainTgAddr3", model.mainTgAddr3));
			base.Database.ExecuteDataset(CommandType.Text, commandText, list.ToArray());
		}

		public Message RegAegnt(AgentInfo model)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("ParentID", model.AgentID));
			list.Add(base.Database.MakeInParam("agentAcc", model.AgentAcc));
			list.Add(base.Database.MakeInParam("Pwd", model.Pwd));
			list.Add(base.Database.MakeInParam("SafePwd", ""));
			list.Add(base.Database.MakeInParam("QQ", model.QQ));
			list.Add(base.Database.MakeInParam("memo", model.Memo));
			list.Add(base.Database.MakeInParam("RealName", model.RealName));
			list.Add(base.Database.MakeInParam("agentRate", model.AgentRate));
			list.Add(base.Database.MakeInParam("agentDomain", model.AgentDomain));
			list.Add(base.Database.MakeInParam("clientIP", model.RegIP));
			list.Add(base.Database.MakeOutParam("strErr", typeof(string), 127));
			return MessageHelper.GetMessage(base.Database, "RYAgentDB..P_Acc_AddAgent", list);
		}

		public Message AgentDraw(AgentInfo agent)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwAgentID", agent.AgentID));
			list.Add(base.Database.MakeInParam("dwScore", agent.Score));
			list.Add(base.Database.MakeInParam("strSafePwd", ""));
			list.Add(base.Database.MakeInParam("strBankName", agent.BankName));
			list.Add(base.Database.MakeInParam("strBankAcc", agent.BankAcc));
			list.Add(base.Database.MakeInParam("strBankAddr", agent.BankAddress));
			list.Add(base.Database.MakeInParam("strOrderID", agent.Memo));
			list.Add(base.Database.MakeInParam("strClientIP", agent.LastIP));
			list.Add(base.Database.MakeOutParam("strErr", typeof(string), 127));
			return MessageHelper.GetMessage(base.Database, "RYAgentDB..P_Acc_AgentDraw", list);
		}

		public Message AgentRecharge(AgentInfo model, byte opType = 0)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("AgentID", model.AgentID));
			list.Add(base.Database.MakeInParam("Score", model.Score));
			list.Add(base.Database.MakeInParam("opType", opType));
			list.Add(base.Database.MakeInParam("Operator", model.Operator));
			list.Add(base.Database.MakeInParam("ClientIP", model.RegIP));
			list.Add(base.Database.MakeOutParam("strErr", typeof(string), 127));
			return MessageHelper.GetMessage(base.Database, "RYAgentDB..P_Acc_AgentRecharge", list);
		}

		public double SumAgentPlayerPay(string sWhere)
		{
			string commandText = "SELECT SUM(PresentScore) FROM RYAgentDB..view_PlayerFillLog " + sWhere;
			object obj = base.Database.ExecuteScalar(CommandType.Text, commandText);
			if (obj is DBNull)
			{
				return 0.0;
			}
			return Convert.ToDouble(obj);
		}

		public void UpdateAegntBank(AgentInfo model)
		{
			string commandText = "UPDATE RYAgentDB..T_Acc_Agent SET BankAcc=@BankAcc,BankName=@BankName,BankAddress=@BankAddress WHERE AgentID=@AgentID";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("AgentID", model.AgentID));
			list.Add(base.Database.MakeInParam("BankAcc", model.BankAcc));
			list.Add(base.Database.MakeInParam("BankName", model.BankName));
			list.Add(base.Database.MakeInParam("BankAddress", model.BankAddress));
			base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		public void UpdateAegntPwd(AgentInfo model)
		{
			string commandText = "UPDATE RYAgentDB..T_Acc_Agent SET Pwd=@Pwd WHERE AgentID=@AgentID";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("AgentID", model.AgentID));
			list.Add(base.Database.MakeInParam("Pwd", model.Pwd));
			base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		public Message AgentUserRecharge(AgentInfo model)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("UserID", model.AgentID));
			list.Add(base.Database.MakeInParam("Score", model.Score));
			list.Add(base.Database.MakeInParam("Operator", model.Operator));
			list.Add(base.Database.MakeInParam("ClientIP", model.RegIP));
			list.Add(base.Database.MakeOutParam("strErr", typeof(string), 127));
			return MessageHelper.GetMessage(base.Database, "RYAgentDB..P_Acc_AgentUserRecharge", list);
		}

		public Message AgentLogonNew(AgentInfo agent)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("AgentAcc", agent.AgentAcc));
			list.Add(base.Database.MakeInParam("Pwd", agent.Pwd));
			list.Add(base.Database.MakeInParam("strClientIP", agent.LastIP));
			list.Add(base.Database.MakeOutParam("strErr", typeof(string), 127));
			return MessageHelper.GetMessageForObject<AgentInfo>(base.Database, "RYAgentDB..P_Acc_AgentLogon", list);
		}

		public DataSet AgentScore(int aid, byte type = 0)
		{
			string text = "SELECT Score FROM RYAgentDB..T_Acc_Agent (NOLOCK)  WHERE AgentID=" + aid;
			if (type > 0)
			{
				object obj = text;
				text = obj + ";SELECT ISNULL(SUM(PayAmount),0) FROM RYTreasureDB.dbo.View_NextPay WHERE AUShow=1 AND ParentID=" + aid + " AND DateDiff(dd,ApplyDate,getdate())=0";
			}
			return base.Database.ExecuteDataset(CommandType.Text, text);
		}

		public AgentStatusInfo AgentBaseURL()
		{
			string commandText = "SELECT TOP 1 regTgAddr1,regTgAddr2,regTgAddr3,mainTgAddr1, mainTgAddr2, mainTgAddr3 FROM RYAgentDB..T_Acc_AgentStatusInfo";
			return base.Database.ExecuteObject<AgentStatusInfo>(commandText);
		}

		public AgentInfo GetAgentDetail(int aid)
		{
			string commandText = "SELECT AgentID,AgentAcc,RealName,QQ,BankName,BankAcc,BankAddress,Score,AgentRate*100 as AgentRate,AgentStatus,ParentAcc,AgentDomain,Memo,ShowName,WeChat,IsClient,ShowSort,QueryRight FROM RYAgentDB..T_Acc_Agent (NOLOCK)  WHERE AgentID=" + aid;
			return base.Database.ExecuteObject<AgentInfo>(commandText);
		}

		public void UpdateAgentSetting(AgentSetting model)
		{
			string commandText = "UPDATE RYAgentDB..T_Acc_AgentStatusInfo SET IsOpen=@IsOpen,WeekOpenDay=@WeekOpenDay,SOpenTime=@SOpenTime,EOpenTime=@EOpenTime, DailyApplyTimes=@DailyApplyTimes, BalancePrice=@BalancePrice,MinBalance=@MinBalance,CounterFee=@CounterFee,MinCounterFee=@MinCounterFee,DiffAgentRate=@DiffAgentRate,MaxAgentRate=@MaxAgentRate";
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("IsOpen", model.IsOpen));
			list.Add(base.Database.MakeInParam("WeekOpenDay", model.WeekOpenDay));
			list.Add(base.Database.MakeInParam("SOpenTime", model.SOpenTime));
			list.Add(base.Database.MakeInParam("EOpenTime", model.EOpenTime));
			list.Add(base.Database.MakeInParam("DailyApplyTimes", model.DailyApplyTimes));
			list.Add(base.Database.MakeInParam("BalancePrice", model.BalancePrice));
			list.Add(base.Database.MakeInParam("MinBalance", model.MinBalance));
			list.Add(base.Database.MakeInParam("CounterFee", model.CounterFee / 100.0));
			list.Add(base.Database.MakeInParam("MinCounterFee", model.MinCounterFee));
			list.Add(base.Database.MakeInParam("DiffAgentRate", model.DiffAgentRate));
			list.Add(base.Database.MakeInParam("MaxAgentRate", model.MaxAgentRate));
			base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		public Message UpdateAegnt(AgentInfo model)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("AgentID", model.AgentID));
			list.Add(base.Database.MakeInParam("IsClient", model.IsClient));
			list.Add(base.Database.MakeInParam("Pwd", model.Pwd));
			list.Add(base.Database.MakeInParam("SafePwd", ""));
			list.Add(base.Database.MakeInParam("AgentRate", model.AgentRate));
			list.Add(base.Database.MakeInParam("RealName", model.RealName));
			list.Add(base.Database.MakeInParam("QQ", model.QQ));
			list.Add(base.Database.MakeInParam("WeChat", model.WeChat));
			list.Add(base.Database.MakeInParam("ShowName", model.ShowName));
			list.Add(base.Database.MakeInParam("ShowSort", model.ShowSort));
			list.Add(base.Database.MakeInParam("AgentDomain", model.AgentDomain));
			list.Add(base.Database.MakeInParam("Memo", model.Memo));
			list.Add(base.Database.MakeInParam("BankAcc", model.BankAcc));
			list.Add(base.Database.MakeInParam("BankName", (model.BankName == null) ? "" : model.BankName));
			list.Add(base.Database.MakeInParam("BankAddress", model.BankAddress));
			list.Add(base.Database.MakeInParam("AgentStatus", model.AgentStatus));
			list.Add(base.Database.MakeInParam("Operator", model.Operator));
			list.Add(base.Database.MakeInParam("ClientIP", model.RegIP));
			list.Add(base.Database.MakeInParam("QueryRight", model.QueryRight));
			list.Add(base.Database.MakeOutParam("strErr", typeof(string), 127));
			return MessageHelper.GetMessage(base.Database, "RYAgentDB..P_Acc_AgentDetail", list);
		}

		public DataTable MySpreadInfo(int uid, int dateType)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwUserID", uid));
			list.Add(base.Database.MakeInParam("dateType", dateType));
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "RYAgentDB..P_Acc_MySpreadInfo", list.ToArray()).Tables[0];
		}

		public DataTable MyOwnSpreadCount(int uid)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwUserID", uid));
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "RYAgentDB..P_Acc_MyOwnSpreadCount", list.ToArray()).Tables[0];
		}

		public Message SpreadBalance(int uid, double balance, string ip)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwUserID", uid));
			list.Add(base.Database.MakeInParam("dwBalance", balance));
			list.Add(base.Database.MakeInParam("strClientIP", ip));
			list.Add(base.Database.MakeOutParam("strErr", typeof(string), 127));
			return MessageHelper.GetMessage(base.Database, "RYAgentDB..P_Acc_SpreadBalance", list);
		}

		public DataTable MySpreadCntInfo(int uid, int type)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwUserID", uid));
			list.Add(base.Database.MakeInParam("type", type));
			return base.Database.ExecuteDataset(CommandType.StoredProcedure, "RYAgentDB..P_Acc_MySpreadCntInfo", list.ToArray()).Tables[0];
		}

		public PagerSet QuerySpreadScore(int uid, int type, int recordType, DateTime bTime, DateTime eTime, int pageIndex, int pageSize)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwUserID", uid));
			list.Add(base.Database.MakeInParam("type", type));
			list.Add(base.Database.MakeInParam("recordType", recordType));
			list.Add(base.Database.MakeInParam("bTime", bTime));
			list.Add(base.Database.MakeInParam("eTime", eTime));
			list.Add(base.Database.MakeInParam("PageIndex", pageIndex));
			list.Add(base.Database.MakeInParam("PageSize", pageSize));
			list.Add(base.Database.MakeOutParam("PageCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("RecordCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("StrErr", typeof(string), 127));
			string commandText = "RYAgentDB..P_Acc_QuerySpreadScore";
			DataSet pageSet = base.Database.ExecuteDataset(CommandType.StoredProcedure, commandText, list.ToArray());
			return new PagerSet(1, 1, 1, Convert.ToInt32(list[list.Count - 3].Value), pageSet);
		}

		public PagerSet QuerySpreadDailyRpt(int uid, string account, int lev, DateTime bTime, DateTime eTime, int pageIndex, int pageSize)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwUserID", uid));
			list.Add(base.Database.MakeInParam("gxAcc", account));
			list.Add(base.Database.MakeInParam("lev", lev));
			list.Add(base.Database.MakeInParam("bTime", bTime));
			list.Add(base.Database.MakeInParam("eTime", eTime));
			list.Add(base.Database.MakeInParam("PageIndex", pageIndex));
			list.Add(base.Database.MakeInParam("PageSize", pageSize));
			list.Add(base.Database.MakeOutParam("PageCount", typeof(int)));
			list.Add(base.Database.MakeOutParam("RecordCount", typeof(int)));
			string commandText = "RYAgentDB..P_Acc_QuerySpreadDailyRpt";
			DataSet pageSet = base.Database.ExecuteDataset(CommandType.StoredProcedure, commandText, list.ToArray());
			return new PagerSet(1, 1, 1, Convert.ToInt32(list[list.Count - 3].Value), pageSet);
		}

		public DataTable GetUserGoldException(string sTime, string eTime)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("sTime", Convert.ToDateTime(sTime)));
			list.Add(base.Database.MakeInParam("eTime", Convert.ToDateTime(eTime).AddDays(1.0)));
			list.Add(base.Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
			DataSet ds = null;
			base.Database.RunProc("NET_PW_GetUserGoldExcept", list, out ds);
			return ds.Tables[0];
		}

		public void InsertAndroid(AndroidConfigure android)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("wServerID", android.ServerID));
			list.Add(base.Database.MakeInParam("dwServiceMode", android.ServiceMode));
			list.Add(base.Database.MakeInParam("dwAndroidCount", android.AndroidCount));
			list.Add(base.Database.MakeInParam("dwEnterTime", android.EnterTime));
			list.Add(base.Database.MakeInParam("dwLeaveTime", android.LeaveTime));
			list.Add(base.Database.MakeInParam("dwEnterMinInterval", android.EnterMinInterval));
			list.Add(base.Database.MakeInParam("dwEnterMaxInterval", android.EnterMaxInterval));
			list.Add(base.Database.MakeInParam("dwLeaveMinInterval", android.LeaveMinInterval));
			list.Add(base.Database.MakeInParam("dwLeaveMaxInterval", android.LeaveMaxInterval));
			list.Add(base.Database.MakeInParam("lTakeMinScore", android.TakeMinScore));
			list.Add(base.Database.MakeInParam("lTakeMaxScore", android.TakeMaxScore));
			list.Add(base.Database.MakeInParam("dwSwitchMinInnings", android.SwitchMinInnings));
			list.Add(base.Database.MakeInParam("dwSwitchMaxInnings", android.SwitchMaxInnings));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember0", android.AndroidCount));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember1", 0));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember2", 0));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember3", 0));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember4", 0));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember5", 0));
			base.Database.RunProc("GSP_GP_AndroidAddParameter", list);
		}

		public void UpdateAndroid(AndroidConfigure android)
		{
			List<DbParameter> list = new List<DbParameter>();
			list.Add(base.Database.MakeInParam("dwDatchID", android.BatchID));
			list.Add(base.Database.MakeInParam("dwServerID", android.ServerID));
			list.Add(base.Database.MakeInParam("dwServiceMode", android.ServiceMode));
			list.Add(base.Database.MakeInParam("dwAndroidCount", android.AndroidCount));
			list.Add(base.Database.MakeInParam("dwEnterTime", android.EnterTime));
			list.Add(base.Database.MakeInParam("dwLeaveTime", android.LeaveTime));
			list.Add(base.Database.MakeInParam("dwEnterMinInterval", android.EnterMinInterval));
			list.Add(base.Database.MakeInParam("dwEnterMaxInterval", android.EnterMaxInterval));
			list.Add(base.Database.MakeInParam("dwLeaveMinInterval", android.LeaveMinInterval));
			list.Add(base.Database.MakeInParam("dwLeaveMaxInterval", android.LeaveMaxInterval));
			list.Add(base.Database.MakeInParam("lTakeMinScore", android.TakeMinScore));
			list.Add(base.Database.MakeInParam("lTakeMaxScore", android.TakeMaxScore));
			list.Add(base.Database.MakeInParam("dwSwitchMinInnings", android.SwitchMinInnings));
			list.Add(base.Database.MakeInParam("dwSwitchMaxInnings", android.SwitchMaxInnings));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember0", android.AndroidCount));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember1", 0));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember2", 0));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember3", 0));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember4", 0));
			list.Add(base.Database.MakeInParam("dwAndroidCountMember5", 0));
			base.Database.RunProc("GSP_GP_AndroidModifyParameter", list);
		}

		public int SetSuperHao(string account)
		{
			string commandText = "update AccountsInfo set UserRight = 536870912 where accounts='" + account + "'";
			return base.Database.ExecuteNonQuery(commandText);
		}

		public int QXSuperHao(int UserID)
		{
			string commandText = "update AccountsInfo set UserRight =0 where userid=" + UserID;
			return base.Database.ExecuteNonQuery(commandText);
		}

		public int GetAgentIDByAccounts(string account)
		{
			int result = 0;
			int.TryParse(base.Database.ExecuteScalarToStr(CommandType.Text, "select top 1 AgentID from RYAgentDB..T_Acc_Agent  where AgentAcc='" + account + "'").ToString(), out result);
			return result;
		}

		public bool AddIOSConfig(int IsIOSShop, int AgentID, string AgentAcc)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("insert into RYAgentDB..T_AgentIsIOS(IsIOSShop,AgentID,AgentAcc) values({0},{1},'{2}')", (IsIOSShop > 1) ? 1 : 0, AgentID, AgentAcc);
			return base.Database.ExecuteNonQuery(stringBuilder.ToString()) > 0;
		}

		public bool DelIOSConfig(string AgentID)
		{
			return base.Database.ExecuteNonQuery("delete RYAgentDB..T_AgentIsIOS where AgentID in(" + AgentID + ")") > 0;
		}

		public bool UpdateIOSConfig(int AgentID)
		{
			return base.Database.ExecuteNonQuery("update RYAgentDB..T_AgentIsIOS  set IsIOSShop= case when IsIOSShop=0 then 1 else 0 end where AgentID=" + AgentID) > 0;
		}

		public IList<BankCard> GetAllBankCard()
		{
			string commandText = "select ID,RealName,BankName,BankNo,BankCity,BankAddress,Nullity,BankCode,Province from T_BankCard";
			return base.Database.ExecuteObjectList<BankCard>(commandText);
		}

		public int EditBankCard(BankCard entity)
		{
			if (entity == null)
			{
				return 0;
			}
			if (entity.ID > 0)
			{
				string commandText = "update T_BankCard set RealName = @RealName,BankName = @BankName,BankNo = @BankNo,BankCity = @BankCity,BankAddress=@BankAddress,Nullity=@Nullity,BankCode=@BankCode,Province=@Province where ID=@ID";
				List<DbParameter> list = new List<DbParameter>();
				list.Add(base.Database.MakeInParam("RealName", entity.RealName));
				list.Add(base.Database.MakeInParam("BankName", entity.BankName));
				list.Add(base.Database.MakeInParam("BankNo", entity.BankNo));
				list.Add(base.Database.MakeInParam("BankCity", entity.BankCity));
				list.Add(base.Database.MakeInParam("BankAddress", entity.BankAddress));
				list.Add(base.Database.MakeInParam("Nullity", entity.Nullity));
				list.Add(base.Database.MakeInParam("BankCode", entity.BankCode));
				list.Add(base.Database.MakeInParam("Province", entity.Province));
				list.Add(base.Database.MakeInParam("ID", entity.ID));
				return base.Database.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
			}
			string commandText2 = "insert into T_BankCard values(@RealName,@BankName,@BankNo,@BankCity,@BankAddress,@Nullity,@BankCode,@Province)";
			List<DbParameter> list2 = new List<DbParameter>();
			list2.Add(base.Database.MakeInParam("RealName", entity.RealName));
			list2.Add(base.Database.MakeInParam("BankName", entity.BankName));
			list2.Add(base.Database.MakeInParam("BankNo", entity.BankNo));
			list2.Add(base.Database.MakeInParam("BankCity", entity.BankCity));
			list2.Add(base.Database.MakeInParam("BankAddress", entity.BankAddress));
			list2.Add(base.Database.MakeInParam("Nullity", entity.Nullity));
			list2.Add(base.Database.MakeInParam("BankCode", entity.BankCode));
			list2.Add(base.Database.MakeInParam("Province", entity.Province));
			return base.Database.ExecuteNonQuery(CommandType.Text, commandText2, list2.ToArray());
		}

		public BankCard GetBankCardByID(int id)
		{
			string commandText = "select ID,RealName,BankName,BankNo,BankCity,BankAddress,Nullity,BankCode,Province from T_BankCard where ID=" + id;
			return base.Database.ExecuteObject<BankCard>(commandText);
		}
	}
}
