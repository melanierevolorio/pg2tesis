namespace SuperGasApp.Data.Models.Inventory;
public class OrderProduct
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Brand { get; set; }
    public int ProductQuantity { get; set; }
    public double ProductPrice { get; set; }
    public double ProductTotalPrice { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}
