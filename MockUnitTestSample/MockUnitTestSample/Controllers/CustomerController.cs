using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockUnitTestSample.Models;
using MockUnitTestSample.Services;
using System.Reflection.Metadata.Ecma335;

namespace MockUnitTestSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerInterface _customerService;
        public CustomerController(ICustomerInterface customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetCustomers", Name = "GetAllCustomers")]
        public async Task<ActionResult> FetchCustomersAsync()
        {
            var customerList =  await _customerService.GetCustomers();
            if (customerList is null | customerList.Count <= 0)
            {
                Console.WriteLine("Customer List empty. Seeding list with dummy item!");
                _customerService.SeedList();
                customerList = await _customerService.GetCustomers();
            }
            return Ok(customerList);
        }

        [HttpPost("AddCustomer", Name = "AddNewCustomer")]
        public async Task<ActionResult> NewCustomer(Customer customer)
        {
            var result = await _customerService.AddNewCustomer(customer);
            if (result is null)
            {
                return BadRequest(result);
            }
            return Ok(result);
            
        }
    }
}
