using MaretOrg2.Domain;

namespace MaretOrg2.Application.Interface;

public interface IItemService
{
    public void AddItem(string name , int price , int quantity , string? role = null);
    public void updateItem(int? itemId = null, string? name = null, string? price = null, string? quantity = null , string? role = null);
    public void deleteItem(int? itemId = null , string role = null);
    public void Buy(int? itemId = null, int? userId = null);
    
    public List<Item> GetItems();

    public Item Search(string name);
}