using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace DailyPlanning.Tests.Pages.ProjectPages
{
    class DeleteProjectPage
    {
        private BrowserWindow browser;

        public DeleteProjectPage(BrowserWindow browser)
        {
            this.browser = browser;
        }

        public DeleteProjectPage DeleteConfirmation()
        {
            var confirmButton = new HtmlButton(browser);

            confirmButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, ProjectsPageConst.INDEX_CONFIRM_BUTTON_ID);
            confirmButton.Find();
            Mouse.Click(confirmButton);

            return this;
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
