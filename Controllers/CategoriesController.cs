using ExpenseTrackerAPI.Dtos.CategoriesDtos;
using ExpenseTrackerAPI.Responses;
using ExpenseTrackerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //GET /api/categories
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PagedResponse<CategoryDto>>> GetCategories(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                    return BadRequest("pageNumber and pageSize must be positive integers.");


                var response = await _categoryService.GetCategoriesAsync(pageNumber, pageSize);
                var totalCount = await _categoryService.GetTotalCategoriesCountAsync();

                return Ok(new PagedResponse<CategoryDto>
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    Data = response
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        //GET /api/categories/id
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CategoryDto?>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            return category is null ? NotFound() : Ok(category);
        }



    }
}
