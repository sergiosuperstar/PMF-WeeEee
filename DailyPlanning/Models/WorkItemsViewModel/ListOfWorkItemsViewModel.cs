using DailyPlanning.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyPlanning.Models.WorkItemsViewModel
{
    public class ListOfWorkItemsViewModel
    {
        public IEnumerable<WorkItemViewModel> WorkItems { get; set; }
    }
}