using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleDockerApp.Models;

namespace SampleDockerApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        public CustomerService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<List<Customer>> AddCustomer(Customer newCustomer)
        {
            var customerList = await _customerRepo.AddNewCustomer(newCustomer);
            return customerList;
        }

        public async Task<List<Customer>> DeleteCustomer(int id)
        {
            var customerList = await _customerRepo.RemoveCustomer(id);
            return customerList;
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            var customer = await _customerRepo.GetCustomerAsync(id);
            return customer;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var customerList = await _customerRepo.GetCustomerListAsync();
            if (customerList.Count <= 0)
            {
                await _customerRepo.SeedCustomerListAsync();
                customerList = await _customerRepo.GetCustomerListAsync();
            }
            return customerList;
        }
    }
}