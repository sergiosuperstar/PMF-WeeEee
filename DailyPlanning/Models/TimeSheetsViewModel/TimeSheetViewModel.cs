using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.TimeSheetsViewModel
{
    public class TimeSheetViewModel
    {
        [Display(Name = "ID")]
        public int TimeSheetID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy }",
              ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Task")]
        public String Task { get; set; }

        [Display(Name = "Comment")]
        public String Comment { get; set; }
        
        [Display(Name = "Time from")]
        public TimeSpan TimeFrom { get; set; }

        [Display(Name = "Time to")]
        public TimeSpan TimeTo { get; set; }
    }
}