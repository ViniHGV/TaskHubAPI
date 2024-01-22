using System.Collections.Generic;
using TaskHubAPI.Models;
using TaskHubAPI.ViewModels;

namespace TaskHubAPI.Services.Tasks
{
    public interface ITaskService
    {
        IEnumerable<Task> GetAllTasks();
        Task TaskById(int id);
        IEnumerable<Task> TasksByStatus(string statusTask);
        Task TaskByTitle(CreateTaskViewModel taskDTO);
        Task DeleteTask(int id);
        Task PostTask(CreateTaskViewModel taskDTO);
        Task UpdateTask(int id, CreateTaskViewModel taskDTO);
        IEnumerable<Task> GetAllTasksByUser(string nameUser);
    }
}