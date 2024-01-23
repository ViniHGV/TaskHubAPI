using System.Collections.Generic;
using System.Threading.Tasks;
using UserModel = TaskHubAPI.Models.User;
using TaskHubAPI.ViewModels;
using ProjectModel =  TaskHubAPI.Models.Project;

namespace TaskHubAPI.Services.Project
{
    public interface IProjectService
    {
        Task<ProjectModel> CreateProject(CreateProjectViewModel ProjectDTO);
        Task<UserModel> AddUserProject(int idUser, int idProject);
        IEnumerable<ProjectModel> GetAllProjects();
        ProjectModel GetProjectByTitle(string TitleProject);
    }
}