using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12.API.ExpenseTypes
{
    public record UpsertExpenseTypeRequest(Guid UserId, string Name);
}
