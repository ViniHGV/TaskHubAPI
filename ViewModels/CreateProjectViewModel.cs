using System.ComponentModel.DataAnnotations;
using TaskHubAPI.Models;

namespace TaskHubAPI.ViewModels
{
    public class CreateProjectViewModel
    {
        [Required(ErrorMessage = "É obrigatório informar o titulo do projeto para a criação!")]
        [MinLength(6, ErrorMessage = "É necessário que o titulo tenha pelo menos 6 caracteres")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "É obrigatório informar o id do usuário responsável pela criação do projeto!")]
        public int IdUserCreated { get; set; }

        // [Required(ErrorMessage = "É obrigatório informar o usuário responsável pela criação do projeto!")]
        // public User CreatedByUser { get; set; }
    }
}