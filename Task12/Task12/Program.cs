using Microsoft.EntityFrameworkCore;
using Task12;
using Task12.Services.Expenses;
using Task12.Services.ExpenseTypes;
using Task12.Services.Incomes;
using Task12.Services.IncomeTypes;
using Task12.Services.Reports;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IExpenseService, ExpenseService>();
builder.Services.AddTransient<IIncomeService, IncomeService>();
builder.Services.AddTransient<IIncomeTypeService, IncomeTypeService>();
builder.Services.AddTransient<IExpenseTypeService, ExpenseTypeService>();
builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("AppDBConnection")));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
