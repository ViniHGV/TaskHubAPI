using System.Collections.Generic;
using System.Linq;
using TaskHubAPI.Context;
using UserModel = TaskHubAPI.Models.User;
using TaskHubAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using TaskHubAPI.Security;
using TaskHubAPI.Services.Token;
using System.Threading.Tasks;

namespace TaskHubAPI.Services.User
{
    public class UserService : IUserService
    {

        private AppDbContext _userContext { get; set; }
        private TokenService _tokenService { get; set; }

        public UserService(AppDbContext _userContext, TokenService _tokenService)
        {
            this._userContext = _userContext;
            this._tokenService = _tokenService;
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

        public async Task<UserModel> GetUserById(int id)
        {
            var user = await _userContext.Users
                .AsNoTracking()
                .Include(x => x.Tasks)
                .FirstOrDefaultAsync(x => x.UserId == id);

            if(user == null)
                return null;

            return user;
        }

        public UserModel PostUsers(CreateUserView userDTO)
        {
            var userSearch = GetUser(userDTO); 

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

        public async Task<UserModel> UpdateUser(int id, CreateUserView userDTO)
        {
            var user = await GetUserById(id);

            if(user == null)
                return null;

            user.Email = userDTO.Email;
            user.Name = userDTO.Name;
            user.Password = userDTO.Password;

            try{

            _userContext.Users.Update(user);
           await _userContext.SaveChangesAsync();

            }catch(Exception e){
                throw new ArgumentException(e.Message);
            }
            return user;
        }

        public string LoginUser(LoginViewModel loginDTO)
        {
            var encryptedPasswordDTO = EncryptedPassword.GeneratedEncryptedPassword(loginDTO.Password);

            var searchAccont = _userContext.Users
                .FirstOrDefault(x => x.Email == loginDTO.Email && x.Password == encryptedPasswordDTO);

            if(searchAccont == null)
                return null;
            
            var token = _tokenService.GenerateToken(searchAccont);

            return token;
        }

        public UserModel GetUser(CreateUserView userDTO)
        {
            var getUser = _userContext.Users.FirstOrDefault(x => x.Email == userDTO.Email && x.Name == userDTO.Name);

            if(getUser == null)
                return null;

            return getUser;
        }
    }
}