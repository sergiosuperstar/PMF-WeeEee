using DailyPlanning.Infrastructure.Entities;
using DailyPlanning.Infrastructure.Enums;
using System;

namespace DailyPlanning.Models.WorkItemsViewModel
{
    public class WorkItemViewModel
    {
        public int WorkItemID { get; set; }

        public string Title { get; set; }

        public String Description { get; set; }

        public Status Status { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; }
    }
}