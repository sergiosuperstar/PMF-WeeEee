using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Entities;
using AutoMapper;
using DailyPlanning.Models.DailyPlansViewModel;
using DailyPlanning.Models.WorkItemsViewModel;
using DailyPlanning.Infrastructure.Enums;

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

        // GET: DailyPlans
        public ActionResult Index()
        {



            var dailyPlansEntities = context.DailyPlans.Where(dp => dp.DayBefore.Any(wi => context.WorkItems.Select(x => x.WorkItemID).Contains(wi.WorkItemID)) ||
                                                         dp.Today.Any(wi => context.WorkItems.Select(x => x.WorkItemID).Contains(wi.WorkItemID))).OrderBy(dp => dp.Date);

            var dailyPlanViewModel = mapper.Map<IEnumerable<DailyPlan>, IEnumerable<DailyPlanViewModel>>(dailyPlansEntities);

            return View(dailyPlanViewModel);



        }
        public ActionResult AddDailyPlan()
        {


            var dailyPlanViewModel = new AddDailyPlanViewModel();

            {

                var workItemEntitiesToday = context.WorkItems.Where(w => w.Status == Status.TO_DO || w.Status == Status.IN_PROGRESS);
                var workItemEntitiesDayBefore = context.WorkItems.Where(w => w.Status == Status.IN_PROGRESS || w.Status == Status.DONE);
                var workItemViewModelToday = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesToday); // pokupio sam podatke iz entiteta i stavio ih u ViewModel
                var workItemViewModelDayBefore = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesDayBefore);

                dailyPlanViewModel.Date = DateTime.Now; // setovao sam vreme
                dailyPlanViewModel.Today = workItemViewModelToday; // postavio dostupne Work Item-e
                dailyPlanViewModel.DayBefore = workItemViewModelDayBefore;
            }
            return View(dailyPlanViewModel);
        }

        [HttpPost]
        public ActionResult AddDailyPlan(AddDailyPlanViewModel newDailyPlanViewModel)
        {
            if (ModelState.IsValid)
            {


                var dailyPlanEntity = mapper.Map<AddDailyPlanViewModel, DailyPlan>(newDailyPlanViewModel);
                List<WorkItem> workItemsToday = new List<WorkItem>();
                List<WorkItem> workItemsDayBefore = new List<WorkItem>();

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

            var workItemEntitiesToday = context.WorkItems.Where(w => w.Status == Status.TO_DO || w.Status == Status.IN_PROGRESS);
            var workItemEntitiesDayBefore = context.WorkItems.Where(w => w.Status == Status.IN_PROGRESS || w.Status == Status.DONE);
            var workItemViewModelToday = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesToday); // pokupio sam podatke iz entiteta i stavio ih u ViewModel
            var workItemViewModelDayBefore = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesDayBefore);

            newDailyPlanViewModel.Date = DateTime.Now; // setovao sam vreme
            newDailyPlanViewModel.Today = workItemViewModelToday; // postavio dostupne Work Item-e
            newDailyPlanViewModel.DayBefore = workItemViewModelDayBefore;

            return View(newDailyPlanViewModel);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {



            var dailyPlanViewModel = new UpdateDailyPlanViewModel();

            var dailyPlanEntity = context.DailyPlans.Where(dp => dp.DailyPlanID == id).FirstOrDefault();
            dailyPlanViewModel = mapper.Map<DailyPlan, UpdateDailyPlanViewModel>(dailyPlanEntity);

            var workItemEntitiesToday = context.WorkItems.Where(w => w.Status == Status.TO_DO || w.Status == Status.IN_PROGRESS);
            var workItemEntitiesDayBefore = context.WorkItems.Where(w => w.Status == Status.IN_PROGRESS || w.Status == Status.DONE);
            var workItemViewModelToday = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesToday); // pokupio sam podatke iz entiteta i stavio ih u ViewModel
            var workItemViewModelDayBefore = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesDayBefore);

            dailyPlanViewModel.Date = DateTime.Now; // setovao sam vreme
            dailyPlanViewModel.Today = workItemViewModelToday; // postavio dostupne Work Item-e
            dailyPlanViewModel.DayBefore = workItemViewModelDayBefore;
            dailyPlanViewModel.SelectedWorkItemsDayBefore = dailyPlanEntity.DayBefore.Select(model => model.WorkItemID);
            dailyPlanViewModel.SelectedWorkItemsToday = dailyPlanEntity.Today.Select(model => model.WorkItemID);

            return View(dailyPlanViewModel);

        }

        [HttpPost]
        public ActionResult Edit(UpdateDailyPlanViewModel newDailyPlanViewModel)
        {

            if (ModelState.IsValid)
            {

                var dailyPlanEntity = context.DailyPlans.Find(newDailyPlanViewModel.DailyPlanID);


                //Remove deselected WorkItems for today
                dailyPlanEntity.Today.Where(m => !newDailyPlanViewModel.SelectedWorkItemsToday.Contains(m.WorkItemID))
                    .ToList().ForEach(workItem => dailyPlanEntity.Today.Remove(workItem));


                //add new work item for today if it already does not exist
                var existingWorkItemsToday = dailyPlanEntity.Today.Select(m => m.WorkItemID);
                context.WorkItems.Where(m => newDailyPlanViewModel.SelectedWorkItemsToday.Except(existingWorkItemsToday).Contains(m.WorkItemID))
                    .ToList().ForEach(workItem => dailyPlanEntity.Today.Add(workItem));

                //Remove deselected WorkItems for day before
                dailyPlanEntity.DayBefore.Where(m => !newDailyPlanViewModel.SelectedWorkItemsDayBefore.Contains(m.WorkItemID))
                    .ToList().ForEach(workItem => dailyPlanEntity.DayBefore.Remove(workItem));


                //add new work item for day before if it already does not exist
                var existingWorkItemsDayBefore = dailyPlanEntity.DayBefore.Select(m => m.WorkItemID);
                context.WorkItems.Where(m => newDailyPlanViewModel.SelectedWorkItemsDayBefore.Except(existingWorkItemsDayBefore).Contains(m.WorkItemID))
                    .ToList().ForEach(workItem => dailyPlanEntity.DayBefore.Add(workItem));

                dailyPlanEntity.Date = DateTime.Now;
                dailyPlanEntity.Note = newDailyPlanViewModel.Note;

                context.SaveChanges();
                return RedirectToAction("Index");


            }

            var workItemEntitiesToday = context.WorkItems.Where(w => w.Status == Status.TO_DO || w.Status == Status.IN_PROGRESS);
            var workItemEntitiesDayBefore = context.WorkItems.Where(w => w.Status == Status.IN_PROGRESS || w.Status == Status.DONE);
            var workItemViewModelToday = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesToday); // pokupio sam podatke iz entiteta i stavio ih u ViewModel
            var workItemViewModelDayBefore = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesDayBefore);

            newDailyPlanViewModel.Date = DateTime.Now; // setovao sam vreme
            newDailyPlanViewModel.Today = workItemViewModelToday; // postavio dostupne Work Item-e
            newDailyPlanViewModel.DayBefore = workItemViewModelDayBefore;

            return View(newDailyPlanViewModel);
        }

        public ActionResult Details(int id)
        {

            var dailyPlanEntity = context.DailyPlans.Where(model => model.DailyPlanID == id).FirstOrDefault();

            var dailyPlanViewModel = mapper.Map<DailyPlan, DetailsDailyPlanViewModel>(dailyPlanEntity);

            var detailsDailyPlanViewModel = new DetailsDailyPlanViewModel();
            detailsDailyPlanViewModel = dailyPlanViewModel;
            return View(detailsDailyPlanViewModel);

        }

    }

}
