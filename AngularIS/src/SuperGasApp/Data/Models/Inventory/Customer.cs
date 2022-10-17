﻿namespace SuperGasApp.Data.Models.Inventory;
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PhoneNumber { get; set; }
    public string Address { get; set; }
    public virtual List<Order>? Orders { get; set; }

}
