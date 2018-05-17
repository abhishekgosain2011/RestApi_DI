using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public interface ICustomerService
    {
        List<Customer> GetAll();
        Customer GetById(Int64 id);
        Customer Insert(Customer model);
        bool Update(Customer model, Int64 id);
        void Delete(Customer model);
        void Save();
    }
}
