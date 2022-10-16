namespace SuperGasApp.Data.Models.Inventory;
public class Customers
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public List<Orders> Orders { get; set; }

}
