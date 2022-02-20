using AlintaCodingTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlintaCodingTest.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CustomerController(DatabaseContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest("Customer is null");

            Customer customerinDb = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);

            if (customerinDb != null)
                return BadRequest("Customer is exists with same Id");

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }


        [HttpGet("{name}", Name = "Get")]
        public async Task<IActionResult> GetCustomerByName(string name)
        {
            List<Customer> customers = await _context.Customers.Where(c => (c.FirstName + " " + c.LastName).ToLower().Contains(name.ToLower())).ToListAsync();

            if (customers == null)
                return NotFound("customer can not be found");

            return Ok(customers);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest("Customer is null");

            Customer customerinDb = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customerinDb == null)
                return NotFound("Customer Not Found");

            customerinDb.FirstName = customer.FirstName;
            customerinDb.LastName = customer.LastName;
            customerinDb.DateOfBirth = customer.DateOfBirth;

            await _context.SaveChangesAsync();

            return Ok(customer);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Customer customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
                return NotFound("Customer not found");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
