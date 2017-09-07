using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using DailyPlanning.Tests.Constants;
using DailyPlanning.Tests.Pages.DailyPlanPages;
using DailyPlanning.Tests.Pages.WorkItemPages;

namespace DailyPlanning.Tests.TestScenarios
{

    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class DailyPlanRelatedTests
    {
        private BrowserWindow browser;
        private DailyPlansPage dailyPlans;

        [TestInitialize()]
        public void Initialize()
        {
            browser = BrowserWindow.Launch("http://localhost:54813");
            dailyPlans = new DailyPlansPage(browser);
            dailyPlans.NavigateDailyPlans();

        }


        [TestMethod]
        public void Home_InsertNewDailyPlan_DailyPlansPageWithAddedDailyPlan()
        {

            AddDailyPlanPage addPage = dailyPlans.NavigateToAddDailyPlan();

            Assert.IsTrue(addPage.CheckPageTitle());

            

            string[] itemsDayBefore = { "WorkItem 2" };
            string[] itemsToday = { "WorkItem 2" };
            addPage.SelectWorkItemsDayBefore(itemsDayBefore)
                .SelectWorkItemsToday(itemsToday)
                .InsertNote("Test")
                .SaveDailyPlan();

        }


        [TestMethod]
        public void Home_EditDailyPlan_DailyPlansPageWithEditedDailyPlan()
        {

            EditDailyPlanPage editPage = dailyPlans.NavigateToEditDailyPlan();

            Assert.IsTrue(editPage.CheckPageTitle());

            string[] itemsDayBefore = { "WorkItem 3" };
            string[] itemsToday = { "WorkItem 1" };
            editPage.SelectWorkItemsDayBefore( itemsDayBefore)
                .SelectWorkItemsToday(itemsToday)
                .InsertNote("Test edit")
                .SaveDailyPlan();

        }

        [TestMethod]
        public void Home_DailyPlanDetails()
        {
            DetailsDailyPlanPage detailsPage = dailyPlans.NavigateToDetailsDailyPlan();

            Assert.IsTrue(detailsPage.CheckPageTitle());

            detailsPage.NavigateBackToList();  

        }
        [TestMethod]
        public void Home_WorkItemsDetailsForToday()
        {
            WorkItemDetailsPage detailsTodayPage = dailyPlans.NavigateToDetailsTodayWorkItem();

            Assert.IsTrue(detailsTodayPage.CheckPageTitle());

        }

        [TestMethod]
        public void Home_WorkItemsDetailsForDayBefore()
        {


            WorkItemDetailsPage detailsDayBeforePage = dailyPlans.NavigateToDetailsDayBeforeWorkItem();

            Assert.IsTrue(detailsDayBeforePage.CheckPageTitle());
        }

        [TestCleanup()]
        public void Cleanup()
        {
            browser.Close();
        }

    }
}
