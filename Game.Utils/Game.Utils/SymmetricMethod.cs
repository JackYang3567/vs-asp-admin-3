using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Game.Utils
{
	public class SymmetricMethod
	{
		private SymmetricAlgorithm mobjCryptoService;

		public SymmetricMethod()
		{
			mobjCryptoService = new RijndaelManaged();
		}

		private byte[] GetLegalKey()
		{
			string text = "A7Df09!325Bg6A5aB@40ahkFCklAuB4D#40Dqy0D7oD8$AvB8Dd6b%aDa8Ae8709*44D41d";
			mobjCryptoService.GenerateKey();
			byte[] key = mobjCryptoService.Key;
			int num = key.Length;
			if (text.Length > num)
			{
				text = text.Substring(0, num);
			}
			else if (text.Length < num)
			{
				text = text.PadRight(num, ' ');
			}
			return Encoding.ASCII.GetBytes(text);
		}

		private byte[] GetLegalIV()
		{
			string text = "GF46dD87%AgD2(3FjC467Bk%&B241A95Fk&7tD3452f*96b4465(e797fAa44A6be8Aa259";
			mobjCryptoService.GenerateIV();
			byte[] iV = mobjCryptoService.IV;
			int num = iV.Length;
			if (text.Length > num)
			{
				text = text.Substring(0, num);
			}
			else if (text.Length < num)
			{
				text = text.PadRight(num, ' ');
			}
			return Encoding.ASCII.GetBytes(text);
		}

		public string Encrypto(string Source)
		{
			if (string.IsNullOrEmpty(Source))
			{
				return "";
			}
			byte[] bytes = Encoding.UTF8.GetBytes(Source);
			MemoryStream memoryStream = new MemoryStream();
			mobjCryptoService.Key = GetLegalKey();
			mobjCryptoService.IV = GetLegalIV();
			ICryptoTransform transform = mobjCryptoService.CreateEncryptor();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			memoryStream.Close();
			byte[] inArray = memoryStream.ToArray();
			return Convert.ToBase64String(inArray);
		}

		public string Decrypto(string Source)
		{
			if (!string.IsNullOrEmpty(Source))
			{
				try
				{
					byte[] array = Convert.FromBase64String(Source);
					MemoryStream stream = new MemoryStream(array, 0, array.Length);
					mobjCryptoService.Key = GetLegalKey();
					mobjCryptoService.IV = GetLegalIV();
					ICryptoTransform transform = mobjCryptoService.CreateDecryptor();
					CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
					StreamReader streamReader = new StreamReader(stream2);
					return streamReader.ReadToEnd();
				}
				catch
				{
					return "";
				}
			}
			return "";
		}
	}
}
