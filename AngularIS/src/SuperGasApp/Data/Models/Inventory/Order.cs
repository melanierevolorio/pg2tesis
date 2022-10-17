namespace SuperGasApp.Data.Models.Inventory;
public class Order
{
    public int Id { get; set; }
    public string? Annotations { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime Date { get; set; }
    public virtual List<OrderProduct> OrdersProducts { get; set; }

}
