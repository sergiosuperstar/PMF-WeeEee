using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanning.Tests.Pages.ProjectPages
{
    class EditProjectPage
    {
        private BrowserWindow browser;

        public EditProjectPage(BrowserWindow browser)
        {
            this.browser = browser;
        }

        public EditProjectPage EditTitle(string title)
        {
            var uiTitle = new HtmlEdit(browser);

            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditProjectPageConst.EDIT_TITLE_TEXTBOX_ID);
            uiTitle.TryFind();
            uiTitle.SetProperty(HtmlEdit.PropertyNames.Text, title);

            return this;
        }

        public EditProjectPage UpdateProject()
        {
            var uiCreateButton = new HtmlInputButton(browser);

            uiCreateButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditProjectPageConst.EDIT_SAVE_BUTTON_ID);
            uiCreateButton.TryFind();
            Mouse.Click(uiCreateButton);

            return this;
        }
    }
}
