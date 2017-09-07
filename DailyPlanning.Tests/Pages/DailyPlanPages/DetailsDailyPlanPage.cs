using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanning.Tests.Pages.DailyPlanPages
{

    
    class DetailsDailyPlanPage
    {
        BrowserWindow browser;

        public DetailsDailyPlanPage(BrowserWindow browserWindow)
        {
            this.browser = browserWindow;
        }

        public DetailsDailyPlanPage NavigateBackToList()
        {
            var uiLinkList = new HtmlHyperlink(browser);
            uiLinkList.SearchProperties.Add(HtmlControl.PropertyNames.Id, DetailsDailyPlanPageConst.DETAILS_BACK_TO_LIST_LINK_ID);
            uiLinkList.Find();
            Mouse.Click(uiLinkList);

            return this;
        }

    }
}
