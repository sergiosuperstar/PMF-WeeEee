﻿using System;
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

namespace DailyPlanning.Controllers
{
    public class DailyPlansController : Controller
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

                var dailyPlansEntities = db.DailyPlans.AsEnumerable();
                IMapper iMapper = config.CreateMapper();
                var dailyPlanViewModel = iMapper.Map<IEnumerable<DailyPlan>, IEnumerable<DailyPlanViewModel>>(dailyPlansEntities);
                          
                return View(dailyPlanViewModel);
            }

            
        }
        public ActionResult AddDailyPlan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDailyPlan(DailyPlan dailyPlan)
        {
           

            return View();
        }

    }

    

      

    
}
