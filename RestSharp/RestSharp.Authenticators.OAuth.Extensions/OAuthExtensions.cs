using System;
using System.Security.Cryptography;
using System.Text;

namespace RestSharp.Authenticators.OAuth.Extensions
{
	internal static class OAuthExtensions
	{
		public static string ToRequestValue(this OAuthSignatureMethod signatureMethod)
		{
			string text = signatureMethod.ToString().ToUpper();
			int num = text.IndexOf("SHA1");
			return (num > -1) ? text.Insert(num, "-") : text;
		}

		public static OAuthSignatureMethod FromRequestValue(this string signatureMethod)
		{
			switch (signatureMethod)
			{
			case "HMAC-SHA1":
				return OAuthSignatureMethod.HmacSha1;
			case "RSA-SHA1":
				return OAuthSignatureMethod.RsaSha1;
			default:
				return OAuthSignatureMethod.PlainText;
			}
		}

		public static string HashWith(this string input, HashAlgorithm algorithm)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(input);
			byte[] inArray = algorithm.ComputeHash(bytes);
			return Convert.ToBase64String(inArray);
		}
	}
}
