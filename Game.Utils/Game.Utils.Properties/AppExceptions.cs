using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Game.Utils.Properties
{
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	internal class AppExceptions
	{
		private static CultureInfo resourceCulture;

		private static ResourceManager resourceMan;

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

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(resourceMan, null))
				{
					ResourceManager resourceManager = resourceMan = new ResourceManager("Game.Utils.Properties.AppExceptions", typeof(AppExceptions).Assembly);
				}
				return resourceMan;
			}
		}

		internal static string Terminator_ExceptionTemplate
		{
			get
			{
				return ResourceManager.GetString("Terminator_ExceptionTemplate", resourceCulture);
			}
		}

		internal AppExceptions()
		{
		}
	}
}
