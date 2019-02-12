using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Game.Kernel
{
	public sealed class ProxyFactory
	{
		public delegate object CreateInstanceHandler(object[] parameters);

		private static Dictionary<string, CreateInstanceHandler> m_Handlers = new Dictionary<string, CreateInstanceHandler>();

		private ProxyFactory()
		{
		}

		private static void CreateHandler(Type objtype, string key, Type[] ptypes)
		{
			lock (typeof(ProxyFactory))
			{
				if (!m_Handlers.ContainsKey(key))
				{
					DynamicMethod dynamicMethod = new DynamicMethod(key, typeof(object), new Type[1]
					{
						typeof(object[])
					}, typeof(ProxyFactory).Module);
					ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
					ConstructorInfo constructor = objtype.GetConstructor(ptypes);
					iLGenerator.Emit(OpCodes.Nop);
					for (int i = 0; i < ptypes.Length; i++)
					{
						iLGenerator.Emit(OpCodes.Ldarg_0);
						iLGenerator.Emit(OpCodes.Ldc_I4, i);
						iLGenerator.Emit(OpCodes.Ldelem_Ref);
						if (ptypes[i].IsValueType)
						{
							iLGenerator.Emit(OpCodes.Unbox_Any, ptypes[i]);
						}
						else
						{
							iLGenerator.Emit(OpCodes.Castclass, ptypes[i]);
						}
					}
					iLGenerator.Emit(OpCodes.Newobj, constructor);
					iLGenerator.Emit(OpCodes.Ret);
					CreateInstanceHandler value = (CreateInstanceHandler)dynamicMethod.CreateDelegate(typeof(CreateInstanceHandler));
					m_Handlers.Add(key, value);
				}
			}
		}

		public static T CreateInstance<T>()
		{
			return CreateInstance<T>(null);
		}

		public static T CreateInstance<T>(params object[] parameters)
		{
			Type typeFromHandle = typeof(T);
			Type[] parameterTypes = GetParameterTypes(parameters);
			string key = typeof(T).FullName + "_" + GetKey(parameterTypes);
			if (!m_Handlers.ContainsKey(key))
			{
				CreateHandler(typeFromHandle, key, parameterTypes);
			}
			return (T)m_Handlers[key](parameters);
		}

		private static string GetKey(params Type[] types)
		{
			if (types == null || types.Length == 0)
			{
				return "null";
			}
			return string.Concat((object[])types);
		}

		private static Type[] GetParameterTypes(params object[] parameters)
		{
			if (parameters == null)
			{
				return new Type[0];
			}
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].GetType();
			}
			return array;
		}
	}
}
