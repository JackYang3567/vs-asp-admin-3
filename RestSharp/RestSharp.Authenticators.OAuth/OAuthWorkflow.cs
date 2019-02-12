using RestSharp.Authenticators.OAuth.Extensions;
using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace RestSharp.Authenticators.OAuth
{
	internal class OAuthWorkflow
	{
		public virtual string Version
		{
			get;
			set;
		}

		public virtual string ConsumerKey
		{
			get;
			set;
		}

		public virtual string ConsumerSecret
		{
			get;
			set;
		}

		public virtual string Token
		{
			get;
			set;
		}

		public virtual string TokenSecret
		{
			get;
			set;
		}

		public virtual string CallbackUrl
		{
			get;
			set;
		}

		public virtual string Verifier
		{
			get;
			set;
		}

		public virtual string SessionHandle
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

		public virtual OAuthParameterHandling ParameterHandling
		{
			get;
			set;
		}

		public virtual string ClientUsername
		{
			get;
			set;
		}

		public virtual string ClientPassword
		{
			get;
			set;
		}

		public virtual string RequestTokenUrl
		{
			get;
			set;
		}

		public virtual string AccessTokenUrl
		{
			get;
			set;
		}

		public virtual string AuthorizationUrl
		{
			get;
			set;
		}

		public OAuthWebQueryInfo BuildRequestTokenInfo(string method)
		{
			return BuildRequestTokenInfo(method, null);
		}

		public virtual OAuthWebQueryInfo BuildRequestTokenInfo(string method, WebParameterCollection parameters)
		{
			ValidateTokenRequestState();
			if (parameters == null)
			{
				parameters = new WebParameterCollection();
			}
			string timestamp = OAuthTools.GetTimestamp();
			string nonce = OAuthTools.GetNonce();
			AddAuthParameters(parameters, timestamp, nonce);
			string signatureBase = OAuthTools.ConcatenateRequestElements(method, RequestTokenUrl, parameters);
			string signature = OAuthTools.GetSignature(SignatureMethod, SignatureTreatment, signatureBase, ConsumerSecret);
			OAuthWebQueryInfo oAuthWebQueryInfo = new OAuthWebQueryInfo();
			oAuthWebQueryInfo.WebMethod = method;
			oAuthWebQueryInfo.ParameterHandling = ParameterHandling;
			oAuthWebQueryInfo.ConsumerKey = ConsumerKey;
			oAuthWebQueryInfo.SignatureMethod = SignatureMethod.ToRequestValue();
			oAuthWebQueryInfo.SignatureTreatment = SignatureTreatment;
			oAuthWebQueryInfo.Signature = signature;
			oAuthWebQueryInfo.Timestamp = timestamp;
			oAuthWebQueryInfo.Nonce = nonce;
			oAuthWebQueryInfo.Version = (Version ?? "1.0");
			oAuthWebQueryInfo.Callback = OAuthTools.UrlEncodeRelaxed(CallbackUrl ?? "");
			oAuthWebQueryInfo.TokenSecret = TokenSecret;
			oAuthWebQueryInfo.ConsumerSecret = ConsumerSecret;
			return oAuthWebQueryInfo;
		}

		public virtual OAuthWebQueryInfo BuildAccessTokenInfo(string method)
		{
			return BuildAccessTokenInfo(method, null);
		}

		public virtual OAuthWebQueryInfo BuildAccessTokenInfo(string method, WebParameterCollection parameters)
		{
			ValidateAccessRequestState();
			if (parameters == null)
			{
				parameters = new WebParameterCollection();
			}
			Uri uri = new Uri(AccessTokenUrl);
			string timestamp = OAuthTools.GetTimestamp();
			string nonce = OAuthTools.GetNonce();
			AddAuthParameters(parameters, timestamp, nonce);
			string signatureBase = OAuthTools.ConcatenateRequestElements(method, uri.ToString(), parameters);
			string signature = OAuthTools.GetSignature(SignatureMethod, SignatureTreatment, signatureBase, ConsumerSecret, TokenSecret);
			OAuthWebQueryInfo oAuthWebQueryInfo = new OAuthWebQueryInfo();
			oAuthWebQueryInfo.WebMethod = method;
			oAuthWebQueryInfo.ParameterHandling = ParameterHandling;
			oAuthWebQueryInfo.ConsumerKey = ConsumerKey;
			oAuthWebQueryInfo.Token = Token;
			oAuthWebQueryInfo.SignatureMethod = SignatureMethod.ToRequestValue();
			oAuthWebQueryInfo.SignatureTreatment = SignatureTreatment;
			oAuthWebQueryInfo.Signature = signature;
			oAuthWebQueryInfo.Timestamp = timestamp;
			oAuthWebQueryInfo.Nonce = nonce;
			oAuthWebQueryInfo.Version = (Version ?? "1.0");
			oAuthWebQueryInfo.Verifier = Verifier;
			oAuthWebQueryInfo.Callback = CallbackUrl;
			oAuthWebQueryInfo.TokenSecret = TokenSecret;
			oAuthWebQueryInfo.ConsumerSecret = ConsumerSecret;
			return oAuthWebQueryInfo;
		}

		public virtual OAuthWebQueryInfo BuildClientAuthAccessTokenInfo(string method, WebParameterCollection parameters)
		{
			ValidateClientAuthAccessRequestState();
			if (parameters == null)
			{
				parameters = new WebParameterCollection();
			}
			Uri uri = new Uri(AccessTokenUrl);
			string timestamp = OAuthTools.GetTimestamp();
			string nonce = OAuthTools.GetNonce();
			AddXAuthParameters(parameters, timestamp, nonce);
			string signatureBase = OAuthTools.ConcatenateRequestElements(method, uri.ToString(), parameters);
			string signature = OAuthTools.GetSignature(SignatureMethod, SignatureTreatment, signatureBase, ConsumerSecret);
			OAuthWebQueryInfo oAuthWebQueryInfo = new OAuthWebQueryInfo();
			oAuthWebQueryInfo.WebMethod = method;
			oAuthWebQueryInfo.ParameterHandling = ParameterHandling;
			oAuthWebQueryInfo.ClientMode = "client_auth";
			oAuthWebQueryInfo.ClientUsername = ClientUsername;
			oAuthWebQueryInfo.ClientPassword = ClientPassword;
			oAuthWebQueryInfo.ConsumerKey = ConsumerKey;
			oAuthWebQueryInfo.SignatureMethod = SignatureMethod.ToRequestValue();
			oAuthWebQueryInfo.SignatureTreatment = SignatureTreatment;
			oAuthWebQueryInfo.Signature = signature;
			oAuthWebQueryInfo.Timestamp = timestamp;
			oAuthWebQueryInfo.Nonce = nonce;
			oAuthWebQueryInfo.Version = (Version ?? "1.0");
			oAuthWebQueryInfo.TokenSecret = TokenSecret;
			oAuthWebQueryInfo.ConsumerSecret = ConsumerSecret;
			return oAuthWebQueryInfo;
		}

		public virtual OAuthWebQueryInfo BuildProtectedResourceInfo(string method, WebParameterCollection parameters, string url)
		{
			ValidateProtectedResourceState();
			if (parameters == null)
			{
				parameters = new WebParameterCollection();
			}
			Uri uri = new Uri(url);
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uri.Query);
			string[] allKeys = nameValueCollection.AllKeys;
			foreach (string name in allKeys)
			{
				string text = method.ToUpperInvariant();
				if (text != null && text == "POST")
				{
					parameters.Add(new HttpPostParameter(name, nameValueCollection[name]));
				}
				else
				{
					parameters.Add(name, nameValueCollection[name]);
				}
			}
			string timestamp = OAuthTools.GetTimestamp();
			string nonce = OAuthTools.GetNonce();
			AddAuthParameters(parameters, timestamp, nonce);
			string signatureBase = OAuthTools.ConcatenateRequestElements(method, url, parameters);
			string signature = OAuthTools.GetSignature(SignatureMethod, SignatureTreatment, signatureBase, ConsumerSecret, TokenSecret);
			OAuthWebQueryInfo oAuthWebQueryInfo = new OAuthWebQueryInfo();
			oAuthWebQueryInfo.WebMethod = method;
			oAuthWebQueryInfo.ParameterHandling = ParameterHandling;
			oAuthWebQueryInfo.ConsumerKey = ConsumerKey;
			oAuthWebQueryInfo.Token = Token;
			oAuthWebQueryInfo.SignatureMethod = SignatureMethod.ToRequestValue();
			oAuthWebQueryInfo.SignatureTreatment = SignatureTreatment;
			oAuthWebQueryInfo.Signature = signature;
			oAuthWebQueryInfo.Timestamp = timestamp;
			oAuthWebQueryInfo.Nonce = nonce;
			oAuthWebQueryInfo.Version = (Version ?? "1.0");
			oAuthWebQueryInfo.Callback = CallbackUrl;
			oAuthWebQueryInfo.ConsumerSecret = ConsumerSecret;
			oAuthWebQueryInfo.TokenSecret = TokenSecret;
			return oAuthWebQueryInfo;
		}

		private void ValidateTokenRequestState()
		{
			if (RequestTokenUrl.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify a request token URL");
			}
			if (ConsumerKey.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify a consumer key");
			}
			if (ConsumerSecret.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify a consumer secret");
			}
		}

		private void ValidateAccessRequestState()
		{
			if (AccessTokenUrl.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify an access token URL");
			}
			if (ConsumerKey.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify a consumer key");
			}
			if (ConsumerSecret.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify a consumer secret");
			}
			if (Token.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify a token");
			}
		}

		private void ValidateClientAuthAccessRequestState()
		{
			if (AccessTokenUrl.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify an access token URL");
			}
			if (ConsumerKey.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify a consumer key");
			}
			if (ConsumerSecret.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify a consumer secret");
			}
			if (ClientUsername.IsNullOrBlank() || ClientPassword.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify user credentials");
			}
		}

		private void ValidateProtectedResourceState()
		{
			if (ConsumerKey.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify a consumer key");
			}
			if (ConsumerSecret.IsNullOrBlank())
			{
				throw new ArgumentException("You must specify a consumer secret");
			}
		}

		private void AddAuthParameters(ICollection<WebPair> parameters, string timestamp, string nonce)
		{
			WebParameterCollection webParameterCollection = new WebParameterCollection();
			webParameterCollection.Add(new WebPair("oauth_consumer_key", ConsumerKey));
			webParameterCollection.Add(new WebPair("oauth_nonce", nonce));
			webParameterCollection.Add(new WebPair("oauth_signature_method", SignatureMethod.ToRequestValue()));
			webParameterCollection.Add(new WebPair("oauth_timestamp", timestamp));
			webParameterCollection.Add(new WebPair("oauth_version", Version ?? "1.0"));
			WebParameterCollection webParameterCollection2 = webParameterCollection;
			if (!Token.IsNullOrBlank())
			{
				webParameterCollection2.Add(new WebPair("oauth_token", Token));
			}
			if (!CallbackUrl.IsNullOrBlank())
			{
				webParameterCollection2.Add(new WebPair("oauth_callback", CallbackUrl));
			}
			if (!Verifier.IsNullOrBlank())
			{
				webParameterCollection2.Add(new WebPair("oauth_verifier", Verifier));
			}
			if (!SessionHandle.IsNullOrBlank())
			{
				webParameterCollection2.Add(new WebPair("oauth_session_handle", SessionHandle));
			}
			foreach (WebPair item in webParameterCollection2)
			{
				parameters.Add(item);
			}
		}

		private void AddXAuthParameters(ICollection<WebPair> parameters, string timestamp, string nonce)
		{
			WebParameterCollection webParameterCollection = new WebParameterCollection();
			webParameterCollection.Add(new WebPair("x_auth_username", ClientUsername));
			webParameterCollection.Add(new WebPair("x_auth_password", ClientPassword));
			webParameterCollection.Add(new WebPair("x_auth_mode", "client_auth"));
			webParameterCollection.Add(new WebPair("oauth_consumer_key", ConsumerKey));
			webParameterCollection.Add(new WebPair("oauth_signature_method", SignatureMethod.ToRequestValue()));
			webParameterCollection.Add(new WebPair("oauth_timestamp", timestamp));
			webParameterCollection.Add(new WebPair("oauth_nonce", nonce));
			webParameterCollection.Add(new WebPair("oauth_version", Version ?? "1.0"));
			WebParameterCollection webParameterCollection2 = webParameterCollection;
			foreach (WebPair item in webParameterCollection2)
			{
				parameters.Add(item);
			}
		}
	}
}
