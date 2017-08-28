using DailyPlanning.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.DailyPlansViewModel
{
    public class UpdateDailyPlanViewModel
    {
        public IEnumerable<WorkItem> DayBefore { get; set; }
        public IEnumerable<WorkItem> Today { get; set; }

        public string Note { get; set; }
    }
}