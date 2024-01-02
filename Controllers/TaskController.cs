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
            [FromServices] AppDbContext context)
        {
            var listTasks = await context
                .Tasks
                .ToListAsync();
            return Ok(listTasks);
        }

        [HttpGet]
        [Route("tasks/{id}")]
        public async Task<IActionResult> TaskForId(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var task = await context
                .Tasks
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return task != null ? Ok(task) : NotFound();

        }

        [HttpDelete]
        [Route("tasks/{id}")]
        public async Task<IActionResult> DeleteTask(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var task = await context
                .Tasks
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if(task == null){
                return NotFound();
            }

            try{
                context.Remove(task);
                await context.SaveChangesAsync();
                return Ok();
            }catch{
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("tasks")]
        public async Task<IActionResult> PostTask(
            [FromServices] AppDbContext context,
            [FromBody] CreateTaskViewModel model)
        {
            var tasks = await context.Tasks.FirstOrDefaultAsync(x=> x.Title == model.Title);
            
            if(!ModelState.IsValid){
                return NotFound();
            }

            if(tasks != null){
                return NotFound();
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