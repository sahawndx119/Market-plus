using MaretOrg2.Application.Interface;
using MaretOrg2.Domain;
using Microsoft.EntityFrameworkCore;

namespace MaretOrg2.Infrastructure.Services;

public class UserRepository : IUserRepository
{
    DatabaseManager db = new DatabaseManager();

    public void AddUserToDatabase(User user)
    {
        db.Users.Add(user);
        db.SaveChanges();
    }

    public int GetUserIdByUserName(string userName, string password)
    {
        var user = db.Users.FirstOrDefault(user => user.Username == userName && user.Password == password);
        return user.UserId;
    }

    public User GetUserByUserId(int UserID, bool withItemsFlag = false)
    {
        User user = null;
        if (!withItemsFlag)
        {
            user = db.Users.FirstOrDefault(user => user.UserId == UserID);
            return user;
        }
        else
        {
            user = db.Users.Include(u => u.Items).FirstOrDefault(user => user.UserId == UserID);
        }

        return user;
    }

    public void RemoveUserFromDatabaseById(User user)
    {
        db.Users.Remove(user);
        db.SaveChanges();
    }

    public void UpdateUserInfo(User user)
    {
        db.Users.Update(user);
        db.SaveChanges();
    }

    public void BuyHanlder()
    {
        db.SaveChanges();
    }

    public string GetUserRoleById(int userId)
    {
        var user = db.Users.FirstOrDefault(user => user.UserId == userId);
        return user.Role.ToLower();
    }
}