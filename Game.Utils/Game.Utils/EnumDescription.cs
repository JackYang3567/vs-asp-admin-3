using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game.Utils
{
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
	public class EnumDescription : Attribute
	{
		public enum SortType
		{
			Default,
			DisplayText,
			Rank
		}

		private static IDictionary<string, IList<EnumDescription>> EnumDescriptionCache = new Dictionary<string, IList<EnumDescription>>();

		private FieldInfo m_fieldIno;

		private string m_description;

		private int m_enumRank;

		public string Description
		{
			get
			{
				return m_description;
			}
		}

		public int EnumRank
		{
			get
			{
				return m_enumRank;
			}
		}

		public int EnumValue
		{
			get
			{
				return (int)m_fieldIno.GetValue(null);
			}
		}

		public string FieldName
		{
			get
			{
				return m_fieldIno.Name;
			}
		}

		public EnumDescription(string description)
			: this(description, 5)
		{
		}

		public EnumDescription(string description, int enumRank)
		{
			m_description = description;
			m_enumRank = enumRank;
		}

		public static bool ExistsEnumValue(Type enumType, int enumValue)
		{
			List<EnumDescription> list = GetFieldTexts(enumType) as List<EnumDescription>;
			if (CollectionHelper.IsNullOrEmpty((ICollection<EnumDescription>)list))
			{
				return false;
			}
			return list.Exists((EnumDescription item) => item.EnumValue == enumValue);
		}

		public static string GetEnumText(Type enumType)
		{
			EnumDescription[] array = (EnumDescription[])enumType.GetCustomAttributes(typeof(EnumDescription), false);
			if (array.Length < 1)
			{
				return string.Empty;
			}
			return array[0].Description;
		}

		public static string GetFieldText(object enumValue)
		{
			List<EnumDescription> list = GetFieldTexts(enumValue.GetType()) as List<EnumDescription>;
			if (CollectionHelper.IsNullOrEmpty((ICollection<EnumDescription>)list))
			{
				return string.Empty;
			}
			EnumDescription enumDescription = list.Find((EnumDescription item) => item.m_fieldIno.Name.Equals(enumValue.ToString()));
			if (enumDescription == null)
			{
				return string.Empty;
			}
			return enumDescription.Description;
		}

		public static string GetFieldText(Type enumType, object enumValue)
		{
			List<EnumDescription> list = GetFieldTexts(enumType) as List<EnumDescription>;
			if (CollectionHelper.IsNullOrEmpty((ICollection<EnumDescription>)list))
			{
				return string.Empty;
			}
			EnumDescription enumDescription = list.Find((EnumDescription item) => item.EnumValue == Convert.ToInt32(enumValue));
			if (enumDescription == null)
			{
				return string.Empty;
			}
			return enumDescription.Description;
		}

		public static IList<EnumDescription> GetFieldTexts(Type enumType)
		{
			return GetFieldTexts(enumType, SortType.Default);
		}

		public static IList<EnumDescription> GetFieldTexts(Type enumType, SortType sortType)
		{
			if (!EnumDescriptionCache.ContainsKey(enumType.FullName))
			{
				FieldInfo[] fields = enumType.GetFields();
				IList<EnumDescription> list = new List<EnumDescription>();
				FieldInfo[] array = fields;
				foreach (FieldInfo fieldInfo in array)
				{
					object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(EnumDescription), false);
					if (customAttributes.Length == 1)
					{
						EnumDescription enumDescription = (EnumDescription)customAttributes[0];
						enumDescription.m_fieldIno = fieldInfo;
						list.Add(enumDescription);
					}
				}
				EnumDescriptionCache.Add(enumType.FullName, list);
			}
			IList<EnumDescription> list2 = EnumDescriptionCache[enumType.FullName];
			if (list2.Count <= 0)
			{
				throw new NotSupportedException("枚举类型[" + enumType.Name + "]未定义属性EnumValueDescription");
			}
			if (sortType != 0)
			{
				for (int j = 0; j < list2.Count; j++)
				{
					for (int k = j; k < list2.Count; k++)
					{
						bool flag = false;
						switch (sortType)
						{
						case SortType.DisplayText:
							if (string.Compare(list2[j].Description, list2[k].Description) > 0)
							{
								flag = true;
							}
							break;
						case SortType.Rank:
							if (list2[j].EnumRank > list2[k].EnumRank)
							{
								flag = true;
							}
							break;
						}
						if (flag)
						{
							EnumDescription value = list2[j];
							list2[j] = list2[k];
							list2[k] = value;
						}
					}
				}
			}
			return list2;
		}
	}
}
