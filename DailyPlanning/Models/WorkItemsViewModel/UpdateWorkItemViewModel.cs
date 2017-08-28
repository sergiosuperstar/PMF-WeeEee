using DailyPlanning.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.WorkItemsViewModel
{
    public class UpdateWorkItemViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public int ProjectID { get; set; }
    }
}