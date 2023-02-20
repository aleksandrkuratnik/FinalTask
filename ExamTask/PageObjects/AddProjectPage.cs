using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace ExamTask.PageObjects
{
    public class AddProjectPage : Form
    {
        public AddProjectPage() : base(inputProjectNameTextBox.Locator, "AddProjectPage")
        {
        }

        private static readonly ITextBox inputProjectNameTextBox = ElementFactory.GetTextBox(By.XPath("//input[@id='projectName']"), "text box for input name of new project");
        private static readonly IButton saveProjectButton = ElementFactory.GetButton(By.XPath("//button[@type='submit']"), "button to save project");
        private static ITextBox successfulSaveProjectTextBox => ElementFactory.GetTextBox(By.XPath("//div[contains(@class,'alert')][contains(@class,'alert-success')]"), "textBox which contains message about successful saving");

        public void InputProjectName(string projectName) => inputProjectNameTextBox.SendKeys(projectName);

        public void ClickSaveProjectButton() => saveProjectButton.Click();

        public bool IsProjectSaveSuccessfully() => successfulSaveProjectTextBox.State.IsDisplayed;
    }
}
