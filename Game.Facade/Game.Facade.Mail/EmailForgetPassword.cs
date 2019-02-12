using System.Collections.Generic;

namespace Game.Facade.Mail
{
	public class EmailForgetPassword : EmailBase
	{
		public EmailForgetPassword(MailConfigInfo mailConfig, MailTMLConfigInfo tmlConfig, Dictionary<string, string> renderVals)
			: base(tmlConfig, mailConfig)
		{
			base.Render.RegisterVariable("reportNO", renderVals["reportNO"]);
			base.Render.RegisterVariable("userName", renderVals["userName"]);
			base.Render.RegisterVariable("url", renderVals["url"]);
			base.Render.RegisterVariable("mail", renderVals["mail"]);
			base.Render.RegisterVariable("sitename", renderVals["sitename"]);
			base.Render.RegisterVariable("reason", renderVals["reason"]);
		}
	}
}
