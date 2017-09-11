using DailyPlanning.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DailyPlanning.Models.DailyPlansViewModel
{
    public class DailyPlanViewModel
    {
        public int DailyPlanID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm:ss}",
              ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public IEnumerable<WorkItem> DayBefore { get; set; } 

        public IEnumerable<WorkItem> Today { get; set; }

        public string Note { get; set; }
    }
}