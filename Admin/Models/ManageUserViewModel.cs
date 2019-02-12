using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
	public class ManageUserViewModel
	{
		[DataType(DataType.Password)]
		[Display(Name = "当前密码")]
		[Required]
		public string OldPassword
		{
			get;
			set;
		}

		[Required]
		[StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
		[Display(Name = "新密码")]
		[DataType(DataType.Password)]
		public string NewPassword
		{
			get;
			set;
		}

		[DataType(DataType.Password)]
		[Display(Name = "确认新密码")]
		[Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
		public string ConfirmPassword
		{
			get;
			set;
		}
	}
}
