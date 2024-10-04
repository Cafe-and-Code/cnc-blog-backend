using AutoMapper;
using Blog.API.Commons;
using Blog.API.Models.DTO;
using Blog.API.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public AuthenController(IMapper mapper, IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userRepository.FindOneAsync(u => loginDTO.Username.Equals(u.Username));
            if (user == null)
            {
                return Unauthorized(Constants.InvalidUsernameOrPasswordMessage);
            }

            if (!loginDTO.Password.ValidatePassword(user.Password))
            {
                return Unauthorized(Constants.InvalidUsernameOrPasswordMessage);
            }

            var userRole = ((UserRole)user.Role).ToString();
            var token = _tokenRepository.CreateJWTToken(user, userRole);
            return Ok(token);
        }
    }
}
