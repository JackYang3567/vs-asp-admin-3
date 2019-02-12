using Game.Entity;
using Game.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace Game.Facade
{
	public class DaiFu
	{
		public static string query(string merchant_order, string method)
		{
			string text = "";
			RestRequest restRequest = new RestRequest("/remit/query/", (method == "POST") ? Method.POST : Method.GET);
			restRequest.AddParameter("merchant_order", merchant_order);
			restRequest.AddParameter("input_charset", "UTF-8");
			restRequest.AddParameter("merchant_code", SysConfig.code);
			List<Parameter> list = (from p in restRequest.Parameters
			orderby p.Name
			select p).ToList();
			list.Add(new Parameter
			{
				Name = "key",
				Value = SysConfig.key
			});
			foreach (Parameter item in list)
			{
				object obj = text;
				text = obj + item.Name + "=" + item.Value + "&";
			}
			restRequest.AddParameter("sign", GetMd5Hash(text.Substring(0, text.Length - 1)));
			RestResponse restResponse = SendRequest(restRequest);
			return restResponse.Content;
		}

		public static string remit(string merchant_order, decimal amount, string bank_account, string bank_card_no, string bank_code, string method)
		{
			string text = "";
			RestRequest restRequest = new RestRequest("/remit/", (method == "POST") ? Method.POST : Method.GET);
			restRequest.AddParameter("merchant_order", merchant_order);
			restRequest.AddParameter("amount", amount);
			restRequest.AddParameter("bank_account", bank_account);
			restRequest.AddParameter("bank_card_no", bank_card_no);
			restRequest.AddParameter("bank_code", bank_code);
			restRequest.AddParameter("input_charset", "UTF-8");
			restRequest.AddParameter("merchant_code", SysConfig.code);
			List<Parameter> list = (from p in restRequest.Parameters
			orderby p.Name
			select p).ToList();
			list.Add(new Parameter
			{
				Name = "key",
				Value = SysConfig.key
			});
			foreach (Parameter item in list)
			{
				object obj = text;
				text = obj + item.Name + "=" + item.Value + "&";
			}
			restRequest.AddParameter("sign", GetMd5Hash(text.Substring(0, text.Length - 1)));
			RestResponse restResponse = SendRequest(restRequest);
			return restResponse.Content;
		}

		public static string GateWayPement(string merchant_order, decimal amount, string bank_account, string bank_card_no, string bank_code, string bankAddress, out string flowid)
		{
			string url = ApplicationSettings.Get("url_san") + "/Pay/GateWayPement.aspx";
			string value = ApplicationSettings.Get("partner_san");
			string password = ApplicationSettings.Get("key_san");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["mer_id"] = value;
			dictionary["pay_type"] = "1";
			dictionary["order_id"] = merchant_order;
			dictionary["order_amt"] = amount.ToString();
			dictionary["acct_name"] = bank_account;
			dictionary["acct_id"] = bank_card_no;
			dictionary["time_stamp"] = DateTime.Now.ToString("yyyyMMddHHmmss");
			string text = "";
			foreach (KeyValuePair<string, string> item in dictionary)
			{
				string text2 = text;
				text = text2 + item.Key + "=" + item.Value + "&";
			}
			password = TextEncrypt.EncryptPassword(password).ToLower();
			string value2 = Jiami.MD5(text + "key=" + password, "UTF-8").ToLower();
			dictionary["acct_name"] = HttpUtility.UrlEncode(bank_account, Encoding.UTF8);
			dictionary["acct_type"] = "0";
			dictionary["bank_code"] = bank_code;
			dictionary["bank_branch"] = HttpUtility.UrlEncode(bankAddress, Encoding.UTF8);
			dictionary["sign"] = value2;
			string param = HttpHelper.GetParam(dictionary);
			string json = HttpHelper.HttpRequest(url, param);
			Dictionary<string, string> dictionary2 = JsonHelper.DeserializeJsonToObject<Dictionary<string, string>>(json);
			flowid = "";
			if (dictionary2.ContainsKey("status_code"))
			{
				if (dictionary2["status_code"] == "0")
				{
					flowid = dictionary2["pay_seq"].ToString();
					return "SUCCESS";
				}
				return dictionary2["status_msg"].ToString();
			}
			return "下单失败";
		}

		public static string Daifu_45(string merchant_order, decimal amount, string bank_account, string bank_card_no, string bank_code, string bankAddress, string city, out string flowid)
		{
			string url = ApplicationSettings.Get("url_45");
			string value = ApplicationSettings.Get("partner_45");
			string str = ApplicationSettings.Get("key_45");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["UserID"] = value;
			dictionary["AccountNo"] = bank_card_no;
			dictionary["AccountName"] = bank_account;
			dictionary["BankName"] = bank_code;
			dictionary["OrderID"] = merchant_order;
			dictionary["Amount"] = (amount * 100m).ToString();
			dictionary["AccountType"] = "0";
			dictionary["BankNo"] = "";
			dictionary["IDType"] = "";
			dictionary["IDNumber"] = "";
			dictionary["MobileNo"] = "";
			dictionary["Province"] = "";
			dictionary["City"] = city;
			dictionary["SubbranchName"] = bankAddress;
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			dictionary2["ActionType"] = "SinglePay";
			dictionary2["Data"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonHelper.SerializeObject(dictionary)));
			dictionary2["Sign"] = TextEncrypt.EncryptPassword(dictionary2["Data"] + str).ToLower();
			string param = JsonHelper.SerializeObject(dictionary2);
			string text = HttpHelper.HttpRequest(url, param);
			flowid = "";
			if (text.Contains("Status"))
			{
				Dictionary<string, string> dictionary3 = JsonHelper.DeserializeJsonToObject<Dictionary<string, string>>(text);
				if (dictionary3.ContainsKey("Status"))
				{
					if (dictionary3["Status"] == "OK")
					{
						flowid = dictionary3["OrderID"].ToString();
						return "SUCCESS";
					}
					return dictionary3["StatusMsg"].ToString();
				}
			}
			return text;
		}

		public static string Daifu_youmifu(string order_no, decimal amount, string full_name, string bank_card_no, string bank_code, string bankAddress, string province, string city, string host, out string flowid)
		{
			switch (bank_code)
			{
			case "CMBCHINA":
				bank_code = "CMB";
				break;
			case "BOCOM":
				bank_code = "COMM";
				break;
			case "ECITIC":
				bank_code = "CNCB";
				break;
			case "PINGAN":
				bank_code = "PAB";
				break;
			case "CGB":
				bank_code = "GDB";
				break;
			}
			string url = ApplicationSettings.Get("url_ymf");
			string value = ApplicationSettings.Get("parter_ymf");
			string key = ApplicationSettings.Get("key_ymf");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["apiName"] = "SINGLE_ENTRUST_SETT";
			dictionary["apiVersion"] = "1.0.0.0";
			dictionary["platformID"] = value;
			dictionary["merchNo"] = value;
			dictionary["orderNo"] = order_no;
			dictionary["tradeDate"] = DateTime.Now.ToString("yyyyMMdd");
			dictionary["merchUrl"] = "http://" + host + "/handler.aspx";
			dictionary["merchParam"] = TextUtility.CreateAuthStr(20, false);
			dictionary["bankAccNo"] = bank_card_no.Trim();
			dictionary["bankAccName"] = full_name.Trim();
			dictionary["bankCode"] = bank_code.Trim();
			dictionary["bankName"] = bankAddress.Trim();
			if (province.Trim() != "")
			{
				dictionary["province"] = province.Trim();
			}
			if (city.Trim() != "")
			{
				dictionary["city"] = city.Trim();
			}
			dictionary["Amt"] = amount.ToString("#0.00");
			dictionary["tradeSummary"] = "shop";
			string sourceData = PayHelper.PrepareSign(dictionary);
			string text2 = dictionary["signMsg"] = Jiami.sign(sourceData, key);
			string param = HttpHelper.GetParam(dictionary);
			string text3 = HttpHelper.HttpRequest(url, param);
			flowid = "";
			if (text3.Contains("respCode"))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(text3);
				string innerText = xmlDocument.SelectSingleNode("/moboAccount/respData/respCode").InnerText;
				if (innerText == "00")
				{
					string text4 = flowid = xmlDocument.SelectSingleNode("/moboAccount/respData/batchNo").InnerText;
					return "SUCCESS";
				}
				return xmlDocument.SelectSingleNode("/moboAccount/respData/respDesc").InnerText;
			}
			return text3;
		}

		public static string Daifu_ruyi(string order_no, decimal amount, string full_name, string bank_card_no, string bank_code, string bankAddress, string province, string city, string host, out string flowid)
		{
			switch (bank_code)
			{
			case "ICBC":
				bank_code = "1";
				break;
			case "ABC":
				bank_code = "3";
				break;
			case "BOC":
				bank_code = "5";
				break;
			case "CCB":
				bank_code = "2";
				break;
			case "BOCOM":
				bank_code = "6";
				break;
			case "CMBCHINA":
				bank_code = "7";
				break;
			case "CGB":
				bank_code = "11";
				break;
			case "ECITIC":
				bank_code = "12";
				break;
			case "CMBC":
				bank_code = "14";
				break;
			case "CEB":
				bank_code = "8";
				break;
			case "PINGAN":
				bank_code = "18";
				break;
			case "SPDB":
				bank_code = "16";
				break;
			case "PSBC":
				bank_code = "4";
				break;
			case "HXB":
				bank_code = "10";
				break;
			case "CIB":
				bank_code = "13";
				break;
			}
			string url = ApplicationSettings.Get("url_ruyi");
			string value = ApplicationSettings.Get("parter_ruyi");
			string value2 = ApplicationSettings.Get("key_ruyi");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["version"] = "2";
			dictionary["agent_id"] = value;
			dictionary["batch_no"] = DateTime.Now.ToString("yyyyMMddHHmmssfff");
			dictionary["batch_amt"] = amount.ToString("#0.00");
			dictionary["batch_num"] = "1";
			dictionary["detail_data"] = string.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}", order_no, bank_code, 0, bank_card_no.Trim(), full_name.Trim(), amount.ToString("#0.00"), "测试", province.Trim(), city.Trim(), bankAddress.Trim());
			dictionary["notify_url"] = "http://" + host + "/handler.aspx";
			dictionary["ext_param1"] = TextUtility.CreateAuthStr(20, false).ToLower();
			dictionary["key"] = value2;
			dictionary = (from p in dictionary
			orderby p.Key
			select p).ToDictionary((KeyValuePair<string, string> p) => p.Key, (KeyValuePair<string, string> o) => o.Value);
			string password = PayHelper.PrepareSign(dictionary);
			string text2 = dictionary["sign"] = TextEncrypt.EncryptPassword(password).ToLower();
			dictionary.Remove("key");
			string param = HttpHelper.GetParam(dictionary);
			string text3 = HttpHelper.HttpRequest(url, param, "post", "GB2312");
			flowid = "";
			if (text3.Contains("ret_code"))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(text3);
				string innerText = xmlDocument.SelectSingleNode("/root/ret_code").InnerText;
				if (innerText == "0000")
				{
					flowid = dictionary["batch_no"];
					return "SUCCESS";
				}
				return xmlDocument.SelectSingleNode("/root/ret_msg").InnerText;
			}
			return text3;
		}

		public static string PementQuery(string merchant_order)
		{
			string url = ApplicationSettings.Get("url_san") + "/Pay/PementQuery.aspx";
			string value = ApplicationSettings.Get("partner_san");
			string password = ApplicationSettings.Get("key_san");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["mer_id"] = value;
			dictionary["order_id"] = merchant_order;
			dictionary["time_stamp"] = DateTime.Now.ToString("yyyyMMddHHmmss");
			string text = "";
			foreach (KeyValuePair<string, string> item in dictionary)
			{
				string text2 = text;
				text = text2 + item.Key + "=" + item.Value + "&";
			}
			password = TextEncrypt.EncryptPassword(password).ToLower();
			string text4 = dictionary["sign"] = TextEncrypt.EncryptPassword(text + "key=" + password).ToLower();
			string param = HttpHelper.GetParam(dictionary);
			string json = HttpHelper.HttpRequest(url, param);
			Dictionary<string, string> dictionary2 = JsonHelper.DeserializeJsonToObject<Dictionary<string, string>>(json);
			if (dictionary2.ContainsKey("status_code"))
			{
				if (dictionary2["status_code"] == "0")
				{
					return "SUCCESS";
				}
				return dictionary2["status_msg"].ToString();
			}
			return "查询订单失败";
		}

		public static string Query_youmifu(string merchant_order)
		{
			string url = ApplicationSettings.Get("url_ymf");
			string value = ApplicationSettings.Get("parter_ymf");
			string key = ApplicationSettings.Get("key_ymf");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["apiName"] = "SINGLE_SETT_QUERY";
			dictionary["apiVersion"] = "1.0.0.0";
			dictionary["platformID"] = value;
			dictionary["merchNo"] = value;
			dictionary["orderNo"] = merchant_order;
			dictionary["tradeDate"] = DateTime.Now.ToString("yyyyMMdd");
			string sourceData = PayHelper.PrepareSign(dictionary);
			string text2 = dictionary["signMsg"] = Jiami.sign(sourceData, key);
			string param = HttpHelper.GetParam(dictionary);
			string text3 = HttpHelper.HttpRequest(url, param);
			if (text3.Contains("respCode"))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(text3);
				string innerText = xmlDocument.SelectSingleNode("/moboAccount/respData/respCode").InnerText;
				string innerText2 = xmlDocument.SelectSingleNode("/moboAccount/respData/Status").InnerText;
				if (innerText == "00" && innerText2 != "2")
				{
					return "SUCCESS";
				}
				return xmlDocument.SelectSingleNode("/moboAccount/respData/respDesc").InnerText;
			}
			return "查询订单失败";
		}

		public static RestResponse SendRequest(RestRequest request)
		{
			request.AddHeader("Accept", "*/*");
			RestClient restClient = new RestClient(ApplicationSettings.Get("apiurl_41"));
			return (RestResponse)restClient.Execute(request);
		}

		public static string GetMd5Hash(string input)
		{
			using (MD5 mD = MD5.Create())
			{
				byte[] array = mD.ComputeHash(Encoding.UTF8.GetBytes(input));
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i].ToString("x2"));
				}
				return stringBuilder.ToString();
			}
		}
	}
}
