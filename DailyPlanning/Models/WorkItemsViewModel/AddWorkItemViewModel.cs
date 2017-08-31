using DailyPlanning.Infrastructure.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DailyPlanning.Models.WorkItemsViewModel
{
    public class AddWorkItemViewModel
    {
        [Display(Name = "Title")]
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title cannot be shorter than 3 characters and longer than 100 characters.")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description cannot be shorter than 5 characters and longer than 500 characters.")]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Status")]
        [Required]
        public Status Status { get; set; }

        [Display(Name = "Project ID")]
        public int? ProjectID { get; set; }

        public IEnumerable<SelectListItem> ListOfProjectIDs { get; set; }
    }
}