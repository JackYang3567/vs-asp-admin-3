using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RestSharp.Contrib
{
	internal class HttpEncoder
	{
		private static char[] hexChars;

		private static object entitiesLock;

		private static SortedDictionary<string, char> entities;

		private static HttpEncoder defaultEncoder;

		private static HttpEncoder currentEncoder;

		private static IDictionary<string, char> Entities
		{
			get
			{
				lock (entitiesLock)
				{
					if (entities == null)
					{
						InitEntities();
					}
					return entities;
				}
			}
		}

		public static HttpEncoder Current
		{
			get
			{
				return currentEncoder;
			}
		}

		public static HttpEncoder Default
		{
			get
			{
				return defaultEncoder;
			}
		}

		static HttpEncoder()
		{
			hexChars = "0123456789abcdef".ToCharArray();
			entitiesLock = new object();
			defaultEncoder = new HttpEncoder();
			currentEncoder = defaultEncoder;
		}

		internal static void HeaderNameValueEncode(string headerName, string headerValue, out string encodedHeaderName, out string encodedHeaderValue)
		{
			if (string.IsNullOrEmpty(headerName))
			{
				encodedHeaderName = headerName;
			}
			else
			{
				encodedHeaderName = EncodeHeaderString(headerName);
			}
			if (string.IsNullOrEmpty(headerValue))
			{
				encodedHeaderValue = headerValue;
			}
			else
			{
				encodedHeaderValue = EncodeHeaderString(headerValue);
			}
		}

		private static void StringBuilderAppend(string s, ref StringBuilder sb)
		{
			if (sb == null)
			{
				sb = new StringBuilder(s);
			}
			else
			{
				sb.Append(s);
			}
		}

		private static string EncodeHeaderString(string input)
		{
			StringBuilder sb = null;
			foreach (char c in input)
			{
				if ((c < ' ' && c != '\t') || c == '\u007f')
				{
					StringBuilderAppend(string.Format("%{0:x2}", (int)c), ref sb);
				}
			}
			if (sb == null)
			{
				return input;
			}
			return sb.ToString();
		}

		internal static string UrlPathEncode(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				MemoryStream memoryStream = new MemoryStream();
				int length = value.Length;
				for (int i = 0; i < length; i++)
				{
					UrlPathEncodeChar(value[i], memoryStream);
				}
				byte[] array = memoryStream.ToArray();
				return Encoding.ASCII.GetString(array, 0, array.Length);
			}
			return value;
		}

		internal static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = bytes.Length;
			if (num != 0)
			{
				if (offset < 0 || offset >= num)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (count < 0 || count > num - offset)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				MemoryStream memoryStream = new MemoryStream(count);
				int num2 = offset + count;
				for (int i = offset; i < num2; i++)
				{
					UrlEncodeChar((char)bytes[i], memoryStream, false);
				}
				return memoryStream.ToArray();
			}
			return new byte[0];
		}

		internal static string HtmlEncode(string s)
		{
			if (s != null)
			{
				if (s.Length != 0)
				{
					bool flag = false;
					foreach (char c in s)
					{
						if (c == '&' || c == '"' || c == '<' || c == '>' || c > '\u009f')
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						StringBuilder stringBuilder = new StringBuilder();
						int length = s.Length;
						for (int i = 0; i < length; i++)
						{
							switch (s[i])
							{
							case '&':
								stringBuilder.Append("&amp;");
								break;
							case '>':
								stringBuilder.Append("&gt;");
								break;
							case '<':
								stringBuilder.Append("&lt;");
								break;
							case '"':
								stringBuilder.Append("&quot;");
								break;
							case '＜':
								stringBuilder.Append("&#65308;");
								break;
							case '＞':
								stringBuilder.Append("&#65310;");
								break;
							default:
							{
								char c2 = s[i];
								if (c2 > '\u009f' && c2 < 'Ā')
								{
									stringBuilder.Append("&#");
									StringBuilder stringBuilder2 = stringBuilder;
									int num = c2;
									stringBuilder2.Append(num.ToString(Helpers.InvariantCulture));
									stringBuilder.Append(";");
								}
								else
								{
									stringBuilder.Append(c2);
								}
								break;
							}
							}
						}
						return stringBuilder.ToString();
					}
					return s;
				}
				return string.Empty;
			}
			return null;
		}

		internal static string HtmlAttributeEncode(string s)
		{
			if (s != null)
			{
				if (s.Length != 0)
				{
					bool flag = false;
					foreach (char c in s)
					{
						if (c == '&' || c == '"' || c == '<')
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						StringBuilder stringBuilder = new StringBuilder();
						int length = s.Length;
						for (int i = 0; i < length; i++)
						{
							switch (s[i])
							{
							case '&':
								stringBuilder.Append("&amp;");
								break;
							case '"':
								stringBuilder.Append("&quot;");
								break;
							case '<':
								stringBuilder.Append("&lt;");
								break;
							default:
								stringBuilder.Append(s[i]);
								break;
							}
						}
						return stringBuilder.ToString();
					}
					return s;
				}
				return string.Empty;
			}
			return null;
		}

		internal static string HtmlDecode(string s)
		{
			if (s != null)
			{
				if (s.Length != 0)
				{
					if (s.IndexOf('&') != -1)
					{
						StringBuilder stringBuilder = new StringBuilder();
						StringBuilder stringBuilder2 = new StringBuilder();
						int length = s.Length;
						int num = 0;
						int num2 = 0;
						bool flag = false;
						bool flag2 = false;
						for (int i = 0; i < length; i++)
						{
							char c = s[i];
							if (num == 0)
							{
								if (c == '&')
								{
									stringBuilder.Append(c);
									num = 1;
								}
								else
								{
									stringBuilder2.Append(c);
								}
							}
							else if (c == '&')
							{
								num = 1;
								if (flag2)
								{
									stringBuilder.Append(num2.ToString(Helpers.InvariantCulture));
									flag2 = false;
								}
								stringBuilder2.Append(stringBuilder.ToString());
								stringBuilder.Length = 0;
								stringBuilder.Append('&');
							}
							else
							{
								switch (num)
								{
								case 1:
									if (c == ';')
									{
										num = 0;
										stringBuilder2.Append(stringBuilder.ToString());
										stringBuilder2.Append(c);
										stringBuilder.Length = 0;
									}
									else
									{
										num2 = 0;
										flag = false;
										num = ((c == '#') ? 3 : 2);
										stringBuilder.Append(c);
									}
									break;
								case 2:
									stringBuilder.Append(c);
									if (c == ';')
									{
										string text = stringBuilder.ToString();
										if (text.Length > 1 && Entities.ContainsKey(text.Substring(1, text.Length - 2)))
										{
											text = Entities[text.Substring(1, text.Length - 2)].ToString();
										}
										stringBuilder2.Append(text);
										num = 0;
										stringBuilder.Length = 0;
									}
									break;
								case 3:
									if (c == ';')
									{
										if (num2 > 65535)
										{
											stringBuilder2.Append("&#");
											stringBuilder2.Append(num2.ToString(Helpers.InvariantCulture));
											stringBuilder2.Append(";");
										}
										else
										{
											stringBuilder2.Append((char)num2);
										}
										num = 0;
										stringBuilder.Length = 0;
										flag2 = false;
									}
									else if (flag && Uri.IsHexDigit(c))
									{
										num2 = num2 * 16 + Uri.FromHex(c);
										flag2 = true;
									}
									else if (char.IsDigit(c))
									{
										num2 = num2 * 10 + (c - 48);
										flag2 = true;
									}
									else if (num2 == 0 && (c == 'x' || c == 'X'))
									{
										flag = true;
									}
									else
									{
										num = 2;
										if (flag2)
										{
											stringBuilder.Append(num2.ToString(Helpers.InvariantCulture));
											flag2 = false;
										}
										stringBuilder.Append(c);
									}
									break;
								}
							}
						}
						if (stringBuilder.Length > 0)
						{
							stringBuilder2.Append(stringBuilder.ToString());
						}
						else if (flag2)
						{
							stringBuilder2.Append(num2.ToString(Helpers.InvariantCulture));
						}
						return stringBuilder2.ToString();
					}
					return s;
				}
				return string.Empty;
			}
			return null;
		}

		internal static bool NotEncoded(char c)
		{
			return c == '!' || c == '(' || c == ')' || c == '*' || c == '-' || c == '.' || c == '_' || c == '\'';
		}

		internal static void UrlEncodeChar(char c, Stream result, bool isUnicode)
		{
			if (c > 'ÿ')
			{
				result.WriteByte(37);
				result.WriteByte(117);
				int num = (int)c >> 12;
				result.WriteByte((byte)hexChars[num]);
				num = (((int)c >> 8) & 0xF);
				result.WriteByte((byte)hexChars[num]);
				num = (((int)c >> 4) & 0xF);
				result.WriteByte((byte)hexChars[num]);
				num = (c & 0xF);
				result.WriteByte((byte)hexChars[num]);
			}
			else if (c > ' ' && NotEncoded(c))
			{
				result.WriteByte((byte)c);
			}
			else if (c == ' ')
			{
				result.WriteByte(43);
			}
			else if (c < '0' || (c < 'A' && c > '9') || (c > 'Z' && c < 'a') || c > 'z')
			{
				if (isUnicode && c > '\u007f')
				{
					result.WriteByte(37);
					result.WriteByte(117);
					result.WriteByte(48);
					result.WriteByte(48);
				}
				else
				{
					result.WriteByte(37);
				}
				int num = (int)c >> 4;
				result.WriteByte((byte)hexChars[num]);
				num = (c & 0xF);
				result.WriteByte((byte)hexChars[num]);
			}
			else
			{
				result.WriteByte((byte)c);
			}
		}

		internal static void UrlPathEncodeChar(char c, Stream result)
		{
			if (c < '!' || c > '~')
			{
				byte[] bytes = Encoding.UTF8.GetBytes(c.ToString());
				for (int i = 0; i < bytes.Length; i++)
				{
					result.WriteByte(37);
					int num = bytes[i] >> 4;
					result.WriteByte((byte)hexChars[num]);
					num = (bytes[i] & 0xF);
					result.WriteByte((byte)hexChars[num]);
				}
			}
			else if (c == ' ')
			{
				result.WriteByte(37);
				result.WriteByte(50);
				result.WriteByte(48);
			}
			else
			{
				result.WriteByte((byte)c);
			}
		}

		private static void InitEntities()
		{
			entities = new SortedDictionary<string, char>(StringComparer.Ordinal);
			entities.Add("nbsp", '\u00a0');
			entities.Add("iexcl", '¡');
			entities.Add("cent", '¢');
			entities.Add("pound", '£');
			entities.Add("curren", '¤');
			entities.Add("yen", '¥');
			entities.Add("brvbar", '¦');
			entities.Add("sect", '§');
			entities.Add("uml", '\u00a8');
			entities.Add("copy", '©');
			entities.Add("ordf", 'ª');
			entities.Add("laquo", '«');
			entities.Add("not", '¬');
			entities.Add("shy", '­');
			entities.Add("reg", '®');
			entities.Add("macr", '\u00af');
			entities.Add("deg", '°');
			entities.Add("plusmn", '±');
			entities.Add("sup2", '²');
			entities.Add("sup3", '³');
			entities.Add("acute", '\u00b4');
			entities.Add("micro", 'µ');
			entities.Add("para", '¶');
			entities.Add("middot", '·');
			entities.Add("cedil", '\u00b8');
			entities.Add("sup1", '¹');
			entities.Add("ordm", 'º');
			entities.Add("raquo", '»');
			entities.Add("frac14", '¼');
			entities.Add("frac12", '½');
			entities.Add("frac34", '¾');
			entities.Add("iquest", '¿');
			entities.Add("Agrave", 'À');
			entities.Add("Aacute", 'Á');
			entities.Add("Acirc", 'Â');
			entities.Add("Atilde", 'Ã');
			entities.Add("Auml", 'Ä');
			entities.Add("Aring", 'Å');
			entities.Add("AElig", 'Æ');
			entities.Add("Ccedil", 'Ç');
			entities.Add("Egrave", 'È');
			entities.Add("Eacute", 'É');
			entities.Add("Ecirc", 'Ê');
			entities.Add("Euml", 'Ë');
			entities.Add("Igrave", 'Ì');
			entities.Add("Iacute", 'Í');
			entities.Add("Icirc", 'Î');
			entities.Add("Iuml", 'Ï');
			entities.Add("ETH", 'Ð');
			entities.Add("Ntilde", 'Ñ');
			entities.Add("Ograve", 'Ò');
			entities.Add("Oacute", 'Ó');
			entities.Add("Ocirc", 'Ô');
			entities.Add("Otilde", 'Õ');
			entities.Add("Ouml", 'Ö');
			entities.Add("times", '×');
			entities.Add("Oslash", 'Ø');
			entities.Add("Ugrave", 'Ù');
			entities.Add("Uacute", 'Ú');
			entities.Add("Ucirc", 'Û');
			entities.Add("Uuml", 'Ü');
			entities.Add("Yacute", 'Ý');
			entities.Add("THORN", 'Þ');
			entities.Add("szlig", 'ß');
			entities.Add("agrave", 'à');
			entities.Add("aacute", 'á');
			entities.Add("acirc", 'â');
			entities.Add("atilde", 'ã');
			entities.Add("auml", 'ä');
			entities.Add("aring", 'å');
			entities.Add("aelig", 'æ');
			entities.Add("ccedil", 'ç');
			entities.Add("egrave", 'è');
			entities.Add("eacute", 'é');
			entities.Add("ecirc", 'ê');
			entities.Add("euml", 'ë');
			entities.Add("igrave", 'ì');
			entities.Add("iacute", 'í');
			entities.Add("icirc", 'î');
			entities.Add("iuml", 'ï');
			entities.Add("eth", 'ð');
			entities.Add("ntilde", 'ñ');
			entities.Add("ograve", 'ò');
			entities.Add("oacute", 'ó');
			entities.Add("ocirc", 'ô');
			entities.Add("otilde", 'õ');
			entities.Add("ouml", 'ö');
			entities.Add("divide", '÷');
			entities.Add("oslash", 'ø');
			entities.Add("ugrave", 'ù');
			entities.Add("uacute", 'ú');
			entities.Add("ucirc", 'û');
			entities.Add("uuml", 'ü');
			entities.Add("yacute", 'ý');
			entities.Add("thorn", 'þ');
			entities.Add("yuml", 'ÿ');
			entities.Add("fnof", 'ƒ');
			entities.Add("Alpha", 'Α');
			entities.Add("Beta", 'Β');
			entities.Add("Gamma", 'Γ');
			entities.Add("Delta", 'Δ');
			entities.Add("Epsilon", 'Ε');
			entities.Add("Zeta", 'Ζ');
			entities.Add("Eta", 'Η');
			entities.Add("Theta", 'Θ');
			entities.Add("Iota", 'Ι');
			entities.Add("Kappa", 'Κ');
			entities.Add("Lambda", 'Λ');
			entities.Add("Mu", 'Μ');
			entities.Add("Nu", 'Ν');
			entities.Add("Xi", 'Ξ');
			entities.Add("Omicron", 'Ο');
			entities.Add("Pi", 'Π');
			entities.Add("Rho", 'Ρ');
			entities.Add("Sigma", 'Σ');
			entities.Add("Tau", 'Τ');
			entities.Add("Upsilon", 'Υ');
			entities.Add("Phi", 'Φ');
			entities.Add("Chi", 'Χ');
			entities.Add("Psi", 'Ψ');
			entities.Add("Omega", 'Ω');
			entities.Add("alpha", 'α');
			entities.Add("beta", 'β');
			entities.Add("gamma", 'γ');
			entities.Add("delta", 'δ');
			entities.Add("epsilon", 'ε');
			entities.Add("zeta", 'ζ');
			entities.Add("eta", 'η');
			entities.Add("theta", 'θ');
			entities.Add("iota", 'ι');
			entities.Add("kappa", 'κ');
			entities.Add("lambda", 'λ');
			entities.Add("mu", 'μ');
			entities.Add("nu", 'ν');
			entities.Add("xi", 'ξ');
			entities.Add("omicron", 'ο');
			entities.Add("pi", 'π');
			entities.Add("rho", 'ρ');
			entities.Add("sigmaf", 'ς');
			entities.Add("sigma", 'σ');
			entities.Add("tau", 'τ');
			entities.Add("upsilon", 'υ');
			entities.Add("phi", 'φ');
			entities.Add("chi", 'χ');
			entities.Add("psi", 'ψ');
			entities.Add("omega", 'ω');
			entities.Add("thetasym", 'ϑ');
			entities.Add("upsih", 'ϒ');
			entities.Add("piv", 'ϖ');
			entities.Add("bull", '•');
			entities.Add("hellip", '…');
			entities.Add("prime", '′');
			entities.Add("Prime", '″');
			entities.Add("oline", '‾');
			entities.Add("frasl", '⁄');
			entities.Add("weierp", '℘');
			entities.Add("image", 'ℑ');
			entities.Add("real", 'ℜ');
			entities.Add("trade", '™');
			entities.Add("alefsym", 'ℵ');
			entities.Add("larr", '←');
			entities.Add("uarr", '↑');
			entities.Add("rarr", '→');
			entities.Add("darr", '↓');
			entities.Add("harr", '↔');
			entities.Add("crarr", '↵');
			entities.Add("lArr", '⇐');
			entities.Add("uArr", '⇑');
			entities.Add("rArr", '⇒');
			entities.Add("dArr", '⇓');
			entities.Add("hArr", '⇔');
			entities.Add("forall", '∀');
			entities.Add("part", '∂');
			entities.Add("exist", '∃');
			entities.Add("empty", '∅');
			entities.Add("nabla", '∇');
			entities.Add("isin", '∈');
			entities.Add("notin", '∉');
			entities.Add("ni", '∋');
			entities.Add("prod", '∏');
			entities.Add("sum", '∑');
			entities.Add("minus", '−');
			entities.Add("lowast", '∗');
			entities.Add("radic", '√');
			entities.Add("prop", '∝');
			entities.Add("infin", '∞');
			entities.Add("ang", '∠');
			entities.Add("and", '∧');
			entities.Add("or", '∨');
			entities.Add("cap", '∩');
			entities.Add("cup", '∪');
			entities.Add("int", '∫');
			entities.Add("there4", '∴');
			entities.Add("sim", '∼');
			entities.Add("cong", '≅');
			entities.Add("asymp", '≈');
			entities.Add("ne", '≠');
			entities.Add("equiv", '≡');
			entities.Add("le", '≤');
			entities.Add("ge", '≥');
			entities.Add("sub", '⊂');
			entities.Add("sup", '⊃');
			entities.Add("nsub", '⊄');
			entities.Add("sube", '⊆');
			entities.Add("supe", '⊇');
			entities.Add("oplus", '⊕');
			entities.Add("otimes", '⊗');
			entities.Add("perp", '⊥');
			entities.Add("sdot", '⋅');
			entities.Add("lceil", '⌈');
			entities.Add("rceil", '⌉');
			entities.Add("lfloor", '⌊');
			entities.Add("rfloor", '⌋');
			entities.Add("lang", '〈');
			entities.Add("rang", '〉');
			entities.Add("loz", '◊');
			entities.Add("spades", '♠');
			entities.Add("clubs", '♣');
			entities.Add("hearts", '♥');
			entities.Add("diams", '♦');
			entities.Add("quot", '"');
			entities.Add("amp", '&');
			entities.Add("lt", '<');
			entities.Add("gt", '>');
			entities.Add("OElig", 'Œ');
			entities.Add("oelig", 'œ');
			entities.Add("Scaron", 'Š');
			entities.Add("scaron", 'š');
			entities.Add("Yuml", 'Ÿ');
			entities.Add("circ", '\u02c6');
			entities.Add("tilde", '\u02dc');
			entities.Add("ensp", '\u2002');
			entities.Add("emsp", '\u2003');
			entities.Add("thinsp", '\u2009');
			entities.Add("zwnj", '\u200c');
			entities.Add("zwj", '\u200d');
			entities.Add("lrm", '\u200e');
			entities.Add("rlm", '\u200f');
			entities.Add("ndash", '–');
			entities.Add("mdash", '—');
			entities.Add("lsquo", '‘');
			entities.Add("rsquo", '’');
			entities.Add("sbquo", '‚');
			entities.Add("ldquo", '“');
			entities.Add("rdquo", '”');
			entities.Add("bdquo", '„');
			entities.Add("dagger", '†');
			entities.Add("Dagger", '‡');
			entities.Add("permil", '‰');
			entities.Add("lsaquo", '‹');
			entities.Add("rsaquo", '›');
			entities.Add("euro", '€');
		}
	}
}
