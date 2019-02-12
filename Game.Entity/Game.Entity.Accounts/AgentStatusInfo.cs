namespace Game.Entity.Accounts
{
	public class AgentStatusInfo
	{
		public string regTgAddr1
		{
			get;
			set;
		}

		public string regTgAddr2
		{
			get;
			set;
		}

		public string regTgAddr3
		{
			get;
			set;
		}

		public string mainTgAddr1
		{
			get;
			set;
		}

		public string mainTgAddr2
		{
			get;
			set;
		}

		public string mainTgAddr3
		{
			get;
			set;
		}

		public AgentStatusInfo()
		{
		}

		public AgentStatusInfo(string regTgAddr1, string regTgAddr2, string regTgAddr3, string mainTgAddr1, string mainTgAddr2, string mainTgAddr3)
		{
			this.regTgAddr1 = regTgAddr1;
			this.regTgAddr2 = regTgAddr2;
			this.regTgAddr3 = regTgAddr3;
			this.mainTgAddr1 = mainTgAddr1;
			this.mainTgAddr2 = mainTgAddr2;
			this.mainTgAddr3 = mainTgAddr3;
		}
	}
}
