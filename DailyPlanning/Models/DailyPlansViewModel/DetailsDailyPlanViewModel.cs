using DailyPlanning.Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace DailyPlanning.Models.DailyPlansViewModel
{
    public class DetailsDailyPlanViewModel
    {

 
        public int DailyPlanID { get; set; }

   
        public DateTime Date { get; set; }

        
        public virtual ICollection<WorkItem> DayBefore { get; set; }

       
        public virtual ICollection<WorkItem> Today { get; set; }

   
        public string Note { get; set; }
    }
}