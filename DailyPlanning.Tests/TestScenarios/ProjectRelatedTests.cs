using DailyPlanning.Tests.Constants;
using DailyPlanning.Tests.Pages.ProjectPages;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyPlanning.Tests.TestScenarios
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class ProjectRelatedTests
    {
        private BrowserWindow browser;
        private ProjectsPage projects;

        [TestInitialize()]
        public void Initialize()
        {
            browser = BrowserWindow.Launch("http://localhost:54813");
            projects = new ProjectsPage(browser);
            projects.Navigate();
        }

        [TestMethod]
        public void AddProjectTest()
        {
            var addPage = projects.GoToAddProject();

            addPage.AddTitle("New Project 4")
                   .SaveProject();
        }

        [TestMethod]
        public void EditProjectTest()
        {
            var editPage = projects.GoToEditProject();

            editPage.EditTitle("Edited Project Title")
                    .UpdateProject();
        }

        [TestMethod]
        public void ProjectDetailsTest()
        {
            var detailsPage = projects.GoToProjectDetails();           
        }

        [TestMethod]
        public void DeleteProjectTest()
        {
            var deleteConfirmation = projects.DeleteProject();
            deleteConfirmation.DeleteConfirmation();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            browser.Close();
        }
    }
}
