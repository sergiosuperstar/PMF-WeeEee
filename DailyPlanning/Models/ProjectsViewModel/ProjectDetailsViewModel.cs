using DailyPlanning.Models.WorkItemsViewModel;
using System.Collections.Generic;

namespace DailyPlanning.Models.ProjectsViewModel
{
    public class ProjectDetailsViewModel
    {
        public ProjectViewModel Project { get; set; }

        public IEnumerable<WorkItemViewModel> WorkItems { get; set; }
    }
}