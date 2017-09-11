using DailyPlanning.Tests.Constants;
using DailyPlanning.Tests.Pages.ProjectPages;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyPlanning.Tests.Pages.WorkItemPages
{
    class WorkItemDetailsPage
    {
        private BrowserWindow browser;

        public WorkItemDetailsPage(BrowserWindow browser)
        {
            this.browser = browser;
        }

        public EditWorkItemPage NavigateToEditPage()
        {
            var uiLinkEdit = new HtmlHyperlink(browser);

            uiLinkEdit.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemDetailsPageConst.DETAILS_EDIT_LINK_ID);
            uiLinkEdit.Find();
            Mouse.Click(uiLinkEdit);

            return new EditWorkItemPage(browser);
        }

        public ProjectDetailsPage NavigateToProjectDetails()
        {
            var uiLinkDetails = new HtmlHyperlink(browser);

            uiLinkDetails.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemDetailsPageConst.DETAILS_PROJECT_LINK_ID);
            uiLinkDetails.Find();
            Mouse.Click(uiLinkDetails);

            return new ProjectDetailsPage(browser);
        }

        public WorkItemsPage NavigateToWorkItemsPage()
        {
            var uiLinkDetails = new HtmlHyperlink(browser);

            uiLinkDetails.SearchProperties.Add(HtmlControl.PropertyNames.Id, WorkItemDetailsPageConst.DETAILS_BACK_TO_LIST_LINK_ID);
            uiLinkDetails.Find();
            Mouse.Click(uiLinkDetails);

            return new WorkItemsPage(browser);
        }

        public bool CheckPageTitle()
        {
            var uiTitle = new HtmlControl(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, PageTitlesConst.PAGE_TITLE_ID);
            uiTitle.Find();
            uiTitle.WaitForControlReady();

            return uiTitle.InnerText.ToString().Equals(PageTitlesConst.WORKITEM_DETAILS_TITLE);
        }
    }
}
