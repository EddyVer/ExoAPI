using AutoMapper;
using ExoAPI.Context;
using Microsoft.AspNetCore.Mvc;

namespace ExoAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EntrepotController
    {
        private readonly IMapper _mapper;
        private readonly BusinessContext _businessContext;
    }
}
