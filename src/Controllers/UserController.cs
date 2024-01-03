using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TaskHubAPI.Context;
using TaskHubAPI.ViewModels;
using User = TaskHubAPI.Models.User;



namespace TaskHubAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers(
            [FromServices] AppDbContext context
        )
        {
            var users = await context
                .Users
                .ToListAsync();
            return Ok(users);
        }
        
        [HttpGet]
        [Route("users/{id}")]
        public async Task<IActionResult> GetUserById(
            [FromServices] AppDbContext context,
            [FromRoute] int id
        )
        {
            var user = await context
                .Users
                .Include(x => x.Tasks)
                .FirstOrDefaultAsync(x => x.UserId == id);

            return user != null ? Ok(user) : NotFound("Usuário não encontrado!");
        }

        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> PostUsers(
            [FromServices] AppDbContext context,
            [FromBody] CreateUserView model)
        {
            
            
           var searchUser = await context
               .Users
               .FirstOrDefaultAsync(x => x.Email == model.Email);

           if(!ModelState.IsValid){
                return NotFound("As informações enviadas são invalidas!");
           }

           if(searchUser != null){
                return NotFound("Uma conta já foi criada com esse E-mail!");
           }

           var userSave = new User{
                Email = model.Email,
                Name = model.Name,
                Password = model.Password
           };

           try{

                await context.AddAsync(userSave);
                await context.SaveChangesAsync();
                return Ok(userSave);

           }catch (Exception e){
                return BadRequest(e);
           }
        }
        
    }

    
}