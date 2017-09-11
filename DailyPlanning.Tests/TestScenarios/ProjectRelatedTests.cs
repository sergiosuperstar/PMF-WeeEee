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
    public class ProjectRelatedTests : BaseTest
    {
        private ProjectsPage projects;
        private int rowCountBefore;

        [TestInitialize()]
        public void ProjectTestsInitialize()
        {
            projects = new ProjectsPage(browser);
            projects.NavigateToProjects();

            Assert.IsTrue(projects.CheckPageTitle());

            rowCountBefore = projects.RowCount();
        }

        [TestMethod]
        public void Home_CreateNewProject_ReturnsListWithAddedProject()
        {
            var addPage = projects.GoToAddProject();

            Assert.IsTrue(addPage.CheckPageTitle());

            addPage.AddTitle("New Project 4")
                   .SaveProject();

            Assert.IsTrue(rowCountBefore + 1 == addPage.RowCount());
        }

        [TestMethod]
        public void Home_UpdateProject_ReturnsListWithUpdatedProject()
        {
            var editPage = projects.GoToEditProject();

            Assert.IsTrue(editPage.CheckPageTitle());

            editPage.EditTitle("Edited Project Title")
                    .UpdateProject();
        }

        [TestMethod]
        public void Home_ProjectDetails_ReturnsDetailsAboutSelectedProject()
        {
            var detailsPage = projects.GoToProjectDetails();

            Assert.IsTrue(detailsPage.CheckPageTitle());

            projects = detailsPage.NavigateToProjectsPage();

            Assert.IsTrue(projects.CheckPageTitle());
        }

        [TestMethod]
        public void Home_DeleteProject_ReturnsListWithoutDeletedProject()
        {
            var deleteConfirmation = projects.DeleteProject();
            deleteConfirmation.DeleteConfirmation();

            Assert.IsTrue(rowCountBefore - 1 == deleteConfirmation.RowCount());
        }

        [TestMethod]
        public void Home_CreateNewProjectDisplayValidation_ReturnsAddProjectPageWithValidationMessages()
        {
            var addPage = projects.GoToAddProject();

            Assert.IsTrue(addPage.CheckPageTitle());

            addPage.AddTitle("N")
                   .SaveProject();

            Assert.IsTrue(addPage.IsValidationDisplayed());
        }

        [TestMethod]
        public void Home_UpdateProjectDisplayValidation_ReturnsEditProjectPageWithValidationMessages()
        {
            var editPage = projects.GoToEditProject();

            Assert.IsTrue(editPage.CheckPageTitle());

            editPage.EditTitle("E")
                    .UpdateProject();

            Assert.IsTrue(editPage.IsValidationDisplayed());
        }
    }
}
