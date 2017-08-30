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
     

        public ICollection<WorkItemViewModel> Today { get; set; }

        [StringLength(500)]
        public string Note { get; set; }
    }
}