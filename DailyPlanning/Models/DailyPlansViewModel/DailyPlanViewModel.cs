using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Models.WorkItemsViewModel;
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

        public IEnumerable<WorkItemViewModel> DayBefore { get; set; }

        public IEnumerable<WorkItemViewModel> Today { get; set; }

        public string Note { get; set; }
    }
}