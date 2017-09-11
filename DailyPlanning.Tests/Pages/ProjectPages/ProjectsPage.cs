using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyPlanning.Tests.Pages.ProjectPages
{
    class ProjectsPage
    {
        private BrowserWindow browser;

        public ProjectsPage(BrowserWindow browser)
        {
            this.browser = browser;
        }

        public void NavigateToProjects()
        {
            var uiLinkProjects = new HtmlHyperlink(browser);

            uiLinkProjects.SearchProperties.Add(HtmlControl.PropertyNames.Id, IndexPageConst.MENU_PROJECT_LINK_ID);
            uiLinkProjects.Find();
            Mouse.Click(uiLinkProjects);
        }

        public AddProjectPage GoToAddProject()
        {
            var uiLinkAdd = new HtmlHyperlink(browser);

            uiLinkAdd.SearchProperties.Add(HtmlControl.PropertyNames.Id, ProjectsPageConst.INDEX_ADD_LINK_ID);
            uiLinkAdd.Find();
            Mouse.Click(uiLinkAdd);

            return new AddProjectPage(browser);
        }

        public EditProjectPage GoToEditProject()
        {
            var uiLinkEdit = new HtmlHyperlink(new HtmlTable(browser));

            uiLinkEdit.SearchProperties.Add(HtmlControl.PropertyNames.Class, ProjectsPageConst.INDEX_EDIT_LINK_CLASS);
            uiLinkEdit.Find();
            Mouse.Click(uiLinkEdit);

            return new EditProjectPage(browser);
        }

        public ProjectDetailsPage GoToProjectDetails()
        {
            var uiLinkDetails = new HtmlHyperlink(browser);

            uiLinkDetails.SearchProperties.Add(HtmlControl.PropertyNames.Class, ProjectsPageConst.INDEX_DETAILS_LINK_CLASS);
            uiLinkDetails.Find();
            Mouse.Click(uiLinkDetails);

            return new ProjectDetailsPage(browser);
        }

        public DeleteProjectPage DeleteProject()
        {
            var uiLinkDelete = new HtmlHyperlink(browser);

            uiLinkDelete.SearchProperties.Add(HtmlControl.PropertyNames.Class, ProjectsPageConst.INDEX_DELETE_LINK_CLASS);
            uiLinkDelete.Find();
            Mouse.Click(uiLinkDelete);

            return new DeleteProjectPage(browser);
        }

        public bool CheckPageTitle()
        {
            var uiTitle = new HtmlControl(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, PageTitlesConst.PAGE_TITLE_ID);
            uiTitle.Find();
            uiTitle.WaitForControlReady();

            return uiTitle.InnerText.ToString().Equals(PageTitlesConst.PROJECT_TITLE);
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
