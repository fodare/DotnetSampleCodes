using MockUnitTestSample.Models;

namespace MockUnitTestSample.Services
{
    public class ICustomerRepository
    {
        private List<Customer> _customerList = new List<Customer>();

        public void SeedCustomerList()
        {
            _customerList.Add(new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                ActiveSubscriber = true,
                CreationDate = DateTime.Now,
            });
            _customerList.Add(new Customer
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                ActiveSubscriber = false,
                CreationDate = DateTime.Now,
            });
            _customerList.Add(new Customer
            {
                Id = 3,
                FirstName = "Person",
                LastName = "One",
                ActiveSubscriber = true,
                CreationDate = DateTime.Now,
            });
            _customerList.Add(new Customer
            {
                Id = 4,
                FirstName = "First",
                LastName = "Lasy",
                ActiveSubscriber = false,
                CreationDate = DateTime.Now,
            });
        }

        public List<Customer> FetchAllCustomers() => _customerList.ToList();

        public List<Customer> AddNewCustomer(Customer customer)
        {
            _customerList.Add(new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                ActiveSubscriber = customer.ActiveSubscriber,
                CreationDate = customer.CreationDate
            });
            return _customerList.ToList();
        }

        public Customer FetchCustomerInfo(int id)
        {
            var customer = new Customer();
            var customerInfo = _customerList.Where(x => x.Id == id).ToList();
            customer.Id = (int)(customerInfo.FirstOrDefault()?.Id);
            customer.FirstName = customerInfo.FirstOrDefault()?.FirstName;
            customer.LastName = customerInfo.FirstOrDefault()?.LastName;
            customer.ActiveSubscriber = (bool)(customerInfo.FirstOrDefault()?.ActiveSubscriber);
            customer.CreationDate = (DateTime)(customerInfo.FirstOrDefault()?.CreationDate);
            return customer;
        }

        public List<Customer> DeleteCustomer(int id)
        {
            int customerIndex = _customerList.FindIndex(x=> x.Id == id);
            _customerList.RemoveAt(customerIndex);
            return _customerList.ToList();
        }
    }
}
