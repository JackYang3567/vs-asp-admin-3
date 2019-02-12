using Microsoft.AspNet.Identity.EntityFramework;

namespace Admin.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection")
		{
		}
	}
}
