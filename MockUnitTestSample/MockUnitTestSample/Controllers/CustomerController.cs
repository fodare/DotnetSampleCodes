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
        public IActionResult FetchCustomersAsync()
        {
            var customerList = _customerService.GetCustomers();
            if (customerList is null | customerList.Count <= 0)
            {
                Console.WriteLine("Customer List empty. Seeding list with dummy item!");
                _customerService.SeedList();
                customerList = _customerService.GetCustomers();
            }
            return Ok(customerList);
        }

        [HttpGet("GetCustomer/{id}", Name = "GeCustomerInfoByID")]
        public IActionResult FetchCustomer(int id)
        {
            var result = _customerService.GetCustomerInfo(id);
            if (result is null)
            {
                return BadRequest("Error fetching customer number");
            }
            return Ok(result);
        }

        [HttpPost("AddCustomer", Name = "AddNewCustomer")]
        public IActionResult NewCustomer(Customer customer)
        {
            var result = _customerService.AddNewCustomer(customer);
            if (result is null)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpDelete("RemoveCustomer/{id}", Name = "RemoveCustomerById")]
        public IActionResult DeleteCustomer(int id)
        {
            var result = _customerService.DeleteCustomer(id);
            return Ok(result);
        }
    }
}
