using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Game.Utils
{
	public class SerializationHelper
	{
		private SerializationHelper()
		{
		}

		public static object Deserialize(byte[] buffer)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream(buffer, 0, buffer.Length, false);
			object result = binaryFormatter.Deserialize(memoryStream);
			memoryStream.Close();
			return result;
		}

		public static object Deserialize(Type type, string filename)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				return new XmlSerializer(type).Deserialize(fileStream);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		public static byte[] Serialize<T>(T t)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream(10240);
			binaryFormatter.Serialize(memoryStream, t);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			byte[] array = new byte[(int)memoryStream.Length];
			memoryStream.Read(array, 0, array.Length);
			memoryStream.Close();
			return array;
		}

		public static void Serialize<T>(T t, string filename)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
				new XmlSerializer(t.GetType()).Serialize(fileStream, t);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}
	}
}
