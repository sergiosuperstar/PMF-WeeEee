using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyPlanning.Infrastructure.Entities
{
    public class DailyPlan
    {
        public DailyPlan()
        {
            this.DayBefore = new Collection<WorkItem>();
            this.Today = new Collection<WorkItem>();
        }

        [Key]
        public int DailyPlanID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public ICollection<WorkItem> DayBefore { get; set; }

        public ICollection<WorkItem> Today { get; set; }
        [StringLength(500)]
        public string Note { get; set; }
        
    }
}