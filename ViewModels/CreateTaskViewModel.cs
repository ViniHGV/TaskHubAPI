using System.ComponentModel.DataAnnotations;

namespace TaskHubAPI.ViewModels
{
    public class CreateTaskViewModel
    {
        [Required]
        public string Title { get; set; }
        
        public string? Content { get; set; }

        [Required]
        public string Status { get; set; }
    }
}