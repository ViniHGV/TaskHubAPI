using System.Collections.Generic;
using System.Linq;
using TaskHubAPI.Context;
using TaskHubAPI.Models;
using TaskHubAPI.Services.Tasks;
using TaskHubAPI.ViewModels;

namespace TaskHubAPI.src.Services.Tasks
{
    public class TaskService : ITaskService
    {
        public AppDbContext _taskContext { get; set; }
        public TaskService(AppDbContext _taskContext)
        {
            this._taskContext = _taskContext;
        }
        public IEnumerable<Task> GetAllTasks()
        {
            return _taskContext.Tasks.ToList();
        }

        public Task TaskForId(int id)
        {
            var Task = _taskContext.Tasks.FirstOrDefault(x => x.TaskId == id);
            
            if (Task == null){
                return null;
            }

            return Task;
        }

        public Task DeleteTask(int id)
        {
            var Task = TaskForId(id);

            if(Task == null){
                return null;
            }
          
             _taskContext.Tasks.Remove(Task);
             _taskContext.SaveChanges();
            return Task;
        }

        public Task PostTask(CreateTaskViewModel model)
        {
            var taskModel = _taskContext.Tasks.FirstOrDefault(x => x.Title == model.Title && x.UserId == model.UserId);

            if(taskModel != null ){
                return null;
            }

            var task = new Task
            {
                Title = model.Title,
                Content = model.Content,
                Status = model.Status,
                UserId = model.UserId,
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
            var taskSearch = TaskForId(id);

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

        public Task TaskForTitle(CreateTaskViewModel model)
        {
            var task = _taskContext.Tasks.FirstOrDefault(x => x.Title == model.Title);

            if(task == null){
                return null;
            }

            return task;
        }
    }
}