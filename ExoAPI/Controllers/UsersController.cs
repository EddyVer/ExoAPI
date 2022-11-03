using AutoMapper;
using ExoAPI.Context;
using ExoAPI.Dto;
using ExoAPI.Mapping;

using Microsoft.AspNetCore.Mvc;

namespace ExoAPI.Controllers
{   [ApiController]
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
        [HttpGet("GetByName/{name}/{password}")]
        public IActionResult FindUser(string name, string password)
        {
            return Ok(_mapper.Map<UserDto>(_context.GetByName(name, password)));
        }
    }
}
