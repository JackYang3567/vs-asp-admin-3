using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "用户名")]
		public string UserName
		{
			get;
			set;
		}

		[Required]
		[StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "密码")]
		public string Password
		{
			get;
			set;
		}

		[DataType(DataType.Password)]
		[Display(Name = "确认密码")]
		[Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
		public string ConfirmPassword
		{
			get;
			set;
		}
	}
}
