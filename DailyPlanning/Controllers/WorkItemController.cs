using AutoMapper;
using DailyPlanning.ExtendedMethods;
using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Models.ProjectsViewModel;
using DailyPlanning.Models.WorkItemsViewModel;
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

        public WorkItemController(DailyPlanningContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkItem, WorkItemViewModel>());
            IMapper mapper = config.CreateMapper();


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
                var config = new MapperConfiguration(cfg => cfg.CreateMap<AddWorkItemViewModel, WorkItem>());
                IMapper mapper = config.CreateMapper();


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

            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkItem, UpdateWorkItemViewModel>());
            IMapper mapper = config.CreateMapper();


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
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateWorkItemViewModel, WorkItem>());
                IMapper mapper = config.CreateMapper();


                var updatedWorkItemEntity = mapper.Map<UpdateWorkItemViewModel, WorkItem>(workItemViewModel);
                updatedWorkItemEntity.Description = updatedWorkItemEntity.Description.Parse();
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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkItem, WorkItemViewModel>());
            IMapper mapper = config.CreateMapper();


            var workItemEntity = dbContext.WorkItems.Where(w => w.WorkItemID == id).FirstOrDefault();
            var workItemViewModel = mapper.Map<WorkItem, WorkItemViewModel>(workItemEntity);

            if (workItemViewModel != null)
            {
                var projectsEntity = dbContext.Projects.Where(p => p.ProjectID == workItemViewModel.ProjectID).FirstOrDefault();

                config = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectViewModel>());
                mapper = config.CreateMapper();
                var projectsViewModel = mapper.Map<Project, ProjectViewModel>(projectsEntity);

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