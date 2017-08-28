using DailyPlanning.Infrastructure.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DailyPlanning.Infrastructure.Entities
{
    public class WorkItem
    {
        public WorkItem()
        {
            this.DailyPlans = new Collection<DailyPlan>();
        }
        public int WorkItemID { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }

        public ICollection<DailyPlan> DailyPlans { get; set; }
    }
}