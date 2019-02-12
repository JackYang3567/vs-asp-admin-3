using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class AccountsProtect
	{
		public const string Tablename = "AccountsProtect";

		public const string _ProtectID = "ProtectID";

		public const string _UserID = "UserID";

		public const string _Question1 = "Question1";

		public const string _Response1 = "Response1";

		public const string _Question2 = "Question2";

		public const string _Response2 = "Response2";

		public const string _Question3 = "Question3";

		public const string _Response3 = "Response3";

		public const string _PassportID = "PassportID";

		public const string _PassportType = "PassportType";

		public const string _SafeEmail = "SafeEmail";

		public const string _CreateIP = "CreateIP";

		public const string _ModifyIP = "ModifyIP";

		public const string _CreateDate = "CreateDate";

		public const string _ModifyDate = "ModifyDate";

		private int m_protectID;

		private int m_userID;

		private string m_question1;

		private string m_response1;

		private string m_question2;

		private string m_response2;

		private string m_question3;

		private string m_response3;

		private string m_passportID;

		private byte m_passportType;

		private string m_safeEmail;

		private string m_createIP;

		private string m_modifyIP;

		private DateTime m_createDate;

		private DateTime m_modifyDate;

		public int ProtectID
		{
			get
			{
				return m_protectID;
			}
			set
			{
				m_protectID = value;
			}
		}

		public int UserID
		{
			get
			{
				return m_userID;
			}
			set
			{
				m_userID = value;
			}
		}

		public string Question1
		{
			get
			{
				return m_question1;
			}
			set
			{
				m_question1 = value;
			}
		}

		public string Response1
		{
			get
			{
				return m_response1;
			}
			set
			{
				m_response1 = value;
			}
		}

		public string Question2
		{
			get
			{
				return m_question2;
			}
			set
			{
				m_question2 = value;
			}
		}

		public string Response2
		{
			get
			{
				return m_response2;
			}
			set
			{
				m_response2 = value;
			}
		}

		public string Question3
		{
			get
			{
				return m_question3;
			}
			set
			{
				m_question3 = value;
			}
		}

		public string Response3
		{
			get
			{
				return m_response3;
			}
			set
			{
				m_response3 = value;
			}
		}

		public string PassportID
		{
			get
			{
				return m_passportID;
			}
			set
			{
				m_passportID = value;
			}
		}

		public byte PassportType
		{
			get
			{
				return m_passportType;
			}
			set
			{
				m_passportType = value;
			}
		}

		public string SafeEmail
		{
			get
			{
				return m_safeEmail;
			}
			set
			{
				m_safeEmail = value;
			}
		}

		public string CreateIP
		{
			get
			{
				return m_createIP;
			}
			set
			{
				m_createIP = value;
			}
		}

		public string ModifyIP
		{
			get
			{
				return m_modifyIP;
			}
			set
			{
				m_modifyIP = value;
			}
		}

		public DateTime CreateDate
		{
			get
			{
				return m_createDate;
			}
			set
			{
				m_createDate = value;
			}
		}

		public DateTime ModifyDate
		{
			get
			{
				return m_modifyDate;
			}
			set
			{
				m_modifyDate = value;
			}
		}

		public AccountsProtect()
		{
			m_protectID = 0;
			m_userID = 0;
			m_question1 = "";
			m_response1 = "";
			m_question2 = "";
			m_response2 = "";
			m_question3 = "";
			m_response3 = "";
			m_passportID = "";
			m_passportType = 0;
			m_safeEmail = "";
			m_createIP = "";
			m_modifyIP = "";
			m_createDate = DateTime.Now;
			m_modifyDate = DateTime.Now;
		}
	}
}
