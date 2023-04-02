using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Models;

namespace Task12.API.Reports
{
    public record PeriodReportResponse(DateOnly DateStart, DateOnly DateEnd, decimal TotalIncome, decimal TotalExpense, List<FinancialOperation> Operations);
    
}
