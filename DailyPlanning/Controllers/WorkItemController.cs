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
using System.Text.RegularExpressions;
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

        public ActionResult Index()
        {            
            var workitemsEntity = dbContext.WorkItems.Where(w => w.IsDeleted == false && w.IsEnabled == true).AsEnumerable();

            var workitemsViewModel = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workitemsEntity);

            return View(workitemsViewModel);
        }

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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var workItemEntity = dbContext.WorkItems.Where(w => w.WorkItemID == id).FirstOrDefault();
            var workItemViewModel = mapper.Map<WorkItem, UpdateWorkItemViewModel>(workItemEntity);
            workItemViewModel.ListOfProjectIDs = getAllProjects();

            if (workItemViewModel != null)
                return View(workItemViewModel);


            return RedirectToAction("Index");
        }

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