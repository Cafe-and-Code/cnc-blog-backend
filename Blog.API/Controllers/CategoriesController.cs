using AutoMapper;
using Blog.API.Repositories.IRepository;
using Blog.API.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
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
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var categories = await _categoryRepository.FindManyAsync(category => name.Contains(category.Name));
            return Ok(categories);
        }
    }
}
