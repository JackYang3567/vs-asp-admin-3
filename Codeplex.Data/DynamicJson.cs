using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Codeplex.Data
{
    public class DynamicJson : DynamicObject
    {
        private enum JsonType
        {
            @string,
            number,
            boolean,
            @object,
            array,
            @null
        }

        private readonly XElement xml;

        private readonly JsonType jsonType;

        public bool IsObject
        {
            get
            {
                return jsonType == JsonType.@object;
            }
        }

        public bool IsArray
        {
            get
            {
                return jsonType == JsonType.array;
            }
        }

        public static dynamic Parse(string json)
        {
            return Parse(json, Encoding.Unicode);
        }

        public static dynamic Parse(string json, Encoding encoding)
        {
            using (XmlDictionaryReader reader = JsonReaderWriterFactory.CreateJsonReader(encoding.GetBytes(json), XmlDictionaryReaderQuotas.Max))
            {
                return ToValue(XElement.Load(reader));
            }
        }

        public static dynamic Parse(Stream stream)
        {
            using (XmlDictionaryReader reader = JsonReaderWriterFactory.CreateJsonReader(stream, XmlDictionaryReaderQuotas.Max))
            {
                return ToValue(XElement.Load(reader));
            }
        }

        public static dynamic Parse(Stream stream, Encoding encoding)
        {
            using (XmlDictionaryReader reader = JsonReaderWriterFactory.CreateJsonReader(stream, encoding, XmlDictionaryReaderQuotas.Max, delegate
            {
            }))
            {
                return ToValue(XElement.Load(reader));
            }
        }

        public static string Serialize(object obj)
        {
            return CreateJsonString(new XStreamingElement("root", CreateTypeAttr(GetJsonType(obj)), CreateJsonNode(obj)));
        }

        private static dynamic ToValue(XElement element)
        {
            JsonType jsonType = (JsonType)Enum.Parse(typeof(JsonType), element.Attribute("type").Value);
            switch (jsonType)
            {
                case JsonType.boolean:
                    return (bool)element;
                case JsonType.number:
                    return (double)element;
                case JsonType.@string:
                    return (string)element;
                case JsonType.@object:
                case JsonType.array:
                    return new DynamicJson(element, jsonType);
                default:
                    return null;
            }
        }

        private static JsonType GetJsonType(object obj)
        {
            if (obj != null)
            {
                switch (Type.GetTypeCode(obj.GetType()))
                {
                    case TypeCode.Boolean:
                        return JsonType.boolean;
                    case TypeCode.Char:
                    case TypeCode.DateTime:
                    case TypeCode.String:
                        return JsonType.@string;
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        return JsonType.number;
                    case TypeCode.Object:
                        if (!(obj is IEnumerable))
                        {
                            return JsonType.@object;
                        }
                        return JsonType.array;
                    default:
                        return JsonType.@null;
                }
            }
            return JsonType.@null;
        }

        private static XAttribute CreateTypeAttr(JsonType type)
        {
            return new XAttribute("type", type.ToString());
        }

        private static object CreateJsonNode(object obj)
        {
            switch (GetJsonType(obj))
            {
                case JsonType.@string:
                case JsonType.number:
                    return obj;
                case JsonType.boolean:
                    return obj.ToString().ToLower();
                case JsonType.@object:
                    return CreateXObject(obj);
                case JsonType.array:
                    return CreateXArray(obj as IEnumerable);
                default:
                    return null;
            }
        }

        private static IEnumerable<XStreamingElement> CreateXArray<T>(T obj) where T : IEnumerable
        {
            return from object o in (IEnumerable)(object)obj
                   select new XStreamingElement("item", CreateTypeAttr(GetJsonType(o)), CreateJsonNode(o));
        }

        private static IEnumerable<XStreamingElement> CreateXObject(object obj)
        {
            return from pi in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                   select new
                   {
                       Name = pi.Name,
                       Value = pi.GetValue(obj, null)
                   } into a
                   select new XStreamingElement(a.Name, CreateTypeAttr(GetJsonType(a.Value)), CreateJsonNode(a.Value));
        }

        private static string CreateJsonString(XStreamingElement element)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlDictionaryWriter xmlDictionaryWriter = JsonReaderWriterFactory.CreateJsonWriter(memoryStream, Encoding.Unicode))
                {
                    element.WriteTo(xmlDictionaryWriter);
                    xmlDictionaryWriter.Flush();
                    return Encoding.Unicode.GetString(memoryStream.ToArray());
                }
            }
        }

        public DynamicJson()
        {
            xml = new XElement("root", CreateTypeAttr(JsonType.@object));
            jsonType = JsonType.@object;
        }

        private DynamicJson(XElement element, JsonType type)
        {
            xml = element;
            jsonType = type;
        }

        public bool IsDefined(string name)
        {
            if (IsObject)
            {
                return xml.Element(name) != null;
            }
            return false;
        }

        public bool IsDefined(int index)
        {
            if (IsArray)
            {
                return xml.Elements().ElementAtOrDefault(index) != null;
            }
            return false;
        }

        public bool Delete(string name)
        {
            XElement xElement = xml.Element(name);
            if (xElement != null)
            {
                xElement.Remove();
                return true;
            }
            return false;
        }

        public bool Delete(int index)
        {
            XElement xElement = xml.Elements().ElementAtOrDefault(index);
            if (xElement != null)
            {
                xElement.Remove();
                return true;
            }
            return false;
        }

        public T Deserialize<T>()
        {
            return (T)Deserialize(typeof(T));
        }

        private object Deserialize(Type type)
        {
            if (!IsArray)
            {
                return DeserializeObject(type);
            }
            return DeserializeArray(type);
        }

        private dynamic DeserializeValue(XElement element, Type elementType)
        {
            dynamic val = ToValue(element);
            if (val is DynamicJson)
            {
                val = ((DynamicJson)val).Deserialize(elementType);
            }
            return Convert.ChangeType(val, elementType);
        }

        private object DeserializeObject(Type targetType)
        {
            object obj = Activator.CreateInstance(targetType);
            Dictionary<string, PropertyInfo> dictionary = (from p in targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                           where p.CanWrite
                                                           select p).ToDictionary((PropertyInfo pi) => pi.Name, (PropertyInfo pi) => pi);
            foreach (XElement item in xml.Elements())
            {
                PropertyInfo value;
                if (dictionary.TryGetValue(item.Name.LocalName, out value))
                {
                    dynamic val = DeserializeValue(item, value.PropertyType);
                    value.SetValue(obj, val, null);
                }
            }
            return obj;
        }

        private object DeserializeArray(Type targetType)
        {
            if (targetType.IsArray)
            {
                Type elementType = targetType.GetElementType();
                dynamic val = Array.CreateInstance(elementType, xml.Elements().Count());
                int num = 0;
                {
                    foreach (XElement item in xml.Elements())
                    {
                        val[num++] = DeserializeValue(item, elementType);
                    }
                    return val;
                }
            }
            Type elementType2 = targetType.GetGenericArguments()[0];
            dynamic val2 = Activator.CreateInstance(targetType);
            foreach (XElement item2 in xml.Elements())
            {
                val2.Add(DeserializeValue(item2, elementType2));
            }
            return val2;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            result = (IsArray ? Delete((int)args[0]) : Delete((string)args[0]));
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (args.Length > 0)
            {
                result = null;
                return false;
            }
            result = IsDefined(binder.Name);
            return true;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type == typeof(IEnumerable) || binder.Type == typeof(object[]))
            {
                IEnumerable<object> enumerable = (IEnumerable<object>)(IsArray ? ((IEnumerable)(from x in xml.Elements()
                                                                                                select ToValue(x))) : ((IEnumerable)(from x in xml.Elements()
                                                                                                                                     select new KeyValuePair<string, object>(x.Name.LocalName, ToValue(x)))));
                result = ((binder.Type == typeof(object[])) ? enumerable.ToArray() : enumerable);
            }
            else
            {
                result = Deserialize(binder.Type);
            }
            return true;
        }

        private bool TryGet(XElement element, out object result)
        {
            if (element == null)
            {
                result = null;
                return false;
            }
            result = ToValue(element);
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            if (!IsArray)
            {
                return TryGet(xml.Element((string)indexes[0]), out result);
            }
            return TryGet(xml.Elements().ElementAtOrDefault((int)indexes[0]), out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (!IsArray)
            {
                return TryGet(xml.Element(binder.Name), out result);
            }
            return TryGet(xml.Elements().ElementAtOrDefault(int.Parse(binder.Name)), out result);
        }

        private bool TrySet(string name, object value)
        {
            JsonType jsonType = GetJsonType(value);
            XElement xElement = xml.Element(name);
            if (xElement == null)
            {
                xml.Add(new XElement(name, CreateTypeAttr(jsonType), CreateJsonNode(value)));
            }
            else
            {
                xElement.Attribute("type").Value = jsonType.ToString();
                xElement.ReplaceNodes(CreateJsonNode(value));
            }
            return true;
        }

        private bool TrySet(int index, object value)
        {
            JsonType jsonType = GetJsonType(value);
            XElement xElement = xml.Elements().ElementAtOrDefault(index);
            if (xElement == null)
            {
                xml.Add(new XElement("item", CreateTypeAttr(jsonType), CreateJsonNode(value)));
            }
            else
            {
                xElement.Attribute("type").Value = jsonType.ToString();
                xElement.ReplaceNodes(CreateJsonNode(value));
            }
            return true;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            if (!IsArray)
            {
                return TrySet((string)indexes[0], value);
            }
            return TrySet((int)indexes[0], value);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!IsArray)
            {
                return TrySet(binder.Name, value);
            }
            return TrySet(int.Parse(binder.Name), value);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            if (!IsArray)
            {
                return from x in xml.Elements()
                       select x.Name.LocalName;
            }
            return xml.Elements().Select((XElement x, int i) => i.ToString());
        }

        public override string ToString()
        {
            foreach (XElement item in from x in xml.Descendants()
                                      where x.Attribute("type").Value == "null"
                                      select x)
            {
                item.RemoveNodes();
            }
            return CreateJsonString(new XStreamingElement("root", CreateTypeAttr(jsonType), xml.Elements()));
        }
    }
}
