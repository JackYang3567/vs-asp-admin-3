using System;
using System.Web.UI;

namespace Admin
{
	public class handler : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Write("SUCCESS");
		}
	}
}
