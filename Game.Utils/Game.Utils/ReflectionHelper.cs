using System;
using System.Reflection;

namespace Game.Utils
{
	public class ReflectionHelper
	{
		public static Type GetType(string typeAndAssName)
		{
			string[] array = typeAndAssName.Split(',');
			if (array.Length < 2)
			{
				return Type.GetType(typeAndAssName);
			}
			return GetType(array[0].Trim(), array[1].Trim());
		}

		public static Type GetType(string typeFullName, string assemblyName)
		{
			if (assemblyName == null)
			{
				return Type.GetType(typeFullName);
			}
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Assembly[] array = assemblies;
			foreach (Assembly assembly in array)
			{
				if (assembly.FullName.Split(',')[0].Trim() == assemblyName.Trim())
				{
					return assembly.GetType(typeFullName);
				}
			}
			Assembly assembly2 = Assembly.Load(assemblyName);
			if (assembly2 != null)
			{
				return assembly2.GetType(typeFullName);
			}
			return null;
		}
	}
}
