using AutoMapper;
using Blog.API.Models.Domain;
using Blog.API.Models.DTO;
using Blog.API.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public PostsController(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postRepository.GetAllAsync();

            if (posts.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<PostDTO>>(posts));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _postRepository.FindOneAsync(post => post.Id == id);
            return Ok(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] AddPostDTO addPostDTO)
        {
            await _postRepository.AddAsync(_mapper.Map<Post>(addPostDTO));

            return Ok();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            if (!await _postRepository.AnyAsync(post => post.Id == id))
            {
                return NotFound();
            }

            var post = _mapper.Map<Post>(updatePostDTO);
            post.Id = id;

            await _postRepository.UpdateAsync(post);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _postRepository.FindOneAsync(post => post.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            await _postRepository.DeleteAsync(user);
            return Ok();
        }
    }
}
