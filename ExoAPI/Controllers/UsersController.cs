using System.Security.Cryptography;
using AutoMapper;
using ExoAPI.Context;
using ExoAPI.Dto;
using ExoAPI.Entitie;
using ExoAPI.Mapping;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExoAPI.Controllers
{ 
    [ApiController]
    [Route("[Controller]")]
    
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly BusinessContext _businessContext;
        public UsersController(IMapper mapper, BusinessContext businessContext)
        {
            _mapper = mapper;
            _businessContext = businessContext;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto dto)
        {
            User user = _mapper.Map<User>(dto);
            CreatePasswordHash(dto.Password, out  byte [] passwordHash,out byte[]passwordSalt);
            user.PasswordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            _businessContext.Users.Add(user);
            _businessContext.SaveChanges();
            return Ok(dto);
        }

        [HttpPost("login/{name}")]
        public IActionResult Login(string name)
        {
            //User user = _mapper.Map<User>();
            if (_businessContext.Users.First(x => x.Name == name).Name != name)
            {
                return BadRequest("user not found");
            }
            return Ok("token");
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte [] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        [HttpGet("GetByName/{name}")]
        public IActionResult FindUser(string name)
        {
            return Ok(_mapper.Map<UserDto>(_businessContext.Users.First(x=> x.Name == name)));
        }
    }
}
