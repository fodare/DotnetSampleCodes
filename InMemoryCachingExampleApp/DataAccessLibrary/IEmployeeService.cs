using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class IEmployeeService
    {
        private readonly IMemoryCache _memoryCache;
        public IEmployeeService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "Damilare", LastName = "Test" });
            output.Add(new() { FirstName = "James", LastName = "Joe" });
            output.Add(new() { FirstName = "Jane", LastName = "Doe" });
            output.Add(new() { FirstName = "Tom", LastName = "Cruise" });
            output.Add(new() { FirstName = "Bruce", LastName = "Lee" });

            Thread.Sleep(4000);
            return output;
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "Damilare", LastName = "Test" });
            output.Add(new() { FirstName = "James", LastName = "Joe" });
            output.Add(new() { FirstName = "Jane", LastName = "Doe" });
            output.Add(new() { FirstName = "Tom", LastName = "Cruise" });
            output.Add(new() { FirstName = "Bruce", LastName = "Lee" });

            await Task.Delay(5000);
            return output;
        }

        public async Task<List<EmployeeModel>> GetEmployeesCached()
        {
            List<EmployeeModel> output;

            output = _memoryCache.Get<List<EmployeeModel>>("employees");
            if (output is null)
            {
                output = new();
                output.Add(new() { FirstName = "Damilare", LastName = "Test" });
                output.Add(new() { FirstName = "James", LastName = "Joe" });
                output.Add(new() { FirstName = "Jane", LastName = "Doe" });
                output.Add(new() { FirstName = "Tom", LastName = "Cruise" });
                output.Add(new() { FirstName = "Bruce", LastName = "Lee" });

                await Task.Delay(5000);
                _memoryCache.Set("employees", output, TimeSpan.FromMinutes(1));
            }
            return output;
        }
    }
}
