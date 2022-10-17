using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperGasApp.Data;
using SuperGasApp.Data.Models.Inventory;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductController(
        ApplicationDbContext context
        )
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var client = await _context.Products.FindAsync(id);

        return Ok(client);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var clients = await _context.Products.ToListAsync();
        return Ok(clients);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] Product request)
    {
        var client = await _context.Products.FindAsync(id);
        var newClient = request.Adapt(client);
        _context.Products.Update(newClient);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(Product request)
    {
        await _context.Products.AddAsync(request);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return Ok();
    }
}