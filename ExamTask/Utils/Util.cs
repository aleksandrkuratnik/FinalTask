using Aquality.Selenium.Browsers;
using ExamTask.Models;
using Newtonsoft.Json;
using RestSharp;

namespace ExamTask.Utils
{
    internal static class Util
    {
        public static bool IsJsonString(RestResponse response)
        {
            try
            {
                if (response.Content != null)
                    JsonConvert.DeserializeObject(response.Content);
            }
            catch (JsonReaderException)
            {
                return false;
            }
            return true;
        }

        public static string GetByteFromScreenshot()
        {
            var screenshotBytes = AqualityServices.Browser.GetScreenshot();
            var screenshot = Convert.ToBase64String(screenshotBytes);
            return screenshot;
        }
    }
}
