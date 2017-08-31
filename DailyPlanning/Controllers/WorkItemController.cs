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
    public class WorkItemController : Controller
    {
        public ActionResult Index()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkItem, WorkItemViewModel>());
            IMapper mapper = config.CreateMapper();

            using (var dbContext = new DailyPlanningContext())
            {
                var workitemsEntity = dbContext.WorkItems.AsEnumerable();
                
                var workitemsViewModel = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workitemsEntity);
                
                return View(workitemsViewModel);
            }
        }

        [HttpGet]
        public ActionResult AddWorkItem()
        {
            using (var dbContext = new DailyPlanningContext())
            {
                var model = new AddWorkItemViewModel();
                
                model.ListOfProjectIDs = getAllProjects();

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult AddWorkItem(AddWorkItemViewModel newWorkItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<AddWorkItemViewModel, WorkItem>());
                IMapper mapper = config.CreateMapper();

                using (var dbContext = new DailyPlanningContext())
                {
                    WorkItem newWorkItemEntity = mapper.Map<AddWorkItemViewModel, WorkItem>(newWorkItemViewModel);
                    dbContext.WorkItems.Add(newWorkItemEntity);
                    dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkItem, UpdateWorkItemViewModel>());
            IMapper mapper = config.CreateMapper();

            using (var dbContext = new DailyPlanningContext())
            {
                var workItemEntity = dbContext.WorkItems.Where(w => w.WorkItemID == id).FirstOrDefault();
                var workItemViewModel = mapper.Map<WorkItem, UpdateWorkItemViewModel>(workItemEntity);
                workItemViewModel.ListOfProjectIDs = getAllProjects();

                if (workItemViewModel != null)
                    return View(workItemViewModel);
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Edit(UpdateWorkItemViewModel workItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateWorkItemViewModel, WorkItem>());
                IMapper mapper = config.CreateMapper();

                using (var dbContext = new DailyPlanningContext())
                {
                    var updatedWorkItemEntity = mapper.Map<UpdateWorkItemViewModel, WorkItem>(workItemViewModel);
                    dbContext.Entry(updatedWorkItemEntity).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View();
        }
        
        public ActionResult Delete(int id)
        {
            using (var dbContext = new DailyPlanningContext())
            {
                var workItemEntity = dbContext.WorkItems.Where(w => w.WorkItemID == id).FirstOrDefault();

                if (workItemEntity != null)
                {
                    dbContext.Entry(workItemEntity).State = EntityState.Deleted;
                    dbContext.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkItem, WorkItemViewModel>());
            IMapper mapper = config.CreateMapper();

            using (var dbContext = new DailyPlanningContext())
            {
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
        }

        private IEnumerable<SelectListItem> getAllProjects()
        {
            using (var dbContext = new DailyPlanningContext())
            {
                var allProjectIDs = dbContext.Projects.Select(p =>
                        new SelectListItem
                        {
                            Value = p.ProjectID.ToString(),
                            Text = p.Title
                        }).ToList();
                var projectIDs = new SelectList(allProjectIDs, "Value", "Text");

                return projectIDs;
            }
        }
    }
}