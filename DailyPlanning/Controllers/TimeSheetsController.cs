﻿using AutoMapper;
using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Models.TimeSheetsViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DailyPlanning.Controllers
{

    public class TimeSheetsController : Controller
    {
        private DailyPlanningContext dbContext;
        private IMapper mapper;

        public TimeSheetsController(DailyPlanningContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        // GET: TimeSheets
        public ActionResult Index()
        {
            var timeSheedEntity = dbContext.TimeSheets.OrderByDescending(w => w.TimeSheetID);

            var timeSheedViewModel = mapper.Map<IEnumerable<TimeSheet>, IEnumerable<TimeSheetViewModel>>(timeSheedEntity);
            var completeTimeSheedViewModel = new CompleteTimeSheetViewModel();
            completeTimeSheedViewModel.TimeSheets = timeSheedViewModel;

            completeTimeSheedViewModel.QuickAddTimeSheet = new QuickAddTimeSheetViewModel();
            completeTimeSheedViewModel.QuickAddTimeSheet.Date = DateTime.Now.Date;
            completeTimeSheedViewModel.QuickAddTimeSheet.TimeFrom = DateTime.Now.TimeOfDay;
            completeTimeSheedViewModel.QuickAddTimeSheet.TimeTo = DateTime.Now.TimeOfDay;
            completeTimeSheedViewModel.FinishTime = DateTime.Now.TimeOfDay;

            return View(completeTimeSheedViewModel);
        }

        [HttpPost]
        public ActionResult QuickAddTimeSheet(CompleteTimeSheetViewModel newTimeSheetViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTimeSheetEntity = mapper.Map<QuickAddTimeSheetViewModel, TimeSheet>(newTimeSheetViewModel.QuickAddTimeSheet);
                newTimeSheetEntity.Date = DateTime.Now;
                var last = dbContext.TimeSheets.OrderByDescending(w => w.TimeSheetID).FirstOrDefault();
                if (last.TimeTo == TimeSpan.Zero)
                {
                    last.TimeTo = newTimeSheetViewModel.QuickAddTimeSheet.TimeFrom;
                }

                var date = newTimeSheetEntity.Date;
                 date = DateTime.Now.Date;
                var dateOnly = new DateTime(date.Year, date.Month, date.Day);

                dbContext.TimeSheets.Add(newTimeSheetEntity);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            var timeSheetEntity = dbContext.TimeSheets.AsEnumerable().OrderByDescending(w => w.TimeSheetID);

            var timeSheetViewModel = mapper.Map<IEnumerable<TimeSheet>, IEnumerable<TimeSheetViewModel>>(timeSheetEntity);
            newTimeSheetViewModel.TimeSheets = timeSheetViewModel;
            return View("Index", newTimeSheetViewModel);
        }

        public ActionResult AddTimeTo(CompleteTimeSheetViewModel model)
        {
            if(ModelState.IsValid)
            {
                var existingTimeSheetEntity = dbContext.TimeSheets.Where(t => t.TimeSheetID == model.FinishTimeSheetID).FirstOrDefault();

                if (existingTimeSheetEntity != null)
                {
                    existingTimeSheetEntity.TimeTo = model.FinishTime;
                    dbContext.Entry(existingTimeSheetEntity).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

            }
            return RedirectToAction("Index");

        }
    }
}