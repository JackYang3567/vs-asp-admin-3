using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Game.Facade
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class ResMessage
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(resourceMan, null))
				{
					ResourceManager resourceManager = resourceMan = new ResourceManager("Game.Facade.ResMessage", typeof(ResMessage).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static string EmptyAccounts
		{
			get
			{
				return ResourceManager.GetString("EmptyAccounts", resourceCulture);
			}
		}

		internal static string EmptyPassword
		{
			get
			{
				return ResourceManager.GetString("EmptyPassword", resourceCulture);
			}
		}

		internal static string Error_DeleteSuperAdministrator
		{
			get
			{
				return ResourceManager.GetString("Error_DeleteSuperAdministrator", resourceCulture);
			}
		}

		internal static string Error_ExistsLinkEmail
		{
			get
			{
				return ResourceManager.GetString("Error_ExistsLinkEmail", resourceCulture);
			}
		}

		internal static string Error_ExistsUser
		{
			get
			{
				return ResourceManager.GetString("Error_ExistsUser", resourceCulture);
			}
		}

		internal static string Hit_SuperAdministrator
		{
			get
			{
				return ResourceManager.GetString("Hit_SuperAdministrator", resourceCulture);
			}
		}

		internal ResMessage()
		{
		}
	}
}
