using System;

namespace Game.Entity.Platform
{
	[Serializable]
	public class OnLineStreamInfo
	{
		public const string Tablename = "OnLineStreamInfo";

		public const string _ID = "ID";

		public const string _MachineID = "MachineID";

		public const string _MachineServer = "MachineServer";

		public const string _InsertDateTime = "InsertDateTime";

		public const string _OnLineCountSum = "OnLineCountSum";

		public const string _OnLineCountKind = "OnLineCountKind";

		private int m_iD;

		private string m_machineID;

		private string m_machineServer;

		private DateTime m_insertDateTime;

		private int m_onLineCountSum;

		private string m_onLineCountKind;

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

		public string MachineID
		{
			get
			{
				return m_machineID;
			}
			set
			{
				m_machineID = value;
			}
		}

		public string MachineServer
		{
			get
			{
				return m_machineServer;
			}
			set
			{
				m_machineServer = value;
			}
		}

		public DateTime InsertDateTime
		{
			get
			{
				return m_insertDateTime;
			}
			set
			{
				m_insertDateTime = value;
			}
		}

		public int OnLineCountSum
		{
			get
			{
				return m_onLineCountSum;
			}
			set
			{
				m_onLineCountSum = value;
			}
		}

		public string OnLineCountKind
		{
			get
			{
				return m_onLineCountKind;
			}
			set
			{
				m_onLineCountKind = value;
			}
		}

		public OnLineStreamInfo()
		{
			m_iD = 0;
			m_machineID = "";
			m_machineServer = "";
			m_insertDateTime = DateTime.Now;
			m_onLineCountSum = 0;
			m_onLineCountKind = "";
		}
	}
}
