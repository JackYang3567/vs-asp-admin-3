using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;

namespace Game.Facade
{
	public class JsonHelper
	{
		public static string SerializeObject(object o)
		{
			IsoDateTimeConverter isoDateTimeConverter = new IsoDateTimeConverter();
			isoDateTimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
			return JsonConvert.SerializeObject(o, Formatting.Indented, isoDateTimeConverter);
		}

		public static dynamic DeserializeObject(string json)
		{
			return JsonConvert.DeserializeObject<object>(json);
		}

		public static T DeserializeJsonToObject<T>(string json) where T : class
		{
			JsonSerializer jsonSerializer = new JsonSerializer();
			StringReader reader = new StringReader(json);
			object obj = jsonSerializer.Deserialize(new JsonTextReader(reader), typeof(T));
			return obj as T;
		}

		public static List<T> DeserializeJsonToList<T>(string json) where T : class
		{
			JsonSerializer jsonSerializer = new JsonSerializer();
			StringReader reader = new StringReader(json);
			object obj = jsonSerializer.Deserialize(new JsonTextReader(reader), typeof(List<T>));
			return obj as List<T>;
		}

		public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
		{
			return JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
		}
	}
}
