using System.ComponentModel.DataAnnotations;

namespace DailyPlanning.Models.ProjectsViewModel
{
    public class UpdateProjectViewModel
    {
        [Display(Name = "ID")]
        public int ProjectID { get; set; }

        [Display(Name = "Title")]
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title cannot be shorter than 3 characters and longer than 100 characters.")]
        public string Title { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsDeleted { get; set; }
    }
}