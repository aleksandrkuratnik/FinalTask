using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using ExamTask.Utils;
using OpenQA.Selenium;

namespace ExamTask.PageObjects
{
    public class HomePage : Form
    {
        private static IButton projectButton(string projectName) =>
             ElementFactory.GetButton(By.XPath($"//div[contains(@class,'list-group')]//a[contains(text(),'{projectName}')]"), $"{projectName} ProjectButton");
        private static IButton addProjectButton => ElementFactory.GetButton(By.XPath("//div[@class='panel-heading']//a"), "button to add new project");

        public HomePage() : base(By.XPath("//div[contains(@class,'list-group')]"), "Home page")
        {

        }

        public void ClickProjectButton(string projectName) => projectButton(projectName).Click();

        public string GetProjectId(string projectName)
        {
            string name = StringUtils.GetTrimString(projectButton(projectName).GetAttribute("href"), "=");
            return name;
        }

        internal void ClickAddProjectBtn()
        {
            addProjectButton.WaitAndClick();
        }

        public bool IsProjectDisplayed(string projectName) => projectButton(projectName).State.WaitForDisplayed();
    }
}
