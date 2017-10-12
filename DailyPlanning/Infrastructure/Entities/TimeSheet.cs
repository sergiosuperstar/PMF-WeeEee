using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyPlanning.Infrastructure.Entities
{
    public class TimeSheet
    {
        public TimeSheet()
        {
        }

        [Key]
        public int TimeSheetID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(500)]
        public string Task { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        [Required]
        public TimeSpan TimeFrom { get; set; }

        [Required]
        public TimeSpan TimeTo { get; set; }


    }
}