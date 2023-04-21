using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12.API.Authentication
{
    public record ValidateRequest(string UserName, string Password);
    
}
