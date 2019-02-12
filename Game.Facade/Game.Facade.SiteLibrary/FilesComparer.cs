using System;
using System.Collections.Generic;

namespace Game.Facade.SiteLibrary
{
	public class FilesComparer : IComparer<HttpFolderInfo>
	{
		private string _sortColumn;

		public FilesComparer(string sortExpression)
		{
			_sortColumn = sortExpression;
		}

		public int Compare(HttpFolderInfo a, HttpFolderInfo b)
		{
			int num = 0;
			switch (_sortColumn.ToLower())
			{
			case "name":
				return string.Compare(a.Name, b.Name, StringComparison.InvariantCultureIgnoreCase);
			case "ext":
				return string.Compare(a.ExtName, b.ExtName, StringComparison.InvariantCultureIgnoreCase);
			case "size":
				return string.Compare(a.FormatSize, b.FormatSize, StringComparison.InvariantCultureIgnoreCase);
			case "modifydate":
				return DateTime.Compare(DateTime.Parse(a.FormatModifyDate), DateTime.Parse(b.FormatModifyDate));
			default:
				return string.Compare(a.Name, b.Name, StringComparison.InvariantCultureIgnoreCase);
			}
		}
	}
}
