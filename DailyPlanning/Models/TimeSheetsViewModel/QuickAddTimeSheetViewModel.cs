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
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",
              ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Task")]
        [Required]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Description cannot be shorter than 3 characters and longer than 500 characters.")]
        public string Task { get; set; }

        [Display(Name = "Comment")]
        [Required]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Description cannot be shorter than 3 characters and longer than 500 characters.")]
        public string Comment { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:%h}:{0:%m}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"(00|0[0-9]|1[0-9]|2[0-3]):([0-9]|[0-5][0-9]):([0-9]|[0-5][0-9])$", ErrorMessage = "Invalid Time.")]
        [Display(Name = "Time from")]
        public TimeSpan TimeFrom { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:%h}:{0:%m}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"(00|0[0-9]|1[0-9]|2[0-3]):([0-9]|[0-5][0-9]):([0-9]|[0-5][0-9])$", ErrorMessage = "Invalid Time.")]
        [Display(Name = "Time to")]
        public TimeSpan TimeTo { get; set; }

    }
}