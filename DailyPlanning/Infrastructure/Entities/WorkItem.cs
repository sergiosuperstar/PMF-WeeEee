using DailyPlanning.Infrastructure.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyPlanning.Infrastructure.Entities
{
    public class WorkItem
    {
        public WorkItem()
        {
            this.DailyPlans = new Collection<DailyPlan>();
        }

        [Key]
        public int WorkItemID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public Status Status { get; set; }
        
        public int ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }

        public ICollection<DailyPlan> DailyPlans { get; set; }
    }
}