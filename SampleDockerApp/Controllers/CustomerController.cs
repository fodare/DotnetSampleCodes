using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleDockerApp.Models;
using SampleDockerApp.Services;

namespace SampleDockerApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetAll", Name = ("GetAllCustomers"))]
        public async Task<ActionResult> FetchCustomers()
        {
            var customersList = await _customerService.GetCustomersAsync();
            if (customersList is null)
            {
                return BadRequest("Error fetching customers!");
            }
            return Ok(customersList);
        }

        [HttpGet("Get/{id}", Name = "GetCustomer")]
        public async Task<ActionResult> FetchCustomer(int id)
        {
            var customer = await _customerService.GetCustomerAsync(id);
            if (customer is null)
            {
                return BadRequest("Error fetching customer!");
            }
            return Ok(customer);
        }

        [HttpPost("Add", Name = "AddNewCustomer")]
        public async Task<ActionResult> Addcustomer([FromBody] Customer newCustomer)
        {
            var customerList = await _customerService.AddCustomer(newCustomer);
            if (customerList is null)
            {
                return BadRequest("Error adding new customer");
            }
            return Ok(customerList);
        }

        [HttpDelete("Remove/{id}", Name = "RemoveCustomer")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customerList = await _customerService.DeleteCustomer(id);
            if (customerList is null)
            {
                return BadRequest("Error removing customer!");
            }
            return Ok(customerList);
        }
    }
}