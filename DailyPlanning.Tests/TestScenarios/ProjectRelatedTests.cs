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
        BrowserWindow browser;

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
            uiLinkProjects.Find();
            Mouse.Click(uiLinkProjects);
        }

        [TestMethod]
        public void AddProjectTest()
        {
            GoToProjectsTest();

            var uiLinkAdd = new HtmlHyperlink(browser);
            uiLinkAdd.SearchProperties.Add(HtmlControl.PropertyNames.Id, IndexPageConst.INDEX_ADD_LINK_ID);
            uiLinkAdd.Find();
            Mouse.Click(uiLinkAdd);

            var uiTitle = new HtmlEdit(browser);
            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddProjectPageConst.ADD_TITLE_TEXTBOX_ID);
            uiTitle.Find();
            Keyboard.SendKeys(uiTitle, "test projekat");

            var uiCreateButton = new HtmlInputButton(browser);
            uiCreateButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddProjectPageConst.ADD_CREATE_BUTTON_ID);
            uiCreateButton.Find();
            Mouse.Click(uiCreateButton);
        }

        [TestMethod]
        public void EditProjectTest()
        {
            GoToProjectsTest();

            var uiLinkEdit = new HtmlHyperlink(new HtmlTable(browser));
            uiLinkEdit.SearchProperties.Add(HtmlControl.PropertyNames.Class, IndexPageConst.INDEX_EDIT_LINK_CLASS);
            uiLinkEdit.Find();
            Mouse.Click(uiLinkEdit);

            var uiTitle = new HtmlEdit(browser);
            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditProjectPageConst.EDIT_TITLE_TEXTBOX_ID);
            uiTitle.Find();
            uiTitle.SetProperty(HtmlEdit.PropertyNames.Text, "test editproject");

            var uiCreateButton = new HtmlInputButton(browser);
            uiCreateButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditProjectPageConst.EDIT_SAVE_BUTTON_ID);
            uiCreateButton.Find();
            Mouse.Click(uiCreateButton);
        }

        [TestMethod]
        public void ProjectDetailsTest()
        {
            GoToProjectsTest();

            var uiLinkEdit = new HtmlHyperlink(new HtmlTable(browser));
            uiLinkEdit.SearchProperties.Add(HtmlControl.PropertyNames.Class, IndexPageConst.INDEX_DETAILS_LINK_CLASS);
            uiLinkEdit.Find();
            Mouse.Click(uiLinkEdit);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            browser.Close();
        }
    }
}
