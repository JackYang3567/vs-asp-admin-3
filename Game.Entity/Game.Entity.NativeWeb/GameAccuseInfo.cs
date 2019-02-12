using System;

namespace Game.Entity.NativeWeb
{
	public class GameAccuseInfo
	{
		public int AccuseID
		{
			get;
			set;
		}

		public int TypeID
		{
			get;
			set;
		}

		public string TypeName
		{
			get;
			set;
		}

		public int UserID
		{
			get;
			set;
		}

		public int GameID
		{
			get;
			set;
		}

		public string Accounts
		{
			get;
			set;
		}

		public string SrcIP
		{
			get;
			set;
		}

		public DateTime AccuseTime
		{
			get;
			set;
		}

		public int TarUserID
		{
			get;
			set;
		}

		public int TarGameID
		{
			get;
			set;
		}

		public string TarAcc
		{
			get;
			set;
		}

		public string Content
		{
			get;
			set;
		}

		public string Images
		{
			get;
			set;
		}

		public DateTime? DealTime
		{
			get;
			set;
		}

		public string Dealer
		{
			get;
			set;
		}

		public string DealerMark
		{
			get;
			set;
		}
	}
}
