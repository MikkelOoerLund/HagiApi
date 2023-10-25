using AutoMapper;
using HagiDomain;
using Microsoft.AspNetCore.Mvc;

namespace HagiApi
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserRepository _userRepository;

        public AuthenticationController(IMapper mapper, UserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticationDTO userAuthenticationDTO)
        {
            var userName = userAuthenticationDTO.Username;

            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("Invalid username");
            }


            using (_userRepository)
            {
                var isUserRegistered = await _userRepository.HasUserWithUsernameAsync(userName);

                if (isUserRegistered)
                {
                    return BadRequest("Username is taken");
                }

                var user = _mapper.Map<User>(userAuthenticationDTO);

                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();

                return Ok("User is registered");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationDTO userAuthenticationDTO)
        {
            var userName = userAuthenticationDTO.Username;

            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("Invalid username");
            }


            using (_userRepository)
            {
                var isUserRegistered = await _userRepository.HasUserWithUsernameAsync(userName);

                if (isUserRegistered == false)
                {
                    return BadRequest("Invalid username or password");
                }

                var hashPassword = userAuthenticationDTO.HashPassword;
                var user = await _userRepository.GetUserWithUsernameAsync(userName);

                user = Assert.NotNull(user);

                if (user.HashPassword != hashPassword)
                {
                    return BadRequest("Invalid username or password");
                }

                return Ok("User is logged in");
            }
        }
    }
}
