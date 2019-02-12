namespace RestSharp
{
	public class SimpleFactory<T> : IHttpFactory where T : IHttp, new()
	{
		public IHttp Create()
		{
			return (IHttp)(object)new T();
		}
	}
}
