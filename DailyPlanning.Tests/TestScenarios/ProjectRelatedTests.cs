using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DailyPlanning.Tests.TestScenarios
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class ProjectRelatedTests
    {
        private BrowserWindow browser;

        [TestInitialize()]
        public void Initialize()
        {
            browser = BrowserWindow.Launch("http://localhost:54813");
        }

        [TestMethod]
        public void GoToProjectsTest()
        {
            var uiLinkProjects = new HtmlHyperlink(browser);
            uiLinkProjects.SearchProperties.Add(HtmlControl.PropertyNames.Id, IndexPageConst.MENU_PROJECT_LINK_ID);

            Assert.IsTrue(uiLinkProjects.TryFind());

            Mouse.Click(uiLinkProjects);
        }

        [TestMethod]
        public void AddProjectTest()
        {
            GoToProjectsTest();

            var uiLinkAdd = new HtmlHyperlink(browser);
            uiLinkAdd.SearchProperties.Add(HtmlControl.PropertyNames.Id, ProjectsPageConst.INDEX_ADD_LINK_ID);

            Assert.IsTrue(uiLinkAdd.TryFind());

            Mouse.Click(uiLinkAdd);

            var uiTitle = new HtmlEdit(browser);
            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddProjectPageConst.ADD_TITLE_TEXTBOX_ID);

            Assert.IsTrue(uiTitle.TryFind());

            Keyboard.SendKeys(uiTitle, "test projekat");

            var uiCreateButton = new HtmlInputButton(browser);
            uiCreateButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddProjectPageConst.ADD_CREATE_BUTTON_ID);

            Assert.IsTrue(uiCreateButton.TryFind());

            Mouse.Click(uiCreateButton);
        }

        [TestMethod]
        public void EditProjectTest()
        {
            GoToProjectsTest();

            var uiLinkEdit = new HtmlHyperlink(new HtmlTable(browser));
            uiLinkEdit.SearchProperties.Add(HtmlControl.PropertyNames.Class, ProjectsPageConst.INDEX_EDIT_LINK_CLASS);

            Assert.IsTrue(uiLinkEdit.TryFind());

            Mouse.Click(uiLinkEdit);

            var uiTitle = new HtmlEdit(browser);
            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditProjectPageConst.EDIT_TITLE_TEXTBOX_ID);

            Assert.IsTrue(uiTitle.TryFind());

            uiTitle.SetProperty(HtmlEdit.PropertyNames.Text, "test editproject");

            var uiCreateButton = new HtmlInputButton(browser);
            uiCreateButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditProjectPageConst.EDIT_SAVE_BUTTON_ID);

            Assert.IsTrue(uiCreateButton.TryFind());

            Mouse.Click(uiCreateButton);
        }

        [TestMethod]
        public void ProjectDetailsTest()
        {
            GoToProjectsTest();

            var uiLinkDetails = new HtmlHyperlink(browser);
            uiLinkDetails.SearchProperties.Add(HtmlControl.PropertyNames.Class, ProjectsPageConst.INDEX_DETAILS_LINK_CLASS);

            Assert.IsTrue(uiLinkDetails.TryFind());

            Mouse.Click(uiLinkDetails);
        }

        [TestMethod]
        public void DeleteProjectTest()
        {
            GoToProjectsTest();

            var uiLinkDelete = new HtmlHyperlink(browser);
            uiLinkDelete.SearchProperties.Add(HtmlControl.PropertyNames.Class, ProjectsPageConst.INDEX_DELETE_LINK_CLASS);

            Assert.IsTrue(uiLinkDelete.TryFind());

            Mouse.Click(uiLinkDelete);

            var confirmButton = new HtmlButton(browser);
            confirmButton.SearchProperties.Add(HtmlButton.PropertyNames.Id, ProjectsPageConst.INDEX_CONFIRM_BUTTON_ID);

            Assert.IsTrue(confirmButton.TryFind());

            Mouse.Click(confirmButton);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            browser.Close();
        }
    }
}
