using Microsoft.AspNetCore.Mvc;
using Task12.API;
using Task12.API.Expenses;
using Task12.API.Incomes;
using Task12.API.Reports;
using Task12.Models;
using Task12.Services.Expenses;
using Task12.Services.Incomes;
using Task12.Services.Reports;

namespace Task12.Controllers
{
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IIncomeService _incomeService;
        private readonly IReportService _reportService;

        public FinanceController(IExpenseService expenseService, IIncomeService incomeService, IReportService reportService)
        {
            _expenseService = expenseService;
            _incomeService = incomeService;
            _reportService = reportService;
        }

        [HttpPost("/expense")]
        public async Task<IActionResult> CreateExpense(CreateExpenseRequest request)
        {
            var expense = new FinancialOperation(Guid.NewGuid(), request.Name, request.Amount, request.DateTime, DateTime.UtcNow, Guid.Empty, request.TypeId);
            await _expenseService.CreateExpense(expense);
            var response = new ExpenseResponse(expense.Id, expense.Name, expense.DT, expense.LastModified, expense.ExpenseTypeId, expense.Amount);

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, response);
        }
        [HttpGet("/expense/{id:guid}")]
        public async Task<IActionResult> GetExpense(Guid id)
        {
            try
            {
                var expense = await _expenseService.GetExpanse(id);
                var response = new ExpenseResponse(expense.Id, expense.Name, expense.DT, expense.LastModified, expense.ExpenseTypeId, expense.Amount);
                return Ok(response);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
        [HttpPut("/expense/{id:guid}")]
        public async Task<IActionResult> UpsertExpense(Guid id, UpsertExpenseRequest request)
        {
            var expense = new FinancialOperation(id, request.Name, request.Amount, request.DateTime, DateTime.UtcNow, Guid.Empty, request.TypeId);
            await _expenseService.UpdateExpense(expense);

            return NoContent();
        }
        [HttpDelete("/expense/{id:guid}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            try
            {
                await _expenseService.DeleteExpense(id);
                return Ok(id);

            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }


        [HttpPost("/income")]
        public async Task<IActionResult> CreateIncome(CreateIncomeRequest request)
        {
            var income = new FinancialOperation(Guid.NewGuid(), request.Name, request.Amount, request.DateTime, DateTime.UtcNow, request.TypeId, Guid.Empty);
            await _incomeService.CreateIncome(income);
            var response = new IncomeResponse(income.Id, income.Name, income.DT, income.LastModified, income.ExpenseTypeId, income.Amount);

            return CreatedAtAction(nameof(GetIncome), new { id = income.Id }, response);
        }
        [HttpGet("/income/{id:guid}")]
        public async Task<IActionResult> GetIncome(Guid id)
        {
            try
            {
                var income = await _incomeService.GetIncome(id);
                var response = new IncomeResponse(income.Id, income.Name, income.DT, income.LastModified, income.ExpenseTypeId, income.Amount);
                return Ok(response);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
        [HttpPut("/income/{id:guid}")]
        public async Task<IActionResult> UpsertIncome(Guid id, UpsertIncomeRequest request)
        {
            var income = new FinancialOperation(id, request.Name, request.Amount, request.DateTime, DateTime.UtcNow, request.TypeId, Guid.Empty);
            await _incomeService.UpdateIncome(income);

            return NoContent();
        }
        [HttpDelete("/income/{id:guid}")]
        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            try
            {
                await _incomeService.DeleteIncome(id);
                return Ok(id);

            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
        [HttpGet("/dailyreport")]
        public IActionResult GetDailyReport([FromQuery] DateTime date)
        {
            var report = _reportService.GetDailyReport(DateOnly.FromDateTime(date));
            var response = new DailyReportResponse(report.Date, report.TotalIncome, report.TotalExpense, report.Operations);
            return Ok(response);
        }
        [HttpGet("/periodreport")]
        public IActionResult GetPeriodReport([FromQuery] DateTime dateStart, [FromQuery] DateTime dateEnd)
        {
            var report = _reportService.GetPeriodReport(DateOnly.FromDateTime(dateStart), DateOnly.FromDateTime(dateEnd));
            var response = new PeriodReportResponse(report.Date, report.DateEnd, report.TotalIncome, report.TotalExpense, report.Operations);
            return Ok(response);
        }

    }
}
