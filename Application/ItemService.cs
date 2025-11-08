using MaretOrg2.Application.Interface;
using MaretOrg2.Domain;
using MaretOrg2.Infrastructure.Services;

namespace MaretOrg2.Application;

public class ItemService : IItemService
{
    IItemRepository _repo = new ItemRepository();
    IUserRepository _userRepo = new UserRepository();

    public void AddItem(string name, int price, int quantity, string? role = null)
    {
        if (role == null)
        {
            throw new Exception("Please login/signup first");
        }

        if (role == "user")
        {
            throw new Exception("You do not have permission to add items");
        }

        if (price <= 0)
        {
            throw new Exception("Price must be greater than zero");
        }

        if (quantity < 0)
        {
            throw new Exception("Quantity must be greater than zero");
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new Exception("Name cannot be empty");
        }

        Item item = new Item(name, price, quantity);
        _repo.AddItemToDatabase(item);
    }

    public void updateItem(int? itemId = null, string? nName = null, string? nPrice = null, string? nQuantity = null,
        string? role = null)
    {
        if (itemId == null)
        {
            throw new Exception("You first have to select your item you have to search its name and find it!");
        }

        int id = itemId ?? -1;
        var item = _repo.GetItemById(id);
        string name = nName ?? item.Name;
        if (!string.IsNullOrEmpty(nPrice))
        {
            int price = Int32.Parse(nPrice);
            if (price <= 0)
            {
                throw new Exception("Price must be greater than zero");
            }

            item.Price = price;
        }

        if (!string.IsNullOrEmpty(nQuantity))
        {
            int quantity = Convert.ToInt32(nQuantity);
            if (quantity < 0)
            {
                throw new Exception("Quantity must be greater than zero");
            }

            item.Quantity = quantity;
        }

        if (role == null)
        {
            throw new Exception("Please login/signup first");
        }


        if (role == "user")
        {
            throw new Exception("You do not have permission to add items");
        }


        if (!string.IsNullOrEmpty(name))
        {
            item.Name = name;
        }
        _repo.UpdateItem(item);
    }

    public void deleteItem(int? itemId = null, string? role = null)
    {
        if (role.ToLower() != "admin")
        {
            throw new Exception("You do not have permission to add items");
        }

        if (itemId == null)
        {
            throw new Exception("You first have to select your item you have to search its name and find it!");
        }

        int id = itemId ?? -1;
        _repo.DeleteItem(id);
    }

    public void Buy(int? itemId = null, int? userId = null)
    {
        if (userId == null)
        {
            throw new Exception("Please login/signup first");
        }

        if (itemId == null)
        {
            throw new Exception("You first have to select your item you have to search its name and find it!");
        }

        int intItemId = itemId ?? -1;
        var item = _repo.GetItemById(intItemId);
        if (item.Quantity == 0)
        {
            throw new Exception("There is no enough quantity to buy this item");
        }

        item.Quantity--;

        int id = userId ?? -1;
        var user = _userRepo.GetUserByUserId(id);
        if (user.Items.Contains(item))
        {
            throw new Exception("There is already an item with that id");
        }
        user.Items.Add(item);
        item.Users.Add(user);
        _repo.UpdateItem(item);
    }

    public List<Item> GetItems()
    {
        var output = _repo.GetItems();
        return output;
    }

    public Item Search(string name)
    {
        int id = _repo.GetItemIdByName(name);
        if (id == -1)
        {
            throw new Exception("Item not found");
        }

        return _repo.GetItemById(id);
    }
}