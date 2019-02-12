using System.Drawing;
using System.Drawing.Imaging;

namespace Game.Utils.Verify
{
	public class VerifyImageInfo
	{
		private Bitmap m_image;

		private string m_contentType = "image/pjpeg";

		private ImageFormat m_imageFormat = ImageFormat.Jpeg;

		public Bitmap Image
		{
			get
			{
				return m_image;
			}
			set
			{
				m_image = value;
			}
		}

		public string ContentType
		{
			get
			{
				return m_contentType;
			}
			set
			{
				m_contentType = value;
			}
		}

		public ImageFormat ImageFormat
		{
			get
			{
				return m_imageFormat;
			}
			set
			{
				m_imageFormat = value;
			}
		}

		public VerifyImageInfo()
		{
		}

		public VerifyImageInfo(string contentType, ImageFormat imageFormat)
		{
			m_contentType = contentType;
			m_imageFormat = imageFormat;
		}
	}
}
