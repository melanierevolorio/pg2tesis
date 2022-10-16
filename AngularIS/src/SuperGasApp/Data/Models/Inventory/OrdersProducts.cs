namespace SuperGasApp.Data.Models.Inventory;
public class OrdersProducts
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Brand { get; set; }
    public int ProductQuantity { get; set; }
    public double ProductPrice { get; set; }
    public double ProductTotalPrice { get; set; }
    public int OrdersId { get; set; }
    public Orders Orders { get; set; }
}
