using DailyPlanning.Models.ProjectsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.WorkItemsViewModel
{
    public class WorkItemDetailsViewModel
    {
        public WorkItemViewModel WorkItem { get; set; }

        public ProjectViewModel Project { get; set; }
    }
}