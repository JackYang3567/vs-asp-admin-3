using System;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class AccountsFace
	{
		public const string Tablename = "AccountsFace";

		public const string _ID = "ID";

		public const string _UserID = "UserID";

		public const string _CustomFace = "CustomFace";

		public const string _InsertTime = "InsertTime";

		public const string _InsertAddr = "InsertAddr";

		public const string _InsertMachine = "InsertMachine";

		private int m_iD;

		private int m_userID;

		private byte[] m_customFace;

		private DateTime m_insertTime;

		private string m_insertAddr;

		private string m_insertMachine;

		public int ID
		{
			get
			{
				return m_iD;
			}
			set
			{
				m_iD = value;
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

		public byte[] CustomFace
		{
			get
			{
				return m_customFace;
			}
			set
			{
				m_customFace = value;
			}
		}

		public DateTime InsertTime
		{
			get
			{
				return m_insertTime;
			}
			set
			{
				m_insertTime = value;
			}
		}

		public string InsertAddr
		{
			get
			{
				return m_insertAddr;
			}
			set
			{
				m_insertAddr = value;
			}
		}

		public string InsertMachine
		{
			get
			{
				return m_insertMachine;
			}
			set
			{
				m_insertMachine = value;
			}
		}

		public AccountsFace()
		{
			m_iD = 0;
			m_userID = 0;
			m_customFace = null;
			m_insertTime = DateTime.Now;
			m_insertAddr = "";
			m_insertMachine = "";
		}
	}
}
