using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            dailyPlans = new DailyPlansPage(browser);
            dailyPlans.NavigateDailyPlans();

 
        }

        [TestMethod]
        public void Home_InsertNewDailyPlan_DailyPlansPageWithAddedDailyPlan_Test()
        {
            var countRowsBefore = dailyPlans.TableRowCount();
            AddDailyPlanPage addPage = dailyPlans.NavigateToAddDailyPlan();
            Assert.IsTrue(addPage.CheckPageTitle());

         
            string[] itemsDayBefore = { "Investigate HtmlSanitizer" };
            string[] itemsToday = { "Add Assert in DetailsDailyPlan test method" };
            addPage.SelectWorkItemsDayBefore(itemsDayBefore)
                .SelectWorkItemsToday(itemsToday)
                .InsertNote("Test")
                .SaveDailyPlan();
            var currentRows = dailyPlans.TableRowCount();

            Assert.IsTrue(currentRows == countRowsBefore + 1);
        }

        [TestMethod]
        public void Home_EditDailyPlan_DailyPlansPageWithEditedDailyPlan_Test()
        {
            Home_InsertNewDailyPlan_DailyPlansPageWithAddedDailyPlan_Test();
            EditDailyPlanPage editPage = dailyPlans.NavigateToEditDailyPlan();
            Assert.IsTrue(editPage.CheckPageTitle());

            string[] itemsDayBefore = { "Implement CKEditor for Description in WorkItems" };
            string[] itemsToday = { "Initialize database before each test scenario" };
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
            var isIndexPage = dailyPlans.CheckPageTitle();
            Assert.IsTrue(isIndexPage);
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
                        .InsertNote("123456789012345678901234567890123456789012" + 
                                    "345678901234567890123456789012345678901234" + 
                                    "56789012345678901234567890")
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
                         .InsertNote("123456789012345678901234567890123456789012" +
                                    "345678901234567890123456789012345678901234" +
                                    "56789012345678901234567890")
                         .SaveDailyPlan();

            Assert.IsTrue(editDailyPlan.IsValidationDisplayedForSelectDayBeforeWorkItems());
            Assert.IsTrue(editDailyPlan.IsValidationDisplayedForSelectTodayWorkItems());
            Assert.IsTrue(editDailyPlan.IsValidationDisplayedForNote());
        }
    }
}
