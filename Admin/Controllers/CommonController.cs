using Admin.Models;
using Game.Facade;
using Game.Facade.Controls;
using Game.Facade.Tools;
using Game.Utils;
using Game.Utils.Cache;
using Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
	public class CommonController : Controller
	{
		[AllowAnonymous]
		public ActionResult GetValidateCode()
		{
			string text = TextUtility.CreateAuthStr(5, true);
			WHCache.Default.Save<SessionCache>("VerifyCodeKey", text, 5);
			VerifyImageInfo verifyImageInfo = new VerifyImageVer2().GenerateImage(text, 0, 0, Color.FromArgb(227, 227, 227), 2);
			Bitmap image = verifyImageInfo.Image;
			byte[] fileContents = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				image.Save(memoryStream, ImageFormat.Bmp);
				fileContents = memoryStream.GetBuffer();
				memoryStream.Close();
			}
			return File(fileContents, "image/pjpeg");
		}

		[HttpPost]
		public string GetFileContext(string extension, int type)
		{
			APIResult aPIResult = new APIResult();
			bool flag = true;
			try
			{
				HttpPostedFileBase httpPostedFileBase = base.Request.Files[0];
				string text = httpPostedFileBase.FileName.Trim().ToLower();
				string value = text.Substring(text.LastIndexOf(".") + 1);
				if (!extension.ToLower().Equals(value))
				{
					flag = false;
					aPIResult.error = -2;
					aPIResult.msg = "文件的格式不对！";
				}
				if (flag)
				{
					string msg = "";
					if (type == 1)
					{
						msg = ReadConfineContentInfo(httpPostedFileBase);
					}
					flag = true;
					aPIResult.error = 0;
					aPIResult.msg = msg;
				}
			}
			catch (Exception ex)
			{
				aPIResult.error = -2;
				aPIResult.msg = "上传文件异常";
				LogUtil.WriteError(ex.ToString());
			}
			return JsonConvert.SerializeObject(aPIResult);
		}

		private string ReadConfineContentInfo(HttpPostedFileBase file)
		{
			int contentLength = file.ContentLength;
			byte[] buffer = new byte[contentLength];
			Stream inputStream = file.InputStream;
			inputStream.Read(buffer, 0, contentLength);
			inputStream.Position = 0L;
			StreamReader streamReader = new StreamReader(inputStream, Encoding.Default);
			string text = streamReader.ReadToEnd();
			streamReader.Close();
			inputStream.Close();
			inputStream = null;
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = text.Split(',');
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					if (!string.IsNullOrEmpty(text2) && FacadeManage.aideAccountsFacade.IsExitStringInConfineContent(text2))
					{
						list.Add(text2);
					}
				}
			}
			if (list.Count <= 0)
			{
				return "";
			}
			return string.Join(",", list.ToArray());
		}

		[HttpPost]
		public string UploadFileToLocal()
		{
			APIResult aPIResult = new APIResult();
			try
			{
				int num = TypeUtil.ObjectToInt(base.Request["op"]);
				int num2 = 0;
				if (num > 0)
				{
					EnumerationList.UploadFileEnum uploadFileEnum = (EnumerationList.UploadFileEnum)Enum.Parse(typeof(EnumerationList.UploadFileEnum), num.ToString());
					string text = "";
					switch (uploadFileEnum)
					{
					case EnumerationList.UploadFileEnum.EditImg:
						text = "/Content/Upload/Editer/";
						num2 = 0;
						break;
					case EnumerationList.UploadFileEnum.MatchImg:
						num2 = 0;
						text = "/Content/Upload/Match/";
						break;
					case EnumerationList.UploadFileEnum.MobileImg:
						num2 = 0;
						text = "/Content/Upload/Mobile/";
						break;
					case EnumerationList.UploadFileEnum.PcNewsImg:
						num2 = 0;
						text = "/Content/Upload/PC/";
						break;
					case EnumerationList.UploadFileEnum.RulesImg:
						num2 = 0;
						text = "/Content/Upload/Rules/";
						break;
					case EnumerationList.UploadFileEnum.ActivityImg:
						num2 = 0;
						text = "/Content/Upload/Activity/";
						break;
                   
					case EnumerationList.UploadFileEnum.SiteLogoImg:
						num2 = 7;
						text = "/Content/Upload/Site/";
						break;
					case EnumerationList.UploadFileEnum.SiteAdminlogoImg:
						num2 = 8;
						text = "/Content/Upload/Site/";
						break;
					case EnumerationList.UploadFileEnum.SiteMobileLogoImg:
						num2 = 9;
						text = "/Content/Upload/Site/";
						break;
					case EnumerationList.UploadFileEnum.SiteMobileRegLogoImg:
						num2 = 10;
						text = "/Content/Upload/Site/";
						break;
                    case EnumerationList.UploadFileEnum.OffLinePayQrCodeAliImg:
                        num2 = 11;
                        text = "/Content/Upload/OffLinePayQrCode/";
                        break;
                    case EnumerationList.UploadFileEnum.OffLinePayQrCodeWeixinImg:
                        num2 = 12;
                        text = "/Content/Upload/OffLinePayQrCode/";
                        break;
					}
					HttpPostedFileBase httpPostedFileBase = base.Request.Files[0];
					if (!string.IsNullOrEmpty(text))
					{
						string text2 = TypeUtil.GetMapPath(text);
						string str = "";
						bool flag = true;
						if (num2 == 0)
						{
							if (!ValidateHelper.IsImgFileName(httpPostedFileBase.FileName))
							{
								flag = false;
								aPIResult.error = -2;
								aPIResult.msg = "上传文件的格式不对！";
							}
							if (httpPostedFileBase.ContentLength >= 2097152)
							{
								aPIResult.error = -2;
								aPIResult.msg = "上传文件的大小不能大于2M！";
								flag = false;
							}
							str = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
							text2 += str;
						}
						if (num2 == 7)
						{
							str = "logo.png";
							text2 += str;
						}
						if (num2 == 8)
						{
							str = "Adminlogo.png";
							text2 += str;
						}
						if (num2 == 9)
						{
							str = "MobileLogo.png";
							text2 += str;
						}
						if (num2 == 10)
						{
							str = "MobileRegLogo.png";
							text2 += str;
						}

                        if (num2 == 11)
                        {
                           // str = DateTime.Now.ToString("yyyyMMddHHmmss") + "_QrCode.png";
                            str =  "Ali_QrCode.png";
                            text2 += str;
                        }
                        if (num2 == 12)
                        {
                           // str = DateTime.Now.ToString("yyyyMMddHHmmss") + "_QrCode.png";
                            str = "Weixin_QrCode.png";
                            text2 += str;
                        }
						if (flag)
						{
							string path = text2.Substring(0, text2.LastIndexOf("\\"));
							DirectoryInfo directoryInfo = new DirectoryInfo(path);
							if (!directoryInfo.Exists)
							{
								directoryInfo.Create();
							}
							FileStream fileStream = new FileStream(text2, FileMode.Create, FileAccess.Write);
							byte[] @byte = TypeUtil.GetByte(httpPostedFileBase.InputStream);
							fileStream.Write(@byte, 0, @byte.Length);
							fileStream.Flush();
							fileStream.Close();
							aPIResult.error = 0;
							aPIResult.msg = "上传成功";
							aPIResult.url = text + str;
						}
					}
					else
					{
						aPIResult.error = -1;
						aPIResult.msg = "路径出错";
					}
				}
				else
				{
					aPIResult.error = -2;
					aPIResult.msg = "op出错";
				}
			}
			catch (Exception ex)
			{
				aPIResult.error = -2;
				aPIResult.msg = "上传图片异常";
				LogUtil.WriteError(ex.ToString());
			}
			return JsonConvert.SerializeObject(aPIResult);
		}

		[HttpPost]
		public string UploadFileToOther()
		{
			string text = TypeUtil.ObjectToString(base.Request["op"]);
			if (text == "")
			{
				text = base.Request["op"];
			}
			string str = ConfigurationManager.AppSettings["resourceurl"];
			string uploadApi = str + "api/file/uploadfile";
			string text2 = "bg_main";
			string result = "";
			if (ValidateHelper.IsNumeric(text))
			{
				EnumerationList.UploadFileEnum uploadFileEnum = (EnumerationList.UploadFileEnum)Enum.Parse(typeof(EnumerationList.UploadFileEnum), text);
				HttpPostedFileBase picFile = base.Request.Files[0];
				Random random = new Random();
				random.Next(10, 100);
				switch (uploadFileEnum)
				{
				case EnumerationList.UploadFileEnum.MatchImg:
					TypeUtil.ObjectToInt(TypeUtil.ObjectToString(base.Request["ID"]));
					text2 = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(10, 100).ToString() + random.Next(10, 100).ToString();
					result = SubmitFile(picFile, text2, TypeUtil.StringToInt(text), 0, 0, "img", uploadApi);
					break;
				case EnumerationList.UploadFileEnum.EditImg:
					text2 = DateTime.Now.ToString("yyyyMMddHHmmss") + "game_big";
					result = SubmitFile(picFile, text2, TypeUtil.StringToInt(text), 100, 100, "img", uploadApi);
					break;
				}
			}
			return result;
		}

		public string SubmitFile(HttpPostedFileBase picFile, string fileName, int op, int height, int wight, string mode, string uploadApi)
		{
			string empty = string.Empty;
			string str = ConfigurationManager.AppSettings["resourceurl"];
			if (picFile == null)
			{
				return JsonConvert.SerializeObject(new
				{
					error = -1,
					url = "",
					msg = "上传内容为空！"
				});
			}
			if (string.IsNullOrEmpty(fileName))
			{
				return JsonConvert.SerializeObject(new
				{
					error = -4,
					url = "",
					msg = "文件名不能为空！"
				});
			}
			if (op >= 1)
			{
				EnumerationList.UploadFileEnum uploadFileEnum = (EnumerationList.UploadFileEnum)Enum.Parse(typeof(EnumerationList.UploadFileEnum), op.ToString());
				UploadFileInfo uploadFileInfo = new UploadFileInfo();
				uploadFileInfo.Op = op;
				uploadFileInfo.FileData = TypeUtil.GetByte(picFile.InputStream);
				APIResult aPIResult = new APIResult();
				try
				{
					if (mode.ToLower() == "img")
					{
						string fileName2 = picFile.FileName;
						if (!ValidateHelper.IsImgFileName(fileName2))
						{
							return JsonConvert.SerializeObject(new
							{
								error = -2,
								url = "",
								msg = "上传文件的格式不对！"
							});
						}
						if (picFile.ContentLength >= 2097152)
						{
							return JsonConvert.SerializeObject(new
							{
								error = -2,
								url = "",
								msg = "上传文件的大小不能大于2M！"
							});
						}
						uploadFileInfo.FileName = Path.GetFileName(fileName + ".jpg");
						string postData = JsonConvert.SerializeObject(uploadFileInfo);
						aPIResult = JsonConvert.DeserializeObject<APIResult>(WebRequestHelper.WebApiPost(uploadApi, postData));
					}
					else
					{
						uploadFileInfo.FileName = Path.GetFileName(picFile.FileName);
						string postData2 = JsonConvert.SerializeObject(uploadFileInfo);
						aPIResult = JsonConvert.DeserializeObject<APIResult>(WebRequestHelper.WebApiPost(uploadApi, postData2));
					}
					if (aPIResult.error == 100)
					{
						switch (uploadFileEnum)
						{
						case EnumerationList.UploadFileEnum.MatchImg:
							return JsonConvert.SerializeObject(new
							{
								error = 0,
								url = str + "editer/" + fileName + ".jpg"
							});
						case EnumerationList.UploadFileEnum.EditImg:
							return JsonConvert.SerializeObject(new
							{
								error = 0,
								url = str + "game/" + fileName
							});
						default:
							return empty;
						}
					}
					return JsonConvert.SerializeObject(new
					{
						error = -2,
						url = "",
						msg = aPIResult.error
					});
				}
				catch (Exception ex)
				{
					LogUtil.WriteError(ex.ToString());
					return JsonConvert.SerializeObject(new
					{
						error = -2,
						msg = "上传文件异常",
						url = ""
					});
				}
			}
			return JsonConvert.SerializeObject(new
			{
				error = -4,
				url = "",
				msg = "op不对！"
			});
		}
	}
}
