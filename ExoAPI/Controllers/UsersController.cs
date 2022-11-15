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
        private readonly UsersContext _context;
        public UsersController(IMapper mapper, UsersContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto dto)
        {
            User user = _mapper.Map<User>(dto);
            CreatePasswordHash(dto.Password, out  byte [] passwordHash,out byte[]passwordSalt);
            user.Name = dto.Name;
            user.PasswordHash = passwordHash;
            user.passwordSalt = passwordSalt; 
            return Ok(user);
        }

        [HttpPost("login/{name}")]
        public IActionResult Login(string name)
        {
            //User user = _mapper.Map<User>();
            if (_context.GetByName(name).Name != name)
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
            return Ok(_mapper.Map<UserDto>(_context.GetByName(name)));
        }
    }
}
