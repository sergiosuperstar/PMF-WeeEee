using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Models.WorkItemsViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.DailyPlansViewModel
{
    public class UpdateDailyPlanViewModel
    {
        public int DailyPlanID { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Select one item at least")]
        public IEnumerable<int> SelectedWorkItemsToday { get; set; }
        [Required(ErrorMessage = "Select one item at least")]
        public IEnumerable<int> SelectedWorkItemsDayBefore { get; set; }

        public IEnumerable<WorkItemViewModel> DayBefore { get; set; }

        public IEnumerable<WorkItemViewModel> Today { get; set; }

        [StringLength(100, ErrorMessage = "Note cannot be longer then 100 characters")]
        public string Note { get; set; }
    }
}