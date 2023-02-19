using DataAccessLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InMemoryCachingExample.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IEmployeeService _dataService;
        public IList<EmployeeModel> EmployeeList { get; set; }

        public IndexModel(IEmployeeService dataService)
        {

            _dataService = dataService;
        }

        public async Task<IActionResult> OnGet()
        {
            List<EmployeeModel> employees;
            employees = await _dataService.GetEmployeesCached();
            EmployeeList = employees;
            return Page();
        }
    }
}