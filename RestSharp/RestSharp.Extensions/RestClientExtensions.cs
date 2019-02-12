namespace RestSharp.Extensions
{
	public static class RestClientExtensions
	{
		public static RestResponse<dynamic> ExecuteDynamic(this IRestClient client, IRestRequest request)
		{
			IRestResponse<object> restResponse = client.Execute<object>(request);
			RestResponse<object> restResponse2 = (RestResponse<object>)restResponse;
			object obj2 = restResponse2.Data = SimpleJson.DeserializeObject(restResponse.Content);
			return restResponse2;
		}
	}
}
