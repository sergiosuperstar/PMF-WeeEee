using DailyPlanning.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.DailyPlansViewModel
{
    public class DailyPlanViewModel
    {
        public int DailyPlanID { get; set; }

        public DateTime Date { get; set; }
        public ICollection<WorkItem> DayBefore { get; set; }
        public ICollection<WorkItem> Today { get; set; }
        public string Note { get; set; }
    }
}