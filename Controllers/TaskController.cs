using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TaskHubAPI.src.Services.Tasks;
using TaskHubAPI.ViewModels;

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
        public IActionResult TaskById(
            [FromRoute] int id)
        {
            var task = _taskService.TaskById(id);
            
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
            var task = _taskService.TaskById(id);
            
            var taskSearch = _taskService.TaskByTitle(model);

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

        [HttpGet]
        [Route("tasks/status/{statusTask}")]
        public IActionResult GetTaskByStatus (
            [FromRoute] string statusTask) 
        {
            var tasksByStatus = _taskService.TasksByStatus(statusTask);

            if(tasksByStatus == null)
                return NotFound("Não existem tarefas com esse status");

            return Ok(tasksByStatus);
        }
    }
}