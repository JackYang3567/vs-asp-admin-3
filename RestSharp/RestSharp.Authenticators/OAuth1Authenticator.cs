using RestSharp.Authenticators.OAuth;
using RestSharp.Authenticators.OAuth.Extensions;
using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestSharp.Authenticators
{
	public class OAuth1Authenticator : IAuthenticator
	{
		public virtual string Realm
		{
			get;
			set;
		}

		public virtual OAuthParameterHandling ParameterHandling
		{
			get;
			set;
		}

		public virtual OAuthSignatureMethod SignatureMethod
		{
			get;
			set;
		}

		public virtual OAuthSignatureTreatment SignatureTreatment
		{
			get;
			set;
		}

		internal virtual OAuthType Type
		{
			get;
			set;
		}

		internal virtual string ConsumerKey
		{
			get;
			set;
		}

		internal virtual string ConsumerSecret
		{
			get;
			set;
		}

		internal virtual string Token
		{
			get;
			set;
		}

		internal virtual string TokenSecret
		{
			get;
			set;
		}

		internal virtual string Verifier
		{
			get;
			set;
		}

		internal virtual string Version
		{
			get;
			set;
		}

		internal virtual string CallbackUrl
		{
			get;
			set;
		}

		internal virtual string SessionHandle
		{
			get;
			set;
		}

		internal virtual string ClientUsername
		{
			get;
			set;
		}

		internal virtual string ClientPassword
		{
			get;
			set;
		}

		public static OAuth1Authenticator ForRequestToken(string consumerKey, string consumerSecret)
		{
			OAuth1Authenticator oAuth1Authenticator = new OAuth1Authenticator();
			oAuth1Authenticator.ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader;
			oAuth1Authenticator.SignatureMethod = OAuthSignatureMethod.HmacSha1;
			oAuth1Authenticator.SignatureTreatment = OAuthSignatureTreatment.Escaped;
			oAuth1Authenticator.ConsumerKey = consumerKey;
			oAuth1Authenticator.ConsumerSecret = consumerSecret;
			oAuth1Authenticator.Type = OAuthType.RequestToken;
			return oAuth1Authenticator;
		}

		public static OAuth1Authenticator ForRequestToken(string consumerKey, string consumerSecret, string callbackUrl)
		{
			OAuth1Authenticator oAuth1Authenticator = ForRequestToken(consumerKey, consumerSecret);
			oAuth1Authenticator.CallbackUrl = callbackUrl;
			return oAuth1Authenticator;
		}

		public static OAuth1Authenticator ForAccessToken(string consumerKey, string consumerSecret, string token, string tokenSecret)
		{
			OAuth1Authenticator oAuth1Authenticator = new OAuth1Authenticator();
			oAuth1Authenticator.ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader;
			oAuth1Authenticator.SignatureMethod = OAuthSignatureMethod.HmacSha1;
			oAuth1Authenticator.SignatureTreatment = OAuthSignatureTreatment.Escaped;
			oAuth1Authenticator.ConsumerKey = consumerKey;
			oAuth1Authenticator.ConsumerSecret = consumerSecret;
			oAuth1Authenticator.Token = token;
			oAuth1Authenticator.TokenSecret = tokenSecret;
			oAuth1Authenticator.Type = OAuthType.AccessToken;
			return oAuth1Authenticator;
		}

		public static OAuth1Authenticator ForAccessToken(string consumerKey, string consumerSecret, string token, string tokenSecret, string verifier)
		{
			OAuth1Authenticator oAuth1Authenticator = ForAccessToken(consumerKey, consumerSecret, token, tokenSecret);
			oAuth1Authenticator.Verifier = verifier;
			return oAuth1Authenticator;
		}

		public static OAuth1Authenticator ForAccessTokenRefresh(string consumerKey, string consumerSecret, string token, string tokenSecret, string sessionHandle)
		{
			OAuth1Authenticator oAuth1Authenticator = ForAccessToken(consumerKey, consumerSecret, token, tokenSecret);
			oAuth1Authenticator.SessionHandle = sessionHandle;
			return oAuth1Authenticator;
		}

		public static OAuth1Authenticator ForAccessTokenRefresh(string consumerKey, string consumerSecret, string token, string tokenSecret, string verifier, string sessionHandle)
		{
			OAuth1Authenticator oAuth1Authenticator = ForAccessToken(consumerKey, consumerSecret, token, tokenSecret);
			oAuth1Authenticator.SessionHandle = sessionHandle;
			oAuth1Authenticator.Verifier = verifier;
			return oAuth1Authenticator;
		}

		public static OAuth1Authenticator ForClientAuthentication(string consumerKey, string consumerSecret, string username, string password)
		{
			OAuth1Authenticator oAuth1Authenticator = new OAuth1Authenticator();
			oAuth1Authenticator.ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader;
			oAuth1Authenticator.SignatureMethod = OAuthSignatureMethod.HmacSha1;
			oAuth1Authenticator.SignatureTreatment = OAuthSignatureTreatment.Escaped;
			oAuth1Authenticator.ConsumerKey = consumerKey;
			oAuth1Authenticator.ConsumerSecret = consumerSecret;
			oAuth1Authenticator.ClientUsername = username;
			oAuth1Authenticator.ClientPassword = password;
			oAuth1Authenticator.Type = OAuthType.ClientAuthentication;
			return oAuth1Authenticator;
		}

		public static OAuth1Authenticator ForProtectedResource(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
		{
			OAuth1Authenticator oAuth1Authenticator = new OAuth1Authenticator();
			oAuth1Authenticator.Type = OAuthType.ProtectedResource;
			oAuth1Authenticator.ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader;
			oAuth1Authenticator.SignatureMethod = OAuthSignatureMethod.HmacSha1;
			oAuth1Authenticator.SignatureTreatment = OAuthSignatureTreatment.Escaped;
			oAuth1Authenticator.ConsumerKey = consumerKey;
			oAuth1Authenticator.ConsumerSecret = consumerSecret;
			oAuth1Authenticator.Token = accessToken;
			oAuth1Authenticator.TokenSecret = accessTokenSecret;
			return oAuth1Authenticator;
		}

		public void Authenticate(IRestClient client, IRestRequest request)
		{
			OAuthWorkflow oAuthWorkflow = new OAuthWorkflow();
			oAuthWorkflow.ConsumerKey = ConsumerKey;
			oAuthWorkflow.ConsumerSecret = ConsumerSecret;
			oAuthWorkflow.ParameterHandling = ParameterHandling;
			oAuthWorkflow.SignatureMethod = SignatureMethod;
			oAuthWorkflow.SignatureTreatment = SignatureTreatment;
			oAuthWorkflow.Verifier = Verifier;
			oAuthWorkflow.Version = Version;
			oAuthWorkflow.CallbackUrl = CallbackUrl;
			oAuthWorkflow.SessionHandle = SessionHandle;
			oAuthWorkflow.Token = Token;
			oAuthWorkflow.TokenSecret = TokenSecret;
			oAuthWorkflow.ClientUsername = ClientUsername;
			oAuthWorkflow.ClientPassword = ClientPassword;
			OAuthWorkflow workflow = oAuthWorkflow;
			AddOAuthData(client, request, workflow);
		}

		private void AddOAuthData(IRestClient client, IRestRequest request, OAuthWorkflow workflow)
		{
			string text = client.BuildUri(request).ToString();
			int num = text.IndexOf('?');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			string method = request.Method.ToString().ToUpperInvariant();
			WebParameterCollection webParameterCollection = new WebParameterCollection();
			if (!request.AlwaysMultipartFormData && !request.Files.Any())
			{
				foreach (Parameter item in from p in client.DefaultParameters
				where p.Type == ParameterType.GetOrPost || p.Type == ParameterType.QueryString
				select p)
				{
					webParameterCollection.Add(new WebPair(item.Name, item.Value.ToString()));
				}
				foreach (Parameter item2 in from p in request.Parameters
				where p.Type == ParameterType.GetOrPost || p.Type == ParameterType.QueryString
				select p)
				{
					webParameterCollection.Add(new WebPair(item2.Name, item2.Value.ToString()));
				}
			}
			else
			{
				foreach (Parameter item3 in from p in client.DefaultParameters
				where (p.Type == ParameterType.GetOrPost || p.Type == ParameterType.QueryString) && p.Name.StartsWith("oauth_")
				select p)
				{
					webParameterCollection.Add(new WebPair(item3.Name, item3.Value.ToString()));
				}
				foreach (Parameter item4 in from p in request.Parameters
				where (p.Type == ParameterType.GetOrPost || p.Type == ParameterType.QueryString) && p.Name.StartsWith("oauth_")
				select p)
				{
					webParameterCollection.Add(new WebPair(item4.Name, item4.Value.ToString()));
				}
			}
			OAuthWebQueryInfo oAuthWebQueryInfo;
			switch (Type)
			{
			case OAuthType.RequestToken:
				workflow.RequestTokenUrl = text;
				oAuthWebQueryInfo = workflow.BuildRequestTokenInfo(method, webParameterCollection);
				break;
			case OAuthType.AccessToken:
				workflow.AccessTokenUrl = text;
				oAuthWebQueryInfo = workflow.BuildAccessTokenInfo(method, webParameterCollection);
				break;
			case OAuthType.ClientAuthentication:
				workflow.AccessTokenUrl = text;
				oAuthWebQueryInfo = workflow.BuildClientAuthAccessTokenInfo(method, webParameterCollection);
				break;
			case OAuthType.ProtectedResource:
				oAuthWebQueryInfo = workflow.BuildProtectedResourceInfo(method, webParameterCollection, text);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			switch (ParameterHandling)
			{
			case OAuthParameterHandling.HttpAuthorizationHeader:
				webParameterCollection.Add("oauth_signature", oAuthWebQueryInfo.Signature);
				request.AddHeader("Authorization", GetAuthorizationHeader(webParameterCollection));
				break;
			case OAuthParameterHandling.UrlOrPostParameters:
				webParameterCollection.Add("oauth_signature", oAuthWebQueryInfo.Signature);
				foreach (WebPair item5 in from parameter in webParameterCollection
				where !parameter.Name.IsNullOrBlank() && (parameter.Name.StartsWith("oauth_") || parameter.Name.StartsWith("x_auth_"))
				select parameter)
				{
					request.AddParameter(item5.Name, HttpUtility.UrlDecode(item5.Value));
				}
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private string GetAuthorizationHeader(WebPairCollection parameters)
		{
			StringBuilder stringBuilder = new StringBuilder("OAuth ");
			if (!Realm.IsNullOrBlank())
			{
				stringBuilder.Append("realm=\"{0}\",".FormatWith(OAuthTools.UrlEncodeRelaxed(Realm)));
			}
			parameters.Sort((WebPair l, WebPair r) => l.Name.CompareTo(r.Name));
			int num = 0;
			List<WebPair> list = (from parameter in parameters
			where !parameter.Name.IsNullOrBlank() && !parameter.Value.IsNullOrBlank() && (parameter.Name.StartsWith("oauth_") || parameter.Name.StartsWith("x_auth_"))
			select parameter).ToList();
			foreach (WebPair item in list)
			{
				num++;
				string format = (num < list.Count) ? "{0}=\"{1}\"," : "{0}=\"{1}\"";
				stringBuilder.Append(format.FormatWith(item.Name, item.Value));
			}
			return stringBuilder.ToString();
		}
	}
}
