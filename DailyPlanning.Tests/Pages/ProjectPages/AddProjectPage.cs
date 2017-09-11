using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

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
            uiTitle.Find();
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

        public bool CheckPageTitle()
        {
            var uiTitle = new HtmlControl(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, PageTitlesConst.PAGE_TITLE_ID);
            uiTitle.Find();
            uiTitle.WaitForControlReady();

            return uiTitle.InnerText.ToString().Equals(PageTitlesConst.ADD_PROJECT_TITLE);
        }

        public bool IsValidationDisplayed()
        {
            var uiValidationError = new HtmlControl(browser);
            uiValidationError.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddProjectPageConst.ADD_TITLE_TEXTBOX_ID_ERROR);

            return uiValidationError.TryFind();
        }

        public int RowCount()
        {
            var uiTable = new HtmlTable(browser);
            uiTable.SearchProperties.Add(HtmlControl.PropertyNames.Class, ProjectsPageConst.INDEX_TABLE_CLASS);
            uiTable.Find();

            return uiTable.RowCount;
        }
    }
}
