using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace DailyPlanning.Tests.Pages.WorkItemPages
{
    class AddWorkItemPage
    {
        private BrowserWindow browser;

        public AddWorkItemPage(BrowserWindow browser)
        {
            this.browser = browser;
        }

        public AddWorkItemPage InsertTitle(string title)
        {
            var uiTitle = new HtmlEdit(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddWorkItemPageConst.ADD_TITLE_TEXTBOX_ID);
            uiTitle.Find();
            Keyboard.SendKeys(uiTitle, title);

            return this;
        }

        public AddWorkItemPage InsertDescription(string description)
        {
            var uiDescription = new HtmlTextArea(browser);

            uiDescription.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddWorkItemPageConst.ADD_DESCRIPTION_TEXTAREA_ID);
            uiDescription.Find();
            browser.ExecuteScript("CKEDITOR.instances['" + AddWorkItemPageConst.ADD_DESCRIPTION_TEXTAREA_ID + "'].setData(\"" + description + "\")");

            return this;
        }

        public AddWorkItemPage SelectProject(int index)
        {
            var uiDropdownProj = new HtmlComboBox(browser);

            uiDropdownProj.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddWorkItemPageConst.ADD_PROJECT_DROPDOWN_ID);
            uiDropdownProj.Find();
            uiDropdownProj.SetProperty(HtmlComboBox.PropertyNames.SelectedIndex, index);

            return this;
        }

        public AddWorkItemPage SaveWorkItem()
        {
            var uiCreateButton = new HtmlInputButton(browser);

            uiCreateButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddWorkItemPageConst.ADD_CREATE_BUTTON_ID);
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

            return uiTitle.InnerText.ToString().Equals(PageTitlesConst.ADD_WORKITEM_TITLE);
        }

        public bool IsValidationDisplayed()
        {
            var uiValidationError = new HtmlControl(browser);
            uiValidationError.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddWorkItemPageConst.ADD_TITLE_VALIDATION_ID_ERROR);

            return uiValidationError.TryFind();
        }
    }
}
