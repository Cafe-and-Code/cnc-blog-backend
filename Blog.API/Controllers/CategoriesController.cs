using AutoMapper;
using Blog.API.Models.Domain;
using Blog.API.Models.DTO;
using Blog.API.Repositories.IRepository;
using Blog.API.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var categories = await _categoryRepository.FindManyAsync(category => category.Name.Contains(name));
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryDTO addCategoryDTO)
        {
            var category = _mapper.Map<Category>(addCategoryDTO);
            var currentDateTime = DateTime.UtcNow;

            category.CreatedAt = currentDateTime;
            category.UpdatedAt = currentDateTime;

            await _categoryRepository.AddAsync(category);
            return Ok();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            if (!await _categoryRepository.AnyAsync(category => category.Id == id))
            {
                return NotFound();
            }

            var category = _mapper.Map<Category>(updateCategoryDTO);
            var currentDateTime = DateTime.UtcNow;
            category.UpdatedAt = currentDateTime;

            await _categoryRepository.UpdateAsync(category);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _categoryRepository.FindOneAsync(category => category.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            await _categoryRepository.DeleteAsync(user);
            return Ok();
        }
    }
}
