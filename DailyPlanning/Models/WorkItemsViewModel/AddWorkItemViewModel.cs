using DailyPlanning.Infrastructure.Enums;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DailyPlanning.Models.WorkItemsViewModel
{
    public class AddWorkItemViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public int ProjectID { get; set; }

        public IEnumerable<SelectListItem> ListOfProjectIDs { get; set; }
    }
}