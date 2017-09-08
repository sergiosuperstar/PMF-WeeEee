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
    public class DailyPlanRelatedTests : BaseTest
    {

        private DailyPlansPage dailyPlans;


        [TestInitialize()]
        public void InitializeDailyPlan()
        {
            base.Initialize();
            dailyPlans = new DailyPlansPage(browser);
            dailyPlans.NavigateDailyPlans();
        }


        [TestMethod]
        public void Home_InsertNewDailyPlan_DailyPlansPageWithAddedDailyPlan_Test()
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
        public void Home_EditDailyPlan_DailyPlansPageWithEditedDailyPlan_Test()
        {
            Home_InsertNewDailyPlan_DailyPlansPageWithAddedDailyPlan_Test();
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
        public void Home_DailyPlanDetails_Test()
        {
            DetailsDailyPlanPage detailsPage = dailyPlans.NavigateToDetailsDailyPlan();

            Assert.IsTrue(detailsPage.CheckPageTitle());

            detailsPage.NavigateBackToList();

            

        }
        [TestMethod]
        public void Home_WorkItemsDetailsForToday_Test()
        {
            WorkItemDetailsPage detailsTodayPage = dailyPlans.NavigateToDetailsTodayWorkItem();

            Assert.IsTrue(detailsTodayPage.CheckPageTitle());

        }

        [TestMethod]
        public void Home_WorkItemsDetailsForDayBefore_Test()
        {


            WorkItemDetailsPage detailsDayBeforePage = dailyPlans.NavigateToDetailsDayBeforeWorkItem();

            Assert.IsTrue(detailsDayBeforePage.CheckPageTitle());
        }

        [TestMethod]
        public void Home_InsertNewDailyPlanValidation_Test()
        {
            var addDailyPlan = dailyPlans.NavigateToAddDailyPlan();

            Assert.IsTrue(addDailyPlan.CheckPageTitle());

            string[] items = { };
            addDailyPlan.SelectWorkItemsDayBefore(items)
                        .SelectWorkItemsToday(items)
                        .InsertNote("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")
                        .SaveDailyPlan();

            Assert.IsTrue(addDailyPlan.IsValidationDisplayedForSelectDayBeforeWorkItems());
            Assert.IsTrue(addDailyPlan.IsValidationDisplayedForSelectTodayWorkItems());
            Assert.IsTrue(addDailyPlan.IsValidationDisplayedForNote());

        }
        [TestMethod]
        public void Home_EditNewDailyPlanValidation_Test()
        {
            Home_InsertNewDailyPlan_DailyPlansPageWithAddedDailyPlan_Test();
            var editDailyPlan = dailyPlans.NavigateToEditDailyPlan();

            Assert.IsTrue(editDailyPlan.CheckPageTitle());

            string[] items = { };
            editDailyPlan.SelectWorkItemsDayBefore(items)
                         .SelectWorkItemsToday(items)
                         .InsertNote("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")
                         .SaveDailyPlan();

            Assert.IsTrue(editDailyPlan.IsValidationDisplayedForSelectDayBeforeWorkItems());
            Assert.IsTrue(editDailyPlan.IsValidationDisplayedForSelectTodayWorkItems());
            Assert.IsTrue(editDailyPlan.IsValidationDisplayedForNote());
        }

    }
}
