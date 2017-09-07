using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace DailyPlanning.Tests.Pages.ProjectPages
{
    class DeleteProjectPage
    {
        private BrowserWindow browser;

        public DeleteProjectPage(BrowserWindow browser)
        {
            this.browser = browser;
        }

        public DeleteProjectPage DeleteConfirmation()
        {
            var confirmButton = new HtmlButton(browser);

            confirmButton.SearchProperties.Add(HtmlControl.PropertyNames.Id, ProjectsPageConst.INDEX_CONFIRM_BUTTON_ID);
            confirmButton.Find();
            Mouse.Click(confirmButton);

            return this;
        }
    }
}
