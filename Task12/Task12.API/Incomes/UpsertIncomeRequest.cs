using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12.API.Incomes
{
    public record UpsertIncomeRequest(string Name, DateTime DateTime, DateTime LastModified, Guid TypeId, decimal Amount);
}
