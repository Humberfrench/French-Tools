using Newtonsoft.Json;

namespace French.Tools.Extensions
{
    public static class Json
    {
        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
