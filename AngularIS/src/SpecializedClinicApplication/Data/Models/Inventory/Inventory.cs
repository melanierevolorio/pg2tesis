namespace SpecializedClinicApplication.Data.Models.Inventory;
public class Inventory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; }

}
