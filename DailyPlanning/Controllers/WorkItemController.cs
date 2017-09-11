using AutoMapper;
using DailyPlanning.ExtendedMethods;
using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Models.ProjectsViewModel;
using DailyPlanning.Models.WorkItemsViewModel;
using Ganss.XSS;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DailyPlanning.Controllers
{
    public class WorkItemController : Controller
    {
        private DailyPlanningContext dbContext;
        private IMapper mapper;

        public WorkItemController(DailyPlanningContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns a view that displays list of WorkItems.
        /// </summary>
        /// <returns>View with list of all workitems</returns>
        public ActionResult Index()
        {            
            var workitemsEntity = dbContext.WorkItems.Where(w => w.IsDeleted == false && w.IsEnabled == true).AsEnumerable();

            var workitemsViewModel = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workitemsEntity);

            return View(workitemsViewModel);
        }

        /// <summary>
        /// Returns a view that displays form for adding new WorkItem.
        /// </summary>
        /// <param name="id">A <see cref="int"/> type representing an ProjectID.</param>
        /// <returns>View with input form for adding new workitem</returns>
        [HttpGet]
        public ActionResult AddWorkItem(int? id)
        {
            var model = new AddWorkItemViewModel();

            if (id != null)
            {
                model.ProjectID = id;
            }
            else
            {
                model.ListOfProjectIDs = getAllProjects();
            }

            return View(model);
        }

        /// <summary>
        /// Saves new WorkItem to database.
        /// </summary>
        /// <param name="newWorkItemViewModel">Object that contains information about WorkItem that will be saved in database.</param>
        /// <returns>If model is valid, returns view with list of all workitems, else returns view with input form</returns>
        [HttpPost]
        public ActionResult AddWorkItem(AddWorkItemViewModel newWorkItemViewModel)
        {
            if (ModelState.IsValid)
            {
                WorkItem newWorkItemEntity = mapper.Map<AddWorkItemViewModel, WorkItem>(newWorkItemViewModel);
                newWorkItemEntity.IsEnabled = true;
                newWorkItemEntity.Description = newWorkItemEntity.Description.Parse();
                dbContext.WorkItems.Add(newWorkItemEntity);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// Returns a view that displays form for editing existing WorkItem.
        /// </summary>
        /// <param name="id">Id of the WorkItem that will be updated.</param>
        /// <returns>Input form for updating selected workitem</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var workItemEntity = dbContext.WorkItems.Where(w => w.WorkItemID == id).FirstOrDefault();
            var workItemViewModel = mapper.Map<WorkItem, UpdateWorkItemViewModel>(workItemEntity);
            workItemViewModel.ListOfProjectIDs = getAllProjects();

            if (workItemViewModel != null)
            {
                return View(workItemViewModel);
            }
                
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Saves changes from existing WorkItem to database.
        /// </summary>
        /// <param name="workItemViewModel">Object that contains changed information about WorkItem that will be saved in database.</param>
        /// <returns>View with list of all workitems</returns>
        [HttpPost]
        public ActionResult Edit(UpdateWorkItemViewModel workItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var updatedWorkItemEntity = mapper.Map<UpdateWorkItemViewModel, WorkItem>(workItemViewModel);

                var sanitizer = new HtmlSanitizer();
                updatedWorkItemEntity.Description = sanitizer.Sanitize(updatedWorkItemEntity.Description);

                dbContext.Entry(updatedWorkItemEntity).State = EntityState.Modified;
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// Removes WorkItem from database.
        /// </summary>
        /// <param name="id">Represents an id of WorkItem that will be removed from database.</param>
        /// <returns>View with list od all workitems</returns>
        public ActionResult Delete(int id)
        {
            var workItemEntity = dbContext.WorkItems.Where(w => w.WorkItemID == id).FirstOrDefault();

            if (workItemEntity != null)
            {
                workItemEntity.IsDeleted = true;
                workItemEntity.IsEnabled = false;

                dbContext.Entry(workItemEntity).State = EntityState.Modified;
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns a view that displays information about WorkItem.
        /// </summary>
        /// <param name="id">Represents an id of WorkItem which information will be displayed.</param>
        /// <returns>View with details about selected workitem</returns>
        public ActionResult Details(int id)
        { 
            var workItemEntity = dbContext.WorkItems.Where(w => w.WorkItemID == id).FirstOrDefault();
            var workItemViewModel = mapper.Map<WorkItem, WorkItemViewModel>(workItemEntity);

            if (workItemViewModel != null)
            {
                var projectEntity = dbContext.Projects.Where(p => p.ProjectID == workItemViewModel.ProjectID).FirstOrDefault();
                var projectsViewModel = mapper.Map<Project, ProjectViewModel>(projectEntity);

                var workItemDetails = new WorkItemDetailsViewModel();
                workItemDetails.WorkItem = workItemViewModel;
                workItemDetails.Project = projectsViewModel;

                return View(workItemDetails);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Helper method for retrieving all projects from database and putting them in a list, which will be used for dropdown list.
        /// </summary>
        /// <returns>List of all projects</returns>
        private IEnumerable<SelectListItem> getAllProjects()
        {
            var allProjects = dbContext.Projects.Where(p => p.IsEnabled == true && p.IsDeleted == false).Select(p =>
                    new SelectListItem
                    {
                        Value = p.ProjectID.ToString(),
                        Text = p.Title
                    }).ToList();
            var projectIDs = new SelectList(allProjects, "Value", "Text");

            return projectIDs;
        }
    }
}