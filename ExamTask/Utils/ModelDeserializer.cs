using Newtonsoft.Json;
using RestSharp;

namespace ExamTask.Utils
{
    internal static class ModelDeserializer
    {
        public static T? DeserializeModel<T>(RestResponse response)
        {
            string jsonString = response.Content;
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static List<T>? DeserializeListModels<T>(RestResponse response)
        {
            string jsonString = response.Content;
            return JsonConvert.DeserializeObject<List<T>>(jsonString);
        }
    }
}
