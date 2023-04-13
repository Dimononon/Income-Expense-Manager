using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Models;

namespace Task12.API.Expenses
{
    public record CreateExpenseRequest(string Name, DateTime DateTime, Guid TypeId, decimal Amount);
}
