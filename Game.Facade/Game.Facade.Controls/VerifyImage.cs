using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace Game.Facade.Controls
{
	public class VerifyImage
	{
		private static byte[] randb = new byte[4];

		private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

		private static Matrix m = new Matrix();

		private static Font font = new Font("宋体", 9.2f, FontStyle.Bold);

		private static int Next(int max)
		{
			rand.GetBytes(randb);
			int num = BitConverter.ToInt32(randb, 0);
			num %= max + 1;
			if (num < 0)
			{
				num = -num;
			}
			return num;
		}

		private static int Next(int min, int max)
		{
			return Next(max - min) + min;
		}

		public VerifyImageInfo GenerateImage(string code, int width, int height, Color bgcolor)
		{
			VerifyImageInfo verifyImageInfo = new VerifyImageInfo("image/pjpeg", ImageFormat.Jpeg);
			width = 47;
			height = 20;
			Bitmap image = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(image);
			graphics.SmoothingMode = SmoothingMode.HighSpeed;
			graphics.Clear(bgcolor);
			int num = 60;
			Pen pen = new Pen(Color.FromArgb(Next(50) + num, Next(50) + num, Next(50) + num), 1f);
			SolidBrush solidBrush = new SolidBrush(Color.FromArgb(Next(100), Next(100), Next(100)));
			for (int i = 0; i < 3; i++)
			{
				graphics.DrawArc(pen, Next(20) - 10, Next(20) - 10, Next(width) + 10, Next(height) + 10, Next(-100, 100), Next(-200, 200));
			}
			for (int j = 0; j < code.Length; j++)
			{
				solidBrush = new SolidBrush(Color.Black);
				graphics.DrawString(point: new PointF((float)j * 7.2f + 4.2f, 2.6f), s: code[j].ToString(), font: font, brush: solidBrush);
			}
			solidBrush.Dispose();
			graphics.Dispose();
			verifyImageInfo.Image = image;
			return verifyImageInfo;
		}
	}
}
