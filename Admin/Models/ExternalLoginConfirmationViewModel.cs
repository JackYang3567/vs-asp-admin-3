using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
	public class ExternalLoginConfirmationViewModel
	{
		[Display(Name = "用户名")]
		[Required]
		public string UserName
		{
			get;
			set;
		}
	}
}
