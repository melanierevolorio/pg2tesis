namespace SuperGasApp.Data.Models.Inventory;
public class Orders
{
    public int Id { get; set; }
    public string Annotations { get; set; }
    public int CustomersId { get; set; }
    public Customers Customers { get; set; }
    public DateTime Date { get; set; }
    public List<OrdersProducts> OrdersProducts { get; set; }

}
