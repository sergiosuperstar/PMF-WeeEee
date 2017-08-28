using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models
{
    public class DailyPlan
    {
        public DateTime Date { get; set; }
        public ICollection<int> DayBefore { get; set; }

        public ICollection<int> Today { get; set; }

        public string Note { get; set; }

    }
}