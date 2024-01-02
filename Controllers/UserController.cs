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
            var users = await context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> PostUsers(
            [FromServices] AppDbContext context,
            [FromBody] CreateUserView model)
        {
           var searchUser = await context
               .Users
               .FirstOrDefaultAsync(x => x.Name == model.Name || x.Email == model.Email);

           if(!ModelState.IsValid){
                return NotFound();
           }

           if(searchUser != null){
                return NotFound();
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
                return BadRequest();
           }


        }
        
    }

    
}