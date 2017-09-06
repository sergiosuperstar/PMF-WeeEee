using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DailyPlanning.Tests.TestScenarios
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class WorkItemRelatedTests
    {
        private BrowserWindow browser;

        [TestInitialize()]
        public void Initialize()
        {
            browser = BrowserWindow.Launch("http://localhost:54813");
        }

        [TestMethod]
        public void GoToWorkItemsTest()
        {
            var uiLinkProjects = new HtmlHyperlink(browser);
            uiLinkProjects.SearchProperties.Add(HtmlControl.PropertyNames.Id, IndexPageConst.MENU_WORKITEM_LINK_ID);

            Assert.IsTrue(uiLinkProjects.TryFind());

            Mouse.Click(uiLinkProjects);
        }

        [TestMethod]
        public void AddWorkItemTest()
        {
            GoToWorkItemsTest();

            var uiLinkAdd = new HtmlHyperlink(browser);

            uiLinkAdd.SearchProperties.Add(HtmlControl.PropertyNames.Id, WorkItemsPageConst.INDEX_ADD_LINK_ID);
            Assert.IsTrue(uiLinkAdd.TryFind());
            Mouse.Click(uiLinkAdd);

            var uiTitle = new HtmlEdit(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddWorkItemPageConst.ADD_TITLE_TEXTBOX_ID);
            Assert.IsTrue(uiTitle.TryFind());
            Keyboard.SendKeys(uiTitle, "test workitem");

            var uiDescription = new HtmlTextArea(browser);

            uiDescription.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddWorkItemPageConst.ADD_DESCRIPTION_TEXTAREA_ID);
            Assert.IsTrue(uiDescription.TryFind());
            browser.ExecuteScript("CKEDITOR.instances['" + AddWorkItemPageConst.ADD_DESCRIPTION_TEXTAREA_ID + "'].setData(\"description ajdjksh\")");

            var uiDropdownProj = new HtmlComboBox(browser);

            uiDropdownProj.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddWorkItemPageConst.ADD_PROJECT_DROPDOWN_ID);
            Assert.IsTrue(uiDropdownProj.TryFind());
            uiDropdownProj.SetProperty(HtmlComboBox.PropertyNames.SelectedIndex, 2);

            var uiCreateButton = new HtmlInputButton(browser);

            uiCreateButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddWorkItemPageConst.ADD_CREATE_BUTTON_ID);
            Assert.IsTrue(uiCreateButton.TryFind());
            Mouse.Click(uiCreateButton);
        }

        [TestMethod]
        public void EditProjectTest()
        {
            GoToWorkItemsTest();

            var uiLinkEdit = new HtmlHyperlink(browser);

            uiLinkEdit.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemsPageConst.INDEX_EDIT_LINK_CLASS);
            Assert.IsTrue(uiLinkEdit.TryFind());
            Mouse.Click(uiLinkEdit);

            var uiTitle = new HtmlEdit(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditWorkItemPageConst.EDIT_TITLE_TEXTBOX_ID);
            Assert.IsTrue(uiTitle.TryFind());
            uiTitle.SetProperty(HtmlEdit.PropertyNames.Text, "novi naziv workitema");

            var uiDescription = new HtmlTextArea(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditWorkItemPageConst.EDIT_DESCRIPTION_TEXTAREA_ID);
            Assert.IsTrue(uiDescription.TryFind());
            browser.ExecuteScript("CKEDITOR.instances['" + AddWorkItemPageConst.ADD_DESCRIPTION_TEXTAREA_ID + "'].setData(\"new description ajdjksh\")");

            var uiDropdownStatus = new HtmlComboBox(browser);

            uiDropdownStatus.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditWorkItemPageConst.EDIT_STATUS_DROPDOWN_ID);
            Assert.IsTrue(uiDropdownStatus.TryFind());
            uiDropdownStatus.SetProperty(HtmlComboBox.PropertyNames.SelectedIndex, 2);

            var uiDropdownProj = new HtmlComboBox(browser);

            uiDropdownProj.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditWorkItemPageConst.EDIT_PROJECT_DROPDOWN_ID);
            Assert.IsTrue(uiDropdownProj.TryFind());
            uiDropdownProj.SetProperty(HtmlComboBox.PropertyNames.SelectedIndex, 2);

            var uiCreateButton = new HtmlInputButton(browser);

            uiCreateButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditWorkItemPageConst.EDIT_SAVE_BUTTON_ID);
            Assert.IsTrue(uiCreateButton.TryFind());
            Mouse.Click(uiCreateButton);
        }

        [TestMethod]
        public void WorkItemDetailsTest()
        {
            GoToWorkItemsTest();

            var uiLinkDetails = new HtmlHyperlink(browser);
            uiLinkDetails.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemsPageConst.INDEX_DETAILS_LINK_CLASS);

            Assert.IsTrue(uiLinkDetails.TryFind());

            Mouse.Click(uiLinkDetails);
        }

        [TestMethod]
        public void DeleteWorkItemTest()
        {
            GoToWorkItemsTest();

            var uiLinkDelete = new HtmlHyperlink(browser);
            uiLinkDelete.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemsPageConst.INDEX_DELETE_LINK_CLASS);

            Assert.IsTrue(uiLinkDelete.TryFind());

            Mouse.Click(uiLinkDelete);

            var confirmButton = new HtmlButton(browser);
            confirmButton.SearchProperties.Add(HtmlButton.PropertyNames.Id, WorkItemsPageConst.INDEX_CONFIRM_BUTTON_ID);

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
