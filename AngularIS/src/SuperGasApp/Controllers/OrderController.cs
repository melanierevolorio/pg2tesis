using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperGasApp.Data;
using SuperGasApp.Data.Models.Inventory;

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
        var client = await _context.Orders.FindAsync(id);

        return Ok(client);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var clients = await _context.Orders.ToListAsync();
        return Ok(clients);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] Orders request)
    {
        var client = await _context.Orders.FindAsync(id);
        var newClient = request.Adapt(client);
        _context.Orders.Update(newClient);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(Orders request)
    {
        await _context.Orders.AddAsync(request);
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