using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12.API.Authentication
{
    public record UserRequest (string UserName, string Password, string Role);
    
}
