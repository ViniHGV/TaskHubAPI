using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TaskHubAPI.Context;
using TaskHubAPI.src.Services.Tasks;
using TaskHubAPI.ViewModels;
using Task = TaskHubAPI.Models.Task;

namespace TaskHubAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TaskController : ControllerBase
    {
        public TaskService _taskService { get; set; }
        public TaskController(TaskService _taskService)
        {
            this._taskService = _taskService;
        }

        [HttpGet]
        [Route("tasks")]
        public IActionResult GetAllTasks()
        {
            return Ok(_taskService.GetAllTasks());
        }

        [HttpGet]
        [Route("tasks/{id}")]
        public IActionResult TaskForId(
            [FromRoute] int id)
        {
            var task = _taskService.TaskForId(id);
            
            return task != null ? Ok(task) : NotFound("Tarefa não encontrada!");
        }

        [HttpDelete]
        [Route("tasks/{id}")]
        public IActionResult DeleteTask(
            [FromRoute] int id)
        {
            var task = _taskService.DeleteTask(id);

            if(task == null){
                return NotFound("Tarefa não Encontrada!");
            }

            return Ok("Tarefa deletada com sucesso!");
        }

        [HttpPost]
        [Route("tasks")]
        public IActionResult PostTask(
            [FromBody] CreateTaskViewModel model)
        {
            var tasks = _taskService.PostTask(model);
            
            if(!ModelState.IsValid){
                return NotFound("Modelo invalido!!");
            }


            if(tasks == null ){
                return NotFound("Tarefa já adicionada anteriormente!!");
            }

            return Ok("Tarefa adicionada com sucesso!");
        }

        [HttpPut]
        [Route("tasks/{id}")]
        public IActionResult UpdateTask(
            [FromRoute] int id,
            [FromBody] CreateTaskViewModel model)
        {
            var task = _taskService.TaskForId(id);
            
            var taskSearch = _taskService.TaskForTitle(model);

            if(task == null )
                return NotFound("Tarefa não encontrada!");

            if(taskSearch != null)
                return NotFound("Essa tarefa já existe!");
            
            try{

             _taskService.UpdateTask(id, model);
            return Ok("Tarefa editada com sucesso!");

            }catch(Exception e){
                return BadRequest(e);
            }
        }
    }
}