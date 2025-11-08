using MaretOrg2.Domain;

namespace MaretOrg2.Application.Interface;

public interface IItemRepository
{
    public void AddItemToDatabase(Item item);
    public Item GetItemById(int id , bool withUsersFlag = false);
    
    public int GetItemIdByName(string name);
    public void UpdateItem(Item item);
    public void DeleteItem(int id);

    public void BuyHanlder();
    public List<Item> GetItems();
}