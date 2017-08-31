using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Models.WorkItemsViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyPlanning.Models.DailyPlansViewModel
{
    public class AddDailyPlanViewModel
    {

        public DateTime Date { get; set; }

        public IEnumerable<int> SelectedWorkItemsToday { get; set; }

        public IEnumerable<int> SelectedWorkItemsDayBefore { get; set; }

        public IEnumerable<WorkItemViewModel> DayBefore { get; set; }

        public IEnumerable<WorkItemViewModel> Today { get; set; }

        [StringLength(500)]
        public string Note { get; set; }
    }
}