﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyPlanning.Controllers
{
    public class WorkItemController : Controller
    {
        // GET: WorkItem
        public ActionResult Index()
        {
            return View();
        }
    }
}