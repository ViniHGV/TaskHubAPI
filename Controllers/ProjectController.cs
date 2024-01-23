using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TaskHubAPI.Services.Project;
using TaskHubAPI.ViewModels;

namespace TaskHubAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ProjectController : ControllerBase
    {
        private ProjectService _projectService { get; set; }
        public ProjectController(ProjectService _projectService)
        {
            this._projectService = _projectService;

        }
        [HttpGet]
        [Route("projects")]
        public IActionResult GetAllProjects(){
            var projetcs = _projectService.GetAllProjects();

            return Ok(projetcs);
        }

        [HttpPost]
        [Route("projects")]
        [Authorize]
        public IActionResult CreateProject([FromBody] CreateProjectViewModel ProjectDTO){
            var idUserLoggedin = int.Parse(User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);

            var projectDTO = new CreateProjectViewModel
            {
                IdUserCreated = idUserLoggedin,
                Title = ProjectDTO.Title,
            };

            var createProject = _projectService.CreateProject(projectDTO);
            if(createProject == null)
                return NotFound("Não foi possivel criar o projeto");

            return Ok(createProject);
        }

        [HttpGet]
        [Route("projects/{idProject}/user/{iduser}")]
        public IActionResult CreateUserInProject(
            [FromRoute] int idProject,
            [FromRoute] int iduser){

            var AddUserProject = _projectService.AddUserProject(iduser, idProject);

            if(AddUserProject == null)
                return NotFound("Não foi possivel adicionar o usuário no projeto!!");
                
            return Ok("Usuário adicionado ao Projeto com sucesso!");
        }
    }
}