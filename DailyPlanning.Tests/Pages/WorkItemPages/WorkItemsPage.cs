using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyPlanning.Tests.Pages.WorkItemPages
{
    class WorkItemsPage
    {
        private BrowserWindow browser;

        public WorkItemsPage(BrowserWindow browser)
        {
            this.browser = browser;
        }
        
        public void NavigateToWorkItems()
        {
            var uiLinkProjects = new HtmlHyperlink(browser);

            uiLinkProjects.SearchProperties.Add(HtmlControl.PropertyNames.Id, IndexPageConst.MENU_WORKITEM_LINK_ID);
            uiLinkProjects.Find();
            Mouse.Click(uiLinkProjects);
        }

        public AddWorkItemPage NavigateToAddWorkItem()
        {
            var uiLinkAdd = new HtmlHyperlink(browser);

            uiLinkAdd.SearchProperties.Add(HtmlControl.PropertyNames.Id, WorkItemsPageConst.INDEX_ADD_LINK_ID);
            uiLinkAdd.Find();
            Mouse.Click(uiLinkAdd);

            return new AddWorkItemPage(browser);
        }

        public EditWorkItemPage NavigateToEditWorkItem()
        {
            var uiLinkEdit = new HtmlHyperlink(browser);

            uiLinkEdit.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemsPageConst.INDEX_EDIT_LINK_CLASS);
            uiLinkEdit.Find();
            Mouse.Click(uiLinkEdit);

            return new EditWorkItemPage(browser);
        }

        public WorkItemDetailsPage NavigateToDetails()
        {
            var uiLinkDetails = new HtmlHyperlink(browser);

            uiLinkDetails.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemsPageConst.INDEX_DETAILS_LINK_CLASS);
            uiLinkDetails.Find();
            Mouse.Click(uiLinkDetails);

            return new WorkItemDetailsPage(browser);
        }

        public DeleteWorkItemPage DeleteWorkItem()
        {
            var uiLinkDelete = new HtmlHyperlink(browser);
            uiLinkDelete.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemsPageConst.INDEX_DELETE_LINK_CLASS);
            uiLinkDelete.Find();
            Mouse.Click(uiLinkDelete);

            return new DeleteWorkItemPage(browser);
        }

        public bool CheckPageTitle()
        {
            var uiTitle = new HtmlControl(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, PageTitlesConst.PAGE_TITLE_ID);
            uiTitle.Find();
            uiTitle.WaitForControlReady();

            return uiTitle.InnerText.ToString().Equals(PageTitlesConst.WORKITEMS_TITLE);
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
