namespace Admin.Models
{
	public class UploadFileInfo
	{
		public string FileName
		{
			get;
			set;
		}

		public int Op
		{
			get;
			set;
		}

		public byte[] FileData
		{
			get;
			set;
		}
	}
}
