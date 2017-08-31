using DailyPlanning.Infrastructure.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DailyPlanning.Models.WorkItemsViewModel
{
    public class UpdateWorkItemViewModel
    {
        [Display(Name = "ID")]
        public int WorkItemID { get; set; }

        [Display(Name = "Title")]
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title cannot be shorter than 3 characters and longer than 100 characters.")]
        public string Title { get; set; }

        [AllowHtml]
        [Display(Name = "Description")]
        [Required]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Description cannot be shorter than 5 characters and longer than 500 characters.")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        [Required]
        public Status Status { get; set; }

        [Display(Name = "Project ID")]
        public int? ProjectID { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<SelectListItem> ListOfProjectIDs { get; set; }
    }
}