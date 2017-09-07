using DailyPlanning.Tests.Pages;
using DailyPlanning.Tests.Pages.WorkItemPages;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DailyPlanning.Tests.TestScenarios
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class WorkItemRelatedTests : BaseTest
    {
        private WorkItemsPage workItems;

        [TestInitialize()]
        public void WorkItemTestsInitialize()
        {
            workItems = new WorkItemsPage(browser);
            workItems.NavigateToWorkItems();
            Assert.IsTrue(workItems.CheckPageTitle());
        }

        [TestMethod]
        public void Home_CreateNewWorkItem_Test()
        {
            AddWorkItemPage addPage = workItems.NavigateToAddWorkItem();

            Assert.IsTrue(addPage.CheckPageTitle());

            addPage.InsertTitle("New WorkItem 4")
                   .InsertDescription("Some description.")
                   .SelectProject(2)
                   .SaveWorkItem();
        }

        [TestMethod]
        public void Home_UpdateWorkItem_Test()
        {
            EditWorkItemPage editPage = workItems.NavigateToEditWorkItem();

            Assert.IsTrue(editPage.CheckPageTitle());

            editPage.EditTitle("Edited WorkItem 1")
                    .EditDescription("Edited description")
                    .EditStatus(2)
                    .EditProject(2)
                    .UpdateWorkItem();
        }

        [TestMethod]
        public void Home_WorkItemDetails_Test()
        {
            WorkItemDetailsPage detailsPage = workItems.NavigateToDetails();

            Assert.IsTrue(detailsPage.CheckPageTitle());
        }

        [TestMethod]
        public void Home_DeleteWorkItem_Test()
        {
            DeleteWorkItemPage deletePage = workItems.DeleteWorkItem();
            deletePage.DeleteConfirmation();
        }

        [TestMethod]
        public void Home_CreateNewWorkItemDisplayValidation_Test()
        {
            AddWorkItemPage addPage = workItems.NavigateToAddWorkItem();

            Assert.IsTrue(addPage.CheckPageTitle());

            addPage.InsertTitle(" ")
                   .InsertDescription("Some description.")
                   .SelectProject(2)
                   .SaveWorkItem();

            Assert.IsTrue(addPage.IsValidationDisplayed());
        }

        [TestMethod]
        public void Home_UpdateWorkItemDisplayValidation_Test()
        {
            EditWorkItemPage editPage = workItems.NavigateToEditWorkItem();

            Assert.IsTrue(editPage.CheckPageTitle());

            editPage.EditTitle("E")
                    .EditDescription("Edited description")
                    .EditStatus(2)
                    .EditProject(2)
                    .UpdateWorkItem();

            Assert.IsTrue(editPage.IsValidationDisplayed());
        }
    }
}
