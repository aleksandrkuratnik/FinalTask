using Aquality.Selenium.Browsers;
using ExamTask.Models;

namespace ExamTask.Utils
{
    public static class AuthorizeUtils
    {
        private const string Protocol = "http://";

        private static readonly UserData? userData = FileReader.ReadJsonData<UserData>(@"Resources\userData.json");

        private static readonly string username = userData.Login;
        private static readonly string password = userData.Password;

        public static void Authorize()
        {
            AqualityServices.Logger.Info("Authorization on the site");
            string url = $"{Protocol}{username}:{password}@{AqualityServices.Browser.Driver.Url.Replace($"{Protocol}", "")}";
            AqualityServices.Browser.GoTo(url);
        }
    }
}
