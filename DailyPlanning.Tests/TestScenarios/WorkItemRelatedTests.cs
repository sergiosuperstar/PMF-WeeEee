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
    public class WorkItemRelatedTests
    {
        private BrowserWindow browser;
        private WorkItemsPage workItems;

        [TestInitialize()]
        public void Initialize()
        {
            browser = BrowserWindow.Launch("http://localhost:54813");
            workItems = new WorkItemsPage(browser);
            workItems.Navigate();
        }

        [TestMethod]
        public void AddWorkItemTest()
        {
            AddWorkItemPage addPage = workItems.NavigateToAddWorkItem();

            Assert.IsTrue(addPage.CheckPageTitle());

            addPage.InsertTitle("New WorkItem 4")
                   .InsertDescription("Some description.")
                   .SelectProject(2)
                   .SaveWorkItem();
        }

        [TestMethod]
        public void EditProjectTest()
        {
            EditWorkItemPage editPage = workItems.NavigateToEditWorkItem();

            editPage.EditTitle("Edited WorkItem 1")
                    .EditDescription("Edited description")
                    .EditStatus(2)
                    .EditProject(2)
                    .UpdateWorkItem();
        }

        [TestMethod]
        public void WorkItemDetailsTest()
        {
            WorkItemDetailsPage detailsPage = workItems.NavigateToDetails();
        }

        [TestMethod]
        public void DeleteWorkItemTest()
        {
            DeleteWorkItemPage deletePage = workItems.DeleteWorkItem();
            deletePage.DeleteConfirmation();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            browser.Close();
        }
    }
}
