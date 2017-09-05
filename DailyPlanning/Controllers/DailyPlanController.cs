using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Entities;
using AutoMapper;
using DailyPlanning.Models.DailyPlansViewModel;
using System.Collections;
using DailyPlanning.Models.WorkItemsViewModel;
using DailyPlanning.Infrastructure.Enums;
using System.Data.Entity.Infrastructure;

namespace DailyPlanning.Controllers
{
    public class DailyPlanController : Controller
    {
        DailyPlanningContext context;


        public DailyPlanController(DailyPlanningContext context)
        {
            this.context = context;
        }

        // GET: DailyPlans
        public ActionResult Index()
        {

            var dbContext = context;
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DailyPlan, DailyPlanViewModel>();
            });

            var dailyPlansEntities = dbContext.DailyPlans.Where(dp => dp.DayBefore.Any(wi => dbContext.WorkItems.Select(x => x.WorkItemID).Contains(wi.WorkItemID)) ||
                                                         dp.Today.Any(wi => dbContext.WorkItems.Select(x => x.WorkItemID).Contains(wi.WorkItemID))).OrderBy(dp => dp.Date);

            IMapper iMapper = config.CreateMapper();
            var dailyPlanViewModel = iMapper.Map<IEnumerable<DailyPlan>, IEnumerable<DailyPlanViewModel>>(dailyPlansEntities);

            return View(dailyPlanViewModel);



        }
        public ActionResult AddDailyPlan()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkItem, WorkItemViewModel>();
            });
            IMapper mapper = config.CreateMapper();

            var dailyPlanViewModel = new AddDailyPlanViewModel();
            var dbContext = context;
            {

                var workItemEntitiesToday = dbContext.WorkItems.Where(w => w.Status == Status.TO_DO || w.Status == Status.IN_PROGRESS);
                var workItemEntitiesDayBefore = dbContext.WorkItems.Where(w => w.Status == Status.IN_PROGRESS || w.Status == Status.DONE);
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
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddDailyPlanViewModel, DailyPlan>();
                cfg.CreateMap<WorkItemViewModel, WorkItem>();
                cfg.CreateMap<WorkItem, WorkItemViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            var dbContext = context;

            if (ModelState.IsValid)
            {


                var dailyPlanEntity = mapper.Map<AddDailyPlanViewModel, DailyPlan>(newDailyPlanViewModel);
                List<WorkItem> workItemsToday = new List<WorkItem>();
                List<WorkItem> workItemsDayBefore = new List<WorkItem>();

                foreach (var id in newDailyPlanViewModel.SelectedWorkItemsToday)
                {
                    var workItem = dbContext.WorkItems.Where(x => x.WorkItemID == id).FirstOrDefault();
                    workItemsToday.Add(workItem);
                }

                foreach (var id in newDailyPlanViewModel.SelectedWorkItemsDayBefore)
                {
                    var workItem = dbContext.WorkItems.Where(x => x.WorkItemID == id).FirstOrDefault();
                    workItemsDayBefore.Add(workItem);
                }
                dailyPlanEntity.Date = DateTime.Now;
                dailyPlanEntity.Today = workItemsToday;
                dailyPlanEntity.DayBefore = workItemsDayBefore;
                dbContext.DailyPlans.Add(dailyPlanEntity);
                dbContext.SaveChanges();

                return RedirectToAction("Index");


            }

            var workItemEntitiesToday = dbContext.WorkItems.Where(w => w.Status == Status.TO_DO || w.Status == Status.IN_PROGRESS);
            var workItemEntitiesDayBefore = dbContext.WorkItems.Where(w => w.Status == Status.IN_PROGRESS || w.Status == Status.DONE);
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

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DailyPlan, UpdateDailyPlanViewModel>();
                cfg.CreateMap<WorkItem, WorkItemViewModel>();
            });
            IMapper mapper = config.CreateMapper();

            var dailyPlanViewModel = new UpdateDailyPlanViewModel();

            var dbContext = context;
            var dailyPlanEntity = dbContext.DailyPlans.Where(dp => dp.DailyPlanID == id).FirstOrDefault();
            dailyPlanViewModel = mapper.Map<DailyPlan, UpdateDailyPlanViewModel>(dailyPlanEntity);

            var workItemEntitiesToday = dbContext.WorkItems.Where(w => w.Status == Status.TO_DO || w.Status == Status.IN_PROGRESS);
            var workItemEntitiesDayBefore = dbContext.WorkItems.Where(w => w.Status == Status.IN_PROGRESS || w.Status == Status.DONE);
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
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateDailyPlanViewModel, DailyPlan>();
                cfg.CreateMap<WorkItemViewModel, WorkItem>();
                cfg.CreateMap<WorkItem, WorkItemViewModel>();
            });
            IMapper mapper = config.CreateMapper();

            var dbContext = context;
            if (ModelState.IsValid)
            {



                var dailyPlanEntity = mapper.Map<UpdateDailyPlanViewModel, DailyPlan>(newDailyPlanViewModel);

                List<WorkItem> workItemsToday = new List<WorkItem>();
                List<WorkItem> workItemsDayBefore = new List<WorkItem>();

                foreach (var id in newDailyPlanViewModel.SelectedWorkItemsToday)
                {
                    var workItem = dbContext.WorkItems.Where(x => x.WorkItemID == id).FirstOrDefault();
                    workItemsToday.Add(workItem);
                }

                foreach (var id in newDailyPlanViewModel.SelectedWorkItemsDayBefore)
                {
                    var workItem = dbContext.WorkItems.Where(x => x.WorkItemID == id).FirstOrDefault();
                    workItemsDayBefore.Add(workItem);
                }


                //dbContext.DailyPlans.Find(newDailyPlanViewModel.DailyPlanID).Today.Where(m => m.WorkItemID);



                dailyPlanEntity.Date = DateTime.Now;
                dailyPlanEntity.Today = workItemsToday;
                dailyPlanEntity.DayBefore = workItemsDayBefore;
      
                dbContext.Entry(dailyPlanEntity).State = EntityState.Modified;
                dbContext.SaveChanges();


                return RedirectToAction("Index");


            }

            var workItemEntitiesToday = dbContext.WorkItems.Where(w => w.Status == Status.TO_DO || w.Status == Status.IN_PROGRESS);
            var workItemEntitiesDayBefore = dbContext.WorkItems.Where(w => w.Status == Status.IN_PROGRESS || w.Status == Status.DONE);
            var workItemViewModelToday = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesToday); // pokupio sam podatke iz entiteta i stavio ih u ViewModel
            var workItemViewModelDayBefore = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesDayBefore);

            newDailyPlanViewModel.Date = DateTime.Now; // setovao sam vreme
            newDailyPlanViewModel.Today = workItemViewModelToday; // postavio dostupne Work Item-e
            newDailyPlanViewModel.DayBefore = workItemViewModelDayBefore;


            return View(newDailyPlanViewModel);
        }

        public ActionResult Details(int id)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DailyPlan, DetailsDailyPlanViewModel>();
            });

            IMapper mapper = config.CreateMapper();

            var dbContext = context;
            var dailyPlanEntity = dbContext.DailyPlans.Where(model => model.DailyPlanID == id).FirstOrDefault();

            var dailyPlanViewModel = mapper.Map<DailyPlan, DetailsDailyPlanViewModel>(dailyPlanEntity);

            var detailsDailyPlanViewModel = new DetailsDailyPlanViewModel();
            detailsDailyPlanViewModel = dailyPlanViewModel;
            return View(detailsDailyPlanViewModel);





        }

    }

}
