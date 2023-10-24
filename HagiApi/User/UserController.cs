using AutoMapper;
using HagiDomain;
using Microsoft.AspNetCore.Mvc;

namespace HagiApi
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserRepository _userRepository;

        public UserController(IMapper mapper, UserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserAuthenticationDTO userAuthenticationDTO)
        {
            var user = _mapper.Map<User>(userAuthenticationDTO);

            using (_userRepository)
            {
                await _userRepository.AddAsync(user);
            }

            return Ok(user);
        }
    }
}
