using Aquality.Selenium.Browsers;
using ExamTask.Models;
using ExamTask.TestConfig;
using ExamTask.TestData;
using ExamTask.Utils;
using RestSharp;
using System.Net;

namespace ExamTask.API
{
    internal static class Api
    {
        private static readonly ApiMethods? apiMethods = FileReader.ReadJsonData<ApiMethods>(@"Resources\endpoints.json");

        private static readonly string baseApiUrl = TestConfigManager.testConfig.ApiUrl;
        private static readonly string variant = TestConfigManager.testConfig.VariantParameter;
        private static readonly string projectIdParameter = TestConfigManager.testConfig.ProjectIdParameter;
        private static readonly string SID = TestConfigManager.testConfig.SIDParameter;
        private static readonly string projectName = TestConfigManager.testConfig.ProjectNameParameter;
        private static readonly string testName = TestConfigManager.testConfig.TestNameParameter;
        private static readonly string methodName = TestConfigManager.testConfig.MethodNameParameter;
        private static readonly string env = TestConfigManager.testConfig.EnvParameter;
        private static readonly string testId = TestConfigManager.testConfig.TestIdParameter;
        private static readonly string content = TestConfigManager.testConfig.ContentParameter;
        private static readonly string contentType = TestConfigManager.testConfig.ContentTypeParameter;

        private static RestRequest request;

        public static RestResponse GetToken()
        {
            var url = baseApiUrl + apiMethods.GetTokenUrl;
            AqualityServices.Logger.Info(url);
            RestClient client = new(url);
            request = new RestRequest();
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(variant, TestDataManager.testData.MyVariant);
            RestResponse response = client.Post(request);
            return response;
        }

        public static RestResponse GetTestsListsResponse(string projectId)
        {
            string url = baseApiUrl + apiMethods.GetTestsUrl;
            AqualityServices.Logger.Info(url);
            RestClient client = new(url);
            request = new RestRequest();
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(projectIdParameter, projectId);
            RestResponse response = client.Post(request);
            AqualityServices.Logger.Info($"testsResponseContent - {response.Content}");
            return response;
        }

        public static string GetPostNewTestResponse(PostModel newModel)
        {
            string url = baseApiUrl + apiMethods.PostNewTestUrl;
            AqualityServices.Logger.Info(url);
            RestClient client = new(url);
            request = new RestRequest();
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(SID, newModel.SID);
            request.AddParameter(projectName, newModel.ProjectName);
            request.AddParameter(testName, newModel.TestName);
            request.AddParameter(methodName, newModel.MethodName);
            request.AddParameter(env, newModel.Env);
            RestResponse response = client.Post(request);
            return response.Content;
        }

        public static void AddLogsToTest(LogModel newLogModel)
        {
            string url = baseApiUrl + apiMethods.AddLogsToTestUrl;
            AqualityServices.Logger.Info(url);
            RestClient client = new(url);
            request = new RestRequest();
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(testId, newLogModel.TestId);
            request.AddParameter(content, newLogModel.Content);
            RestResponse response = client.Post(request);
            AqualityServices.Logger.Info($"LogsResponseContent - {response.StatusCode}");
        }

        public static void AddScreenshotToTest(AttachmentModel newAttachmentModel)
        {
            string url = baseApiUrl + apiMethods.AddScreenshotToTestUrl;
            AqualityServices.Logger.Info(url);
            RestClient client = new(url);
            request = new RestRequest();
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(testId, newAttachmentModel.TestId);
            request.AddParameter(content, newAttachmentModel.Content);
            request.AddParameter(contentType, newAttachmentModel.ContentType);
            RestResponse response = client.Post(request);
            AqualityServices.Logger.Info($"ScreenshotResponseContent - {response.StatusCode}");
        }
    }
}
