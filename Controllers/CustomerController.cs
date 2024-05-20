using DemoProject.Database;
using DemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerDbContext _context;

        public CustomerController(ILogger<CustomerController> logger, CustomerDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            return Ok((await _context.Customers.ToListAsync()).Select(x => new CustomerDto { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName }));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomers(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer is null) 
            {
                return NotFound();
            }
            return Ok(new CustomerDto { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName });
        }
        [HttpPost("Create")]
        public async Task<ActionResult> CreateCustomer(CustomerDto customer)
        {
            var toBeInserted = new Customer { FirstName = customer.FirstName, LastName = customer.LastName };
            await _context.AddAsync(toBeInserted);
            await _context.SaveChangesAsync();
            return Created($"customer/{customer.Id}", toBeInserted);
        }
    }
}
