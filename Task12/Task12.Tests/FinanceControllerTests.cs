using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task12.API.Expenses;
using Task12.API.Incomes;
using Task12.API.Reports;
using Task12.Controllers;
using Task12.Models;
using Task12.Services.Expenses;
using Task12.Services.Incomes;
using Task12.Services.Reports;

namespace Task12.Tests
{
    public class Tests
    {
        private FinanceController _financeController;
        private AppDBContext _appDBContext;
        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                             .UseInMemoryDatabase(databaseName: "MockDB")
                             .Options;

            _appDBContext = new AppDBContext(options);
            IExpenseService expenseService = new ExpenseService(_appDBContext);
            IIncomeService incomeService = new IncomeService(_appDBContext);
            IReportService reportService = new ReportService(_appDBContext);
            _financeController = new FinanceController(expenseService, incomeService, reportService);
            await ClearDb();
        }
        private async Task ClearDb()
        {
            _appDBContext.FinancialOperations.RemoveRange(_appDBContext.FinancialOperations);
            await _appDBContext.SaveChangesAsync();
        }
        [Test]
        public async Task CreateExpenseTest()
        {
            Guid typeId = Guid.NewGuid();
            CreateExpenseRequest request = new("test", DateTime.MinValue, typeId, 12.5m);
            CreatedAtActionResult response = (CreatedAtActionResult)await _financeController.CreateExpense(request);
            var expected = new ExpenseResponse(Guid.Empty, "test", DateTime.MinValue, DateTime.UtcNow, typeId, 12.5m, OperationType.Expense);
            if (response.StatusCode == 201)
            {
                var actual = (ExpenseResponse)response.Value;
                if (actual != null &&
                   actual.Name == expected.Name &&
                   actual.Amount == expected.Amount &&
                   actual.DateTime == expected.DateTime &&
                   actual.TypeId == expected.TypeId)
                {
                    Assert.Pass();
                }

            }
        }
        [Test]
        public async Task GetExpenseTest()
        {
            Guid typeId = Guid.NewGuid();
            Guid typeId2 = Guid.NewGuid();
            DateTime dateTime = DateTime.UtcNow;
            FinancialOperation financialOperation = new FinancialOperation(typeId, "test", 12.5m, dateTime, dateTime, Guid.Empty, typeId2, OperationType.Expense);
            await _appDBContext.FinancialOperations.AddAsync(financialOperation);
            await _appDBContext.SaveChangesAsync();

            OkObjectResult response = (OkObjectResult)await _financeController.GetExpense(typeId);
            var expected = new ExpenseResponse(typeId, "test", dateTime, dateTime, typeId2, 12.5m, OperationType.Expense);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(200));
                Assert.That(response.Value, Is.EqualTo(expected));
            });
        }
        [Test]
        public async Task UpsertExpenseTest()
        {
            Guid typeId = Guid.NewGuid();
            Guid typeId2 = Guid.NewGuid();
            DateTime dateTime = DateTime.UtcNow;
            FinancialOperation financialOperation = new FinancialOperation(typeId, "test", 12.5m, dateTime, dateTime, Guid.Empty, typeId2, OperationType.Expense);
            FinancialOperation financialOperation2 = new FinancialOperation(typeId, "test2", 13, dateTime, dateTime, Guid.Empty, typeId2, OperationType.Expense);
            await _appDBContext.FinancialOperations.AddAsync(financialOperation);
            await _appDBContext.SaveChangesAsync();
            var request = new UpsertExpenseRequest("test2", dateTime, dateTime, typeId2, 13);
            var response = (OkResult)await _financeController.UpsertExpense(typeId, request);
            Assert.Multiple(async () =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(200));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)).DT, Is.EqualTo(financialOperation2.DT));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)).IncomeTypeId, Is.EqualTo(financialOperation2.IncomeTypeId));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)).ExpenseTypeId, Is.EqualTo(financialOperation2.ExpenseTypeId));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)).Amount, Is.EqualTo(financialOperation2.Amount));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)).Name, Is.EqualTo(financialOperation2.Name));


            });
        }
        [Test]
        public async Task DeleteExpenseTest()
        {
            Guid typeId = Guid.NewGuid();
            Guid typeId2 = Guid.NewGuid();
            DateTime dateTime = DateTime.UtcNow;
            FinancialOperation financialOperation = new FinancialOperation(typeId, "test", 12.5m, dateTime, dateTime, Guid.Empty, typeId2, OperationType.Expense);
            await _appDBContext.FinancialOperations.AddAsync(financialOperation);
            await _appDBContext.SaveChangesAsync();

            var response = (OkObjectResult)await _financeController.DeleteExpense(typeId);
            Assert.Multiple(async () =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(200));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)), Is.EqualTo(null));
            });
        }


        [Test]
        public async Task CreateIncomeTest()
        {
            Guid typeId = Guid.NewGuid();
            CreateIncomeRequest request = new("test", DateTime.MinValue, typeId, 12.5m);
            CreatedAtActionResult response = (CreatedAtActionResult)await _financeController.CreateIncome(request);
            var expected = new IncomeResponse(Guid.Empty, "test", DateTime.MinValue, DateTime.UtcNow, typeId, 12.5m, OperationType.Income);
            if (response.StatusCode == 201)
            {
                var actual = (IncomeResponse)response.Value;
                if (actual != null &&
                   actual.Name == expected.Name &&
                   actual.Amount == expected.Amount &&
                   actual.DateTime == expected.DateTime &&
                   actual.TypeId == expected.TypeId)
                {
                    Assert.Pass();
                }

            }
        }
        [Test]
        public async Task GetIncomeTest()
        {
            Guid typeId = Guid.NewGuid();
            Guid typeId2 = Guid.NewGuid();
            DateTime dateTime = DateTime.UtcNow;
            FinancialOperation financialOperation = new FinancialOperation(typeId, "test", 12.5m, dateTime, dateTime,  typeId2,Guid.Empty, OperationType.Income);
            await _appDBContext.FinancialOperations.AddAsync(financialOperation);
            await _appDBContext.SaveChangesAsync();

            OkObjectResult response = (OkObjectResult)await _financeController.GetIncome(typeId);
            var expected = new IncomeResponse(typeId, "test", dateTime, dateTime, typeId2, 12.5m, OperationType.Income);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(200));
                Assert.That(response.Value, Is.EqualTo(expected));
            });
        }
        [Test]
        public async Task UpsertIncomeTest()
        {
            Guid typeId = Guid.NewGuid();
            Guid typeId2 = Guid.NewGuid();
            DateTime dateTime = DateTime.UtcNow;
            FinancialOperation financialOperation = new FinancialOperation(typeId, "test", 12.5m, dateTime, dateTime,  typeId2, Guid.Empty, OperationType.Income);
            FinancialOperation financialOperation2 = new FinancialOperation(typeId, "test2", 13, dateTime, dateTime, typeId2, Guid.Empty, OperationType.Income);
            await _appDBContext.FinancialOperations.AddAsync(financialOperation);
            await _appDBContext.SaveChangesAsync();
            var request = new UpsertIncomeRequest("test2", dateTime, dateTime, typeId2, 13);
            var response = (OkResult)await _financeController.UpsertIncome(typeId, request);
            Assert.Multiple(async () =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(200));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)).DT, Is.EqualTo(financialOperation2.DT));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)).ExpenseTypeId, Is.EqualTo(financialOperation2.ExpenseTypeId));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)).IncomeTypeId, Is.EqualTo(financialOperation2.IncomeTypeId));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)).Amount, Is.EqualTo(financialOperation2.Amount));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)).Name, Is.EqualTo(financialOperation2.Name));


            });
        }
        [Test]
        public async Task DeleteIncomeTest()
        {
            Guid typeId = Guid.NewGuid();
            Guid typeId2 = Guid.NewGuid();
            DateTime dateTime = DateTime.UtcNow;
            FinancialOperation financialOperation = new FinancialOperation(typeId, "test", 12.5m, dateTime, dateTime, Guid.Empty, typeId2, OperationType.Income);
            await _appDBContext.FinancialOperations.AddAsync(financialOperation);
            await _appDBContext.SaveChangesAsync();

            var response = (OkObjectResult)await _financeController.DeleteIncome(typeId);
            Assert.Multiple(async () =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(200));
                Assert.That((await _appDBContext.FinancialOperations.FindAsync(typeId)), Is.EqualTo(null));
            });
        }

        [Test]
        public async Task DailyReportTest()
        {
            Guid typeId2 = Guid.NewGuid();
            DateTime dateTime = DateTime.UtcNow;
            FinancialOperation financialOperation = new FinancialOperation(Guid.NewGuid(), "test", 12.5m, dateTime, dateTime, Guid.Empty, typeId2, OperationType.Expense);
            FinancialOperation financialOperation2 = new FinancialOperation(Guid.NewGuid(), "test2", 15, dateTime, dateTime, Guid.Empty, typeId2, OperationType.Expense);
            FinancialOperation financialOperation3 = new FinancialOperation(Guid.NewGuid(), "test3", 14, dateTime, dateTime,  typeId2, Guid.Empty, OperationType.Income);
            FinancialOperation financialOperation4 = new FinancialOperation(Guid.NewGuid(), "test4", 2, DateTime.MinValue, dateTime,  typeId2, Guid.Empty, OperationType.Income);
            await _appDBContext.FinancialOperations.AddRangeAsync(financialOperation, financialOperation2,financialOperation3,financialOperation4);
            await _appDBContext.SaveChangesAsync();

            var response = (OkObjectResult) _financeController.GetDailyReport(dateTime);
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(200));
                Assert.That(((DailyReportResponse)response.Value).Date, Is.EqualTo(DateOnly.FromDateTime(dateTime)));
                Assert.That(((DailyReportResponse)response.Value).TotalExpense, Is.EqualTo(27.5m));
                Assert.That(((DailyReportResponse)response.Value).TotalIncome, Is.EqualTo(14));
            });
        }
        [Test]
        public async Task PeriodReportTest()
        {
            Guid typeId2 = Guid.NewGuid();
            DateTime dateTime = DateTime.UtcNow;
            FinancialOperation financialOperation = new FinancialOperation(Guid.NewGuid(), "test", 12.5m, DateTime.Parse("2023-04-01"), dateTime, Guid.Empty, typeId2, OperationType.Expense);
            FinancialOperation financialOperation2 = new FinancialOperation(Guid.NewGuid(), "test2", 15, DateTime.Parse("2023-04-15"), dateTime, Guid.Empty, typeId2, OperationType.Expense);
            FinancialOperation financialOperation3 = new FinancialOperation(Guid.NewGuid(), "test3", 14, DateTime.Parse("2023-04-30"), dateTime, typeId2, Guid.Empty, OperationType.Income);
            FinancialOperation financialOperation4 = new FinancialOperation(Guid.NewGuid(), "test4", 2, DateTime.Parse("2023-05-01"), dateTime, typeId2, Guid.Empty, OperationType.Income);
            await _appDBContext.FinancialOperations.AddRangeAsync(financialOperation, financialOperation2, financialOperation3, financialOperation4);
            await _appDBContext.SaveChangesAsync();

            var response = (OkObjectResult)_financeController.GetPeriodReport(DateTime.Parse("2023-04-01"),DateTime.Parse("2023-04-30"));
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(200));
                Assert.That(((PeriodReportResponse)response.Value).DateStart, Is.EqualTo(DateTime.Parse("2023-04-01")));
                Assert.That(((PeriodReportResponse)response.Value).DateEnd, Is.EqualTo(DateTime.Parse("2023-04-30")));
                Assert.That(((PeriodReportResponse)response.Value).TotalExpense, Is.EqualTo(27.5m));
                Assert.That(((PeriodReportResponse)response.Value).TotalIncome, Is.EqualTo(14));
            });
        }
    }
}