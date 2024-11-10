using AutoMapper;
using Blog.API.Commons;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostCategoryRepository _postCategoryRepository;

        public PostsController(IMapper mapper, IPostRepository postRepository, ICategoryRepository categoryRepository, IPostCategoryRepository postCategoryRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _postCategoryRepository = postCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var posts = await _postRepository.GetAllAsync(pageNumber, pageSize);

            if (posts.Count == 0)
            {
                return NotFound(
                    new ApiErrorResponse(StatusCodes.Status404NotFound, Constants.NotFound, Constants.NotFound)
                );
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
            var post = await _postRepository.AddAsync(_mapper.Map<Post>(addPostDTO));

            if (addPostDTO.Categories != null)
            {
                var categories = await _categoryRepository.GetAllAsync();

                foreach (var category in addPostDTO.Categories)
                {
                    if (!categories.Any(c => category.Equals(c.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        await _categoryRepository.AddAsync(new Category() { Name = category });
                    }
                }

                var categoriesLowerCase = addPostDTO?.Categories?.Select(x => x.ToLower()).ToList() ?? new List<string>();
                var categoriesOfPost = await _categoryRepository.FindManyAsync(c => categoriesLowerCase.Contains(c.Name.ToLower()));

                await _postCategoryRepository.AddManyAsync(categoriesOfPost.Select(c => new PostCategory() { PostId = post.Id, CategoryId = c.Id, Post = post, Category = c }).ToList());
            }

            return Ok();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePostDTO updatePostDTO)
        {
            if (!await _postRepository.AnyAsync(post => post.Id == id))
            {
                return NotFound(
                    new ApiErrorResponse(StatusCodes.Status404NotFound, Constants.NotFound, Constants.NotFound)
                );
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
                return NotFound(
                    new ApiErrorResponse(StatusCodes.Status404NotFound, Constants.NotFound, Constants.NotFound)
                );
            }

            await _postRepository.DeleteAsync(user);
            return Ok();
        }
    }
}
