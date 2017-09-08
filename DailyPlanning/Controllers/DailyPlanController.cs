using AutoMapper;
using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Infrastructure.Enums;
using DailyPlanning.Models.DailyPlansViewModel;
using DailyPlanning.Models.WorkItemsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DailyPlanning.Controllers
{
    public class DailyPlanController : Controller
    {
        DailyPlanningContext context;
        IMapper mapper;

        public DailyPlanController(DailyPlanningContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // This action get all Daily Plans. 
        public ActionResult Index()
        {
            var dailyPlansEntities = context.DailyPlans.Where(dp => dp.DayBefore.Any(wi => context.WorkItems
                                            .Select(x => x.WorkItemID).Contains(wi.WorkItemID)) || dp.Today.Any(wi => context.WorkItems
                                            .Select(x => x.WorkItemID)
                                            .Contains(wi.WorkItemID))).OrderBy(dp => dp.Date);
            var dailyPlanViewModel = mapper.Map<IEnumerable<DailyPlan>, IEnumerable<DailyPlanViewModel>>(dailyPlansEntities);
            return View(dailyPlanViewModel);
        }

        // Get action for a new Daily Plan
        [HttpGet]
        public ActionResult AddDailyPlan()
        {
            var dailyPlanViewModel = new AddDailyPlanViewModel();
            
            dailyPlanViewModel.Date = DateTime.Now;
            dailyPlanViewModel.Today = FillListWorkItemsToday();
            dailyPlanViewModel.DayBefore = FillListWorkItemsDayBefore();

            return View(dailyPlanViewModel);
        }

        // Post action for a new Daily Plan
        [HttpPost]
        public ActionResult AddDailyPlan(AddDailyPlanViewModel newDailyPlanViewModel)
        {
            if (ModelState.IsValid)
            {
                var dailyPlanEntity = mapper.Map<AddDailyPlanViewModel, DailyPlan>(newDailyPlanViewModel);
                var workItemsToday = new List<WorkItem>();
                var workItemsDayBefore = new List<WorkItem>();



                foreach (var id in newDailyPlanViewModel.SelectedWorkItemsToday)
                {
                    var workItem = context.WorkItems.Where(x => x.WorkItemID == id).FirstOrDefault();
                    workItemsToday.Add(workItem);
                }

                foreach (var id in newDailyPlanViewModel.SelectedWorkItemsDayBefore)
                {
                    var workItem = context.WorkItems.Where(x => x.WorkItemID == id).FirstOrDefault();
                    workItemsDayBefore.Add(workItem);
                }
                dailyPlanEntity.Date = DateTime.Now;
                dailyPlanEntity.Today = workItemsToday;
                dailyPlanEntity.DayBefore = workItemsDayBefore;
                context.DailyPlans.Add(dailyPlanEntity);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            newDailyPlanViewModel.Date = DateTime.Now;
            newDailyPlanViewModel.Today = FillListWorkItemsToday();
            newDailyPlanViewModel.DayBefore = FillListWorkItemsDayBefore();

            return View(newDailyPlanViewModel);
        }

        // Get action for Updating Daily Plan
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dailyPlanViewModel = new UpdateDailyPlanViewModel();

            var dailyPlanEntity = context.DailyPlans.Where(dp => dp.DailyPlanID == id).FirstOrDefault();
            dailyPlanViewModel = mapper.Map<DailyPlan, UpdateDailyPlanViewModel>(dailyPlanEntity);

            dailyPlanViewModel.Date = DateTime.Now;
            dailyPlanViewModel.Today = FillListWorkItemsToday();
            dailyPlanViewModel.DayBefore = FillListWorkItemsDayBefore();
            dailyPlanViewModel.SelectedWorkItemsDayBefore = dailyPlanEntity.DayBefore.Select(model => model.WorkItemID);
            dailyPlanViewModel.SelectedWorkItemsToday = dailyPlanEntity.Today.Select(model => model.WorkItemID);

            return View(dailyPlanViewModel);
        }

        // Post action for Updating Daily Plan
        [HttpPost]
        public ActionResult Edit(UpdateDailyPlanViewModel newDailyPlanViewModel)
        {
            if (ModelState.IsValid)
            {

                // Takes all unselected work items and remove them from today list
                var dailyPlanEntity = context.DailyPlans.Find(newDailyPlanViewModel.DailyPlanID);
                dailyPlanEntity.Today.Where(m => !newDailyPlanViewModel.SelectedWorkItemsToday.Contains(m.WorkItemID))
                    .ToList().ForEach(workItem => dailyPlanEntity.Today.Remove(workItem));
                
                // Takes all new selected work items and put them in list today list
                var existingWorkItemsToday = dailyPlanEntity.Today.Select(m => m.WorkItemID);
                context.WorkItems.Where(m => newDailyPlanViewModel.SelectedWorkItemsToday.Except(existingWorkItemsToday).Contains(m.WorkItemID))
                    .ToList().ForEach(workItem => dailyPlanEntity.Today.Add(workItem));

                // Takes all unselected work items and remove them from day before list
                dailyPlanEntity.DayBefore.Where(m => !newDailyPlanViewModel.SelectedWorkItemsDayBefore.Contains(m.WorkItemID))
                    .ToList().ForEach(workItem => dailyPlanEntity.DayBefore.Remove(workItem));
                // Takes all new selected work items and put them in list day before list
                var existingWorkItemsDayBefore = dailyPlanEntity.DayBefore.Select(m => m.WorkItemID);
                context.WorkItems.Where(m => newDailyPlanViewModel.SelectedWorkItemsDayBefore.Except(existingWorkItemsDayBefore).Contains(m.WorkItemID))
                    .ToList().ForEach(workItem => dailyPlanEntity.DayBefore.Add(workItem));

                dailyPlanEntity.Date = DateTime.Now;
                dailyPlanEntity.Note = newDailyPlanViewModel.Note;

                context.SaveChanges();
                return RedirectToAction("Index");
            }            
            newDailyPlanViewModel.Date = DateTime.Now;
            newDailyPlanViewModel.Today = FillListWorkItemsToday();
            newDailyPlanViewModel.DayBefore = FillListWorkItemsDayBefore();

            return View(newDailyPlanViewModel);
        }

        // Gets details about chosen daily plan
        public ActionResult Details(int id)
        {
            var dailyPlanEntity = context.DailyPlans.Where(model => model.DailyPlanID == id).FirstOrDefault();
            var dailyPlanViewModel = mapper.Map<DailyPlan, DetailsDailyPlanViewModel>(dailyPlanEntity);
            var detailsDailyPlanViewModel = new DetailsDailyPlanViewModel();

            detailsDailyPlanViewModel = dailyPlanViewModel;
            return View(detailsDailyPlanViewModel);
        }

        private IEnumerable<WorkItemViewModel> FillListWorkItemsToday()
        {
            var workItemEntitiesToday = context.WorkItems.Where(w => w.Status == Status.TO_DO || w.Status == Status.IN_PROGRESS);

            return mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesToday);

        }

        private IEnumerable<WorkItemViewModel> FillListWorkItemsDayBefore()
        {
            var workItemEntitiesDayBefore = context.WorkItems.Where(w => w.Status == Status.IN_PROGRESS || w.Status == Status.DONE);

            return mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesDayBefore);
        }
    }
}
