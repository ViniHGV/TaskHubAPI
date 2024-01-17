using System.Collections.Generic;
using System.Linq;
using TaskHubAPI.Context;
using UserModel = TaskHubAPI.Models.User;
using TaskHubAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace TaskHubAPI.Services.User
{
    public class UserService : IUserService
    {
        public AppDbContext _userContext { get; set; }
        public UserService(AppDbContext _userContext)
        {
            this._userContext = _userContext;
        }
        public IEnumerable<UserModel> GetAllUsers()
        {
            return _userContext.Users
                .AsNoTracking()
                .Include(x => x.Tasks)
                .ToList();
        }

        public UserModel GetUserByEmail(string email)
        {
            var user = _userContext.Users
                .AsNoTracking()
                .Include(x => x.Tasks)
                .FirstOrDefault(x => x.Email == email);

             if(user == null)
                return null;

            return user;
        }

        public UserModel GetUserById(int id)
        {
            var user = _userContext.Users
                .AsNoTracking()
                .Include(x => x.Tasks)
                .FirstOrDefault(x => x.UserId == id);

            if(user == null)
                return null;

            return user;
        }

        public UserModel PostUsers(CreateUserView model)
        {
            var userSearch = GetUserByEmail(model.Email); 

            if(userSearch == null){
                
                var userPost = new UserModel{
                    Email = model.Email,
                    Name = model.Name,
                    Password = model.Password,
                };

                try{
                    _userContext.Users.Add(userPost);
                    _userContext.SaveChanges();
                } catch(Exception e){
                    throw new ArgumentException(e.Message);
                }
            }
            return userSearch;
        }

        public UserModel UpdateUser(int id, CreateUserView model)
        {
            var user = GetUserById(id);

            if(user == null)
                return null;

            user.Email = model.Email;
            user.Name = model.Name;
            user.Password = model.Password;

            try{

            _userContext.Users.Update(user);
            _userContext.SaveChanges();

            }catch(Exception e){
                throw new ArgumentException(e.Message);
            }
            return user;
        }

    }
}