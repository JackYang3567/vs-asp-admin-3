namespace RestSharp.Extensions
{
	public static class ResponseExtensions
	{
		public static IRestResponse<T> ToAsyncResponse<T>(this IRestResponse response)
		{
			RestResponse<T> restResponse = new RestResponse<T>();
			restResponse.ContentEncoding = response.ContentEncoding;
			restResponse.ContentLength = response.ContentLength;
			restResponse.ContentType = response.ContentType;
			restResponse.Cookies = response.Cookies;
			restResponse.ErrorException = response.ErrorException;
			restResponse.ErrorMessage = response.ErrorMessage;
			restResponse.Headers = response.Headers;
			restResponse.RawBytes = response.RawBytes;
			restResponse.ResponseStatus = response.ResponseStatus;
			restResponse.ResponseUri = response.ResponseUri;
			restResponse.Server = response.Server;
			restResponse.StatusCode = response.StatusCode;
			restResponse.StatusDescription = response.StatusDescription;
			return restResponse;
		}
	}
}
