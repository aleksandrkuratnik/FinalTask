using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using ExamTask.Models;
using OpenQA.Selenium;

namespace ExamTask.PageObjects
{
    internal class ProjectPage : Form
    {
        private ILabel testParameter(int RowIndex, int ElementIndex) => ElementFactory.GetLabel(By.XPath($"//table[contains(@class,'table')]//tr[td][{RowIndex}]//td[{ElementIndex}]"), "test parameter");
        private static ILink testNameLink(string testId) => ElementFactory.GetLink(By.XPath($"//table[@id='allTests']//a[contains(@href,'testId={testId}')]"), $"Test:{testId}");

        public ProjectPage() : base(By.Id("pie"), "Nexage page")
        {
        }

        public List<TestsModel> GetTestsList()
        {
            List<TestsModel> tests = new List<TestsModel>();
            testParameter(1, 1).State.WaitForExist();

            for (int i = 1; testParameter(i, 1).State.IsExist; i++)
            {
                tests.Add(new TestsModel()
                {
                    Name = testParameter(i, 1).Text.ToLower(),
                    Method = testParameter(i, 2).Text.ToLower(),
                    Status = testParameter(i, 3).Text.ToLower(),
                    StartTime = testParameter(i, 4).Text.ToLower(),
                    EndTime = testParameter(i, 5).Text.ToLower(),
                    Duration = testParameter(i, 6).Text.ToLower(),
                });
            }
            return tests;
        }

        public bool IsTestDisplayed(string testId) => testNameLink(testId).State.WaitForDisplayed();
    }
}
