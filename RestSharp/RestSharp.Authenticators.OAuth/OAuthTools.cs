using RestSharp.Authenticators.OAuth.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestSharp.Authenticators.OAuth
{
	[Serializable]
	internal static class OAuthTools
	{
		private const string AlphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

		private const string Digit = "1234567890";

		private const string Lower = "abcdefghijklmnopqrstuvwxyz";

		private const string Unreserved = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-._~";

		private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		private static readonly Random _random;

		private static readonly object _randomLock;

		private static readonly RandomNumberGenerator _rng;

		private static readonly Encoding _encoding;

		private static readonly string[] UriRfc3986CharsToEscape;

		private static readonly string[] UriRfc3968EscapedHex;

		static OAuthTools()
		{
			_randomLock = new object();
			_rng = RandomNumberGenerator.Create();
			_encoding = Encoding.UTF8;
			UriRfc3986CharsToEscape = new string[5]
			{
				"!",
				"*",
				"'",
				"(",
				")"
			};
			UriRfc3968EscapedHex = new string[5]
			{
				"%21",
				"%2A",
				"%27",
				"%28",
				"%29"
			};
			byte[] array = new byte[4];
			_rng.GetNonZeroBytes(array);
			_random = new Random(BitConverter.ToInt32(array, 0));
		}

		public static string GetNonce()
		{
			char[] array = new char[16];
			lock (_randomLock)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = "abcdefghijklmnopqrstuvwxyz1234567890"[_random.Next(0, "abcdefghijklmnopqrstuvwxyz1234567890".Length)];
				}
			}
			return new string(array);
		}

		public static string GetTimestamp()
		{
			return GetTimestamp(DateTime.UtcNow);
		}

		public static string GetTimestamp(DateTime dateTime)
		{
			return dateTime.ToUnixTime().ToString();
		}

		public static string UrlEncodeRelaxed(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(Uri.EscapeDataString(value));
			for (int i = 0; i < UriRfc3986CharsToEscape.Length; i++)
			{
				string oldValue = UriRfc3986CharsToEscape[i];
				stringBuilder.Replace(oldValue, UriRfc3968EscapedHex[i]);
			}
			return stringBuilder.ToString();
		}

		public static string UrlEncodeStrict(string value)
		{
			string result = "";
			value.ForEach(delegate(char c)
			{
				result += ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-._~".Contains(c) ? c.ToString() : c.ToString().PercentEncode());
			});
			return result;
		}

		public static string NormalizeRequestParameters(WebParameterCollection parameters)
		{
			WebParameterCollection collection = SortParametersExcludingSignature(parameters);
			return collection.Concatenate("=", "&");
		}

		public static WebParameterCollection SortParametersExcludingSignature(WebParameterCollection parameters)
		{
			WebParameterCollection webParameterCollection = new WebParameterCollection(parameters);
			IEnumerable<WebPair> parameters2 = from n in webParameterCollection
			where n.Name.EqualsIgnoreCase("oauth_signature")
			select n;
			webParameterCollection.RemoveAll(parameters2);
			webParameterCollection.ForEach(delegate(WebPair p)
			{
				p.Name = UrlEncodeStrict(p.Name);
				p.Value = UrlEncodeStrict(p.Value);
			});
			webParameterCollection.Sort((WebPair x, WebPair y) => (string.CompareOrdinal(x.Name, y.Name) != 0) ? string.CompareOrdinal(x.Name, y.Name) : string.CompareOrdinal(x.Value, y.Value));
			return webParameterCollection;
		}

		public static string ConstructRequestUrl(Uri url)
		{
			if (url == (Uri)null)
			{
				throw new ArgumentNullException("url");
			}
			StringBuilder stringBuilder = new StringBuilder();
			string value = "{0}://{1}".FormatWith(url.Scheme, url.Host);
			string text = ":{0}".FormatWith(url.Port);
			bool flag = url.Scheme == "http" && url.Port == 80;
			bool flag2 = url.Scheme == "https" && url.Port == 443;
			stringBuilder.Append(value);
			stringBuilder.Append((!flag && !flag2) ? text : "");
			stringBuilder.Append(url.AbsolutePath);
			return stringBuilder.ToString();
		}

		public static string ConcatenateRequestElements(string method, string url, WebParameterCollection parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string value = method.ToUpper().Then("&");
			string value2 = UrlEncodeRelaxed(ConstructRequestUrl(url.AsUri())).Then("&");
			string value3 = UrlEncodeRelaxed(NormalizeRequestParameters(parameters));
			stringBuilder.Append(value);
			stringBuilder.Append(value2);
			stringBuilder.Append(value3);
			return stringBuilder.ToString();
		}

		public static string GetSignature(OAuthSignatureMethod signatureMethod, string signatureBase, string consumerSecret)
		{
			return GetSignature(signatureMethod, OAuthSignatureTreatment.Escaped, signatureBase, consumerSecret, null);
		}

		public static string GetSignature(OAuthSignatureMethod signatureMethod, OAuthSignatureTreatment signatureTreatment, string signatureBase, string consumerSecret)
		{
			return GetSignature(signatureMethod, signatureTreatment, signatureBase, consumerSecret, null);
		}

		public static string GetSignature(OAuthSignatureMethod signatureMethod, string signatureBase, string consumerSecret, string tokenSecret)
		{
			return GetSignature(signatureMethod, OAuthSignatureTreatment.Escaped, consumerSecret, tokenSecret);
		}

		public static string GetSignature(OAuthSignatureMethod signatureMethod, OAuthSignatureTreatment signatureTreatment, string signatureBase, string consumerSecret, string tokenSecret)
		{
			if (tokenSecret.IsNullOrBlank())
			{
				tokenSecret = string.Empty;
			}
			consumerSecret = UrlEncodeRelaxed(consumerSecret);
			tokenSecret = UrlEncodeRelaxed(tokenSecret);
			string text;
			switch (signatureMethod)
			{
			case OAuthSignatureMethod.HmacSha1:
			{
				HMACSHA1 hMACSHA = new HMACSHA1();
				string s = "{0}&{1}".FormatWith(consumerSecret, tokenSecret);
				hMACSHA.Key = _encoding.GetBytes(s);
				text = signatureBase.HashWith(hMACSHA);
				break;
			}
			case OAuthSignatureMethod.PlainText:
				text = "{0}&{1}".FormatWith(consumerSecret, tokenSecret);
				break;
			default:
				throw new NotImplementedException("Only HMAC-SHA1 is currently supported.");
			}
			return (signatureTreatment == OAuthSignatureTreatment.Escaped) ? UrlEncodeRelaxed(text) : text;
		}
	}
}
