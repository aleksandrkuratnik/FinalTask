using Newtonsoft.Json;

namespace ExamTask.Utils
{
    internal class FileReader
    {
        public static T? ReadJsonData<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(ReadFile(path));
        }

        private static string ReadFile(string path)
        {
            using StreamReader sr = new StreamReader(path);
            return sr.ReadToEnd();
        }
    }
}
