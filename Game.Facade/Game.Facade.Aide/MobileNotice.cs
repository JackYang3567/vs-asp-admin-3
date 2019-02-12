using System;

namespace Game.Facade.Aide
{
	public class MobileNotice
	{
		private string _title;

		private DateTime _date;

		private string _content;

		public string title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
			}
		}

		public DateTime date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
			}
		}

		public string content
		{
			get
			{
				return _content;
			}
			set
			{
				_content = value;
			}
		}

		public MobileNotice(string startTitle, DateTime startDate, string startContent)
		{
			_title = startTitle;
			_date = startDate;
			_content = startContent;
		}
	}
}
