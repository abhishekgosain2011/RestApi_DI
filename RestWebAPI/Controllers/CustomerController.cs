using Entities;
using Newtonsoft.Json;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace RestWebAPI.Controllers
{
  
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
     
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerRepository)
        {
            _customerService = customerRepository;
        }
        public IHttpActionResult GetAllCustomer([FromUri]PagingParameterModel pagingparametermodel)

        {
            // Return List of Customer  
            var source = (from prod in _customerService.GetAll().
                            OrderBy(a => a.Id)
                          select prod).AsQueryable();
            
           // -------------------------------- Search Parameter-------------------   

            if (!string.IsNullOrEmpty(pagingparametermodel.QuerySearch))
            {
                source = source.Where(a => a.Email.Contains(pagingparametermodel.QuerySearch));
            }

            // ------------------------------------ Search Parameter-------------------  

            // Get's No of Rows Count   
            int count = source.Count();

            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage,

                QuerySearch = string.IsNullOrEmpty(pagingparametermodel.QuerySearch) ?
                      "No Parameter Passed" : pagingparametermodel.QuerySearch

            };

            // Setting Header  
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            // Returing List of Customers Collections  
            return Ok(items);
        }


        //public IHttpActionResult Get()
        //{
        //var data = _customerService.GetAll();
        //if (data == null)
        //{
        //    return NotFound();
        //}
        //return Ok(data);
        // }


        public IHttpActionResult GetById(int id)
        {
            var data = _customerService.GetById(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);

        }


        [System.Web.Http.HttpPost]
        public IHttpActionResult Create(Customer data)
        {
         
            _customerService.Insert(data);
            _customerService.Save();
            return Ok(data);
        }


        public void Delete(int id)
        {

            var emp = _customerService.GetById(id);
            _customerService.Delete(emp);
            _customerService.Save();
           
        }
      
        public IHttpActionResult Put(Int32 id,Customer model)
        {
            model.Id = id;
            var cust = _customerService.Update(model,id);
           
            return Ok(cust);
        }

        //[HttpPost]
        //public ActionResult Update(Customer em, Int32 id)
        //{
        //    var cust = _customerService.GetById(id);
        //    cust.Name = em.Name;
        //    cust.Age = em.Age;
        //    cust.Email = em.Email;

        //    _customerService.Update(cust);
        //    _customerService.Save();

        //}

    }
    }