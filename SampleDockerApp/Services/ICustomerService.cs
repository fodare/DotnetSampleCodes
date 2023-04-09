using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleDockerApp.Models;

namespace SampleDockerApp.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(int id);
        Task<List<Customer>> AddCustomer(Customer newCustomer);
        Task<List<Customer>> DeleteCustomer(int id);
    }
}