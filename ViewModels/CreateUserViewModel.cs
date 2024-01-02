using System.ComponentModel.DataAnnotations;

namespace TaskHubAPI.ViewModels
{
    public class CreateUserView
    {
        [Required]
         public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}