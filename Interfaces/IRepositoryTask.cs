using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TaskHubAPI.Context;
using TaskHubAPI.Models;
using TaskHubAPI.ViewModels;

namespace TaskHubAPI.Interfaces
{
    public interface IRepository
    {
        IAsyncEnumerable<List<Task>> GetAllTasks ( [FromServices] AppDbContext context);
        IAsyncEnumerable<Task> TaskForId(
                [FromServices] AppDbContext context,
                [FromRoute] int id);
            
        void DeleteTask(
                [FromServices] AppDbContext context,
                [FromRoute] int id);
                
        void PostTask(
                [FromServices] AppDbContext context,
                [FromBody] CreateTaskViewModel model);
                
        void UpdateTask(
                [FromServices] AppDbContext context,
                [FromRoute] int id,
                [FromBody] CreateTaskViewModel model);
        
    }
}