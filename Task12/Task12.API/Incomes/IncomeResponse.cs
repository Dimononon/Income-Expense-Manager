using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12.Models;

namespace Task12.API.Incomes
{
    public record IncomeResponse(Guid id, Guid UserId, string Name, DateTime DateTime, DateTime LastModified, Guid TypeId, decimal Amount, OperationType Type);

}
