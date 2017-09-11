using DailyPlanning.Models.ProjectsViewModel;

namespace DailyPlanning.Models.WorkItemsViewModel
{
    public class WorkItemDetailsViewModel
    {
        public WorkItemViewModel WorkItem { get; set; }

        public ProjectViewModel Project { get; set; }
    }
}