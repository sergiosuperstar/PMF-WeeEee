using DailyPlanning.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.DailyPlansViewModel
{
    public class ListOfDailyPlansViewModel
    {
        public IEnumerable<DailyPlanViewModel> DailyPlans { get; set; }
    }
}