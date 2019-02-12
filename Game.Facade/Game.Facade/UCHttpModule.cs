using Game.Utils;
using System;
using System.Text;
using System.Web;

namespace Game.Facade
{
	public class UCHttpModule : IHttpModule
	{
		private const int HTTP_ERROR_CODE = 404;

		public void Init(HttpApplication context)
		{
			context.Error += Application_OnError;
		}

		public void Dispose()
		{
		}

		private void Application_OnError(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			HttpContext context = httpApplication.Context;
			Exception ex = context.Server.GetLastError();
			if (ex is HttpException)
			{
				HttpException ex2 = ex as HttpException;
				if (ex2.GetHttpCode() == 404)
				{
					string physicalPath = httpApplication.Request.PhysicalPath;
					TextLogger.Write(string.Format("文件不存在:{0}", httpApplication.Request.Url.AbsoluteUri));
					return;
				}
			}
			if (ex.InnerException != null)
			{
				ex = ex.InnerException;
			}
			StringBuilder stringBuilder = new StringBuilder().AppendFormat("访问路径:{0}", GameRequest.GetRawUrl()).AppendLine().AppendFormat("{0} thrown {1}", ex.Source, ex.GetType().ToString())
				.AppendLine()
				.Append(ex.Message)
				.Append(ex.StackTrace);
			TextLogger.Write(stringBuilder.ToString());
		}
	}
}
