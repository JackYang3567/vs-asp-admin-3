namespace Game.Facade
{
	public class AjaxJsonValid : AjaxJson
	{
		public AjaxJsonValid()
		{
			AddDataItem("valid", false);
		}

		public AjaxJsonValid(bool result)
		{
			AddDataItem("valid", result);
		}

		public void SetValidDataValue(bool result)
		{
			SetDataItem("valid", result);
		}
	}
}
