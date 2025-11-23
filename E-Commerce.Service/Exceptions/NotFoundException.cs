using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base($"Product with Id {id} was not found.")
        {
        }
    }
    public sealed class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(Guid id) : base($"Basket with Id {id} was not found.")
        {
        }
    }
}
