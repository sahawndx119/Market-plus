using MaretOrg2.Application.Interface;
using MaretOrg2.Domain;
using Microsoft.EntityFrameworkCore;

namespace MaretOrg2.Infrastructure.Services
{
    public class ItemRepository : IItemRepository
    {
        DatabaseManager db = new DatabaseManager();

        public void AddItemToDatabase(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
        }

        public Item GetItemById(int id, bool withUsersFlag = false)
        {
            Item? item;
            if (!withUsersFlag)
            {
                item = db.Items.FirstOrDefault(i => i.ItemId == id);
            }else
            {
                item = db.Items.Include(i => i.Users).FirstOrDefault(i => i.ItemId == id);
            }

            return item;
        }

        public int GetItemIdByName(string name)
        {
            var item = db.Items.FirstOrDefault(i => i.Name == name);
            return item != null ? item.ItemId : -1;
        }

        public void UpdateItem(Item item)
        {
            db.Items.Update(item);
            db.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var item = db.Items.FirstOrDefault(i => i.ItemId == id);
            if (item != null)
            {
                db.Items.Remove(item);
                db.SaveChanges();
            }
        }

        public void BuyHanlder()
        {
            db.SaveChanges();
        }

        public List<Item> GetItems()
        {
            return db.Items.ToList();
        }
    }
}