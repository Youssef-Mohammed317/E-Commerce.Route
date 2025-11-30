using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.Common
{
    public enum ErrorType
    {
        Unknown = 0,
        Failure = 1,
        Validation = 2,
        NotFound = 3,
        Unauthorized = 4,
        Forbidden = 5,
        InvalidCrendentials = 6
    }

}
