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

        public EditProjectPage NavigateToEditProject()
        {
            var uiLinkEdit = new HtmlHyperlink(new HtmlTable(browser));

            uiLinkEdit.SearchProperties.Add(HtmlControl.PropertyNames.Id, ProjectDetailsPageConst.DETAILS_EDIT_PROJECT_LINK_ID);
            uiLinkEdit.Find();
            Mouse.Click(uiLinkEdit);

            return new EditProjectPage(browser);
        }

        public AddWorkItemPage NavigateToAddWorkItem()
        {
            var uiLinkAdd = new HtmlHyperlink(browser);

            uiLinkAdd.SearchProperties.Add(HtmlControl.PropertyNames.Id, ProjectDetailsPageConst.DETAILS_ADD_WORKITEM_LINK_ID);
            uiLinkAdd.Find();
            Mouse.Click(uiLinkAdd);

            return new AddWorkItemPage(browser);
        }

        public WorkItemDetailsPage NavigateToWorkItemDetails()
        {
            var uiLinkDetails = new HtmlHyperlink(browser);

            uiLinkDetails.SearchProperties.Add(HtmlControl.PropertyNames.Class, ProjectDetailsPageConst.DETAILS_WORKITEM_LINK_CLASS);
            uiLinkDetails.Find();
            Mouse.Click(uiLinkDetails);

            return new WorkItemDetailsPage(browser);
        }

        public ProjectsPage NavigateToProjectsPage()
        {
            var uiLinkDetails = new HtmlHyperlink(browser);

            uiLinkDetails.SearchProperties.Add(HtmlControl.PropertyNames.Id, ProjectDetailsPageConst.DETAILS_BACK_TO_LIST_LINK_ID);
            uiLinkDetails.Find();
            Mouse.Click(uiLinkDetails);

            return new ProjectsPage(browser);
        }

        public bool CheckPageTitle()
        {
            var uiTitle = new HtmlControl(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, PageTitlesConst.PAGE_TITLE_ID);
            uiTitle.Find();
            uiTitle.WaitForControlReady();

            return uiTitle.InnerText.ToString().Equals(PageTitlesConst.PROJECT_DETAILS_TITLE);
        }
    }
}
