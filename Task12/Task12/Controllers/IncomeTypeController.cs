using Microsoft.AspNetCore.Mvc;
using Task12.API.IncomeTypes;
using Task12.Models;
using Task12.Services.IncomeTypes;

namespace Task12.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeTypeController : ControllerBase
    {
        public readonly IIncomeTypeService _incomeTypeService;
        public IncomeTypeController(IIncomeTypeService incomeTypeService)
        {
            _incomeTypeService = incomeTypeService;
        }
        [HttpPost()]
        public async Task<IActionResult> CreateIncomeType(CreateIncomeTypeRequest request)
        {
            var type = new IncomeType(Guid.NewGuid(), request.Name, DateTime.UtcNow);
            await _incomeTypeService.CreateIncomeType(type);
            var response = new IncomeTypeResponse(type.Id, type.Name, type.LastModified);

            return CreatedAtAction(nameof(GetIncomeType), new { id = type.Id }, response);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetIncomeType(Guid id)
        {
                var type = await _incomeTypeService.GetIncomeType(id);
                var response = new IncomeTypeResponse(type.Id, type.Name, type.LastModified);
                return Ok(response);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpsertIncomeType(Guid id, UpsertIncomeTypeRequest request)
        {
            var type = new IncomeType(id, request.Name, DateTime.UtcNow);
            
            if(await _incomeTypeService.UpdateIncomeType(type))
            {
            return Ok();

            }
            else
            {
                return CreatedAtAction(nameof(GetIncomeType), new { id = id });
            }

        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteIncomeType(Guid id)
        {
                await _incomeTypeService.DeleteIncomeType(id);
                return Ok(id);
        }
    }
}
