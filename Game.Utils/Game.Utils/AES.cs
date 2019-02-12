using System;
using System.Security.Cryptography;
using System.Text;

namespace Game.Utils
{
	public sealed class AES
	{
		private static byte[] Keys = new byte[16]
		{
			65,
			114,
			101,
			121,
			111,
			117,
			109,
			121,
			83,
			110,
			111,
			119,
			109,
			97,
			110,
			63
		};

		private AES()
		{
		}

		public static string Decrypt(string cipherText, string cipherkey)
		{
			try
			{
				cipherkey = TextUtility.CutLeft(cipherkey, 32);
				cipherkey = cipherkey.PadRight(32, ' ');
				RijndaelManaged rijndaelManaged = new RijndaelManaged();
				rijndaelManaged.Key = Encoding.UTF8.GetBytes(cipherkey);
				rijndaelManaged.IV = Keys;
				ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
				byte[] array = Convert.FromBase64String(cipherText);
				byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
				return Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				return "";
			}
		}

		public static byte[] DecryptBuffer(byte[] cipherText, string cipherkey)
		{
			try
			{
				cipherkey = TextUtility.CutLeft(cipherkey, 32);
				cipherkey = cipherkey.PadRight(32, ' ');
				RijndaelManaged rijndaelManaged = new RijndaelManaged();
				rijndaelManaged.Key = Encoding.UTF8.GetBytes(cipherkey);
				rijndaelManaged.IV = Keys;
				RijndaelManaged rijndaelManaged2 = rijndaelManaged;
				return rijndaelManaged2.CreateDecryptor().TransformFinalBlock(cipherText, 0, cipherText.Length);
			}
			catch
			{
				return null;
			}
		}

		public static string Encrypt(string plainText, string cipherkey)
		{
			cipherkey = TextUtility.CutLeft(cipherkey, 32);
			cipherkey = cipherkey.PadRight(32, ' ');
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.Key = Encoding.UTF8.GetBytes(cipherkey.Substring(0, 32));
			rijndaelManaged.IV = Keys;
			ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor();
			byte[] bytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length));
		}

		public static byte[] EncryptBuffer(byte[] plainText, string cipherkey)
		{
			cipherkey = TextUtility.CutLeft(cipherkey, 32);
			cipherkey = cipherkey.PadRight(32, ' ');
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			rijndaelManaged.Key = Encoding.UTF8.GetBytes(cipherkey.Substring(0, 32));
			rijndaelManaged.IV = Keys;
			RijndaelManaged rijndaelManaged2 = rijndaelManaged;
			return rijndaelManaged2.CreateEncryptor().TransformFinalBlock(plainText, 0, plainText.Length);
		}
	}
}
