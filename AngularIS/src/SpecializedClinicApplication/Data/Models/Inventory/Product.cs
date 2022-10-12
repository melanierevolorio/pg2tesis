namespace SpecializedClinicApplication.Data.Models.Inventory;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int InventoryId { get; set; }
    public Inventory Inventory { get; set; }

}
