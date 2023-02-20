using Aquality.Selenium.Browsers;
using ExamTask.API;
using ExamTask.Enum;
using ExamTask.Models;
using ExamTask.TestConfig;
using ExamTask.TestData;
using ExamTask.Utils;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace ExamTask.Tests
{
    [TestFixture]
    public class Tests : BaseTest
    {
        private readonly string myVariant = TestDataManager.testData.MyVariant;
        private readonly string projectNameForClick = TestDataManager.testData.ProjectNameForClick;
        private readonly string newProjectName = TestDataManager.testData.NewProjectName;
        private Browser browser = AqualityServices.Browser;

        [Test]
        public void TryToTestUiAndApiRequiredAplication()
        {
            RestResponse tokenResponse = Api.GetToken();
            string token = tokenResponse.Content;
            HttpStatusCode statusCode = tokenResponse.StatusCode;
            Assert.Multiple(() =>
            {
                Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK), "Status code doesn't match");
                Assert.IsNotNull(token, "Token is empty");
            });

            browser.Maximize();
            browser.GoTo(TestConfigManager.testConfig.MainUrl);
            browser.WaitForPageToLoad();
            AuthorizeUtils.Authorize();
            CookieUtils.AddCookie("token", token);
            Assert.IsTrue(homePage.State.IsEnabled, "Home page isn't opened");

            AqualityServices.Browser.Refresh();
            Assert.AreEqual(myVariant, footer.GetTaskVariant(), "Task variant doesn't match");

            string projectId = homePage.GetProjectId(projectNameForClick);
            homePage.ClickProjectButton(projectNameForClick);
            AqualityServices.Logger.Info("project id - " + projectId);
            RestResponse responseTestsListFromApi = Api.GetTestsListsResponse(projectId);
            bool isJsonFormatResponse = Util.IsJsonString(responseTestsListFromApi);
            Assert.IsTrue(isJsonFormatResponse, "Tests lists response isn't Json format");

            List<TestsModel> testsListFromApi = ModelDeserializer.DeserializeListModels<TestsModel>(responseTestsListFromApi);
            AqualityServices.Logger.Info("number of APitests - " + testsListFromApi.Count);
            List<TestsModel> testsListFromUI = nexagePage.GetTestsList();
            AqualityServices.Logger.Info("number of Uitests - " + testsListFromUI.Count);
            List<TestsModel> sortedListTestsModelFromUI = TestsModel.SortPostCollectionByDescending(testsListFromUI);
            Assert.Multiple(() =>
            {
                CollectionAssert.IsSubsetOf(testsListFromUI, testsListFromApi, "Tests in UI doesn't match with API Request tests");
                Assert.That(sortedListTestsModelFromUI, Is.EqualTo(testsListFromUI), "Lists are not sorted descending");
            });

            browser.GoBack();
            int browserTabsCount = browser.Tabs().TabHandles.Count;
            homePage.ClickAddProjectBtn();
            browser.Tabs().SwitchToLastTab();
            addProjectPage.InputProjectName(newProjectName);
            addProjectPage.ClickSaveProjectButton();
            Assert.IsTrue(addProjectPage.IsProjectSaveSuccessfully(), "The message about the successful saving of the project doesn't appear");
            browser.Tabs().CloseTab();
            bool isRightAmountOfTabs = browser.Tabs().TabHandles.Count == browserTabsCount;
            Assert.IsTrue(isRightAmountOfTabs, "Add project window is not closed");

            browser.Tabs().SwitchToLastTab();
            browser.Refresh();
            bool isNewProjectDisplayed = homePage.IsProjectDisplayed(newProjectName);
            Assert.IsTrue(isNewProjectDisplayed, $"Project: '{newProjectName}' doesn't appear in project list");

            homePage.ClickProjectButton(newProjectName);
            PostModel newTest = new PostModel()
            {
                SID = RandomString.GetGeneratedRandomString(),
                ProjectName = newProjectName,
                Env = RandomString.GetGeneratedRandomString(),
                MethodName = RandomString.GetGeneratedRandomString(),
                TestName = RandomString.GetGeneratedRandomString(),
            };
            string recordId = Api.GetPostNewTestResponse(newTest);
            LogModel testLog = new LogModel()
            {
                TestId = recordId,
                Content = RandomString.GetGeneratedRandomString(),
            };
            Api.AddLogsToTest(testLog);
            string screenshot = Util.GetByteFromScreenshot();
            ContentType image = (ContentType)0;
            AttachmentModel attachmentModel = new AttachmentModel()
            {
                TestId = recordId,
                Content = screenshot,
                ContentType = image.ToString()
            };
            Api.AddScreenshotToTest(attachmentModel);
            bool isNewTestDisplayed = nexagePage.IsTestDisplayed(recordId);
            Assert.IsTrue(isNewTestDisplayed, $"Test {recordId} doesn't appear");
        }
    }
}
