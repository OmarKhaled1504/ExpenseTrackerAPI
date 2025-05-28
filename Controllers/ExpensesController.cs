using ExpenseTrackerAPI.Dtos.ExpensesDtos;
using ExpenseTrackerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }


        //GET /api/expenses/1
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ExpenseDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult<ExpenseDto>> GetExpense(int id)
        {
            try
            {
                var expenseDto = await _expenseService.GetExpenseAsync(id);
                return expenseDto is null ? NotFound() : Ok(expenseDto);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        //POST /api/expenses
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ExpenseDto), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ExpenseDto>> PostExpense(ExpenseCreateDto dto)
        {
            var createdDto = await _expenseService.CreateExpenseAsync(dto);
            return createdDto is null ? BadRequest($"Category with ID {dto.CategoryId} does not exist") : CreatedAtAction(nameof(GetExpense), new { id = createdDto.Id }, createdDto);
        }

    }
}
