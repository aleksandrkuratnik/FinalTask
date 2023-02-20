using Aquality.Selenium.Browsers;
using ExamTask.PageObjects;
using NUnit.Framework;

namespace ExamTask.Tests
{
    [TestFixture]
    public class BaseTest
    {
        private HomePage _homePage;
        private protected HomePage homePage => _homePage ??= new HomePage();

        private Footer _footer;
        private protected Footer footer => _footer ??= new Footer();

        private ProjectPage _nexagePage;
        private protected ProjectPage nexagePage => _nexagePage ??= new ProjectPage();

        private AddProjectPage _addProjectPage;
        private protected AddProjectPage addProjectPage => _addProjectPage ??= new AddProjectPage();

        [TearDown]
        public void AfterEach()
        {
            AqualityServices.Browser.Quit();
        }
    }
}
