using System.Collections.Generic;
using TaskHubAPI.ViewModels;
using UserModel = TaskHubAPI.Models.User;

namespace TaskHubAPI.Services.User
{
    public interface IUserService
    {
     IEnumerable<UserModel> GetAllUsers();
     UserModel GetUserById(int id);
     UserModel GetUserByEmail(string email);
     UserModel PostUsers(CreateUserView model);
     UserModel UpdateUser(int id, CreateUserView model);
    }
}