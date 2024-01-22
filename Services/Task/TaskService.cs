using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskHubAPI.Context;
using TaskHubAPI.Models;
using TaskHubAPI.Services.Tasks;
using TaskHubAPI.ViewModels;

namespace TaskHubAPI.src.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private AppDbContext _taskContext { get; set; }
        public TaskService(AppDbContext _taskContext)
        {
            this._taskContext = _taskContext;
        }
        public IEnumerable<Task> GetAllTasks()
        {
            return _taskContext.Tasks
                .AsNoTracking()
                .Include(x => x.User)
                .ToList();
        }

        public Task TaskById(int id)
        {
            var Task = _taskContext.Tasks
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefault(x => x.TaskId == id);
            
            if (Task == null){
                return null;
            }

            return Task;
        }

        public Task DeleteTask(int id)
        {
            var Task = TaskById(id);

            if(Task == null){
                return null;
            }
          
             _taskContext.Tasks.Remove(Task);
             _taskContext.SaveChanges();
            return Task;
        }

        public Task PostTask(CreateTaskViewModel taskDTO)
        {
            var taskModel = _taskContext.Tasks
                .FirstOrDefault(x => x.Title == taskDTO.Title && x.UserId == taskDTO.UserId);

            if(taskModel != null ){
                return null;
            }

            var task = new Task
            {
                Title = taskDTO.Title,
                Content = taskDTO.Content,
                Status = taskDTO.Status,
                UserId = taskDTO.UserId,
            };

            try{

            _taskContext.Tasks.Add(task);
            _taskContext.SaveChanges();

            return task;
            } catch{
                return null;
            }

        }

        public Task UpdateTask(int id, CreateTaskViewModel model)
        {
            var taskSearch = TaskById(id);

            if(taskSearch == null){
                return null;
            }

            taskSearch.Title = model.Title;
            taskSearch.Content = model.Content;
            taskSearch.Status = model.Status;
            
            _taskContext.Update(taskSearch);
            _taskContext.SaveChanges();
            return taskSearch;
        }

        public Task TaskByTitle(CreateTaskViewModel model)
        {
            var task = _taskContext.Tasks
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefault(x => x.Title == model.Title);

            if(task == null){
                return null;
            }

            return task;
        }

        public IEnumerable<Task> TasksByStatus(string statusTask)
        {
            var tasksForStatus = _taskContext.Tasks.Where(x => x.Status == statusTask).ToList();

            if(tasksForStatus == null)
                return null;

            return tasksForStatus;    
        }

        public IEnumerable<Task> GetAllTasksByUser(string nameUser)
        {
            var TasksByUser = _taskContext.Tasks
                .Include(x => x.User)
                .Where(x => x.User.Name == nameUser)
                .ToList();
            
            if(TasksByUser == null)
                return null;
                
            return TasksByUser;;
        }
    }
}