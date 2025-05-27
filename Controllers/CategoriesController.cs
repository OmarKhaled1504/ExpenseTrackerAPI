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

        //POST /api/categories/
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(CategoryDto), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CategoryDto>> PostCategory(CategoryCreateDto dto)
        {
            var created = await _categoryService.CreateCategoryAsync(dto);
            return created is null ? BadRequest("Category already exists.") : CreatedAtAction(nameof(GetCategory), new { id = created.Id }, created);
        }

        //PUT /api/categories/1
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CategoryDto>> PutCategory(int id, CategoryUpdateDto dto)
        {
            var updated = await _categoryService.UpdateCategoryAsync(id, dto);
            return updated is null ? NotFound() : Ok(updated);
        }

        //DELETE /api/categories/1
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);
            return response ? NoContent() : NotFound();
        }
    }
}
