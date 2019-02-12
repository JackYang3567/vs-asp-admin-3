using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Game.Utils
{
	public sealed class IPQuery
	{
		public class CZ_INDEX_INFO
		{
			public uint IpEnd;

			public uint IpSet;

			public uint Offset;
		}

		public class PHCZIP
		{
			protected bool bFilePathInitialized;

			protected string FilePath;

			protected FileStream FileStrm;

			protected uint Index_Count;

			protected uint Index_End;

			protected uint Index_Set;

			protected CZ_INDEX_INFO Search_End;

			protected uint Search_Index_End;

			protected uint Search_Index_Set;

			protected CZ_INDEX_INFO Search_Mid;

			protected CZ_INDEX_INFO Search_Set;

			public PHCZIP()
			{
				bFilePathInitialized = false;
			}

			public PHCZIP(string dbFilePath)
			{
				bFilePathInitialized = false;
				SetDbFilePath(dbFilePath);
			}

			public void Dispose()
			{
				if (bFilePathInitialized)
				{
					bFilePathInitialized = false;
					FileStrm.Close();
				}
			}

			public string GetAddressWithIP(string IPValue)
			{
				if (!bFilePathInitialized)
				{
					return "";
				}
				Initialize();
				uint num = IPToUInt32(IPValue);
				while (true)
				{
					Search_Set = IndexInfoAtPos(Search_Index_Set);
					Search_End = IndexInfoAtPos(Search_Index_End);
					if (num >= Search_Set.IpSet && num <= Search_Set.IpEnd)
					{
						return ReadAddressInfoAtOffset(Search_Set.Offset);
					}
					if (num >= Search_End.IpSet && num <= Search_End.IpEnd)
					{
						return ReadAddressInfoAtOffset(Search_End.Offset);
					}
					Search_Mid = IndexInfoAtPos((Search_Index_End + Search_Index_Set) / 2u);
					if (num >= Search_Mid.IpSet && num <= Search_Mid.IpEnd)
					{
						break;
					}
					if (num < Search_Mid.IpSet)
					{
						Search_Index_End = (Search_Index_End + Search_Index_Set) / 2u;
					}
					else
					{
						Search_Index_Set = (Search_Index_End + Search_Index_Set) / 2u;
					}
				}
				return ReadAddressInfoAtOffset(Search_Mid.Offset);
			}

			private uint GetOffset()
			{
				return BitConverter.ToUInt32(new byte[4]
				{
					(byte)FileStrm.ReadByte(),
					(byte)FileStrm.ReadByte(),
					(byte)FileStrm.ReadByte(),
					0
				}, 0);
			}

			protected byte GetTag()
			{
				return (byte)FileStrm.ReadByte();
			}

			protected uint GetUInt32()
			{
				byte[] array = new byte[4];
				FileStrm.Read(array, 0, 4);
				return BitConverter.ToUInt32(array, 0);
			}

			protected CZ_INDEX_INFO IndexInfoAtPos(uint Index_Pos)
			{
				CZ_INDEX_INFO cZ_INDEX_INFO = new CZ_INDEX_INFO();
				FileStrm.Seek(Index_Set + 7 * Index_Pos, SeekOrigin.Begin);
				cZ_INDEX_INFO.IpSet = GetUInt32();
				cZ_INDEX_INFO.Offset = GetOffset();
				FileStrm.Seek(cZ_INDEX_INFO.Offset, SeekOrigin.Begin);
				cZ_INDEX_INFO.IpEnd = GetUInt32();
				return cZ_INDEX_INFO;
			}

			public void Initialize()
			{
				Search_Index_Set = 0u;
				Search_Index_End = Index_Count - 1;
			}

			public uint IPToUInt32(string IpValue)
			{
				string[] array = IpValue.Split('.');
				int upperBound = array.GetUpperBound(0);
				if (upperBound != 3)
				{
					array = new string[4];
					for (int i = 1; i <= 3 - upperBound; i++)
					{
						array[upperBound + i] = "0";
					}
				}
				byte[] array2 = new byte[4];
				for (int i = 0; i <= 3; i++)
				{
					if (IsNumeric(array[i]))
					{
						array2[3 - i] = (byte)(Convert.ToInt32(array[i]) & 0xFF);
					}
				}
				return BitConverter.ToUInt32(array2, 0);
			}

			protected bool IsNumeric(string str)
			{
				if (str != null)
				{
					return Regex.IsMatch(str, "^-?\\d+$");
				}
				return false;
			}

			private string ReadAddressInfoAtOffset(uint Offset)
			{
				string text = "";
				string text2 = "";
				uint num = 0u;
				byte b = 0;
				FileStrm.Seek(Offset + 4, SeekOrigin.Begin);
				switch (GetTag())
				{
				case 1:
					FileStrm.Seek(GetOffset(), SeekOrigin.Begin);
					if (GetTag() == 2)
					{
						num = GetOffset();
						text2 = ReadArea();
						FileStrm.Seek(num, SeekOrigin.Begin);
						text = ReadString();
					}
					else
					{
						FileStrm.Seek(-1L, SeekOrigin.Current);
						text = ReadString();
						text2 = ReadArea();
					}
					break;
				case 2:
					num = GetOffset();
					text2 = ReadArea();
					FileStrm.Seek(num, SeekOrigin.Begin);
					text = ReadString();
					break;
				default:
					FileStrm.Seek(-1L, SeekOrigin.Current);
					text = ReadString();
					text2 = ReadArea();
					break;
				}
				return text + " " + text2;
			}

			protected string ReadArea()
			{
				switch (GetTag())
				{
				case 1:
				case 2:
					FileStrm.Seek(GetOffset(), SeekOrigin.Begin);
					return ReadString();
				default:
					FileStrm.Seek(-1L, SeekOrigin.Current);
					return ReadString();
				}
			}

			protected string ReadString()
			{
				uint num = 0u;
				byte[] array = new byte[256];
				array[num] = (byte)FileStrm.ReadByte();
				while (array[num] != 0)
				{
					num++;
					array[num] = (byte)FileStrm.ReadByte();
				}
				return Encoding.Default.GetString(array).TrimEnd(default(char));
			}

			public bool SetDbFilePath(string dbFilePath)
			{
				if (dbFilePath == "")
				{
					return false;
				}
				try
				{
					FileStrm = new FileStream(dbFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				}
				catch
				{
					return false;
				}
				if (FileStrm.Length < 8)
				{
					FileStrm.Close();
					return false;
				}
				FileStrm.Seek(0L, SeekOrigin.Begin);
				Index_Set = GetUInt32();
				Index_End = GetUInt32();
				Index_Count = (Index_End - Index_Set) / 7u + 1;
				bFilePathInitialized = true;
				return true;
			}
		}

		private static bool fileIsExsit;

		private static string filePath;

		private static PHCZIP pcz;

		static IPQuery()
		{
			fileIsExsit = true;
			filePath = "";
			pcz = new PHCZIP();
			filePath = TextUtility.GetFullPath(Utility.GetIPDbFilePath);
			fileIsExsit = FileManager.Exists(filePath, FsoMethod.File);
			if (!fileIsExsit)
			{
				throw new FrameworkExcption("IPdataFileNotExists");
			}
			pcz.SetDbFilePath(filePath);
		}

		public static string GetAddressWithIP(string IPValue)
		{
			string addressWithIP = pcz.GetAddressWithIP(IPValue.Trim());
			if (fileIsExsit)
			{
				if (addressWithIP.IndexOf("IANA") >= 0)
				{
					return "";
				}
				return addressWithIP;
			}
			return null;
		}
	}
}
