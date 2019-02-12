using System;
using System.Text;

namespace Game.Utils
{
	public sealed class CWHEncryptNet
	{
		private static ushort ENCRYPT_KEY_LEN = 8;

		private static ushort MAX_ENCRYPT_LEN = (ushort)(MAX_SOURCE_LEN * XOR_TIMES);

		private static ushort MAX_SOURCE_LEN = 64;

		private static ushort XOR_TIMES = 8;

		private CWHEncryptNet()
		{
		}

		public static string XorCrevasse(string encrypData)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ushort num = (ushort)encrypData.Length;
			if (num < ENCRYPT_KEY_LEN * 8)
			{
				return "";
			}
			ushort num2 = Convert.ToUInt16(encrypData.Substring(0, 4), 16);
			if (num != (num2 + ENCRYPT_KEY_LEN - 1) / (int)ENCRYPT_KEY_LEN * ENCRYPT_KEY_LEN * 8)
			{
				return "";
			}
			for (int i = 0; i < num2; i++)
			{
				string text = "";
				string text2 = "";
				text = encrypData.Substring(i * 8, 4);
				text2 = encrypData.Substring(i * 8 + 4, 4);
				ushort num3 = Convert.ToUInt16(text, 16);
				ushort num4 = Convert.ToUInt16(text2, 16);
				stringBuilder.Append((char)(num3 ^ num4));
			}
			return stringBuilder.ToString();
		}

		public static string XorEncrypt(string sourceData)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ushort[] array = new ushort[ENCRYPT_KEY_LEN];
			array[0] = (ushort)sourceData.Length;
			Random random = new Random();
			for (int i = 1; i < array.Length; i++)
			{
				array[i] = (ushort)(random.Next(0, 65535) % 65535);
			}
			ushort num = 0;
			ushort num2 = (ushort)((array[0] + ENCRYPT_KEY_LEN - 1) / (int)ENCRYPT_KEY_LEN * ENCRYPT_KEY_LEN);
			for (ushort num3 = 0; num3 < num2; num3 = (ushort)(num3 + 1))
			{
				num = ((num3 >= array[0]) ? ((ushort)(array[(int)num3 % (int)ENCRYPT_KEY_LEN] ^ (ushort)(random.Next(0, 65535) % 65535))) : ((ushort)(sourceData[num3] ^ array[(int)num3 % (int)ENCRYPT_KEY_LEN])));
				stringBuilder.Append(array[(int)num3 % (int)ENCRYPT_KEY_LEN].ToString("X4")).Append(num.ToString("X4"));
			}
			return stringBuilder.ToString();
		}
	}
}
