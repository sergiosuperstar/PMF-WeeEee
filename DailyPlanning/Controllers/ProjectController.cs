using AutoMapper;
using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Models.ProjectsViewModel;
using DailyPlanning.Models.WorkItemsViewModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DailyPlanning.Controllers
{
    public class ProjectController : Controller
    {
        private DailyPlanningContext dbContext;
        private IMapper mapper;

        public ProjectController(DailyPlanningContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var projectsEntity = dbContext.Projects.Where(p => p.IsDeleted == false && p.IsEnabled == true).AsEnumerable();

            var projectsViewModel = mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projectsEntity);

            return View(projectsViewModel);

        }

        [HttpGet]
        public ActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProject(AddProjectViewModel newProjectViewModel)
        {
            if (ModelState.IsValid)
            {
                Project newProjectEntity = mapper.Map<AddProjectViewModel, Project>(newProjectViewModel);
                newProjectEntity.IsEnabled = true;
                dbContext.Projects.Add(newProjectEntity);
                dbContext.SaveChanges();

                return RedirectToAction("Index");

            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var projectEntity = dbContext.Projects.Where(p => p.ProjectID == id).FirstOrDefault();
            var projectViewModel = mapper.Map<Project, UpdateProjectViewModel>(projectEntity);

            if (projectViewModel != null)
            {
                return View(projectViewModel);
            }



            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(UpdateProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                var updatedProjectEntity = mapper.Map<UpdateProjectViewModel, Project>(projectViewModel);
                dbContext.Entry(updatedProjectEntity).State = EntityState.Modified;
                dbContext.SaveChanges();

                return RedirectToAction("Index");

            }

            return View();
        }

        public ActionResult Details(int id)
        {
            var projectEntity = dbContext.Projects.Where(p => p.ProjectID == id).FirstOrDefault();
            var projectViewModel = mapper.Map<Project, ProjectViewModel>(projectEntity);

            if (projectViewModel != null)
            {
                var workItemsEntity = dbContext.WorkItems.Where(w => w.ProjectID == projectViewModel.ProjectID).AsEnumerable();
                var workItemsViewModel = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemsEntity);

                var projectDetails = new ProjectDetailsViewModel();
                projectDetails.Project = projectViewModel;
                projectDetails.WorkItems = workItemsViewModel;

                return View(projectDetails);
            }

            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {

            var projectEntity = dbContext.Projects.Where(p => p.ProjectID == id).FirstOrDefault();

            if (projectEntity != null)
            {
                projectEntity.IsDeleted = true;
                projectEntity.IsEnabled = false;

                dbContext.Entry(projectEntity).State = EntityState.Modified;
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");

        }
    }
}