using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HealthcareApi.Data;
using HealthcareApi.DataTransferObjects;
using HealthcareApi.Interface;
using HealthcareApi.Models;
using HealthcareApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HealthcareApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepo;
        private Object _signinResp = new object();

        public IConfiguration _config;
        
        public UserController(IUser userRepository,IConfiguration config)
        {
            _userRepo = userRepository;
            _config = config;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
           
            var users = await Task.FromResult(_userRepo.GetAllUsers());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await Task.FromResult(_userRepo.GetUserById(id));
            if (user == null)
                return NotFound();
            else
                return Ok(user);
            
        }

        //get user by username..used for logging in
        [HttpGet("GetUserByUserName/{username}")]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            var user = await Task.FromResult(_userRepo.GetUserByUserName(username));

            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpPost("signin")]
        //Test Git Update
        public async Task<ActionResult<UserResponseDTO>> SignIn(string username, string password)
        {
            var user = await Task.FromResult(_userRepo.GetUserByUserName(username));
            if (user == null)
                return NotFound();
            else
            {
                if (user.password == password)
                {
                    var token = GetToken(username);
                    var signInResp = new UserResponseDTO(user.FirstName, user.LastName, user.Email);
                    signInResp.Token = token;
                    signInResp.cartid = user.Cart.Id;
                    signInResp.Id = user.Id;
                    signInResp.Role = user.Role;
                    return Ok(signInResp);
                }
                else
                {
                    return Problem("Invalid Credentials");
                }

            }
            return  Problem("Invalid Credentials");
        }

        [NonAction]
        private string GetToken(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                
            return jwt;
        }
        
        [HttpPost("signup")]
        public async Task<IActionResult> CreateUser(AddUserDTO user)
        {
            var newUser = _userRepo.AddUser(user);
            return Ok(newUser);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (user == null)
                return BadRequest();
            if (id != user.Id)
                return BadRequest();
            _userRepo.UpdateUser(user);
            return Ok(user);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            
            var user = _userRepo.GetUserById(id);
            if (user == null)
                return NotFound();
            else
            {
                _userRepo.DeleteUserById(id);
                return NoContent();
            }            
        }

        [HttpPost("NewAddress")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddNewAddress(AddUserAddressDTO addrdto)
        {

            var newUser = _userRepo.AddNewAddress(addrdto);
            return Ok(newUser);
        }
    }

}
