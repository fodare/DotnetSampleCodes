using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleDockerApp.Models;

namespace SampleDockerApp.Services
{
    public class ICustomerRepository
    {
        public List<Customer> customersList = new List<Customer>();

        public async Task SeedCustomerListAsync()
        {
            await Task.Run(() =>
            {
                customersList.Add(new Customer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    IsActiveSubscriber = true,
                    CreatedDate = DateTime.Now
                });
                customersList.Add(new Customer
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    IsActiveSubscriber = false,
                    CreatedDate = DateTime.Now
                });
                customersList.Add(new Customer
                {
                    Id = 3,
                    FirstName = "First",
                    LastName = "Man",
                    IsActiveSubscriber = true,
                    CreatedDate = DateTime.Now
                });
                customersList.Add(new Customer
                {
                    Id = 4,
                    FirstName = "First",
                    LastName = "Lasy",
                    IsActiveSubscriber = true,
                    CreatedDate = DateTime.Now
                });
            });

        }

        public async Task<List<Customer>> GetCustomerListAsync()
        {
            var customers = Task.Run(() => customersList.ToList());
            return await customers;
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            var customer = Task.Run(() => customersList.Single(x => x.Id == id));
            return await customer;
        }

        public async Task<List<Customer>> AddNewCustomer(Customer newCustomer)
        {
            await Task.Run(() => customersList.Add(newCustomer));
            return await GetCustomerListAsync();
        }
        public async Task<List<Customer>> RemoveCustomer(int id)
        {
            await Task.Run(() =>
            {
                var customer = customersList.Single(x => x.Id == id);
                customersList.Remove(customer);
            });
            return await GetCustomerListAsync();
        }
    }
}