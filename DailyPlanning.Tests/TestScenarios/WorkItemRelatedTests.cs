using System.Data.SqlClient;
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
        private int rowCountBefore;

        [TestInitialize()]
        public void WorkItemTestsInitialize()
        {
            workItems = new WorkItemsPage(browser);
            workItems.NavigateToWorkItems();
            
            Assert.IsTrue(workItems.CheckPageTitle());

            rowCountBefore = workItems.RowCount();
        }
       
       [TestMethod]
        public void Home_CreateNewWorkItem_ReturnsListWithAddedWorkItem()
        {
            AddWorkItemPage addPage = workItems.NavigateToAddWorkItem();

            Assert.IsTrue(addPage.CheckPageTitle());
            
            addPage.InsertTitle("New WorkItem 4")
                   .InsertDescription("Some description.")
                   .SelectStatus("TO_DO")
                   .SelectProject("Project 2")
                   .SaveWorkItem();

            Assert.IsTrue(rowCountBefore + 1 == addPage.RowCount());
        }

        [TestMethod]
        public void Home_UpdateWorkItem_ReturnsListWithUpdatedWorkItem()
        {
            EditWorkItemPage editPage = workItems.NavigateToEditWorkItem();

            Assert.IsTrue(editPage.CheckPageTitle());

            editPage.EditTitle("Edited WorkItem 1")
                    .EditDescription("Edited description")
                    .EditStatus("DONE")
                    .EditProject("Project 2")
                    .UpdateWorkItem();
        }

        [TestMethod]
        public void Home_WorkItemDetails_ReturnsDetailsAboutSelectedWorkItem()
        {
            WorkItemDetailsPage detailsPage = workItems.NavigateToDetails();

            Assert.IsTrue(detailsPage.CheckPageTitle());

            workItems = detailsPage.NavigateToWorkItemsPage();

            Assert.IsTrue(workItems.CheckPageTitle());

        }

        [TestMethod]
        public void Home_DeleteWorkItem_ReturnsListWithoutDeletedWorkItem()
        {
            DeleteWorkItemPage deletePage = workItems.DeleteWorkItem();
            deletePage.DeleteConfirmation();

            Assert.IsTrue(rowCountBefore - 1 == deletePage.RowCount());
        }

        [TestMethod]
        public void Home_CreateNewWorkItemDisplayValidation_ReturnsAddWorkItemPageWithValidationMessages()
        {
            AddWorkItemPage addPage = workItems.NavigateToAddWorkItem();

            Assert.IsTrue(addPage.CheckPageTitle());

            addPage.InsertTitle(" ")
                   .InsertDescription("Some description.")
                   .SelectStatus("TO_DO")
                   .SelectProject("Project 2")
                   .SaveWorkItem();

            Assert.IsTrue(addPage.IsValidationDisplayed());
        }

        [TestMethod]
        public void Home_UpdateWorkItemDisplayValidation_ReturnsEditWorkItemPageWithValidationMessages()
        {
            EditWorkItemPage editPage = workItems.NavigateToEditWorkItem();

            Assert.IsTrue(editPage.CheckPageTitle());

            editPage.EditTitle("E")
                    .EditDescription("Edited description")
                    .EditStatus("DONE")
                    .EditProject("Project 2")
                    .UpdateWorkItem();

            Assert.IsTrue(editPage.IsValidationDisplayed());
        }
    }
}
