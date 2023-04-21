using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12.API.ExpenseTypes
{
    public record ExpenseTypeResponse(Guid Id, Guid UserId, string Name, DateTime LastModified);
}
