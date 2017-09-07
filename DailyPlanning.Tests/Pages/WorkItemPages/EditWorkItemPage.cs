using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyPlanning.Tests.Pages.WorkItemPages
{
    class EditWorkItemPage
    {
        private BrowserWindow browser;

        public EditWorkItemPage(BrowserWindow browser)
        {
            this.browser = browser;
        }

        public EditWorkItemPage EditTitle(string title)
        {
            var uiTitle = new HtmlEdit(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditWorkItemPageConst.EDIT_TITLE_TEXTBOX_ID);
            uiTitle.Find();
            uiTitle.SetProperty(HtmlEdit.PropertyNames.Text, title);

            return this;
        }

        public EditWorkItemPage EditDescription(string description)
        {
            var uiDescription = new HtmlTextArea(browser);

            uiDescription.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditWorkItemPageConst.EDIT_DESCRIPTION_TEXTAREA_ID);
           uiDescription.Find();
            browser.ExecuteScript("CKEDITOR.instances['" + EditWorkItemPageConst.EDIT_DESCRIPTION_TEXTAREA_ID + "'].setData(\"" + description + "\")");

            return this;
        }

        public EditWorkItemPage EditStatus(int index)
        {
            var uiDropdownStatus = new HtmlComboBox(browser);

            uiDropdownStatus.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditWorkItemPageConst.EDIT_STATUS_DROPDOWN_ID);
            uiDropdownStatus.Find();
            uiDropdownStatus.SetProperty(HtmlComboBox.PropertyNames.SelectedIndex, index);

            return this;
        }

        public EditWorkItemPage EditProject(int index)
        {
            var uiDropdownProj = new HtmlComboBox(browser);

            uiDropdownProj.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditWorkItemPageConst.EDIT_PROJECT_DROPDOWN_ID);
            uiDropdownProj.Find();
            uiDropdownProj.SetProperty(HtmlComboBox.PropertyNames.SelectedIndex, index);

            return this;
        }

        public EditWorkItemPage UpdateWorkItem()
        {
            var uiCreateButton = new HtmlInputButton(browser);

            uiCreateButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditWorkItemPageConst.EDIT_SAVE_BUTTON_ID);
            uiCreateButton.Find();
            Mouse.Click(uiCreateButton);

            return this;
        }

        public bool CheckPageTitle()
        {
            var uiTitle = new HtmlControl(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, PageTitlesConst.PAGE_TITLE_ID);
            uiTitle.Find();
            uiTitle.WaitForControlReady();

            return uiTitle.InnerText.ToString().Equals(PageTitlesConst.EDIT_WORKITEM_TITLE);
        }
    }
}
