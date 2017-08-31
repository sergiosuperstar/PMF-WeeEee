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

namespace DailyPlanning.Controllers
{
    public class DailyPlanController : Controller
    {

        // GET: DailyPlans
        public ActionResult Index()
        {

            using (var db = new DailyPlanningContext())
            {
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DailyPlan, DailyPlanViewModel>();
                });

                var dailyPlansEntities = db.DailyPlans.Where(dp => dp.DayBefore.Any(wi => db.WorkItems.Select(x => x.WorkItemID).Contains(wi.WorkItemID)) ||
                                                             dp.Today.Any(wi => db.WorkItems.Select(x => x.WorkItemID).Contains(wi.WorkItemID)));

                IMapper iMapper = config.CreateMapper();
                var dailyPlanViewModel = iMapper.Map<IEnumerable<DailyPlan>, IEnumerable<DailyPlanViewModel>>(dailyPlansEntities);

                return View(dailyPlanViewModel);
            }


        }
        public ActionResult AddDailyPlan()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkItem, WorkItemViewModel>();
            });
            IMapper mapper = config.CreateMapper();

            var dailyPlanViewModel = new AddDailyPlanViewModel();
            using (var dbContext = new DailyPlanningContext())
            {

                var workItemEntitiesToday = dbContext.WorkItems.Where(w => w.Status == Status.TODO || w.Status == Status.IN_PROGRESS);
                var workItemEntitiesDayBefore = dbContext.WorkItems.Where(w => w.Status == Status.IN_PROGRESS || w.Status == Status.DONE);
                var workItemViewModelToday = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesToday); // pokupio sam podatke iz entiteta i stavio ih u ViewModel
                var workItemViewModelDayBefore = mapper.Map<IEnumerable<WorkItem>, IEnumerable<WorkItemViewModel>>(workItemEntitiesDayBefore);

                dailyPlanViewModel.Date = DateTime.UtcNow; // setovao sam vreme
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
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AddDailyPlanViewModel, DailyPlan>();
                    cfg.CreateMap<WorkItemViewModel, WorkItem>();
                });
                IMapper mapper = config.CreateMapper();
                using (var dbContext = new DailyPlanningContext())
                {

                    var dailyPlanEntity = mapper.Map<AddDailyPlanViewModel, DailyPlan>(newDailyPlanViewModel);

                    // napraviti listu koja je tipa work items i dodati pronadjene iteme u nju

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

            }

            return View(newDailyPlanViewModel);
        }

        public ActionResult Edit(int id)
        {

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DailyPlan, UpdateDailyPlanViewModel>();
            });
            IMapper mapper = config.CreateMapper();

            //var dailyplan



            return View();
        }

    }






}
