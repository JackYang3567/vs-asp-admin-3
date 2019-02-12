using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Game.Facade
{
	public class PasswordCard
	{
		private int _serialNumber;

		public int SerialNumber
		{
			get
			{
				return _serialNumber;
			}
			set
			{
				_serialNumber = value;
			}
		}

		public string AddSpace()
		{
			string text = SerialNumber.ToString();
			return text.Substring(0, 3) + " " + text.Substring(3, 3) + " " + text.Substring(6, 3);
		}

		public string GetNumberByCoordinate(string coordinate)
		{
			int num = Convert.ToInt32(ConfigurationManager.AppSettings[coordinate]);
			num = SerialNumber / num % 1000;
			string text = num.ToString();
			switch (text.Length)
			{
			case 1:
				text += "00";
				break;
			case 2:
				text += "0";
				break;
			}
			return text;
		}

		public string[] RandomString()
		{
			string[] array = new string[3];
			string[] array2 = new string[4]
			{
				"1",
				"2",
				"3",
				"4"
			};
			string[] array3 = new string[6]
			{
				"A",
				"B",
				"C",
				"D",
				"E",
				"F"
			};
			Random random = new Random();
			bool flag = true;
			while (flag)
			{
				for (int i = 0; i < 3; i++)
				{
					string str = array2[random.Next(0, 3)];
					string str2 = array3[random.Next(0, 5)];
					array[i] = str2 + str;
				}
				flag = false;
				if (array[0] == array[1] || array[0] == array[2] || array[1] == array[2])
				{
					flag = true;
				}
			}
			return array;
		}

		public byte[] WritePasswordCardImg(string bgPath)
		{
			Image image = Image.FromFile(bgPath);
			Graphics graphics = Graphics.FromImage(image);
			LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Black, Color.Green, 4f, true);
			Font font = new Font("Arial", 10f);
			Font font2 = new Font("Arial", 10f, FontStyle.Bold);
			graphics.DrawString(AddSpace(), font2, brush, 65f, 8f);
			string[] array = new string[24]
			{
				"A1",
				"A2",
				"A3",
				"A4",
				"B1",
				"B2",
				"B3",
				"B4",
				"C1",
				"C2",
				"C3",
				"C4",
				"D1",
				"D2",
				"D3",
				"D4",
				"E1",
				"E2",
				"E3",
				"E4",
				"F1",
				"F2",
				"F3",
				"F4"
			};
			int num = 85;
			int num2 = 72;
			int num3 = 0;
			string empty = string.Empty;
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					empty = GetNumberByCoordinate(array[num3]);
					graphics.DrawString(empty, font, brush, (float)num, (float)num2);
					num3++;
					num += 90;
				}
				num = 85;
				num2 += 32;
			}
			MemoryStream memoryStream = new MemoryStream();
			image.Save(memoryStream, ImageFormat.Jpeg);
			image.Dispose();
			graphics.Dispose();
			return memoryStream.ToArray();
		}
	}
}
