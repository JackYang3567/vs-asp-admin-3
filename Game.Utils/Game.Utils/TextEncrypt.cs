using System;
using System.Security.Cryptography;
using System.Text;

namespace Game.Utils
{
	public class TextEncrypt
	{
		private TextEncrypt()
		{
		}

		public static string Base64Decode(string message)
		{
			byte[] bytes = Convert.FromBase64String(message);
			return Encoding.UTF8.GetString(bytes);
		}

		public static string Base64Encode(string message)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
		}

		public static string DSAEncryptPassword(string password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			DSACryptoServiceProvider dSACryptoServiceProvider = new DSACryptoServiceProvider();
			string text = BitConverter.ToString(dSACryptoServiceProvider.SignData(Encoding.UTF8.GetBytes(password)));
			dSACryptoServiceProvider.Clear();
			return text.Replace("-", null);
		}

		public static string EncryptPassword(string password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			return MD5EncryptPassword(password);
		}

		public static string MD5EncryptPassword(string password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			return MD5EncryptPassword(password, MD5ResultMode.Strong);
		}

		public static string MD5EncryptPassword(string password, MD5ResultMode mode)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			string text = BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(password)));
			mD5CryptoServiceProvider.Clear();
			if (mode != 0)
			{
				return text.Replace("-", null).Substring(8, 16);
			}
			return text.Replace("-", null);
		}

		public static string SHA1EncryptPassword(string password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			string text = BitConverter.ToString(sHA1CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(password)));
			sHA1CryptoServiceProvider.Clear();
			return text.Replace("-", null);
		}

		public static string SHA256(string password)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			SHA256Managed sHA256Managed = new SHA256Managed();
			return Convert.ToBase64String(sHA256Managed.ComputeHash(bytes));
		}
	}
}
