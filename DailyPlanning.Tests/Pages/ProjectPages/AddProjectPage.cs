using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyPlanning.Tests.Pages.ProjectPages
{
    class AddProjectPage
    {
        private BrowserWindow browser;

        public AddProjectPage(BrowserWindow browser)
        {
            this.browser = browser;
        }

        public AddProjectPage AddTitle(string title)
        {
            var uiTitle = new HtmlEdit(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddProjectPageConst.ADD_TITLE_TEXTBOX_ID);
           uiTitle.TryFind();
            Keyboard.SendKeys(uiTitle, title);

            return this;
        }

        public AddProjectPage SaveProject()
        {
            var uiCreateButton = new HtmlInputButton(browser);

            uiCreateButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddProjectPageConst.ADD_CREATE_BUTTON_ID);
            uiCreateButton.Find();
            Mouse.Click(uiCreateButton);

            return this;
        }
    }
}
