using AutoMapper;
using Blog.API.Commons;
using Blog.API.Models.Domain;
using Blog.API.Models.DTO;
using Blog.API.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UsersController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            if (!users.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<UserDTO>>(users));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _userRepository.FindOneAsync(user => user.Id == id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUserDTO addUserDTO)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(addUserDTO);
                var currentDateTime = DateTime.UtcNow;

                user.CreatedAt = currentDateTime;
                user.UpdatedAt = currentDateTime;
                user.Role = (int)UserRole.User;
                user.Password = user.Password.EncryptPassword();

                await _userRepository.AddAsync(user);
                return Ok();
            }
            
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            var user = await _userRepository.FindOneAsync(user => user.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var currentDateTime = DateTime.UtcNow;
            user.Id = id;
            user.UpdatedAt = currentDateTime;

            await _userRepository.UpdateAsync(user);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _userRepository.FindOneAsync(user => user.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(user);
            return Ok();
        }
    }
}
