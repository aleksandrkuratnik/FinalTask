using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using ExamTask.Utils;
using OpenQA.Selenium;

namespace ExamTask.PageObjects
{
    public class Footer : Form
    {
        private ILabel taskVariant = ElementFactory.GetLabel(By.XPath("//footer[contains(@class,'footer')]//span"), "label with the task variant");

        public Footer() : base(By.XPath("//footer[contains(@class,'footer')]"), "Footer")
        {
        }

        public string GetTaskVariant()
        {
            string taskVariantFromFooter = taskVariant.Text;
            return StringUtils.GetTrimString(taskVariantFromFooter, " ");
        }
    }
}
