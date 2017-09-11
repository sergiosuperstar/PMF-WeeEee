using System.ComponentModel.DataAnnotations;

namespace DailyPlanning.Models.ProjectsViewModel
{
    public class AddProjectViewModel
    {
        [Display(Name = "Title")]
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title cannot be shorter than 3 characters and longer than 100 characters.")]
        public string Title { get; set; }
    }
}