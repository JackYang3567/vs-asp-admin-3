namespace Game.Facade
{
	public class FacadeManage
	{
		private static object lockObj = new object();

		private static volatile NativeWebFacade _aideNativeWebFacade;

		private static volatile PlatformManagerFacade _aidePlatformManagerFacade;

		private static volatile PlatformFacade _aidePlatformFacade;

		private static volatile TreasureFacade _aideTreasureFacade;

		private static volatile AccountsFacade _aideAccountsFacade;

		private static volatile RecordFacade _aideRecordFacade;

		private static volatile GameMatchFacade _aideGameMatchFacade;

		public static NativeWebFacade aideNativeWebFacade
		{
			get
			{
				if (_aideNativeWebFacade == null)
				{
					lock (lockObj)
					{
						if (_aideNativeWebFacade == null)
						{
							_aideNativeWebFacade = new NativeWebFacade();
						}
					}
				}
				return _aideNativeWebFacade;
			}
		}

		public static PlatformManagerFacade aidePlatformManagerFacade
		{
			get
			{
				if (_aidePlatformManagerFacade == null)
				{
					lock (lockObj)
					{
						if (_aidePlatformManagerFacade == null)
						{
							_aidePlatformManagerFacade = new PlatformManagerFacade();
						}
					}
				}
				return _aidePlatformManagerFacade;
			}
		}

		public static PlatformFacade aidePlatformFacade
		{
			get
			{
				if (_aidePlatformFacade == null)
				{
					lock (lockObj)
					{
						if (_aidePlatformFacade == null)
						{
							_aidePlatformFacade = new PlatformFacade();
						}
					}
				}
				return _aidePlatformFacade;
			}
		}

		public static TreasureFacade aideTreasureFacade
		{
			get
			{
				if (_aideTreasureFacade == null)
				{
					lock (lockObj)
					{
						if (_aideTreasureFacade == null)
						{
							_aideTreasureFacade = new TreasureFacade();
						}
					}
				}
				return _aideTreasureFacade;
			}
		}

		public static AccountsFacade aideAccountsFacade
		{
			get
			{
				if (_aideAccountsFacade == null)
				{
					lock (lockObj)
					{
						if (_aideAccountsFacade == null)
						{
							_aideAccountsFacade = new AccountsFacade();
						}
					}
				}
				return _aideAccountsFacade;
			}
		}

		public static RecordFacade aideRecordFacade
		{
			get
			{
				if (_aideRecordFacade == null)
				{
					lock (lockObj)
					{
						if (_aideRecordFacade == null)
						{
							_aideRecordFacade = new RecordFacade();
						}
					}
				}
				return _aideRecordFacade;
			}
		}

		public static GameMatchFacade aideGameMatchFacade
		{
			get
			{
				if (_aideGameMatchFacade == null)
				{
					lock (lockObj)
					{
						if (_aideGameMatchFacade == null)
						{
							_aideGameMatchFacade = new GameMatchFacade();
						}
					}
				}
				return _aideGameMatchFacade;
			}
		}
	}
}
