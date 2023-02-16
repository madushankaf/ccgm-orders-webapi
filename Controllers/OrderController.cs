using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCGM.Controllers;

[ApiController]
//[Route("[controller]")]
public class OrdersController : ControllerBase
{


    // Create a SqlConnection object

    private readonly ILogger<OrdersController> _logger;
    private readonly OrdersDbContext _context;

    public OrdersController(ILogger<OrdersController> logger, OrdersDbContext context)
    {
        _logger = logger;
        _context = context;

    }

    [HttpGet("orders")]
    public async Task<IEnumerable<Order>> GetAsync()
    {
        return await _context.Orders.ToListAsync();

    }

    [HttpGet("orders/{id}")]
    public async Task<ActionResult<Order>> GetByIdAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return order;
    }

    [HttpPost("orders")]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByIdAsync), new { id = order.Id }, order);
    }
}
