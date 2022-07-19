using CustomerioChallenge.Eventing;
using CustomerioChallenge.Summarization;
using CustomerioChallenge.Summarization.DomainModels;
using CustomerioChallenge.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerioChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // GET: api/<CustomersController>
        [HttpGet]
        public PagedCustomers Get([FromQuery(Name = "page")] int pageNumer, [FromQuery(Name = "per_page")] int pageSize)
        {
            var customers = DataReadStore.List();
            var pagedList = customers.ToPagedList(pageNumer, pageSize);
            return new PagedCustomers {
                Customers = pagedList.ToList(),
                Meta = new PageData
                {
                    Page = pageNumer,
                    Per_page= pageSize,
                    Total= customers.Count
                }
            };
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return DataReadStore.GetCustomer(id);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] IEnumerable<KeyValuePair<string,string>> attributes)
        {
            ActivityStore.AddAttributeChangeActivity(id, attributes.ToDictionary(p => p.Key, p => p.Value));
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DataWriteStore.DeleteCustomer(id);
            return NoContent();
        }
    }
}
