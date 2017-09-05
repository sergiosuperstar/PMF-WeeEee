using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DailyPlanning.Infrastructure.Entities
{
    public class DailyPlan
    {
        public DailyPlan()
        {
            this.DayBefore = new HashSet<WorkItem>();
            //this.Today = new HashSet<WorkItem>();
        }

        [Key]
        public int DailyPlanID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Select one at least")]
        [InverseProperty("DailyPlansDayBefore")]
        public virtual ICollection<WorkItem> DayBefore { get; set; }

        [Required(ErrorMessage = "Select one at least")]
        [InverseProperty("DailyPlansToday")]
        public virtual ICollection<WorkItem> Today { get; set; }
        
        [StringLength(100)]
        public string Note { get; set; }
        
    }
}