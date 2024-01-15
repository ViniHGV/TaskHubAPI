using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TaskHubAPI.Services.User;
using TaskHubAPI.ViewModels;

namespace TaskHubAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserController : ControllerBase
    {
        public UserService _userService { get; set; }
        public UserController(UserService _userService)
        {
            this._userService = _userService;
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }
        
        [HttpGet]
        [Route("users/{id}")]
        public IActionResult GetUserById(
            [FromRoute] int id
        )
        {
            var user = _userService.GetUserById(id);

            if(user == null)
                return NotFound("Usuário não encontrado!");

            return Ok(user);
        }

        [HttpPost]
        [Route("users")]
        public IActionResult PostUsers(
            [FromBody] CreateUserView model)
        {
           var searchUser = _userService.PostUsers(model);

           if(!ModelState.IsValid){
                return NotFound("As informações enviadas são invalidas!");
           }

           if(searchUser != null){
                return NotFound("Uma conta já foi criada com esse E-mail!");
           }

            return Ok("Usuário criado com sucesso!");
        }

        [Route("users/{id}")]
        [HttpPut]
        public IActionResult UpdateUser(int id,    
        [FromBody] CreateUserView model){
            var user = _userService.UpdateUser(id, model);

            if(user == null)
                return NotFound("Usuário não encontrado!");

            return Ok("Usuário editado com sucesso!");
        }
        
    }

    
}