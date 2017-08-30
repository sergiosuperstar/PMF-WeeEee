using DailyPlanning.Models.WorkItemsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.ProjectsViewModel
{
    public class ProjectDetailsViewModel
    {
        public ProjectViewModel Project { get; set; }

        public IEnumerable<WorkItemViewModel> WorkItems { get; set; }
    }
}