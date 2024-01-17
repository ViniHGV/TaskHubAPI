using System.Collections.Generic;
using System.Linq;
using TaskHubAPI.Context;
using UserModel = TaskHubAPI.Models.User;
using TaskHubAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using TaskHubAPI.Security;

namespace TaskHubAPI.Services.User
{
    public class UserService : IUserService
    {

        private AppDbContext _userContext { get; set; }
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

        public UserModel PostUsers(CreateUserView userDTO)
        {
            var userSearch = GetUserByEmail(userDTO.Email); 

            if(userSearch == null){
                
                var userPost = new UserModel{
                    Email = userDTO.Email,
                    Name = userDTO.Name,
                    Password = EncryptedPassword.GeneratedEncryptedPassword(userDTO.Password),
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

        public UserModel UpdateUser(int id, CreateUserView userDTO)
        {
            var user = GetUserById(id);

            if(user == null)
                return null;

            user.Email = userDTO.Email;
            user.Name = userDTO.Name;
            user.Password = userDTO.Password;

            try{

            _userContext.Users.Update(user);
            _userContext.SaveChanges();

            }catch(Exception e){
                throw new ArgumentException(e.Message);
            }
            return user;
        }

        public bool LoginUser(LoginViewModel loginDTO)
        {
            var encryptedPasswordDTO = EncryptedPassword.GeneratedEncryptedPassword(loginDTO.Password);

            var searchAccont = _userContext.Users.FirstOrDefault(x => x.Email == loginDTO.Email && x.Password == encryptedPasswordDTO);

            if(searchAccont == null)
                return false;
            
            return true;
        }
    }
}