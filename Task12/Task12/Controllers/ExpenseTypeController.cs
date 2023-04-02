using Microsoft.AspNetCore.Mvc;
using Task12.API.ExpenseTypes;
using Task12.Models;
using Task12.Services.ExpenseTypes;

namespace Task12.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseTypeController : ControllerBase
    {
        public readonly IExpenseTypeService _expenseTypeService;
        public ExpenseTypeController(IExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }
        [HttpPost()]
        public async Task<IActionResult> CreateExpenseType(CreateExpenseTypeRequest request)
        {
            var type = new ExpenseType(Guid.NewGuid(), request.Name, DateTime.UtcNow);
            await _expenseTypeService.CreateExpenseType(type);
            var response = new ExpenseTypeResponse(type.Id, type.Name, type.LastModified);

            return CreatedAtAction(nameof(GetExpenseType), new { id = type.Id }, response);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetExpenseType(Guid id)
        {
            try
            {
                var type = await _expenseTypeService.GetExpenseType(id);
                var response = new ExpenseTypeResponse(type.Id, type.Name, type.LastModified);
                return Ok(response);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpsertExpenseType(Guid id, UpsertExpenseTypeRequest request)
        {
            var type = new ExpenseType(id, request.Name, DateTime.UtcNow);
            if (await _expenseTypeService.UpdateExpenseType(type))
            {
                return Ok();
            }
            else
            {
                return CreatedAtAction(nameof(GetExpenseType), new { id = id });
            }

        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteExpenseType(Guid id)
        {
            try
            {
                await _expenseTypeService.DeleteExpenseType(id);
                return Ok(id);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
    }
}
