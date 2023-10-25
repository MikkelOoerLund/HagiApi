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
            using (_userRepository)
            {
                var users = await _userRepository.GetAllAsync();
                return Ok(users);
            }
        }

        [HttpGet("{username:regex(.*)}")]
        public async Task<IActionResult> GetUserWithUsername(string username)
        {
            using (_userRepository)
            {
                var hasUser = await _userRepository.HasUserWithUsernameAsync(username);

                if (hasUser == false) return BadRequest($"User with name: {username} was not found");


                var user = await _userRepository.GetUserWithUsernameAsync(username);
                return Ok(user);
            }
        }

     

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AuthenticationDTO userAuthenticationDTO)
        {
            var user = _mapper.Map<User>(userAuthenticationDTO);

            using (_userRepository)
            {
                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();
            }

            return Ok(user);
        }




    }
}
