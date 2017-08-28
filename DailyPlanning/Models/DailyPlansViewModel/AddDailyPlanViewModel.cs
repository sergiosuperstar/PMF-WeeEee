using DailyPlanning.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.DailyPlansViewModel
{
    public class AddDailyPlanViewModel
    {
        public ICollection<WorkItem> Today { get; set; } 

        public string Note { get; set; }
    }
}