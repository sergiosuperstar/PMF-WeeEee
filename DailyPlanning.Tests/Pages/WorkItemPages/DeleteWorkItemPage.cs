using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyPlanning.Tests.Pages.WorkItemPages
{
    class DeleteWorkItemPage
    {
        private BrowserWindow browser;

        public DeleteWorkItemPage(BrowserWindow browser)
        {
            this.browser = browser;
        }

        public DeleteWorkItemPage DeleteConfirmation()
        {
            var confirmButton = new HtmlButton(browser);

            confirmButton.SearchProperties.Add(HtmlButton.PropertyNames.Id, WorkItemsPageConst.INDEX_CONFIRM_BUTTON_ID);
            confirmButton.Find();
            Mouse.Click(confirmButton);

            return this;
        }

        public int RowCount()
        {
            var uiTable = new HtmlTable(browser);
            uiTable.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemsPageConst.INDEX_TABLE_CLASS);
            uiTable.Find();

            return uiTable.RowCount;
        }
    }
}
