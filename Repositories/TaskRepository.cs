using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskHubAPI.Context;
using TaskHubAPI.Interfaces;
using TaskHubAPI.ViewModels;
using Task = TaskHubAPI.Models.Task;

namespace TaskHubAPI.Repositories
{
    public class TaskRepository : IRepository
    {
        public async void DeleteTask([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var TaskSelected = await context.Tasks.FirstOrDefaultAsync(x => x.TaskId == id);
            context.Tasks.Remove(TaskSelected);
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<List<Task>> GetAllTasks(
            [FromServices] AppDbContext context)
        {
            await context.Tasks.ToListAsync();
            throw new NotImplementedException();
        }

        public async void PostTask(
            [FromServices] AppDbContext context,
            [FromBody] CreateTaskViewModel model)
        {
            var Task = new Task{
                Title = model.Title,
                Content = model.Content,
                Status = model.Status,
                UserId = model.UserId
            };

            await context.Tasks.AddAsync(Task);
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<Task> TaskForId(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            await context.Tasks.FirstOrDefaultAsync(x => x.TaskId == id);
            throw new NotImplementedException();
        }

        public async void UpdateTask([FromServices] AppDbContext context,
            [FromRoute] int id,
            [FromBody] CreateTaskViewModel model)
        {
            var TaskSelected = await context.Tasks.FirstOrDefaultAsync(x => x.TaskId == id);
            TaskSelected.Title = model.Title;
            TaskSelected.Content = model.Content;
            TaskSelected.Status = model.Status;
            context.Tasks.Update(TaskSelected);
            throw new NotImplementedException();
        }
    }
}