using System;
using System.Collections.Generic;

namespace Game.Facade.SiteLibrary
{
	public class FilesComparer : IComparer<HttpFolderInfo>
	{
		private string _sortColumn;

		public FilesComparer(string sortExpression)
		{
			this._sortColumn = sortExpression;
		}

		public int Compare(HttpFolderInfo a, HttpFolderInfo b)
		{
			int num = 0;
			string lower = this._sortColumn.ToLower();
			string str = lower;
			if (lower == null)
			{
				num = string.Compare(a.Name, b.Name, StringComparison.InvariantCultureIgnoreCase);
				return num;
			}
			else if (str == "name")
			{
				num = string.Compare(a.Name, b.Name, StringComparison.InvariantCultureIgnoreCase);
			}
			else if (str == "ext")
			{
				num = string.Compare(a.ExtName, b.ExtName, StringComparison.InvariantCultureIgnoreCase);
			}
			else if (str == "size")
			{
				num = string.Compare(a.FormatSize, b.FormatSize, StringComparison.InvariantCultureIgnoreCase);
			}
			else
			{
				if (str != "modifydate")
				{
					num = string.Compare(a.Name, b.Name, StringComparison.InvariantCultureIgnoreCase);
					return num;
				}
				num = DateTime.Compare(DateTime.Parse(a.FormatModifyDate), DateTime.Parse(b.FormatModifyDate));
			}
			return num;
		}
	}
}