using MockUnitTestSample.Models;

namespace MockUnitTestSample.Services
{
    public interface ICustomerInterface
    {
        void SeedList();
        Customer GetCustomerInfo(int id);
        List<Customer> GetCustomers();
        List<Customer> AddNewCustomer(Customer customer);
        List<Customer> DeleteCustomer(int id);
    }
}
