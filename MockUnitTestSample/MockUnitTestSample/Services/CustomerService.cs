using MockUnitTestSample.Models;
using System.Security.Cryptography;

namespace MockUnitTestSample.Services
{
    public class CustomerService : ICustomerInterface
    {
        private readonly ICustomerRepository _customerRepo;
        public CustomerService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public void SeedList()
        {
            _customerRepo.SeedCustomerList();
        }
        public async Task<List<Customer>> AddNewCustomer(Customer customer)
        {
            await _customerRepo.AddNewCustomer(customer);
            return _customerRepo.FetchAllCustomers();
        }

        public Task<bool> DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomerInfo(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return _customerRepo.FetchAllCustomers().ToList();
        }
    }
}
