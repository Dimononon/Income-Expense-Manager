using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Models;

namespace Task12.API.ExpenseTypes
{
    public record AllExpenseTypeResponse(List<ExpenseType> ExpenseTypes);
}
