using System.Collections.Generic;
using TaskHubAPI.Models;
using TaskHubAPI.ViewModels;

namespace TaskHubAPI.Services.Tasks
{
    public interface ITaskService
    {
        IEnumerable<Task> GetAllTasks();
        Task TaskForId(int id);
        Task TaskForTitle(CreateTaskViewModel taskDTO);
        Task DeleteTask(int id);
        Task PostTask(CreateTaskViewModel taskDTO);
        Task UpdateTask(int id, CreateTaskViewModel taskDTO);
    }
}