using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskHubAPI.Models;

namespace TaskHubAPI.ViewModels
{
    public class CreateTaskViewModel
    {
        [Required(ErrorMessage = "O Campo titulo é obrigatório")]
        public string Title { get; set; }
        
        [MinLength(10, ErrorMessage = "O conteúdo deve ter no minimo 10 caracteres")]
        public string Content { get; set; }

        [Required(ErrorMessage = "O Campo Status é obrigatório")]
        public string Status { get; set; }
        
        [Required(ErrorMessage = "O Id do Usuário é obrigatório para o relacionamento")]
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}