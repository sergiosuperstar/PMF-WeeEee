using AutoMapper;
using DailyPlanning.Infrastructure.Context;
using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Models.TimeSheetsViewModel;
using System;
using System.Collections.Generic;
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
            completeTimeSheedViewModel.QuickAddTimeSheet.Date = DateTime.Now;
            completeTimeSheedViewModel.QuickAddTimeSheet.TimeFrom = DateTime.Now.TimeOfDay;
            completeTimeSheedViewModel.QuickAddTimeSheet.TimeTo = DateTime.Now.TimeOfDay;

            return View(completeTimeSheedViewModel);
        }

        [HttpPost]
        public ActionResult QuickAddTimeSheet(CompleteTimeSheetViewModel newTimeSheetViewModel)
        {
            if (ModelState.IsValid)
            {
                var newTimeSheetEntity = mapper.Map<QuickAddTimeSheetViewModel, TimeSheet>(newTimeSheetViewModel.QuickAddTimeSheet);
                newTimeSheetEntity.Date = DateTime.Now;
                dbContext.TimeSheets.Add(newTimeSheetEntity);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
           
            return View("Index", newTimeSheetViewModel);
        }
    }
}