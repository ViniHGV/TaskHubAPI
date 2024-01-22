using System.Collections.Generic;
using TaskHubAPI.ViewModels;
using UserModel = TaskHubAPI.Models.User;

namespace TaskHubAPI.Services.User
{
    public interface IUserService
    {
     IEnumerable<UserModel> GetAllUsers();
     string LoginUser(LoginViewModel loginDTO);
     UserModel GetUserById(int id);
     UserModel GetUserByEmail(string email);
     UserModel PostUsers(CreateUserView userDTO);
     UserModel GetUser(CreateUserView userDTO);
     UserModel UpdateUser(int id, CreateUserView userDTO);
    }
}