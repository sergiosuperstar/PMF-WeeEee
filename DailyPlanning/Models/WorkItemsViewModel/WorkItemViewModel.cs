using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Infrastructure.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DailyPlanning.Models.WorkItemsViewModel
{
    public class WorkItemViewModel
    {
        [Display(Name = "ID")]
        public int WorkItemID { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "Status")]
        public Status Status { get; set; }

        [Display(Name = "Project ID")]
        public int ProjectID { get; set; }
        
        public Project Project { get; set; }

        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
    }
}