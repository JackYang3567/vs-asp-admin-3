namespace RestSharp
{
	public class RestResponse<T> : RestResponseBase, IRestResponse<T>, IRestResponse
	{
		public T Data
		{
			get;
			set;
		}

		public static explicit operator RestResponse<T>(RestResponse response)
		{
			RestResponse<T> restResponse = new RestResponse<T>();
			restResponse.ContentEncoding = response.ContentEncoding;
			restResponse.ContentLength = response.ContentLength;
			restResponse.ContentType = response.ContentType;
			restResponse.Cookies = response.Cookies;
			restResponse.ErrorMessage = response.ErrorMessage;
			restResponse.ErrorException = response.ErrorException;
			restResponse.Headers = response.Headers;
			restResponse.RawBytes = response.RawBytes;
			restResponse.ResponseStatus = response.ResponseStatus;
			restResponse.ResponseUri = response.ResponseUri;
			restResponse.Server = response.Server;
			restResponse.StatusCode = response.StatusCode;
			restResponse.StatusDescription = response.StatusDescription;
			restResponse.Request = response.Request;
			return restResponse;
		}
	}
	public class RestResponse : RestResponseBase, IRestResponse
	{
	}
}
