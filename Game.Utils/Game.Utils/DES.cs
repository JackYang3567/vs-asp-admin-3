using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Game.Utils
{
	public class DES
	{
		private static byte[] Keys = new byte[8]
		{
			18,
			52,
			86,
			120,
			144,
			171,
			205,
			239
		};

		public static string Decrypt(string decryptString, string decryptKey)
		{
			try
			{
				decryptKey = TextUtility.CutLeft(decryptKey, 8);
				decryptKey = decryptKey.PadRight(8, ' ');
				byte[] bytes = Encoding.UTF8.GetBytes(decryptKey);
				byte[] keys = Keys;
				byte[] array = Convert.FromBase64String(decryptString);
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, keys), CryptoStreamMode.Write);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				return Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			catch
			{
				return "";
			}
		}

		public static string Encrypt(string encryptString, string encryptKey)
		{
			encryptKey = TextUtility.CutLeft(encryptKey, 8);
			encryptKey = encryptKey.PadRight(8, ' ');
			byte[] bytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
			byte[] keys = Keys;
			byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
			DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, keys), CryptoStreamMode.Write);
			cryptoStream.Write(bytes2, 0, bytes2.Length);
			cryptoStream.FlushFinalBlock();
			return Convert.ToBase64String(memoryStream.ToArray());
		}
	}
}
