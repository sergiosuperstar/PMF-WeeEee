using DailyPlanning.Models.DailyPlansViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyPlanning.Controllers
{
    public class DailyPlanController : Controller
    {
        // GET: DailyPlan
        public ActionResult Index()
        {
          
            return View();
        }
    }
}