using System.ComponentModel.DataAnnotations;

namespace MaretOrg2.Domain;

public class Item
{
    [Key]
    public int ItemId { get; set; }
    public string Name { get; set; }
    public int Price {get; set;}
    public int Quantity {get; set;}
    public List<User> Users { get; set; } = new();

    public Item() { }

    public Item(string Name , int Price, int Quantity)
    {
        this.Name = Name;
        this.Price = Price;
        this.Quantity = Quantity;
    }
}