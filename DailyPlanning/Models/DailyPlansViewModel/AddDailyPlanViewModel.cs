using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Models.WorkItemsViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.DailyPlansViewModel
{
    public class AddDailyPlanViewModel
    {

        public DateTime Date { get; set; }

        public ICollection<WorkItem> DayBefore { get; set; }

        public ICollection<WorkItemViewModel> Today { get; set; }

        [StringLength(500)]
        public string Note { get; set; }
    }
}