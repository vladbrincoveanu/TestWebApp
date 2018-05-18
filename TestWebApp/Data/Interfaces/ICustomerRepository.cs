using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Model;

namespace TestWebApp.Data.Interfaces
{
    public interface ICustomerRepository :IRepository<Customer>
    {
    }
}
