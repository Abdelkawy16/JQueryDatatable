using JQueryDatatable.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace JQueryDatatable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult GetCustomers()
        {
            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);
            var searchValue = Request.Form["search[value]"].ToString().ToLower();
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortColumnDirection = Request.Form["order[0][dir]"];

            var customers = _context.Customers.Where(c=> 
            c.FirstName.ToLower().Contains(searchValue)
            || c.FirstName.ToLower().Contains(searchValue)
            || c.LastName.ToLower().Contains(searchValue)
            || c.Contact.ToLower().Contains(searchValue)
            || c.Email.ToLower().Contains(searchValue)
            );

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                customers = customers.OrderBy(sortColumn + " " + sortColumnDirection);

            var data = customers.Skip(skip).Take(pageSize).ToList();
            var totalRecords = customers.Count();
            var jsonData = new {recordsFiltered=totalRecords, totalRecords, data};
            return Ok(jsonData);
        }
    }
}
