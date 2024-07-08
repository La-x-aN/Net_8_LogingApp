using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Net_8_LogingApp.Models;

namespace Net_8_LogingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        public UsersController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;       
        }
        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(UserDTO userDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var objUser = dbContext.Users.FirstOrDefault(x => x.Email == userDTO.Email);
            if (objUser == null) 
            { 
            dbContext.Users.Add(new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = userDTO.Password
            });
            dbContext.SaveChanges();
                return Ok("User Registrated Successfully..");
            }
            else
            {
                return BadRequest("User already Exist with same Email Address..");
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var user = dbContext.Users.FirstOrDefault(x=>x.Email== loginDTO.Email && x.Password == loginDTO.Password );
            if (user != null) 
            {
                return Ok(user);
            }
            return NoContent();
        }
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(dbContext.Users.ToList());
        }
        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(int id)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == id);
            if (user != null)
                return Ok(user);
            else
                return NoContent();
        }
    }
}
