using DailyPlanning.Infrastructure.Enums;

namespace DailyPlanning.Infrastructure.Entities
{
    public class WorkItem
    {
        public WorkItem()
        {
        }
        public int WorkItemID { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }
    }
}