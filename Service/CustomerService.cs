using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private IRepository<Customer> customerRepository;
        private CustomerContext customerContext=new CustomerContext();

        public CustomerService(IRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public CustomerService(CustomerContext customerContext)
        {
            this.customerContext = customerContext;
        }

        public void Delete(Customer model)
        {
            if (model == null)
                throw new ArgumentNullException("Customer");
            customerRepository.Delete(model);
        }

        public List<Customer> GetAll()
        {
            return customerRepository.GetAll().ToList();
        }

        public Customer GetById(long id)
        {
            if(id == 0)
                return null;
            return customerRepository.GetById(id);
        }

        public Customer Insert(Customer model)
        {
            if (model == null)
                throw new ArgumentNullException("Customer");
            customerRepository.Insert(model);
            return model;
        }

        public void Save()
        {
            customerRepository.Save();
        }

        public bool Update(Customer model , long id)
        {

            //if (model == null)
            //    throw new ArgumentNullException("Customer");
            //customerRepository.Update(model);
            //return true;

            if (model == null)
            {
                throw new ArgumentNullException("item");
            }
            customerContext.Entry(model).State = EntityState.Modified;
            customerContext.SaveChanges();
            return true;
        }
      
    }
   

    }

