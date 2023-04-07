using MockUnitTestSample.Models;
using MockUnitTestSample.Services;
using Moq;

namespace CustomerServiceTest
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _sut;
        private readonly Mock<ICustomerRepository> _CustomerRepo = new Mock<ICustomerRepository>();

        public CustomerServiceTests()
        {
            _sut = new CustomerService(_CustomerRepo.Object);
        }
        [Fact]
        public void SeedList_Adds_Dummy_Customes_To_CustomerLists()
        {
            // Arrgange
            _sut.SeedList();

            // Act
            var customerList = _sut.GetCustomers().ToList();
            var customerCount = customerList.Count;

            // Assert
            Assert.Equal(4, customerCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetCustomerInfo_Returns_Customer_Properties(int id)
        {
            // Arrange
            _sut.SeedList();

            // Act 
            var customer = _sut.GetCustomerInfo(id);

            // Assert
            Assert.Equal(id, customer.Id);

        }

        [Fact]
        public void DeleteCustomer_Removes_Customer_From_CustomerList()
        {
            // Arrange
            _sut.SeedList();
            var customerList = _sut.GetCustomers();
            int customerCount = customerList.Count;

            // Act
            var newCustomerList = _sut.DeleteCustomer(1);
            int newCustomerCount = newCustomerList.Count();

            // Assert
            Assert.Equal(newCustomerCount, customerCount - 1);
        }

        [Fact]
        public void AddNewCustomer_Adds_To_CustomerList()
        {
            // Arrange
            _sut.SeedList();
            var newCustomer = new Customer
            {
                Id = 1,
                FirstName = "Uncle",
                LastName = "Bob",
                ActiveSubscriber = false,
                CreationDate = DateTime.Now,
            };
            var customerList = _sut.GetCustomers();
            var customerCount = customerList.Count;

            // Act
            var newCustomerList = _sut.AddNewCustomer(newCustomer);
            var newCustomerCount = newCustomerList.Count;

            // Assert
            Assert.Equal(newCustomerCount, customerCount + 1);
        }
    }
}