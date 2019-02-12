using System.Security.Cryptography;
using System.Text;

namespace Game.Facade
{
	public class Jiami
	{
		public static string MD5(string str)
		{
			return MD5(str, "UTF-8");
		}

		public static string MD5(string str, string charset)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.GetEncoding(charset).GetBytes(str);
			byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i].ToString("x2");
			}
			return text;
		}

		public static string sign(string sourceData, string key)
		{
			MD5 mD = System.Security.Cryptography.MD5.Create();
			byte[] bytes = Encoding.UTF8.GetBytes(sourceData + key);
			byte[] data = mD.ComputeHash(bytes);
			return GetbyteToString(data);
		}

		private static string GetbyteToString(byte[] data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				stringBuilder.Append(data[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}
	}
}
