using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12.API.IncomeTypes
{
    public record IncomeTypeResponse(Guid id, Guid UserId,string name, DateTime lm);
}
