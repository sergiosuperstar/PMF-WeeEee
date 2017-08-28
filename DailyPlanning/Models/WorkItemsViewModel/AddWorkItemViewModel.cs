using DailyPlanning.Infrastructure.Enums;

namespace DailyPlanning.Models.WorkItemsViewModel
{
    public class AddWorkItemViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public int ProjectID { get; set; }

    }
}