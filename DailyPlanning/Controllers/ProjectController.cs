﻿using AutoMapper;
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

        /// <summary>
        /// Returns a view that displays list of Projects.
        /// </summary>
        /// <returns>View with list of all projects</returns>
        public ActionResult Index()
        {
            var projectsEntity = dbContext.Projects.Where(p => p.IsDeleted == false && p.IsEnabled == true).AsEnumerable().OrderByDescending(p => p.ProjectID); ;

            var projectsViewModel = mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projectsEntity);

            return View(projectsViewModel);
        }

        /// <summary>
        /// Returns a view that displays form for adding new Project.
        /// </summary>
        /// <returns>View with input form for adding new project</returns>
        [HttpGet]
        public ActionResult AddProject()
        {
            return View();
        }

        /// <summary>
        /// Saves new Project to database.
        /// </summary>
        /// <param name="newProjectViewModel">Object that contains information about Project that will be saved in database.</param>
        /// <returns>If model is valid, returns view with list of all project, else returns view with input form</returns>
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

        /// <summary>
        /// Returns a view that displays form for editing existing Project.
        /// </summary>
        /// <param name="id">Id of the Project that will be updated.</param>
        /// <returns>Input form for updating selected project</returns>
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

        /// <summary>
        /// Saves changes from existing Project to database.
        /// </summary>
        /// <param name="projectViewModel">Object that contains changed information about Project that will be saved in database.</param>
        /// <returns>View with list of all projects</returns>
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

        /// <summary>
        /// Returns a view that displays information about Project.
        /// </summary>
        /// <param name="id">Represents an id of Project which information will be displayed.</param>
        /// <returns>View with details about selected project</returns>
        public ActionResult Details(int id)
        {
            var projectEntity = dbContext.Projects.Where(p => p.ProjectID == id).FirstOrDefault();
            var projectViewModel = mapper.Map<Project, ProjectViewModel>(projectEntity);

            if (projectViewModel != null)
            {
                var workItemsEntity = dbContext.WorkItems.Where(w => w.ProjectID == projectViewModel.ProjectID).AsEnumerable().OrderByDescending(w => w.WorkItemID);
                var workItemsViewModel = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemsEntity);

                var projectDetails = new ProjectDetailsViewModel();
                projectDetails.Project = projectViewModel;
                projectDetails.WorkItems = workItemsViewModel;

                return View(projectDetails);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Removes Project from database.
        /// </summary>
        /// <param name="id">Represents an id of Project that will be removed from database.</param>
        /// <returns>View with list od all projects</returns>
        public ActionResult Delete(int id)
        {
            var projectEntity = dbContext.Projects.Where(p => p.ProjectID == id).FirstOrDefault();
            var workItems = dbContext.WorkItems.Where(wi => wi.ProjectID == id);

            if (projectEntity != null)
            {
                projectEntity.IsDeleted = true;
                projectEntity.IsEnabled = false;
                foreach(var workItem in workItems)
                {
                    workItem.IsDeleted = true;
                    workItem.IsEnabled = false;
                    dbContext.Entry(workItem).State = EntityState.Modified;
                }

                dbContext.Entry(projectEntity).State = EntityState.Modified;

                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}