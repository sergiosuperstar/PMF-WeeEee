using DailyPlanning.Tests.Constants;
using DailyPlanning.Tests.Pages.WorkItemPages;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace DailyPlanning.Tests.Pages.ProjectPages
{
    class ProjectDetailsPage
    {
        private BrowserWindow browser;

        public ProjectDetailsPage(BrowserWindow browser)
        {
            this.browser = browser;
        }

        public EditProjectPage GoToEditProject()
        {
            var uiLinkEdit = new HtmlHyperlink(new HtmlTable(browser));

            uiLinkEdit.SearchProperties.Add(HtmlControl.PropertyNames.Id, ProjectDetailsPageConst.DETAILS_EDIT_PROJECT_LINK_ID);
            uiLinkEdit.Find();
            Mouse.Click(uiLinkEdit);

            return new EditProjectPage(browser);
        }

        public AddWorkItemPage GoToAddWorkItem()
        {
            var uiLinkAdd = new HtmlHyperlink(browser);

            uiLinkAdd.SearchProperties.Add(HtmlControl.PropertyNames.Id, ProjectDetailsPageConst.DETAILS_ADD_WORKITEM_LINK_ID);
            uiLinkAdd.Find();
            Mouse.Click(uiLinkAdd);

            return new AddWorkItemPage(browser);
        }

        public WorkItemDetailsPage GoToWorkItemDetails()
        {
            var uiLinkDetails = new HtmlHyperlink(browser);

            uiLinkDetails.SearchProperties.Add(HtmlControl.PropertyNames.Class, ProjectDetailsPageConst.DETAILS_WORKITEM_LINK_CLASS);
            uiLinkDetails.Find();
            Mouse.Click(uiLinkDetails);

            return new WorkItemDetailsPage(browser);
        }
    }
}
