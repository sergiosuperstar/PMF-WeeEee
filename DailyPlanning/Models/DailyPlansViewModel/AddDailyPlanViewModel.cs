﻿using DailyPlanning.Models.WorkItemsViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DailyPlanning.Models.DailyPlansViewModel
{
    public class AddDailyPlanViewModel
    {

        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please select at least one item")]
        public IEnumerable<int> SelectedWorkItemsToday { get; set; }
        [Required(ErrorMessage = "Please select at least one item")]
        public IEnumerable<string> SelectedWorkItemsDayBefore { get; set; }
        
        public IEnumerable<WorkItemViewModel> DayBefore { get; set; }
        
        public IEnumerable<WorkItemViewModel> Today { get; set; }

        [StringLength(100, ErrorMessage = "Note cannot be longer then 100 characters")]
        public string Note { get; set; }
      
    }
}