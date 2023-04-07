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
        public List<Customer> AddNewCustomer(Customer customer)
        {
            _customerRepo.AddNewCustomer(customer);
            return _customerRepo.FetchAllCustomers().ToList();
        }

        public List<Customer> DeleteCustomer(int id)
        {
            var result = _customerRepo.DeleteCustomer(id);
            return result.ToList();
        }

        public Customer GetCustomerInfo(int id)
        {
            var result = _customerRepo.FetchCustomerInfo(id);
            if (result is null)
            {
                ;
            }
            return result;
        }

        public List<Customer> GetCustomers()
        {
            var result = _customerRepo.FetchAllCustomers() ;
            return result.ToList();
        }
    }
}
