using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12.API.Incomes
{
    public record CreateIncomeRequest(Guid UserId, string Name, DateTime DateTime, Guid TypeId, decimal Amount);
}
