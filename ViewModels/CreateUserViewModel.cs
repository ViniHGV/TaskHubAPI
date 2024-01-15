using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TaskHubAPI.ViewModels
{
    public class CreateUserView
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
         public string Name { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail invalido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string Password { get; set; }
    }
}