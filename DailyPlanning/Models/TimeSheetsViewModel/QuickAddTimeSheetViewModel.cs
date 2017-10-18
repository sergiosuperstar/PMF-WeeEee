using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.TimeSheetsViewModel
{
    public class QuickAddTimeSheetViewModel
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}",
              ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Task")]
        [Required]
        public string Task { get; set; }

        [Display(Name = "Comment")]
        [Required]
        public string Comment { get; set; }

        [Display(Name = "Time from")]
        public TimeSpan TimeFrom { get; set; }

        [Display(Name = "Time to")]
        public TimeSpan TimeTo { get; set; }

    }
}