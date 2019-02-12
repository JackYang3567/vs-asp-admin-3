using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.Utils
{
	public static class CollectionHelper
	{
		public static void ActionOnEach<TObject>(IEnumerable<TObject> collection, Action<TObject> action)
		{
			ActionOnSpecification(collection, action, null);
		}

		public static void ActionOnSpecification<TObject>(IEnumerable<TObject> collection, Action<TObject> action, Predicate<TObject> predicate)
		{
			if (collection != null)
			{
				if (predicate == null)
				{
					foreach (TObject item in collection)
					{
						action(item);
					}
				}
				else
				{
					foreach (TObject item2 in collection)
					{
						if (predicate(item2))
						{
							action(item2);
						}
					}
				}
			}
		}

		public static bool BinarySearch<T>(IList<T> sortedList, T target, out int minIndex) where T : IComparable
		{
			if (target.CompareTo(sortedList[0]) == 0)
			{
				minIndex = 0;
				return true;
			}
			if (target.CompareTo(sortedList[0]) < 0)
			{
				minIndex = -1;
				return false;
			}
			if (target.CompareTo(sortedList[((ICollection<T>)sortedList).Count - 1]) == 0)
			{
				minIndex = ((ICollection<T>)sortedList).Count - 1;
				return true;
			}
			if (target.CompareTo(sortedList[((ICollection<T>)sortedList).Count - 1]) > 0)
			{
				minIndex = ((ICollection<T>)sortedList).Count - 1;
				return false;
			}
			int num = 0;
			int num2 = ((ICollection<T>)sortedList).Count - 1;
			while (num2 - num > 1)
			{
				int num3 = (num + num2) / 2;
				if (target.CompareTo(sortedList[num3]) == 0)
				{
					minIndex = num3;
					return true;
				}
				if (target.CompareTo(sortedList[num3]) < 0)
				{
					num2 = num3;
				}
				else
				{
					num = num3;
				}
			}
			minIndex = num;
			return false;
		}

		public static bool Contains<TObject>(IEnumerable<TObject> source, Predicate<TObject> predicate)
		{
			TObject specification;
			return Contains(source, predicate, out specification);
		}

		public static bool Contains<TObject>(IEnumerable<TObject> source, Predicate<TObject> predicate, out TObject specification)
		{
			specification = default(TObject);
			foreach (TObject item in source)
			{
				if (predicate(item))
				{
					specification = item;
					return true;
				}
			}
			return false;
		}

		public static IList<TObject> Find<TObject>(IEnumerable<TObject> source, Predicate<TObject> predicate)
		{
			IList<TObject> list = (IList<TObject>)new List<TObject>();
			ActionOnSpecification(source, delegate(TObject ele)
			{
				((ICollection<TObject>)list).Add(ele);
			}, predicate);
			return (IList<TObject>)list;
		}

		public static TObject FindFirst<TObject>(IEnumerable<TObject> source, Predicate<TObject> predicate)
		{
			foreach (TObject item in source)
			{
				if (predicate(item))
				{
					return item;
				}
			}
			return default(TObject);
		}

		public static T[] GetPart<T>(T[] ary, int startIndex, int count)
		{
			return GetPart(ary, startIndex, count, false);
		}

		public static T[] GetPart<T>(T[] ary, int startIndex, int count, bool reverse)
		{
			if (startIndex >= ary.Length)
			{
				return null;
			}
			if (ary.Length < startIndex + count)
			{
				count = ary.Length - startIndex;
			}
			T[] array = new T[count];
			if (!reverse)
			{
				for (int i = 0; i < count; i++)
				{
					array[i] = ary[startIndex + i];
				}
				return array;
			}
			for (int i = 0; i < count; i++)
			{
				array[i] = ary[ary.Length - startIndex - 1 - i];
			}
			return array;
		}

		public static bool IsNullOrEmpty<T>(ICollection<T> collection)
		{
			if (collection != null)
			{
				return collection.Count == 0;
			}
			return true;
		}

		public static bool IsNullOrEmpty(ICollection collection)
		{
			if (collection != null)
			{
				return collection.Count == 0;
			}
			return true;
		}
	}
}
