using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TaskHubAPI.Context;
using TaskHubAPI.ViewModels;
using Task = TaskHubAPI.Models.Task;

namespace TaskHubAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TaskController : ControllerBase
    {

        [HttpGet]
        [Route("tasks")]
        public async Task<IActionResult> GetAllTasks(
            [FromServices] AppDbContext context
        ){
            var listTasks = await context
                .Tasks
                .ToListAsync();
            return Ok(listTasks);
        }

        [HttpPost]
        [Route("tasks")]
        public async Task<IActionResult> PostTask(
            [FromServices] AppDbContext context,
            [FromBody] CreateTaskViewModel model){

            if(!ModelState.IsValid){
                return BadRequest();
            }

            var task = new Task
            {
                Title = model.Title,
                Content = model.Content,
                Status = model.Status,
            };

            try{

            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();
            return Ok(task);

            }catch(Exception e){
                return BadRequest(e);
            }
            
        }

    }
}