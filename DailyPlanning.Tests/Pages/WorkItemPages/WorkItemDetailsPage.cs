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

        public EditWorkItemPage goToEditPage()
        {
            var uiLinkEdit = new HtmlHyperlink(browser);

            uiLinkEdit.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemDetailsPageConst.DETAILS_EDIT_LINK_ID);
            uiLinkEdit.Find();
            Mouse.Click(uiLinkEdit);

            return new EditWorkItemPage(browser);
        }

        public ProjectDetailsPage goToProjectDetails()
        {
            var uiLinkDetails = new HtmlHyperlink(browser);

            uiLinkDetails.SearchProperties.Add(HtmlControl.PropertyNames.Class, ProjectsPageConst.INDEX_DETAILS_LINK_CLASS);
            uiLinkDetails.Find();
            Mouse.Click(uiLinkDetails);

            return new ProjectDetailsPage(browser);
        }
    }
}
