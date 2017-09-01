using DailyPlanning.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyPlanning.Controllers
{
    public class HomeController : Controller
    {
        DailyPlanningContext context;

        public HomeController(DailyPlanningContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}