using AlintaCodingTest.Controllers;
using AlintaCodingTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AlintaCodingTest.Test
{
    public class CustomerControllerTest
    {
        private readonly CustomerController _controller;
        private readonly DatabaseContext _context;

        public CustomerControllerTest(DatabaseContext context)
        {
            _context = context;
            _controller = new CustomerController(_context);
        }

        [Fact]
        public void GetCustomerByNameTest()
        {
            //Arrange
            var name = "Adam";
            //Act
            var result = _controller.GetCustomerByName(name);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;

            Assert.IsType<List<Customer>>(list.Value);

            var listCustomers = list.Value as List<Customer>;
            foreach (var item in listCustomers)
            {
                Assert.Contains(name, item.FirstName + " " + item.LastName);
            }
        }

    }
}
