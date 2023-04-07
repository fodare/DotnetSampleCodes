using MockUnitTestSample.Models;

namespace MockUnitTestSample.Services
{
    public interface ICustomerInterface
    {
        void SeedList();
        Task<Customer> GetCustomerInfo(int id);
        Task<List<Customer>> GetCustomers();
        Task<List<Customer>> AddNewCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
    }
}
