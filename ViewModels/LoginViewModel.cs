using System.ComponentModel.DataAnnotations;

namespace TaskHubAPI.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "O E-mail é inválido!")]
        [Required(ErrorMessage = "O campo E-mail é obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório!")]
        public string Password { get; set; }
    }
}