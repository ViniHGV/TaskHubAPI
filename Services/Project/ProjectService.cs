using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskHubAPI.Context;
using UserModel = TaskHubAPI.Models.User;
using TaskHubAPI.Services.User;
using TaskHubAPI.ViewModels;
using ProjectModel = TaskHubAPI.Models.Project;

namespace TaskHubAPI.Services.Project
{
    public class ProjectService : IProjectService
    {
        private AppDbContext _projectContext { get; set; }
        private UserService _userService { get; set; }
        public ProjectService(AppDbContext _projectContext, UserService _userService)
        {
            this._projectContext = _projectContext;
            this._userService = _userService;
        }
        public async Task<ProjectModel> CreateProject(CreateProjectViewModel ProjectDTO)
        {
            var searchProject = GetProjectByTitle(ProjectDTO.Title);

            var userCreatedProject = await _userService.GetUserById(ProjectDTO.IdUserCreated);

            if(searchProject != null)
                return null;

            var newProject = new ProjectModel {
                Title = ProjectDTO.Title,
                IdUser = ProjectDTO.IdUserCreated,
            };

            try{
                newProject.Users.Add(userCreatedProject);
                _projectContext.Projects.Add(newProject);
                await _projectContext.SaveChangesAsync();
                return newProject;
            }catch(Exception e){
               throw new ArgumentException(e.Message);
            }

        }

        public IEnumerable<ProjectModel> GetAllProjects()
        {
            var allProjects = _projectContext.Projects
            .Include(x => x.Users)
            .Include(x => x.Tasks)
            .ToList();

            return allProjects;
        }

        public ProjectModel GetProjectByTitle(string TitleProject)
        {
            var getProject = _projectContext.Projects.FirstOrDefault(x => x.Title == TitleProject);

            if (getProject == null)
                return null;

            return getProject;
        }

        public async Task<UserModel> AddUserProject(int idUser, int idProject)
        {
            var searchProject = await _projectContext.Projects.FirstOrDefaultAsync(x => x.IdProject == idProject);
            var searchUser = await _userService.GetUserById(idUser);

            if(searchProject == null)
                return null;

            searchProject.Users.Add(searchUser);
            _projectContext.SaveChanges();
            return searchUser; 
        }
    }
}