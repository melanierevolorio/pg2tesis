using SuperGasApp.Data.Models.Inventory;

namespace SuperGasApp.Model;

public class OrderModel
{
    public int Id { get; set; }
    public string? Annotations { get; set; }
    public DateTime Date { get; set; }
    public List<OrderProduct>? OrdersProducts { get; set; }
    public int CustomerId { get; set; }
    public CustomerModel? Customer { get; set; }
}
