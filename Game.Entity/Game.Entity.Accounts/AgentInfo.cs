using System;
using System.Collections.Generic;

namespace Game.Entity.Accounts
{
	[Serializable]
	public class AgentInfo
	{
		public int AgentID
		{
			get;
			set;
		}

		public string AgentAcc
		{
			get;
			set;
		}

		public string Pwd
		{
			get;
			set;
		}

		public double AgentRate
		{
			get;
			set;
		}

		public string RealName
		{
			get;
			set;
		}

		public string QQ
		{
			get;
			set;
		}

		public string Memo
		{
			get;
			set;
		}

		public string AgentDomain
		{
			get;
			set;
		}

		public string RegIP
		{
			get;
			set;
		}

		public double Score
		{
			get;
			set;
		}

		public string BankAcc
		{
			get;
			set;
		}

		public string BankName
		{
			get;
			set;
		}

		public string BankAddress
		{
			get;
			set;
		}

		public byte AgentStatus
		{
			get;
			set;
		}

		public string ParentAcc
		{
			get;
			set;
		}

		public int Operator
		{
			get;
			set;
		}

		public string LastIP
		{
			get;
			set;
		}

		public DateTime LastDate
		{
			get;
			set;
		}

		public int AgentLevel
		{
			get;
			set;
		}

		public DateTime RegDate
		{
			get;
			set;
		}

		public int ParentID
		{
			get;
			set;
		}

		public int safeOpen
		{
			get;
			set;
		}

		public string ShowName
		{
			get;
			set;
		}

		public string WeChat
		{
			get;
			set;
		}

		public int IsClient
		{
			get;
			set;
		}

		public int ShowSort
		{
			get;
			set;
		}

		public int QueryRight
		{
			get;
			set;
		}

		public decimal Payrate
		{
			get;
			set;
		}

		public List<AgentMenu> menus
		{
			get;
			set;
		}

		public AgentStatusInfo AgentURL
		{
			get;
			set;
		}

		public decimal CZScore
		{
			get;
			set;
		}

		public AgentInfo()
		{
		}

		public AgentInfo(int aid, string bankName, string bankAcc, string bankAddress)
		{
			AgentID = aid;
			BankName = bankName;
			BankAcc = bankAcc;
			BankAddress = bankAddress;
		}

		public AgentInfo(string agentAccount, string pword, double agentNum, string trueName, string qq, string memo, string domain)
		{
			AgentAcc = agentAccount;
			Pwd = pword;
			QQ = qq;
			Memo = memo;
			RealName = trueName;
			AgentRate = agentNum;
			AgentDomain = domain;
		}

		public AgentInfo(string agentAccount, string pword, double agentNum, string trueName, string qq, string memo, string domain, string clientIP)
			: this(agentAccount, pword, agentNum, trueName, qq, memo, domain)
		{
			RegIP = clientIP;
		}

		public AgentInfo(string agentAccount, string pword, double agentNum, string trueName, string qq, string memo, string domain, string clientIP, int aid)
			: this(agentAccount, pword, agentNum, trueName, qq, memo, domain, clientIP)
		{
			AgentID = aid;
		}
	}
}
