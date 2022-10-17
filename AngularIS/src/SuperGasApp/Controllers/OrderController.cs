using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperGasApp.Data;
using SuperGasApp.Data.Models.Inventory;
using SuperGasApp.Model;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrderController(
        ApplicationDbContext context
        )
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) 
    {
        var order = await _context.Orders.Include(x=>x.Customer).SingleAsync(x=>x.Id == id);

        return Ok(order.Adapt<OrderModel>());
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var orders = await _context.Orders.Include(x => x.Customer).ToListAsync();
        return Ok(orders.Adapt<List<OrderModel>>());
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] OrderModel request)
    {
        var client = await _context.Orders.FindAsync(id);
        var newClient = request.Adapt(client);
        _context.Orders.Update(newClient);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(OrderModel request)
    {
        await _context.Orders.AddAsync(request.Adapt<Order>());
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return Ok();
    }
}